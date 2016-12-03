/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using S3NoSql.Document;

namespace S3NoSql.Engine.Queries
{
    /// <summary>
    /// Contains query do not work with index, only full scan
    /// </summary>
    internal class QueryContains : Query
    {
        private BsonValue _value;

        public QueryContains(string field, BsonValue value)
            : base(field)
        {
            _value = value;
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    return indexer
        //        .FindAll(index, Query.Ascending)
        //        .Where(x => x.Key.IsString && x.Key.AsString.Contains(_value));
        //}
    }
}