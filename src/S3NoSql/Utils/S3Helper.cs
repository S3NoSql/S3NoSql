using System;
using Amazon;
using S3NoSql.Utils.Extensions;

namespace S3NoSql.Utils
{
    public static class S3Helper
    {
        public static RegionEndpoint GetRegionEndpoint(string _region)
        {
            _region.AssertNullOrWhiteSpace();
            string region = _region.ToLower();

            switch (region)
            {
                case "useast1":
                    return RegionEndpoint.USEast1;
                case "useeast2":
                    return RegionEndpoint.USEast2;
                case "uswest1":
                    return RegionEndpoint.USWest1;
                case "uswest2":
                    return RegionEndpoint.USWest2;
            }

            throw new NotImplementedException("Region not currently implemented in S3Helper.  Add requested region");
        }
    }
}
