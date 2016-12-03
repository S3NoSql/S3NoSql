# S3NoSql

##Overview and Goals
This project seeks to build a simple C# database that sits on top of AWS S3.  It is intended to allow for implementation of a database that is server-less and that can be accessed directly or be placed behind a low cost web server.

S3 provides a server-less file system that scales, auto versions files, is highly durable, auto archiving, and is low cost.

Since S3 is heavily concurrent, record locking is not initially practical.  If a record is edited at the same time, the last record in will win.  This will raise challenges when indexes are attempted to be implemented.  As such, the initial implementation is best suited for bulk writes, with many reads.

The initial focus is reading and writing specific objects, as opposed to querying a range of documents.  There is essentially only the primary key which is the filename.  Queries may come along later, and may be based on the newly released Athena functionality.  See Index notes below.

### SaaS - Bring your own DB

With open source projects cost of hosting can be an issue.  One goal of this db is to allow for the ability to offer services to users, but not incur as many of the costs of hosting these projects.  This can be done by having users enter their own AWS Key, Secret and Bucket name and the open source project then only have to pay for a web server that would proxy the reads and writes from the App to S3.  

Costs with Web Server Proxy
* Storage - Covered by Bucket Owner
* Backups to Glacier - Covered by Bucket Owner
* File Access (Reads/Writes) - Covered by Bucket Owner
* Web Server Proxy - Covered by Project Owner
* SQS Queue - Covered by Project Owner
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



#### Bucket Policy



### Database
A database is a folder at the root of the s3 bucket.

	s3://bucketname/databasename
	
Note - All names below the bucket will be made to be lower cased.

### Collection
A Collection is a set of similar BSON documents.

	s3://bucketname/databasename/collectionname
	

### Document
A document is a individual BSON document (record) in a Collection.

	s3://bucketname/databasename/collectionname/814cf4d4-3d00-4e5f-99aa-9684b47384a6.bson
	s3://bucketname/databasename/collectionname/c3aa5edd-3da9-44bc-838b-b91d84395e49.bson
	s3://bucketname/databasename/collectionname/43660bfc-661f-4e57-856e-1f3cc5951f9e.bson
	s3://bucketname/databasename/collectionname/97e9ac2a-bdc4-4c9a-8043-03adae254285.bson

Documents are written directly to the S3 bucket.


### Indexes - Future
Each table can have a collection of indexes.

	s3://bucketname/databasename/collectionname/indexes
	
Each index will have a index file that describes the type and columns in the index.

	s3://bucketname/databasename/collectionname/indexes/indexname.index
	

### Index - Future
The Index is built from a series of index pages that are stored in a folder under the indexes folder

	s3://bucketname/databasename/collectionname/indexes/indexname/1.page
	s3://bucketname/databasename/collectionname/indexes/indexname/2.page
	
Changes to index pages will be queued to a AWS SQS topic.  This will enable one thread per bucket to safely make changes to pages without worrying about multiple threads overwriting index pages at the same time.  The index will need to include a pointer to the document as well as the version of the document that was indexed at the time.  This will enable the indexing system to return consistent results when queries are executed.

###Files
Files may be any size up to 5TB.  Files will be stored in two parts 
* File Info
* File Data

####File Info

	s3://bucketname/databasename/_file/92386a7e-698e-4826-a976-fb796fb1e8c1.bson
	
Data Stored in file info
* Id - The unique identifier of the file.
* Filename - The original name of the file when loaded.
* MimeType - The type of data that the file contains
* Length - The size in bytes of the file
* UploadDate - The data and time that the file was uploaded (UTC).

	
####File Data
The file data is the raw bytes.

	s3://bucketname/databasename/_data/92386a7e-698e-4826-a976-fb796fb1e8c1.bin
	

## Suggested Bucket Settings
* Enable Versioning - On
* Lifecycle - Create a lifecycle workflow that will push old versions of the records to Glacier after a period of time.  It is not recommended to transfer data objects to STANDARD_IA as there is a minimum file size that is likely larger than the records that are being stored in the bucket.  It may be possible to construct a workflow that moves the data files to IA (s3://bucketname/databasename/files/data/) if appropriate.
* Cross Region Replication - If the data stored in the system is of sufficient value, it may be desirable to automatically replicate it to another region using the Cross Region Replication capabilities.