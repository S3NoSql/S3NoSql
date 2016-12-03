using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Runtime;
using Amazon.S3;
using S3NoSql.Utils;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine : IDisposable
    {
        private string m_AwsKey = null;
        private string m_AwsSecret = null;
        private string m_AwsRegion = null;
        private IAmazonS3 m_S3Client;

        public S3NoSqlEngine(string _awsKey, string _awsSecret, string _awsRegion, string _bucket)
        {
            m_AwsKey = _awsKey;
            m_AwsSecret = _awsSecret;
            m_AwsRegion = _awsRegion;

            AWSCredentials credentials = new BasicAWSCredentials(m_AwsKey, m_AwsSecret);

            m_S3Client = new AmazonS3Client(credentials, S3Helper.GetRegionEndpoint(m_AwsRegion));

            Bucket = _bucket;
        }

        /// <summary>
        /// Uses machine role permissions.
        /// </summary>
        /// <param name="_awsRegion">Aws region.</param>
        /// <param name="_bucket">Bucket.</param>
        /// <param name="_roleName">If specified will use the requested role.</param>
        public S3NoSqlEngine(string _awsRegion, string _bucket, string _roleName = null)
        {
            m_AwsRegion = _awsRegion;

            AWSCredentials credentials;

            if (_roleName == null)
            {
                credentials = new InstanceProfileAWSCredentials();
            }
            else
            {
                credentials = new InstanceProfileAWSCredentials(_roleName);
            }

            m_S3Client = new AmazonS3Client(credentials, S3Helper.GetRegionEndpoint(m_AwsRegion));

            Bucket = _bucket;
        }

        public string Bucket { get; private set; }

        public void Dispose()
        {

        }
    }
}
