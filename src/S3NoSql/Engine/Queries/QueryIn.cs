/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using S3NoSql.Document;

namespace S3NoSql.Engine.Queries
{
    internal class QueryIn : Query
    {
        private IEnumerable<BsonValue> _values;

        public QueryIn(string field, IEnumerable<BsonValue> values)
            : base(field)
        {
            _values = values;
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    foreach (var value in _values.Distinct())
        //    {
        //        foreach (var node in Query.EQ(this.Field, value).ExecuteIndex(indexer, index))
        //        {
        //            yield return node;
        //        }
        //    }
        //}
    }
}