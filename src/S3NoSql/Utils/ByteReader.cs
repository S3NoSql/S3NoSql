﻿/*--------------------------------------------------------------------------------*/
/* Taken from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
using System;
using System.Text;
using S3NoSql.Document;
using S3NoSql.Document.Bson;
using S3NoSql.Engine.Structures;

namespace S3NoSql.Utils
{
    internal class ByteReader
    {
        private byte[] _buffer;
        private int _pos;

        public int Position { get { return _pos; } }

        public ByteReader(byte[] buffer)
        {
            _buffer = buffer;
            _pos = 0;
        }

        public void Skip(int length)
        {
            _pos += length;
        }

        #region Native data types

        public Byte ReadByte()
        {
            var value = _buffer[_pos];

            _pos++;

            return value;
        }

        public Boolean ReadBoolean()
        {
            var value = _buffer[_pos];

            _pos++;

            return value == 0 ? false : true;
        }

        public UInt16 ReadUInt16()
        {
            _pos += 2;
            return BitConverter.ToUInt16(_buffer, _pos - 2);
        }

        public UInt32 ReadUInt32()
        {
            _pos += 4;
            return BitConverter.ToUInt32(_buffer, _pos - 4);
        }

        public UInt64 ReadUInt64()
        {
            _pos += 8;
            return BitConverter.ToUInt64(_buffer, _pos - 8);
        }

        public Int16 ReadInt16()
        {
            _pos += 2;
            return BitConverter.ToInt16(_buffer, _pos - 2);
        }

        public Int32 ReadInt32()
        {
            _pos += 4;
            return BitConverter.ToInt32(_buffer, _pos - 4);
        }

        public Int64 ReadInt64()
        {
            _pos += 8;
            return BitConverter.ToInt64(_buffer, _pos - 8);
        }

        public Single ReadSingle()
        {
            _pos += 4;
            return BitConverter.ToSingle(_buffer, _pos - 4);
        }

        public Double ReadDouble()
        {
            _pos += 8;
            return BitConverter.ToDouble(_buffer, _pos - 8);
        }

        public Byte[] ReadBytes(int count)
        {
            var buffer = new byte[count];

            System.Buffer.BlockCopy(_buffer, _pos, buffer, 0, count);

            _pos += count;

            return buffer;
        }

        #endregion Native data types

        #region Extended types

        public string ReadString()
        {
            var length = this.ReadInt32();
            var bytes = this.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes, 0, length);
        }

        public string ReadString(int length)
        {
            var bytes = this.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes, 0, length);
        }

        public DateTime ReadDateTime()
        {
            return new DateTime(this.ReadInt64(), DateTimeKind.Utc);
        }

        public Guid ReadGuid()
        {
            return new Guid(this.ReadBytes(16));
        }

        public ObjectId ReadObjectId()
        {
            return new ObjectId(this.ReadBytes(12));
        }

        public PageAddress ReadPageAddress()
        {
            return new PageAddress(this.ReadUInt32(), this.ReadUInt16());
        }

        public BsonValue ReadBsonValue(ushort length)
        {
            var type = (BsonType)this.ReadByte();

            switch (type)
            {
                case BsonType.Null: return BsonValue.Null;

                case BsonType.Int32: return this.ReadInt32();
                case BsonType.Int64: return this.ReadInt64();
                case BsonType.Double: return this.ReadDouble();

                case BsonType.String: return this.ReadString(length);

                case BsonType.Document: return new BsonReader().ReadDocument(this);
                case BsonType.Array: return new BsonReader().ReadArray(this);

                case BsonType.Binary: return this.ReadBytes(length);
                case BsonType.ObjectId: return this.ReadObjectId();
                case BsonType.Guid: return this.ReadGuid();

                case BsonType.Boolean: return this.ReadBoolean();
                case BsonType.DateTime: return this.ReadDateTime();

                case BsonType.MinValue: return BsonValue.MinValue;
                case BsonType.MaxValue: return BsonValue.MaxValue;
            }

            throw new NotImplementedException();
        }

        #endregion Extended types
    }
}