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
    /// Not is an Index Scan operation
    /// </summary>
    internal class QueryNotEquals : Query
    {
        private BsonValue _value;

        public QueryNotEquals(string field, BsonValue value)
            : base(field)
        {
            _value = value;
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    return indexer
        //        .FindAll(index, Query.Ascending)
        //        .Where(x => x.Key.CompareTo(_value) != 0);
        //}
    }
}