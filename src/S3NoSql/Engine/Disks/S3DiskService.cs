using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S3NoSql.Utils;
using Amazon.S3;
using System.IO;
using S3NoSql.Utils.Extensions;

namespace S3NoSql.Engine.Disks
{
    public class S3DiskService : IDiskService
    {
        private IAmazonS3 m_S3Client;
        private string m_BucketName;
        private string m_DatabaseName;

        public S3DiskService(
            IAmazonS3 _s3Client,
            string _bucketName,
            string _databaseName)
        {
            m_S3Client = _s3Client;
            m_BucketName = _bucketName;
            m_DatabaseName = _databaseName;
        }              

        public long FileLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsJournalEnabled
        {
            get
            {
                return false;
            }
        }

        public void ClearJournal()
        {
            
        }

        public void Dispose()
        {
           
        }

        public void Initialize(Logger log, string password)
        {
           
        }

        public IEnumerable<byte[]> ReadJournal()
        {
            return null;
        }

        public byte[] ReadPage(uint pageID)
        {
            Task<Stream> task = S3Helper.ReadPage(m_S3Client, m_BucketName, m_DatabaseName, pageID);
            Task.WaitAll(task);

            return task.Result.ReadToEnd();
        }

        public void SetLength(long fileSize)
        {
            
        }

        public void WriteJournal(uint pageID, byte[] page)
        {
           
        }

        public void WritePage(uint _pageID, byte[] _buffer)
        {
            Stream stream = new MemoryStream(_buffer);

            S3Helper.WritePage(m_S3Client, m_BucketName, m_DatabaseName, _pageID, stream);
        }
    }
}
