/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using S3NoSql.Database.Collections;
using S3NoSql.Document;

namespace S3NoSql.Mapper
{
    internal class AutoId
    {
        /// <summary>
        /// Function to test if type is empty
        /// </summary>
        public Func<object, bool> IsEmpty { get; set; }

        /// <summary>
        /// Function that implements how generate a new Id for this type
        /// </summary>
        public Func<S3NoSqlCollection<BsonDocument>, object> NewId { get; set; }
    }
}
