﻿using System;
using System.Collections.Generic;
using S3NoSql.Engine.Enums;
using S3NoSql.Utils;

namespace S3NoSql.Engine.Pages
{
    internal class HeaderPage : BasePage
    {
        /// <summary>
        /// Page type = Header
        /// </summary>
        public override PageType PageType { get { return PageType.Header; } }

        /// <summary>
        /// Header info the validate that datafile is a LiteDB file (27 bytes)
        /// </summary>
        private const string HEADER_INFO = "** This is a LiteDB file **";

        /// <summary>
        /// Datafile specification version
        /// </summary>
        private const byte FILE_VERSION = 7;

        /// <summary>
        /// Get/Set the pageID that start sequenece with a complete empty pages (can be used as a new page)
        /// Must be a field to be used as "ref"
        /// </summary>
        public uint FreeEmptyPageID;

        /// <summary>
        /// Last created page - Used when there is no free page inside file
        /// </summary>
        public uint LastPageID { get; set; }

        /// <summary>
        /// Database user version [2 bytes]
        /// </summary>
        public ushort UserVersion { get; set; }

        /// <summary>
        /// Password hash in SHA1 [20 bytes]
        /// </summary>
        public byte[] Password { get; set; }

        /// <summary>
        /// When using encryption, store salt for password
        /// </summary>
        public byte[] Salt { get; set; }

        /// <summary>
        /// Get a dictionary with all collection pages with pageID link
        /// </summary>
        public Dictionary<string, uint> CollectionPages { get; set; }

        public HeaderPage()
            : base(0)
        {
            this.FreeEmptyPageID = uint.MaxValue;
            this.LastPageID = 0;
            this.ItemCount = 1; // fixed for header
            this.FreeBytes = 0; // no free bytes on header
            this.UserVersion = 0;
            this.Password = new byte[20];
            this.Salt = new byte[16];
            this.CollectionPages = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Update freebytes + items count
        /// </summary>
        public override void UpdateItemCount()
        {
            this.ItemCount = 1; // fixed for header
            this.FreeBytes = 0; // no free bytes on header
        }

        #region Read/Write pages

        protected override void ReadContent(ByteReader reader)
        {
            var info = reader.ReadString(HEADER_INFO.Length);
            var ver = reader.ReadByte();

            if (info != HEADER_INFO) throw S3NoSqlException.InvalidDatabase();
            if (ver != FILE_VERSION) throw S3NoSqlException.InvalidDatabaseVersion(ver);

            reader.ReadUInt16(); // to be compatible with old "ChangeID"

            this.FreeEmptyPageID = reader.ReadUInt32();
            this.LastPageID = reader.ReadUInt32();
            this.UserVersion = reader.ReadUInt16();
            this.Password = reader.ReadBytes(this.Password.Length);
            this.Salt = reader.ReadBytes(this.Salt.Length);

            // read page collections references (position on end of page)
            var cols = reader.ReadByte();
            for (var i = 0; i < cols; i++)
            {
                this.CollectionPages.Add(reader.ReadString(), reader.ReadUInt32());
            }
        }

        protected override void WriteContent(ByteWriter writer)
        {
            writer.Write(HEADER_INFO, HEADER_INFO.Length);
            writer.Write(FILE_VERSION);
            writer.Write((ushort)0); // to be compatible with old "ChangeID"
            writer.Write(this.FreeEmptyPageID);
            writer.Write(this.LastPageID);
            writer.Write(this.UserVersion);
            writer.Write(this.Password);
            writer.Write(this.Salt);

            writer.Write((byte)this.CollectionPages.Count);
            foreach (var key in this.CollectionPages.Keys)
            {
                writer.Write(key);
                writer.Write(this.CollectionPages[key]);
            }
        }

        #endregion Read/Write pages
    }
}