﻿using System;
using S3NoSql.Engine.Enums;
using S3NoSql.Utils;

namespace S3NoSql.Engine.Pages
{
    /// <summary>
    /// Represent a empty page (reused)
    /// </summary>
    internal class EmptyPage : BasePage
    {
        /// <summary>
        /// Page type = Empty
        /// </summary>
        public override PageType PageType { get { return PageType.Empty; } }

        public EmptyPage(uint pageID)
            : base(pageID)
        {
            this.ItemCount = 0;
            this.FreeBytes = PAGE_AVAILABLE_BYTES;
        }

        public EmptyPage(BasePage page)
            : base(page.PageID)
        {
            if(page.DiskData.Length > 0)
            {
                this.DiskData = new byte[BasePage.PAGE_SIZE];
                Buffer.BlockCopy(page.DiskData, 0, this.DiskData, 0, BasePage.PAGE_SIZE);
            }
        }

        /// <summary>
        /// Update freebytes + items count
        /// </summary>
        public override void UpdateItemCount()
        {
            this.ItemCount = 0;
            this.FreeBytes = PAGE_AVAILABLE_BYTES;
        }

        #region Read/Write pages

        protected override void ReadContent(ByteReader reader)
        {
        }

        protected override void WriteContent(ByteWriter writer)
        {
        }

        #endregion Read/Write pages
    }
}