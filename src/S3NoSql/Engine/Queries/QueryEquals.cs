/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using S3NoSql.Document;

namespace S3NoSql.Engine.Queries
{
    internal class QueryEquals : Query
    {
        private BsonValue _value;

        public QueryEquals(string field, BsonValue value)
            : base(field)
        {
            _value = value;
        }


        internal BsonValue Value
        {
            get
            {
                return _value;
            }
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    var node = indexer.Find(index, _value, false, Query.Ascending);

        //    if (node == null) yield break;

        //    yield return node;

        //    if (index.Unique == false)
        //    {
        //        // navigate using next[0] do next node - if equals, returns
        //        while (!node.Next[0].IsEmpty && ((node = indexer.GetNode(node.Next[0])).Key.CompareTo(_value) == 0))
        //        {
        //            if (node.IsHeadTail(index)) yield break;

        //            yield return node;
        //        }
        //    }
        //}
    }
}