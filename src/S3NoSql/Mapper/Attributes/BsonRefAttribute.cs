/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;

namespace S3NoSql.Mapper.Attributes
{
    /// <summary>
    /// Indicate that field are not persisted inside this document but it's a reference for another document (DbRef)
    /// </summary>
    public class BsonRefAttribute : Attribute
    {
        public string Collection { get; set; }

        public BsonRefAttribute(string collection)
        {
            this.Collection = collection;
        }
    }
}