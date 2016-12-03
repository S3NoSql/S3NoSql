using System;
using S3NoSql.Engine;
using S3NoSql.Mapper;
using S3NoSql.Utils;

namespace S3NoSql.Database.Collections
{
    public sealed partial class S3NoSqlCollection<T> : IDisposable where T : new()
    {
        private S3NoSqlEngine m_Engine;
        private BsonMapper m_Mapper;
        private Logger m_Log;

        public S3NoSqlCollection(string _name, S3NoSqlEngine _engine, BsonMapper _mapper, Logger _log)
        {
            Name = _name;
            m_Engine = _engine;
            m_Mapper = _mapper;
            m_Log = _log;
        }

        public string Name { get; private set; }






        public void Dispose()
        {
            m_Engine = null;
        }
    }
}
