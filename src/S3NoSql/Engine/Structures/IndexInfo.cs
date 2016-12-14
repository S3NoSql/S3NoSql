using System;
using System.Collections.Generic;

namespace S3NoSql.Engine.Structures
{
    /// <summary>
    /// Represent a index information
    /// </summary>
    public class IndexInfo
    {
        internal IndexInfo(CollectionIndex index)
        {
            this.Slot = index.Slot;
            this.Field = index.Field;
            this.Unique = index.Unique;
        }

        /// <summary>
        /// Slot number of index
        /// </summary>
        public int Slot { get; private set; }

        /// <summary>
        /// Field index name
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Index is Unique?
        /// </summary>
        public bool Unique { get; private set; }
    }
}