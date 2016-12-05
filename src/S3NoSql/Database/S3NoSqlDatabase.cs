using System;
using S3NoSql.Database.Collections;
using S3NoSql.Engine;
using S3NoSql.Mapper;
using S3NoSql.Utils;
using S3NoSql.Utils.Extensions;

namespace S3NoSqlLibrary.Database
{
    public class S3NoSqlDatabase
    {
        private S3NoSqlEngine m_Engine = null;
        private BsonMapper m_Mapper = BsonMapper.Global;
        private Logger m_Log = new Logger();

        public S3NoSqlDatabase(
            string _awsKey, 
            string _awsSecret, 
            string _awsRegion, 
            string _dataBucket, 
            string _indexBucket,
            string _database)
        {
            m_Engine = new S3NoSqlEngine(_awsKey, _awsSecret, _awsRegion, _dataBucket, _indexBucket, _database);
        }

        public S3NoSqlDatabase(
            string _awsRegion, 
            string _dataBucket, 
            string _indexBucket,
            string _database)
        {
            m_Engine = new S3NoSqlEngine(_awsRegion, _dataBucket, _indexBucket, _database);
        }

        public S3NoSqlCollection<T> GetCollection<T>(string _name) where T : new()
        {
            if (_name.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(_name));

            return new S3NoSqlCollection<T>(_name, m_Engine, m_Mapper, m_Log);
        }
    }
}
