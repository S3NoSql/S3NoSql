using System;
using System.Collections.Generic;
using System.IO;
using S3NoSql.Document;
using S3NoSql.Document.Bson;
using S3NoSql.Engine.Queries;
using S3NoSql.Utils;
using S3NoSql.Utils.Extensions;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine
    {
        public IEnumerable<BsonDocument> Find(string _collectionName, Query _query, int _skip = 0, int _limit = int.MaxValue)
        {
            //Only all and equal id are currently supported.
            //These do not require indexes.
            if (_query is QueryAll)
            {
                return GetAll(_collectionName);
            }

            if (_query is QueryEquals
                && (_query as QueryEquals).Field == "id")
            {
                QueryEquals equals = (QueryEquals)_query;

                BsonDocument doc = GetById(_collectionName, equals.Value.AsString);

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

        internal IEnumerable<BsonDocument> GetAll(string _collectionName)
        {
            var ids = S3Helper.ListIds(m_S3Client, DataBucket, Database, _collectionName);

            List<BsonDocument> docs = new List<BsonDocument>();

            foreach (var id in ids)
            {
                BsonDocument doc = GetById(_collectionName, id);

                yield return doc;
            }
        }

        internal BsonDocument GetById(string _collectionName, string _id)
        {
            using (Stream stream = S3Helper.ReadDocument(m_S3Client, DataBucket, Database, _collectionName, _id))
            {
                byte[] data = stream.ReadToEnd();
                BsonDocument doc = BsonSerializer.Deserialize(data);
                return doc;
            }
        }
    }
}
