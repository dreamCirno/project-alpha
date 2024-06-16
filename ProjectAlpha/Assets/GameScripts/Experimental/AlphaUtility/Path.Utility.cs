namespace ProjectAlpha
{
    public static partial class AlphaUtility
    {
        /// <summary>
        /// 路径相关的实用函数。
        /// </summary>
        public static class Path
        {
            /// <summary>
            /// Combines two paths, and replaces all backslases with forward slash.
            /// </summary>
            public static string Combine(string a, string b)
            {
                a = a.Replace("\\", "/").TrimEnd('/');
                b = b.Replace("\\", "/").TrimStart('/');
                return a + "/" + b;
            }
        }
    }
}