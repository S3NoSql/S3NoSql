using System;
using System.Collections.Generic;
using System.Linq;
using S3NoSql.Document;

namespace S3NoSql.Database.Collections
{
    public partial class S3NoSqlCollection<T>
    {
        public bool Update(T document)
        {
            if (document == null) throw new ArgumentNullException("document");

            // get BsonDocument from object
            var doc = m_Mapper.ToDocument(document);

            return m_Engine.Update(Name, new BsonDocument[] { doc }) > 0;
        }

        public bool Update(BsonValue id, T document)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (id == null || id.IsNull) throw new ArgumentNullException("id");

            // get BsonDocument from object
            var doc = m_Mapper.ToDocument(document);

            // set document _id using id parameter
            doc["_id"] = id;

            return m_Engine.Update(Name, new BsonDocument[] { doc }) > 0;
        }

        public int Update(IEnumerable<T> documents)
        {
            if (documents == null) throw new ArgumentNullException("document");

            return m_Engine.Update(Name, documents.Select(x => m_Mapper.ToDocument(x)));
        }
    }
}
