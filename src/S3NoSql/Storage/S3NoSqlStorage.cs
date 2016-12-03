using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using S3NoSql.Document;
using S3NoSql.Engine;
using S3NoSql.Engine.Queries;
using S3NoSql.Utils;

namespace S3NoSql.Storage
{
    /// <summary>
    /// Storage is a special collection to store files/streams.
    /// </summary>
    public class S3NoSqlStorage
    {
        internal const string FILE = "_file";
        internal const string DATA = "_data";

        private S3NoSqlEngine m_Engine;

        public S3NoSqlStorage(S3NoSqlEngine engine)
        {
            m_Engine = engine;
        }

        #region Upload

        /// <summary>
        /// Open/Create new file storage and returns linked Stream to write operations
        /// </summary>
        public S3NoSqlFileInfo OpenWrite(string id, string filename, BsonDocument metadata = null)
        {
            // checks if file exists
            var file = this.FindById(id);

            if (file == null)
            {
                file = new S3NoSqlFileInfo(m_Engine, id, filename ?? id);

                // insert if new
                m_Engine.Insert(FILE, file.AsDocument);
            }

            // update metadata if passed
            if (metadata != null)
            {
                file.Metadata = metadata;
            }

            return file;
        }

        /// <summary>
        /// Upload a file based on stream data
        /// </summary>
        public S3NoSqlFileInfo Upload(string id, string filename, Stream stream)
        {
            // checks if file exists
            var file = this.FindById(id);

            if (file == null)
            {
                file = new S3NoSqlFileInfo(m_Engine, id, filename ?? id);

                // insert if new
                m_Engine.Insert(FILE, file.AsDocument);
            }

            // copy stream content to litedb file stream
            stream.CopyTo(file.OpenWriteData());

            return file;
        }


        /// <summary>
        /// Update metada on a file. File must exisits
        /// </summary>
        public bool SetMetadata(string id, BsonDocument metadata)
        {
            var file = this.FindById(id);

            if (file == null) return false;

            file.Metadata = metadata ?? new BsonDocument();
            m_Engine.Update(FILE, file.AsDocument);

            return true;
        }

        #endregion

        #region Download

        /// <summary>
        /// Load data inside storage and returns as Stream
        /// </summary>
        public Stream OpenRead(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");

            var file = this.FindById(id);

            if (file == null) throw S3NoSqlException.FileNotFound(id);

            return file.OpenReadData();
        }

        /// <summary>
        /// Copy all file content to a steam
        /// </summary>
        public void Download(string id, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            var file = this.FindById(id);

            if (file == null) throw S3NoSqlException.FileNotFound(id);

            file.CopyDataTo(stream);
        }

        #endregion

        #region Find Files

        /// <summary>
        /// Find a file inside datafile and returns FileEntry instance. Returns null if not found
        /// </summary>
        public S3NoSqlFileInfo FindById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");

            var doc = m_Engine.Find(FILE, Query.EQ("_id", id)).FirstOrDefault();

            if (doc == null) return null;

            return new S3NoSqlFileInfo(m_Engine, doc);
        }

        /// <summary>
        /// Returns if a file exisits in database
        /// </summary>
        public bool Exists(string id)
        {
            return m_Engine.Exists(FILE, Query.EQ("_id", id));
        }

        /// <summary>
        /// Returns all FileEntry inside database
        /// </summary>
        public IEnumerable<S3NoSqlFileInfo> FindAll()
        {
            var query = Query.All();

            var docs = m_Engine.Find(FILE, query);

            foreach (var doc in docs)
            {
                yield return new S3NoSqlFileInfo(m_Engine, doc);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete a file inside datafile and all metadata related
        /// </summary>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");

            S3NoSqlFileInfo file = FindById(id);

            if (file == null) return true;

            file.DeleteData();

            // remove file reference in _files
            var d = m_Engine.Delete(FILE, Query.EQ("_id", id));

            // if not found, just return false
            if (d == 0) return false;

            return true;
        }

        #endregion
    }
}