/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using S3NoSql.Document;
using S3NoSql.Engine;
using S3NoSql.Utils;

namespace S3NoSql.Storage
{
    /// <summary>
    /// Represets a file inside storage collection
    /// </summary>
    public class S3NoSqlFileInfo
    {
        /// <summary>
        /// File id have a specific format - it's like file path.
        /// </summary>
        public const string ID_PATTERN = @"^[\w-$@!+%;\.]+(\/[\w-$@!+%;\.]+)*$";

        private static Regex IdPattern = new Regex(ID_PATTERN);

        public string Id { get; private set; }
        public string Filename { get; private set; }
        public string MimeType { get; private set; }
        public long Length { get; internal set; }
        public int Chunks { get; internal set; }
        public DateTime UploadDate { get; internal set; }
        public BsonDocument Metadata { get; set; }

        private S3NoSqlEngine m_Engine;

        internal S3NoSqlFileInfo(S3NoSqlEngine engine, string id, string filename)
        {
            if (!IdPattern.IsMatch(id)) throw S3NoSqlException.InvalidFormat("FileId", id);

            m_Engine = engine;

            this.Id = id;
            this.Filename = Path.GetFileName(filename);
            this.MimeType = MimeTypeConverter.GetMimeType(this.Filename);
            this.Length = 0;
            this.Chunks = 0;
            this.UploadDate = DateTime.Now;
            this.Metadata = new BsonDocument();
        }

        internal S3NoSqlFileInfo(S3NoSqlEngine engine, BsonDocument doc)
        {
            m_Engine = engine;

            this.Id = doc["_id"].AsString;
            this.Filename = doc["filename"].AsString;
            this.MimeType = doc["mimeType"].AsString;
            this.Length = doc["length"].AsInt64;
            this.Chunks = doc["chunks"].AsInt32;
            this.UploadDate = doc["uploadDate"].AsDateTime;
            this.Metadata = doc["metadata"].AsDocument;
        }

        public BsonDocument AsDocument
        {
            get
            {
                return new BsonDocument
                {
                    { "_id", this.Id },
                    { "filename", this.Filename },
                    { "mimeType", this.MimeType },
                    { "length", this.Length },
                    { "chunks", this.Chunks },
                    { "uploadDate", this.UploadDate },
                    { "metadata", this.Metadata ?? new BsonDocument() }
                };
            }
        }

        /// <summary>
        /// File data is read directly from S3 
        /// </summary>
        public Stream OpenReadData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// File data is written directly to s3
        /// </summary>
        public Stream OpenWriteData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy file content to another stream
        /// </summary>
        public void CopyDataTo(Stream stream)
        {

        }

        public void DeleteData()
        {
        }
    }
}