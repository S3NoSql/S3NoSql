using System;
using S3NoSql.Document;
using S3NoSql.Engine.Queries;

namespace S3NoSql.Database.Collections
{
    public partial class S3NoSqlCollection<T>
    {

        public int Delete(Query query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // keep trying execute query to auto-create indexes when not found
            while (true)
            {
                //try
                //{
                return m_Engine.Delete(Name, query);
                //}
                //catch (IndexNotFoundException ex)
                //{
                //    this.EnsureIndex(ex);
                //}
            }
        }

        /// <summary>
        /// Remove an document in collection using Document Id - returns false if not found document
        /// </summary>
        public bool Delete(BsonValue id)
        {
            if (id == null || id.IsNull) throw new ArgumentNullException("id");

            return this.Delete(Query.EQ("_id", id)) > 0;
        }
    }
}
