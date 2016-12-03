using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using S3NoSql.Document;
using S3NoSql.Document.Bson;
using S3NoSql.Engine.Queries;
using S3NoSql.Utils;
using S3NoSql.Utils.Extensions;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine
    {
        public async Task<IEnumerable<BsonDocument>> Find(string _collectionName, Query _query, int _skip = 0, int _limit = int.MaxValue)
        {
            //Only all and equal id are currently supported.
            //These do not require indexes.
            if (_query is QueryAll)
            {
                return await GetAll(_collectionName);
            }

            if (_query is QueryEquals
                && (_query as QueryEquals).Field == "id")
            {
                QueryEquals equals = (QueryEquals)_query;

                BsonDocument doc = await GetById(_collectionName, equals.Value.AsString);

                if (doc != null)
                {
                    return new List<BsonDocument> { doc };
                }
                else
                {
                    return new List<BsonDocument>();
                }
            }

            throw new NotImplementedException();
        }

        internal async Task<IEnumerable<BsonDocument>> GetAll(string _collectionName)
        {
            IEnumerable<string> ids = await S3Helper.ListIds(m_S3Client, Bucket, Database, _collectionName);

            List<BsonDocument> docs = new List<BsonDocument>();

            foreach (var id in ids)
            {
                BsonDocument doc = await GetById(_collectionName, id);

                if (doc != null)
                {
                    docs.Add(doc);
                }
            }

            return docs;
        }

        internal async Task<BsonDocument> GetById(string _collectionName, string _id)
        {
            using (Stream stream = await S3Helper.ReadDocument(m_S3Client, Bucket, Database, _collectionName, _id))
            {
                byte[] data = stream.ReadToEnd();
                BsonDocument doc = BsonSerializer.Deserialize(data);
                return doc;
            }
        }
    }
}
