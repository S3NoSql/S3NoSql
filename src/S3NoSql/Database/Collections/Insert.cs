using System;
using System.Collections.Generic;
using S3NoSql.Document;

namespace S3NoSql.Database.Collections
{
    public partial class S3NoSqlCollection<T>
    {
        /// <summary>
        /// Insert a new document to this collection. Document Id must be a new value in collection - Returns document Id
        /// </summary>
        public BsonValue Insert(T document)
        {
            if (document == null) throw new ArgumentNullException("document");

            m_Mapper.SetAutoId(document, new S3NoSqlCollection<BsonDocument>(Name, m_Engine, m_Mapper, m_Log));

            var doc = m_Mapper.ToDocument(document);

            m_Engine.Insert(Name, doc);

            return doc["_id"];
        }

        /// <summary>
        /// Insert an array of new documents to this collection. Document Id must be a new value in collection. Can be set buffer size to commit at each N documents
        /// </summary>
        public int Insert(IEnumerable<T> docs)
        {
            if (docs == null) throw new ArgumentNullException("docs");

            return m_Engine.Insert(Name, this.GetBsonDocs(docs));
        }

        /// <summary>
        /// Convert each T document in a BsonDocument, setting autoId for each one
        /// </summary>
        private IEnumerable<BsonDocument> GetBsonDocs(IEnumerable<T> docs)
        {
            foreach (var document in docs)
            {
                m_Mapper.SetAutoId(document, new S3NoSqlCollection<BsonDocument>(Name, m_Engine, m_Mapper, m_Log));

                yield return m_Mapper.ToDocument(document);
            }
        }
    }
}