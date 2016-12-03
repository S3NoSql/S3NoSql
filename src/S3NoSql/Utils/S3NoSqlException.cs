/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Reflection;
using S3NoSql.Document;

namespace S3NoSql.Utils
{
    /// <summary>
    /// The main exception for LiteDB
    /// </summary>
    public class S3NoSqlException : Exception
    {
        #region Errors code

        public const int NO_DATABASE = 100;
        public const int FILE_NOT_FOUND = 101;
        public const int FILE_CORRUPTED = 102;
        public const int INVALID_DATABASE = 103;
        public const int INVALID_DATABASE_VERSION = 104;
        public const int FILE_SIZE_EXCEEDED = 105;
        public const int COLLECTION_LIMIT_EXCEEDED = 106;
        public const int JOURNAL_FILE_FOUND = 107;
        public const int INDEX_DROP_IP = 108;
        public const int INDEX_LIMIT_EXCEEDED = 109;
        public const int INDEX_DUPLICATE_KEY = 110;
        public const int INDEX_KEY_TOO_LONG = 111;
        public const int INDEX_NOT_FOUND = 112;
        public const int LOCK_TIMEOUT = 120;
        public const int INVALID_COMMAND = 121;
        public const int ALREADY_EXISTS_COLLECTION_NAME = 122;
        public const int DATABASE_WRONG_PASSWORD = 123;
        public const int READ_ONLY_DATABASE = 125;

        public const int INVALID_FORMAT = 200;
        public const int DOCUMENT_MAX_DEPTH = 201;
        public const int INVALID_CTOR = 202;
        public const int UNEXPECTED_TOKEN = 203;
        public const int INVALID_DATA_TYPE = 204;
        public const int PROPERTY_NOT_MAPPED = 206;
        public const int INVALID_TYPED_NAME = 207;

        #endregion

        public int ErrorCode { get; private set; }

        public S3NoSqlException(string message)
            : base(message)
        {
        }

        internal S3NoSqlException(int code, string message, params object[] args)
            : base(string.Format(message, args))
        {
            this.ErrorCode = code;
        }

        internal S3NoSqlException(int code, Exception inner, string message, params object[] args)
        : base(string.Format(message, args), inner)
        {
            this.ErrorCode = code;
        }

        #region Database Errors

        internal static S3NoSqlException NoDatabase()
        {
            return new S3NoSqlException(NO_DATABASE, "There is no database.");
        }

        internal static S3NoSqlException FileNotFound(string fileId)
        {
            return new S3NoSqlException(FILE_NOT_FOUND, "File '{0}' not found.", fileId);
        }

        internal static S3NoSqlException FileCorrupted(string fileId)
        {
            return new S3NoSqlException(FILE_CORRUPTED, "File '{0}' has no content or is corrupted.", fileId);
        }

        internal static S3NoSqlException InvalidDatabase()
        {
            return new S3NoSqlException(INVALID_DATABASE, "Datafile is not a LiteDB database.");
        }

        internal static S3NoSqlException InvalidDatabaseVersion(int version)
        {
            return new S3NoSqlException(INVALID_DATABASE_VERSION, "Invalid database version: {0}", version);
        }


        internal static S3NoSqlException CollectionLimitExceeded(int limit)
        {
            return new S3NoSqlException(COLLECTION_LIMIT_EXCEEDED, "This database exceeded the maximum limit of collection names size: {0} bytes", limit);
        }

        internal static S3NoSqlException JournalFileFound(string journal)
        {
            return new S3NoSqlException(JOURNAL_FILE_FOUND, "Journal file found on '{0}'. Try to reopen the database.", journal);
        }

        internal static S3NoSqlException IndexDropId()
        {
            return new S3NoSqlException(INDEX_DROP_IP, "Primary key index '_id' can't be dropped.");
        }

        internal static S3NoSqlException IndexDuplicateKey(string field, BsonValue key)
        {
            return new S3NoSqlException(INDEX_DUPLICATE_KEY, "Cannot insert duplicate key in unique index '{0}'. The duplicate value is '{1}'.", field, key);
        }

        internal static S3NoSqlException IndexNotFound(string colName, string field)
        {
            return new S3NoSqlException(INDEX_NOT_FOUND, "Index not found on '{0}.{1}'.", colName, field);
        }

        internal static S3NoSqlException LockTimeout(TimeSpan ts)
        {
            return new S3NoSqlException(LOCK_TIMEOUT, "Timeout. Database is locked for more than {0}.", ts.ToString());
        }

        internal static S3NoSqlException InvalidCommand(string command)
        {
            return new S3NoSqlException(INVALID_COMMAND, "Command '{0}' is not a valid shell command.", command);
        }

        internal static S3NoSqlException AlreadyExistsCollectionName(string newName)
        {
            return new S3NoSqlException(ALREADY_EXISTS_COLLECTION_NAME, "New collection name '{0}' already exists.", newName);
        }

        internal static S3NoSqlException DatabaseWrongPassword()
        {
            return new S3NoSqlException(DATABASE_WRONG_PASSWORD, "Invalid database password.");
        }

        internal static S3NoSqlException ReadOnlyDatabase()
        {
            return new S3NoSqlException(READ_ONLY_DATABASE, "This action are not supported because database was opened in read only mode.");
        }

        #endregion

        #region Document/Mapper Errors

        internal static S3NoSqlException InvalidFormat(string field, string format)
        {
            return new S3NoSqlException(INVALID_FORMAT, "Invalid format: {0}", field);
        }

        internal static S3NoSqlException DocumentMaxDepth(int depth, Type type)
        {
            return new S3NoSqlException(DOCUMENT_MAX_DEPTH, "Document has more than {0} nested documents in '{1}'. Check for circular references (use DbRef).", depth, type == null ? "-" : type.Name);
        }

        internal static S3NoSqlException InvalidCtor(Type type, Exception inner)
        {
            return new S3NoSqlException(INVALID_CTOR, inner, "Failed to create instance for type '{0}' from assembly '{1}'. Checks if the class has a public constructor with no parameters.", type.FullName, type.AssemblyQualifiedName);
        }

        internal static S3NoSqlException UnexpectedToken(string token)
        {
            return new S3NoSqlException(UNEXPECTED_TOKEN, "Unexpected JSON token: {0}", token);
        }

        internal static S3NoSqlException InvalidDataType(string field, BsonValue value)
        {
            return new S3NoSqlException(INVALID_DATA_TYPE, "Invalid BSON data type '{0}' on field '{1}'.", value.Type, field);
        }

        public const int PROPERTY_READ_WRITE = 204;

        internal static S3NoSqlException PropertyReadWrite(PropertyInfo prop)
        {
            return new S3NoSqlException(PROPERTY_READ_WRITE, "'{0}' property must have public getter and setter.", prop.Name);
        }

        internal static S3NoSqlException PropertyNotMapped(string name)
        {
            return new S3NoSqlException(PROPERTY_NOT_MAPPED, "Property '{0}' was not mapped into BsonDocument.", name);
        }

        internal static S3NoSqlException InvalidTypedName(string type)
        {
            return new S3NoSqlException(INVALID_TYPED_NAME, "Type '{0}' not found in current domain (_type format is 'Type.FullName, AssemblyName').", type);
        }

        #endregion
    }
}