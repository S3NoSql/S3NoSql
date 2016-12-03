# S3NoSql

This project seeks to build a simple c# native database that sits on top of AWS S3.  

The focus is reading and writing specific objects, as opposed to querying a range of documents.  There is essentially only the primary key which is the filename.  Queries may come along later, and may be based on the newly released Athena functionality.

This effort seeks to provide a server-less database that scales, auto versions records, is durable, and low cost.

S3NoSql is designed modeled after LiteDb (https://github.com/mbdavid/LiteDB/).   

LiteDb was designed as a c# native embedded database after MongoDb concepts.  LiteDb is an excellent c# database that can be used as an embedded db on any .net project.