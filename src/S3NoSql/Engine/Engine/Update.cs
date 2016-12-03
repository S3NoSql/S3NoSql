using System;
using System.Collections.Generic;
using S3NoSql.Document;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine
    {
        /// <summary>
        /// Implement update command to a document inside a collection. Returns true if document was updated
        /// </summary>
        public bool Update(string colName, BsonDocument doc)
        {
            return this.Update(colName, new BsonDocument[] { doc }) == 1;
        }

        /// <summary>
        /// Implement update command to a document inside a collection. Return number of documents updated
        /// </summary>
        public int Update(string colName, IEnumerable<BsonDocument> docs)
        {
            if (colName == null) return 0;

            var count = 0;

            foreach (var doc in docs)
            {
                if (InsertDocument(colName, doc))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
