# S3NoSql

##Overview and Goals
This project seeks to build a simple C# database that sits on top of AWS S3.  It is intended to allow for implementation of a database that is server-less and that can be accessed directly or be placed behind a low cost web server.

S3 provides a server-less file system that scales, auto versions files, is highly durable, auto archiving, and is low cost.

Since S3 is heavily concurrent, record locking is not initially practical.  If a record is edited at the same time, the last record in will win.  This will raise challenges when indexes are attempted to be implemented.  As such, the initial implementation is best suited for bulk writes, with many reads.

The initial focus is reading and writing specific objects, as opposed to querying a range of documents.  There is essentially only the primary key which is the filename.  Queries may come along later, and may be based on the newly released Athena functionality.

### SaaS - Bring your own DB

With open source projects cost of hosting can be an issue.  One goal of this db is to allow for the ability to offer services to users, but not incur as many of the costs of hosting these projects.  This can be done by having users enter their own AWS Key, Secret and Bucket name and the open source project then only have to pay for a web server that would proxy the reads and writes from the App to S3.  

Costs with Web Server Proxy
* Storage - Covered by Bucket Owner
* Backups to Glacier - Covered by Bucket Owner
* File Access (Reads/Writes) - Covered by Bucket Owner
* Web Server Proxy - Covered by Project Owner
* Inbound Data Traffic - Free on AWS
* Outbound Data Traffic - Covered by Project Owner

This doesn't cover all costs, and depending on the project, Outbound Data Traffic could be large.  Different services may offer different data cost structures.  For instance you could put the Web server in Digital Ocean and then S3 would host the data.  This might add latency, but may allow for managing costs.  Another option would be to have the end app download the results of large queries directly from S3.  This would allow the costs to be shifted to the Bucket Owner.  This might add some security risks,  but those may be mitigated through encryption and other means.

Obviously, requiring users to setup a AWS Account, create a bucket, assign a policy to the bucket, and then enter all of the required data into the system limits the number of users that the system will be open to.    


## C# Api

S3NoSql is borrows heavily from LiteDb (https://github.com/mbdavid/LiteDB/).   

LiteDb was designed as a c# native embedded database after MongoDb concepts.  LiteDb is an excellent C# database that can be used as an embedded db on any .net project (Windows, Mac, iOS & Android (Xamarin), Linux, etc).


## Database Structure

### Bucket
The bucket is a collection of databases.  It represents the "Server" in a typical database environment.

Bucket Policy



### Database
A database is a folder at the root of the s3 bucket.

	s3://bucketname/databasename


### Collection
A Collection is a set of similar BSON documents.

	s3://bucketname/databasename/collectionname
	

### Records
A record is a individual BSON document in a Collection.

	s3://bucketname/databasename/collectionname/814cf4d4-3d00-4e5f-99aa-9684b47384a6.bson
	s3://bucketname/databasename/collectionname/c3aa5edd-3da9-44bc-838b-b91d84395e49.bson
	s3://bucketname/databasename/collectionname/43660bfc-661f-4e57-856e-1f3cc5951f9e.bson
	s3://bucketname/databasename/collectionname/97e9ac2a-bdc4-4c9a-8043-03adae254285.bson


### Indexes - Future
Each table can have a collection of indexes.

	s3://bucketname/databasename/collectionname/indexes
	
Each index will have a index file that describes the type and columns in the index.

	s3://bucketname/databasename/collectionname/indexes/indexname.index
	

### Index - Future
Future - The Index is built from a series of index pages that are stored in a folder under the indexes folder

	s3://bucketname/databasename/collectionname/indexes/indexname/1.page
	s3://bucketname/databasename/collectionname/indexes/indexname/2.page
