using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using S3NoSql.Document;
using S3NoSql.Document.Bson;
using S3NoSql.Utils;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine
    {
        /// <summary>
        /// Implements insert documents in a collection - returns _id value
        /// </summary>
        public BsonValue Insert(string colName, BsonDocument doc)
        {
            this.Insert(colName, new BsonDocument[] { doc });
            return doc["_id"];
        }

        /// <summary>
        /// Implements insert documents in a collection - use a buffer to commit transaction in each buffer count
        /// </summary>
        public int Insert(string _collectionName, IEnumerable<BsonDocument> _docs)
        {
            var count = 0;

            foreach (var doc in _docs)
            {
                InsertDocument(_collectionName, doc);

                count++;
            }

            return count;
        }

        private void InsertDocument(string _collectionName, BsonDocument doc)
        {
            BsonValue id;

            // if no _id, add one as ObjectId
            if (!doc.RawValue.TryGetValue("_id", out id))
            {
                doc["_id"] = id = ObjectId.NewObjectId();
            }

            // test if _id is a valid type
            if (id.IsNull || id.IsMinValue || id.IsMaxValue)
            {
                throw S3NoSqlException.InvalidDataType("_id", id);
            }

            //m_Log.Write(Logger.COMMAND, "insert document on '{0}' :: _id = {1}", col.CollectionName, id);

            // serialize object
            var bytes = BsonSerializer.Serialize(doc);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                Task task = S3Helper.WriteDocument(m_S3Client, Bucket, Database, _collectionName, id.AsString, "application/ubjson", stream);
                Task.WaitAll(task);
            }

            //TODO Queue calculation of the indexes for this document 
        }
    }
}
