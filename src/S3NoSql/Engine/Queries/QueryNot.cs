/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace S3NoSql.Engine.Queries
{
    /// <summary>
    /// Not is an Index Scan operation
    /// </summary>
    internal class QueryNot : Query
    {
        private Query _query;
        private int _order;

        public QueryNot(Query query, int order)
            : base("_id")
        {
            _query = query;
            _order = order;
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    throw new NotSupportedException();
        //}

        //internal override IEnumerable<IndexNode> Run(CollectionPage col, IndexService indexer)
        //{
        //    var result = _query.Run(col, indexer);
        //    var all = new QueryAll("_id", _order).Run(col, indexer);

        //    return all.Except(result, new IndexNodeComparer());
        //}
    }
}