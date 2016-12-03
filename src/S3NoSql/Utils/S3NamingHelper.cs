using System;
using S3NoSql.Utils.Extensions;

namespace S3NoSql.Utils
{
    public static class S3NamingHelper
    {
        public static string GetDatabase(string _databaseName)
        {
            string databaseName = FormatName(_databaseName);

            return string.Format("{0}", _databaseName);
        }

        public static string GetCollection(string _databaseName, string _collectionName)
        {
            string databaseName = FormatName(_databaseName);
            string collectionName = FormatName(_collectionName);

            return string.Format("{0}/{1}", databaseName, collectionName);
        }

        public static string GetDocument(string _databaseName, string _collectionName, string _documentId)
        {
            string databaseName = FormatName(_databaseName);
            string collectionName = FormatName(_collectionName);
            string documentId = FormatName(_documentId);

            return string.Format("{0}/{1}/{2}.bson", databaseName, collectionName, documentId);
        }

        public static string GetBinaryFile(string _databaseName, string _collectionName, string _documentId)
        {
            string databaseName = FormatName(_databaseName);
            string collectionName = FormatName(_collectionName);
            string documentId = FormatName(_documentId);

            return string.Format("{0}/{1}/{2}.bin", databaseName, collectionName, documentId);
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
