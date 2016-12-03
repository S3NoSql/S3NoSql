/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace S3NoSql.Engine.Queries
{
    /// <summary>
    /// Placeholder query for returning no values from a collection.
    /// </summary>
    internal class QueryEmpty : Query
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryEmpty" /> class.
        /// </summary>
        public QueryEmpty()
            : base("")
        {
        }

        //internal override IEnumerable<IndexNode> ExecuteIndex(IndexService indexer, CollectionIndex index)
        //{
        //    yield break;
        //}
    }
}