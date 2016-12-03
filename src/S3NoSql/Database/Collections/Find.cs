using System;
using System.Collections.Generic;
using S3NoSql.Document;

namespace S3NoSql.Database.Collections
{
    public partial class S3NoSqlCollection<T>
    {
        public T FindById(BsonValue id)
        {
            if (id == null || id.IsNull) throw new ArgumentNullException("id");

            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
