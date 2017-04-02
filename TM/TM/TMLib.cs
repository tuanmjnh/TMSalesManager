using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for TMLIB
/// </summary>
namespace TM
{
    public class lib
    {
        public static Dictionary<int, string> day()
        {
            Dictionary<int, string> dday = new Dictionary<int, string>();
            for (int i = 1; i < 32; i++)
                dday.Add(i, i.ToString());
            return dday;
        }
        public static Dictionary<int, string> day(string select)
        {
            Dictionary<int, string> dday = day();
            dday.Add(0, select);
            return dday;
        }
        public static Dictionary<int, string> month()
        {
            Dictionary<int, string> dmonth = new Dictionary<int, string>();
            for (int i = 1; i < 13; i++)
                dmonth.Add(i, i.ToString());
            return dmonth;
        }
        public static Dictionary<int, string> month(string select)
        {
            Dictionary<int, string> dmonth = month();
            dmonth.Add(0, select);
            return dmonth;
        }
        public static Dictionary<int, string> year()
        {
            Dictionary<int, string> dyear = new Dictionary<int, string>();
            for (int i = 1900; i < DateTime.Now.Year; i++)
                dyear.Add(i, i.ToString());
            return dyear;
        }
        public static Dictionary<int, string> year(string select)
        {
            Dictionary<int, string> dyear = year();
            dyear.Add(0, select);
            return dyear;
        }
        public static bool IsNull(string input)
        {
            try { return (!String.IsNullOrWhiteSpace(input) || input.ToLower() != "null") ? true : false; }
            catch (Exception) { return false; }
        }
        public static string SubStr(string s, int firstOfLast, int lenght)
        {
            try
            {
                if (s.Length >= firstOfLast + lenght) s = s.Substring(s.Length - lenght - firstOfLast, lenght);
                else s = null;
                return s;
            }
            catch (Exception) { throw; }
        }
        public static int SumStr(string str)
        {
            try
            {
                int t = 0;
                if (str != string.Empty && RE.RegEx.isNumber(str))
                    for (int i = 0; i < str.Length; i++)
                        t += Convert.ToInt32(str.Substring(i, 1));
                return t;
            }
            catch (Exception) { throw; }
        }
        public static string ReplaceChars(string str)
        {
            return str.Replace(".", "").Replace(",", "").Replace(" ", "");
        }
        public static string ReplaceChars(string str, string[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
                str.Replace(chars[i], "");
            return str;
        }
        public static System.Globalization.NumberFormatInfo FormatNumber(string location)
        {
            string reg = "vi-vn";
            switch (location)
            {
                case "vi": reg = "vi-vn"; break;
                case "en": reg = "en-US"; break;
                case "fr": reg = "fr-FR"; break;
                case "jp": reg = "ja-JP"; break;
                case "sv": reg = "sv-SE"; break;
                case "ru": reg = "ru-RU"; break;
                case "da": reg = "da-DK"; break;
                default: reg = "vi-vn"; break;
            }
            return new System.Globalization.CultureInfo(reg, false).NumberFormat;
        }
        public static System.Globalization.NumberFormatInfo FormatNumberVI
        {
            get { return new System.Globalization.CultureInfo("vi-vn", false).NumberFormat; }
        }
        public static bool CheckImagesType(string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif": return true;
                case ".png": return true;
                case ".jpg": return true;
                case ".jpeg": return true;
                default: return false;
            }
        }
        public static bool CheckFileType(string fileName, string[] extend)
        {
            bool check = false;
            fileName = System.IO.Path.GetExtension(fileName);
            fileName = fileName.ToLower().Substring(1, fileName.Length - 1);
            for (int i = 0; i < extend.Length; i++)
                if (fileName == extend[i].ToLower()) { check = true; break; }
            return check;
        }
        public static bool CheckExcelType(string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".xlsx": return true;
                case ".xls": return true;
                default: return false;
            }
        }
        public string Replace(string str, string[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
                str = str.Replace(chars[i], "");
            return str;
        }
        public static string randoms(int from, int to)
        {
            Random rd = new Random();
            int i = rd.Next(from, to);
            return Encrypt.CryptoMD5(i.ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Month.ToString() +
                DateTime.Now.Year.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() +
                DateTime.Now.Millisecond.ToString());
        }
        public static string randoms()
        {
            Random rd = new Random();
            return Encrypt.CryptoMD5(rd.Next(6, 9).ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Month.ToString() +
                DateTime.Now.Year.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() +
                DateTime.Now.Millisecond.ToString()).Substring(0, 15).ToUpper();
        }
    }
}