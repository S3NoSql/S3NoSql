using System;
using System.Collections.Generic;
using System.Linq;

namespace S3NoSql.Engine
{
    public partial class S3NoSqlEngine : IDisposable
    {
        private string m_AwsKey = null;
        private string m_AwsSecret = null;

        public S3NoSqlEngine(string _awsKey, string _awsSecret, string _bucket)
        {
            m_AwsKey = _awsKey;
            m_AwsSecret = _awsSecret;

            Bucket = _bucket;
        }

        public S3NoSqlEngine(string _bucket)
        {
            Bucket = _bucket;
        }

        public string Bucket { get; private set; }

        public void Dispose()
        {

        }
    }
}
