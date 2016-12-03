/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace S3NoSql.Engine.Queries
{
    internal class QueryAnd : Query
    {
        private Query _left;
        private Query _right;

        public QueryAnd(Query left, Query right)
            : base(null)
        {
            _left = left;
            _right = right;
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    throw new NotSupportedException();
        //}

        //internal override IEnumerable<IndexNode> Run(CollectionPage col, IndexService indexer)
        //{
        //    var left = _left.Run(col, indexer);
        //    var right = _right.Run(col, indexer);

        //    return left.Intersect(right, new IndexNodeComparer());
        //}
    }
}