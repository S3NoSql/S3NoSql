using System;
using S3NoSql.Database.Collections;
using S3NoSql.Engine;
using S3NoSql.Utils.Extensions;

namespace S3NoSqlLibrary.Database
{
    public class S3NoSqlDatabase
    {
        private S3NoSqlEngine m_Engine = null;

        public S3NoSqlDatabase(string _awsKey, string _awsSecret, string _bucket)
        {
            m_Engine = new S3NoSqlEngine(_awsKey, _awsSecret, _bucket);
        }

        public S3NoSqlDatabase(string _bucket)
        {
            m_Engine = new S3NoSqlEngine(_bucket);
        }


        public S3NoSqlCollection<T> GetCollection<T>(string _name)
        {
            if (_name.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(_name));

            return new LiteCollection<T>(name, _engine);
        }



    }
}
