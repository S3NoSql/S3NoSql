using System;
using System.Collections.Generic;
using S3NoSql.Document;

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
        public int Insert(string colName, IEnumerable<BsonDocument> docs)
        {
            var count = 0;

            string path = null;

            foreach (var doc in docs)
            {
                this.Insert(path, doc);

                count++;
            }

            return count;
        }

        private bool InsertDocument(string path, BsonDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}
