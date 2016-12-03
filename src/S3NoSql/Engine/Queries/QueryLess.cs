/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using S3NoSql.Document;

namespace S3NoSql.Engine.Queries
{
    internal class QueryLess : Query
    {
        private BsonValue _value;
        private bool _equals;

        public QueryLess(string field, BsonValue value, bool equals)
            : base(field)
        {
            _value = value;
            _equals = equals;
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    foreach (var node in indexer.FindAll(index, Query.Ascending))
        //    {
        //        var diff = node.Key.CompareTo(_value);

        //        if (diff == 1 || (!_equals && diff == 0)) break;

        //        if (node.IsHeadTail(index)) yield break;

        //        yield return node;
        //    }
        //}
    }
}