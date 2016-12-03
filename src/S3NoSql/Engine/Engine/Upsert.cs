using System;
using System.Collections.Generic;
using S3NoSql.Document;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine
    {
        /// <summary>
        /// Implement upsert command to documents in a collection. Calls update on all documents,
        /// then any documents not updated are then attempted to insert.
        /// This will have the side effect of throwing if duplicate items are attempted to be inserted. Returns true if document is inserted
        /// </summary>
        public bool Upsert(string colName, BsonDocument doc)
        {
            return this.Upsert(colName, new BsonDocument[] { doc }) == 1;
        }

        /// <summary>
        /// Implement upsert command to documents in a collection. Calls update on all documents,
        /// then any documents not updated are then attempted to insert.
        /// This will have the side effect of throwing if duplicate items are attempted to be inserted.
        /// </summary>
        public int Upsert(string colName, IEnumerable<BsonDocument> docs)
        {

            var count = 0;

            foreach (var doc in docs)
            {
                //// first try update document, if not found, insert
                //if (this.UpdateDocument(col, doc) == false)
                //{
                this.InsertDocument(colName, doc);
                count++;
                //}
            }

            // returns how many document was inserted
            return count;
        }
    }
}
