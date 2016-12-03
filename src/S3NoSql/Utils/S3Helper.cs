using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using S3NoSql.Utils.Extensions;

namespace S3NoSql.Utils
{
    public static class S3Helper
    {
        public static RegionEndpoint GetRegionEndpoint(string _region)
        {
            _region.AssertNullOrWhiteSpace();
            string region = _region.ToLower();

            switch (region)
            {
                case "useast1":
                    return RegionEndpoint.USEast1;
                case "useeast2":
                    return RegionEndpoint.USEast2;
                case "uswest1":
                    return RegionEndpoint.USWest1;
                case "uswest2":
                    return RegionEndpoint.USWest2;
            }

            throw new NotImplementedException("Region not currently implemented in S3Helper.  Add requested region");
        }

        public static async Task WriteDocument(
            IAmazonS3 _s3Client,
            string _bucketName,
            string _databaseName,
            string _collectionName,
            string _id,
            string _contentType,
            Stream _dataStream)
        {
            string key = S3NamingHelper.GetDocument(_databaseName, _collectionName, _id);

            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = key,
                ContentType = _contentType,
                InputStream = _dataStream
            };

            PutObjectResponse response = await _s3Client.PutObjectAsync(request);
        }

        public static async Task<Stream> ReadDocument(
            IAmazonS3 _s3Client,
            string _bucketName,
            string _databaseName,
            string _collectionName,
            string _id)
        {
            string key = S3NamingHelper.GetDocument(_databaseName, _collectionName, _id);

            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = _bucketName,
                Key = key
            };

            GetObjectResponse response = await _s3Client.GetObjectAsync(request);

            return response.ResponseStream;
        }

        public static async Task WriteBinary(
            IAmazonS3 _s3Client,
            string _bucketName,
            string _databaseName,
            string _collectionName,
            string _id,
            string _contentType,
            Stream _dataStream)
        {
            string key = S3NamingHelper.GetBinaryFile(_databaseName, _collectionName, _id);

            PutObjectRequest request = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = key,
                ContentType = _contentType,
                InputStream = _dataStream
            };

            PutObjectResponse response = await _s3Client.PutObjectAsync(request);
        }

        public static async Task<Stream> ReadBinary(
            IAmazonS3 _s3Client,
            string _bucketName,
            string _databaseName,
            string _collectionName,
            string _id)
        {
            string key = S3NamingHelper.GetBinaryFile(_databaseName, _collectionName, _id);

            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = _bucketName,
                Key = key
            };

            GetObjectResponse response = await _s3Client.GetObjectAsync(request);

            return response.ResponseStream;
        }

        public static async Task<IEnumerable<string>> ListIds(
            IAmazonS3 _s3Client,
            string _bucketName,
            string _databaseName,
            string _collectionName)
        {
            string prefix = S3NamingHelper.GetCollection(_databaseName, _collectionName);

            ListObjectsRequest request = new ListObjectsRequest
            {
                BucketName = _bucketName,
                Prefix = prefix
            };

            ListObjectsResponse response = await _s3Client.ListObjectsAsync(request);

            List<string> ids = new List<string>();

            foreach (var s3object in response.S3Objects)
            {
                string id = Path.GetFileNameWithoutExtension(s3object.Key);
                ids.Add(id);
            }

            return ids;
        }
    }
}
