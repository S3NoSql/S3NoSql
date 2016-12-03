using System;
using System.Collections.Generic;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine
    {
        /// <summary>
        /// Returns all collection inside datafile
        /// </summary>
        public IEnumerable<string> GetCollectionNames()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Drop collection including all documents, indexes and extended pages
        /// </summary>
        public bool DropCollection(string colName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rename a collection
        /// </summary>
        public bool RenameCollection(string colName, string newName)
        {
            throw new NotImplementedException();
        }
    }
}