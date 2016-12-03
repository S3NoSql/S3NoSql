/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace S3NoSql.Engine.Queries
{
    /// <summary>
    /// All is an Index Scan operation
    /// </summary>
    internal class QueryAll : Query
    {
        private int _order;

        public QueryAll(string field, int order)
            : base(field)
        {
            _order = order;
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    return indexer.FindAll(index, _order);
        //}
    }
}