using System;
using S3NoSql.Engine;

namespace S3NoSql.Database.Collections
{
    public class S3NoSqlCollection<T> : IDisposable
    {
        private S3NoSqlEngine m_Engine;

        public S3NoSqlCollection(string _name, S3NoSqlEngine _engine)
        {
            m_Engine = _engine;
            Name = _name;
        }

        public string Name { get; private set; }






        public void Dispose()
        {
            m_Engine = null;
        }
    }
}
