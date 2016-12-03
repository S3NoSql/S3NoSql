using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S3NoSql.Document;
using S3NoSql.Engine.Queries;

namespace S3NoSql.Database.Collections
{
    public partial class S3NoSqlCollection<T>
    {
        public IEnumerable<T> Find(Query query, int skip = 0, int limit = int.MaxValue)
        {
            if (query == null) throw new ArgumentNullException("query");

            IEnumerator<BsonDocument> enumerator = null;
            var more = true;

            try
            {
                // keep trying execute query to auto-create indexes when not found
                // must try get first doc inside try/catch to get index not found (yield returns are not supported inside try/catch)
                while (true)
                {
                    var result = m_Engine.Find(Name, query, skip, limit);
                    enumerator = result.GetEnumerator();

                    more = enumerator.MoveNext();

                    break;
                }

                if (more == false) yield break;

                // do...while
                do
                {
                    //// executing all includes in BsonDocument
                    //foreach (var action in _includes)
                    //{
                    //    action(enumerator.Current);
                    //}

                    // get object from BsonDocument
                    var obj = m_Mapper.ToObject<T>(enumerator.Current);

                    yield return obj;
                }
                while (more = enumerator.MoveNext());
            }
            finally
            {
                if (enumerator != null) enumerator.Dispose();
            }
        }

        public T FindById(BsonValue id)
        {
            if (id == null || id.IsNull) throw new ArgumentNullException("id");

            IEnumerable<T> results = Find(Query.EQ("_id", id));
            return results.SingleOrDefault();
        }

        public IEnumerable<T> FindAll()
        {
            return Find(Query.All());
        }
    }
}
