using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Silkier.Extensions
{
    public static class StringExtension
    {
        public static string ToTitleCase(this string str)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        public static string Left(this string str, int length)
        {
            str = (str ?? string.Empty);
            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string Right(this string str, int length)
        {
            str = (str ?? string.Empty);
            return (str.Length >= length)
                ? str.Substring(str.Length - length, length)
                : str;
        }

        public static char[] Right(this char[] str, int length)
        {
            return str.Skip(str.Length - length).Take(length).ToArray();
        }
        public static char[] Left(this char[] str, int length)
        {
            return str.Take(length).ToArray();
        }



        public static byte[] Right(this byte[] str, int length)
        {
            return str.Skip(str.Length - length).Take(length).ToArray();
        }
        public static byte[] Left(this byte[] str, int length)
        {
            return str.Take(length).ToArray();
        }
    static    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        public static byte[] ToBytes(this string s)
        {
            return System.Text.Encoding.Default.GetBytes(s);
        }
        public static byte[] ToBytes(this string s, Encoding encoding)
        {
            return encoding?.GetBytes(s);
        }
        public static string GetMd5Sum(this string s, Encoding encoding)
        {
            string t2 = BitConverter.ToString(md5.ComputeHash(s.ToBytes(encoding)));
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static string GetMd5Sum(this string  s)
        {
            string t2 = BitConverter.ToString(md5.ComputeHash(s.ToBytes()));
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static string GetMd5Sum<T>(this T s) where T:class
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(s);
            string t2 = BitConverter.ToString(md5.ComputeHash(str.ToBytes()));
            t2 = t2.Replace("-", "");
            return t2;
        }
    }
}
