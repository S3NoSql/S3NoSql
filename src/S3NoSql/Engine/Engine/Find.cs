using System;
using System.Collections.Generic;
using S3NoSql.Document;
using S3NoSql.Engine.Queries;

namespace S3NoSql.Engine.Engine
{
    public partial class S3NoSqlEngine
    {
        public IEnumerable<BsonDocument> Find(string colName, Query query, int skip = 0, int limit = int.MaxValue)
        {
            throw new NotImplementedException();
        }
    }
}
