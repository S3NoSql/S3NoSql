using System;
using S3NoSql.Utils.Extensions;

namespace S3NoSql.Utils
{
    public static class S3NamingHelper
    {
        public static string GetDatabase(string _bucketName, string _databaseName)
        {
            ValidateName(_bucketName);
            string databaseName = FormatName(_databaseName);

            return string.Format("{0}/{1}", _bucketName, _databaseName);
        }

        public static string GetCollection(string _bucketName, string _databaseName, string _collectionName)
        {
            ValidateName(_bucketName);
            string databaseName = FormatName(_databaseName);
            string collectionName = FormatName(_collectionName);

            return string.Format("{0}/{1}/{2}", _bucketName, databaseName, collectionName);
        }

        public static string GetDocument(string _bucketName, string _databaseName, string _collectionName, string _documentId)
        {
            ValidateName(_bucketName);
            string databaseName = FormatName(_databaseName);
            string collectionName = FormatName(_collectionName);
            string documentId = FormatName(_documentId);

            return string.Format("{0}/{1}/{2}/{3}.bson", _bucketName, databaseName, collectionName, documentId);
        }

        public static string GetBinaryInfo(string _bucketName, string _databaseName, string _documentId)
        {
            ValidateName(_bucketName);
            string databaseName = FormatName(_databaseName);
            string documentId = FormatName(_documentId);

            return string.Format("{0}/{1}/files/info/{2}.bson", _bucketName, databaseName, documentId);
        }

        public static string GetBinaryFile(string _bucketName, string _databaseName, string _documentId)
        {
            ValidateName(_bucketName);
            string databaseName = FormatName(_databaseName);
            string documentId = FormatName(_documentId);

            return string.Format("{0}/{1}/files/data/{2}.bin", _bucketName, databaseName, documentId);
        }

        private static void ValidateName(string _sourceName)
        {
            _sourceName.IsNullOrWhiteSpace();
        }

        private static string FormatName(string _sourceName)
        {
            ValidateName(_sourceName);
            return _sourceName.ToLower();
        }
    }
}
