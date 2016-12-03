/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;

namespace S3NoSql.Mapper.Attributes
{
    /// <summary>
    /// Add an index in this entity property.
    /// </summary>
    public class BsonIndexAttribute : Attribute
    {
        public bool Unique { get; private set; }

        public BsonIndexAttribute()
            : this(false)
        {
        }

        public BsonIndexAttribute(bool unique)
        {
            this.Unique = unique;
        }
    }
}