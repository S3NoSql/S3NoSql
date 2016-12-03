using System;
using System.Collections.Generic;
using System.Linq;
using S3NoSql.Document;

namespace S3NoSql.Database.Collections
{
    public partial class S3NoSqlCollection<T>
    {
        /// <summary>
        /// Insert or Update a document in this collection.
        /// </summary>
        public bool Upsert(T document)
        {
            if (document == null) throw new ArgumentNullException("document");

            // get BsonDocument from object
            var doc = m_Mapper.ToDocument(document);

            return m_Engine.Upsert(Name, doc);
        }

        /// <summary>
        /// Insert or Update a document in this collection.
        /// </summary>
        public bool Upsert(BsonValue id, T document)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (id == null || id.IsNull) throw new ArgumentNullException("id");

            // get BsonDocument from object
            var doc = m_Mapper.ToDocument(document);

            // set document _id using id parameter
            doc["_id"] = id;

            return m_Engine.Upsert(Name, doc);
        }

        /// <summary>
        /// Insert or Update all documents
        /// </summary>
        public int Upsert(IEnumerable<T> documents)
        {
            if (documents == null) throw new ArgumentNullException("document");

            return m_Engine.Upsert(Name, documents.Select(x => m_Mapper.ToDocument(x)));
        }
    }
}
