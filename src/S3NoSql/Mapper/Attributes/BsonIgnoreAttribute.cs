/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;

namespace S3NoSql.Mapper.Attributes
{
    /// <summary>
    /// Indicate that property will not be persist in Bson serialization
    /// </summary>
    public class BsonIgnoreAttribute : Attribute
    {
    }
}