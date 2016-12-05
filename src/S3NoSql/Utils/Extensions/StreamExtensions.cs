﻿/*--------------------------------------------------------------------------------*/
/* Adapted from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System.IO;

namespace S3NoSql.Utils.Extensions
{
    internal static class StreamExtensions
    {
        public static byte ReadByte(this Stream stream, long position)
        {
            var buffer = new byte[1];
            stream.Seek(position, SeekOrigin.Begin);
            stream.Read(buffer, 0, 1);
            return buffer[0];
        }

        public static void WriteByte(this Stream stream, long position, byte value)
        {
            stream.Seek(position, SeekOrigin.Begin);
            stream.Write(new byte[] { value }, 0, 1);
        }

        public static void CopyTo(this Stream input, Stream output)
        {
            var buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        public static byte[] ReadToEnd(this Stream input)
        {
            long lenth = input.Length;
            long remaining = lenth;

            byte[] data = new byte[remaining];
            input.Read(data, 0, (int)remaining);
            return data;
        }
    }
}