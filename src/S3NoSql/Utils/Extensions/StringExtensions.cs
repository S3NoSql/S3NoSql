/*--------------------------------------------------------------------------------*/
/* Adapted from LiteDB (https://github.com/mbdavid/LiteDB/)
/*--------------------------------------------------------------------------------*/
namespace S3NoSql.Utils.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string _str)
        {
            return string.IsNullOrWhiteSpace(_str);
        }
    }
}
