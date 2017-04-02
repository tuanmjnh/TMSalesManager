using System;

public static class handleString
{
    public static string TMReplaceArray(string str, string reg, string val)
    {
        for (int i = 0; i < str.Length; i++)
            for (int j = 0; j < reg.Length; j++)
                str = str[i] == reg[j] ? str.Replace(str[i].ToString(), val) : str;
        return str;
    }
    public static string TMAscii(this string s)
    {
        s = s.Replace("[", "").Replace("]", "").ToLower(); ;
        s = TMReplaceArray(s, "[áàãạảâầấậẫẩăằắẵặẳ]", "a"); //"[áàãạảâầấậẫẩăằắẵặẳÀÁÃẠẢÂẦẤẪẬĂẰẮẲẴẶẨ]"
        s = TMReplaceArray(s, "[èéẹẽẻêếềễểệ]", "e"); //[èéẹẽẻêếềễểệÈÉẼẺẸÊỀẾỆỄỂ]
        s = TMReplaceArray(s, "[ìíịỉĩ]", "i"); //[ìíịỉĩỈÌÍỊĨ]
        s = TMReplaceArray(s, "[òóõọỏôỗộồốổơỡờớợỡở]", "o"); //[òóõọỏôỗộồốổơỡờớợỡởÒÓỌÕỎÔỖỘỒỐỔƠỢỚỜỠỞ]
        s = TMReplaceArray(s, "[ùúụũủưừứựữử]", "u"); //[ùúụũủưừứựữửÙÚỤŨỦƯỰỪỮỨỬ]
        s = TMReplaceArray(s, "[ýỳỹỷỵ]", "y"); //[ýỳỹỷỵỲÝỴỸỶ]
        s = TMReplaceArray(s, "[đ]", "d"); //[đĐ]
        s = TMReplaceArray(s, "[~`!@#$%^&*()-_+={}\\|;:'\"<,>.?/”“‘’„‰‾–—]", "");
        return s.Replace(" ", "-");
    }
    public static string TMAscii2(this string s)
    {
        s = s.Replace("[", "").Replace("]", "").ToLower(); ;
        s = System.Text.RegularExpressions.Regex.Replace(s, "[áàãạảâầấậẫẩăằắẵặẳ]", "a"); //"[áàãạảâầấậẫẩăằắẵặẳÀÁÃẠẢÂẦẤẪẬĂẰẮẲẴẶẨ]"
        s = System.Text.RegularExpressions.Regex.Replace(s, "[èéẹẽẻêếềễểệ]", "e"); //[èéẹẽẻêếềễểệÈÉẼẺẸÊỀẾỆỄỂ]
        s = System.Text.RegularExpressions.Regex.Replace(s, "[ìíịỉĩ]", "i"); //[ìíịỉĩỈÌÍỊĨ]
        s = System.Text.RegularExpressions.Regex.Replace(s, "[òóõọỏôỗộồốổơỡờớợỡở]", "o"); //[òóõọỏôỗộồốổơỡờớợỡởÒÓỌÕỎÔỖỘỒỐỔƠỢỚỜỠỞ]
        s = System.Text.RegularExpressions.Regex.Replace(s, "[ùúụũủưừứựữử]", "u"); //[ùúụũủưừứựữửÙÚỤŨỦƯỰỪỮỨỬ]
        s = System.Text.RegularExpressions.Regex.Replace(s, "[ýỳỹỷỵ]", "y"); //[ýỳỹỷỵỲÝỴỸỶ]
        s = System.Text.RegularExpressions.Regex.Replace(s, "[đ]", "d"); //[đĐ]
        s = System.Text.RegularExpressions.Regex.Replace(s, "[~`!@#$%^&*()-_+={}\\|;:'\"<,>.?/”“‘’„‰‾–—]", "");
        //s = System.Text.RegularExpressions.Regex.Replace(s, "[ ]", " ");
        return s.Replace(" ", "-");
    }
    public static String Replace(this string str, string[][] chars)
    {
        //chars = new string[][] { new[] { "", "" }, new[] { "", "" } };
        for (int i = 0; i < chars.Length; i++)
            str = str.Replace(chars[i][0], chars[i][1]);
        return str;
    }
    public static String Replace(this string str, string[,] chars)
    {
        //chars = new string[,] { { "", "" }, { "", "" } };
        for (int i = 0; i < (chars.Length / 2); i++)
            str = str.Replace(chars[i, 0], chars[i, 1]);
        return str;
    }
    public static String ArrayToString(this string[] str, string chars)
    {
        try
        {
            string rs = "";
            for (int i = 0; i < str.Length; i++)
                rs += str[i] + chars;
            return rs.Substring(0, rs.Length - 1);
        }
        catch { return null; }
    }
    public static String ArrayToString(this string[] str)
    {
        return ArrayToString(str, ",");
    }

    public static String ArrayToString(this object[] str, string chars)
    {
        try
        {
            string rs = "";
            for (int i = 0; i < str.Length; i++)
                rs += str[i] + chars;
            return rs.Substring(0, rs.Length - 1);
        }
        catch { return null; }
    }
    public static String ArrayToString(this object[] str)
    {
        return ArrayToString(str, ",");
    }
    public static String ArrayToString(this System.Collections.Generic.List<string> str, string chars)
    {
        try
        {
            string rs = "";
            for (int i = 0; i < str.Count; i++)
                rs += str[i] + chars;
            return rs.Substring(0, rs.Length - 1);
        }
        catch { return null; }
    }
    public static String ArrayToString(this System.Collections.Generic.List<string> str)
    {
        return ArrayToString(str, ",");
    }

    public static bool Contains(this string[] str, string chars)
    {
        for (int i = 0; i < str.Length; i++)
            if (str[i].ToLower() == chars.ToLower())
                return true;
        return false;
    }
    public static int[] SplitToInt(this string str, int leng, char chars)
    {
        int[] rs = new int[leng];
        string[] tmp = str.Split(chars);
        if (tmp.Length > 0)
            for (int i = 0; i < tmp.Length; i++)
            {
                try
                {
                    rs[i] = Convert.ToInt32(tmp[i]);
                }
                catch (Exception) { continue; }
            }
        return rs;
    }
    public static System.Collections.Generic.List<int> SplitToListInt(this string str, char chars)
    {
        System.Collections.Generic.List<int> rs = new System.Collections.Generic.List<int>();
        string[] tmp = str.Split(chars);
        if (tmp.Length > 0)
            for (int i = 0; i < tmp.Length; i++)
                try
                {
                    rs.Add(int.Parse(tmp[i]));
                }
                catch (Exception) { continue; }
        return rs;
    }
    public static String TMDateFullVN(this DateTime s)
    {
        return s.ToString("dd/MM/yyyy hh:mm:ss tt");
    }
    public static String TMDateFullVN2(this DateTime s)
    {
        return s.ToString("dd/MM/yyyy hh:mm");
    }
    public static String TMDateShortVN(this DateTime s)
    {
        return s.ToString("dd/MM/yyyy");
    }
    public static String TMDateSimpleVN(this DateTime s)
    {
        return s.ToString("dd/MM");
    }
    public static String TMTimeVN(this DateTime s)
    {
        return s.ToString("hh:mm:ss");
    }
    public static String TMDateFullVN(this object s)
    {
        try { return DateTime.Parse(s.ToString()).ToString("dd/MM/yyyy hh:mm:ss tt"); }
        catch (Exception) { return s.ToString(); }
    }
    public static String TMDateFullVN2(this object s)
    {
        try { return DateTime.Parse(s.ToString()).ToString("dd/MM/yyyy hh:mm"); }
        catch (Exception) { return s.ToString(); }
    }
    public static String TMDateShortVN(this object s)
    {
        try { return DateTime.Parse(s.ToString()).ToString("dd/MM/yyyy"); }
        catch (Exception) { return s.ToString(); }
    }
    public static String TMDateSimpleVN(this object s)
    {
        try { return DateTime.Parse(s.ToString()).ToString("dd/MM"); }
        catch (Exception) { return s.ToString(); }
    }
    public static String TMTimeVN(this object s)
    {
        try { return DateTime.Parse(s.ToString()).ToString("hh:mm:ss"); }
        catch (Exception) { return s.ToString(); }
    }
    public static String TMPriceVN(this object obj)
    {
        try { return Convert.ToDecimal(obj).ToString("N0", TM.lib.FormatNumberVI) + " ₫"; }
        catch (Exception) { return obj.ToString(); }
    }
    public static String TMPriceVN(this object obj, string s)
    {
        try { return Convert.ToDecimal(obj).ToString("N0", TM.lib.FormatNumberVI) + " " + s; }
        catch (Exception) { return obj.ToString(); }
    }
    public static String TMPriceVN(this decimal obj)
    {
        try { return obj.ToString("N0", TM.lib.FormatNumberVI) + " ₫"; }
        catch (Exception) { return obj.ToString(); }
    }
    public static String TMPriceVN(this decimal obj, string s)
    {
        try { return obj.ToString("N0", TM.lib.FormatNumberVI) + " " + s; ; }
        catch (Exception) { return obj.ToString(); }
    }
    public static String TMPrice(this float obj)
    {
        try { return obj.ToString("N0", TM.lib.FormatNumberVI); }
        catch (Exception) { return obj.ToString(); }
    }
    public static String TMPrice(this float obj, string s)
    {
        try { return obj.ToString("N0", TM.lib.FormatNumberVI) + " " + s; ; }
        catch (Exception) { return obj.ToString(); }
    }
    public static String TMPrice(this int obj)
    {
        try { return obj.ToString("N0", TM.lib.FormatNumberVI); }
        catch (Exception) { return obj.ToString(); }
    }
    public static String TMPrice(this int obj, string s)
    {
        try { return obj.ToString("N0", TM.lib.FormatNumberVI) + " " + s; ; }
        catch (Exception) { return obj.ToString(); }
    }
    public static string CutFirstLast(this string s)
    {
        try
        {
            s = s.Trim();
            if (s != "Null" && s != string.Empty && s.Length > 1)
                return s.Substring(1, s.Length - 2);
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string CutFirstLast(this object s)
    {
        try
        {
            s = s.ToString().Trim();
            if (s.ToString() != "Null" && s.ToString() != string.Empty && s.ToString().Length > 1)
                return s.ToString().Substring(1, s.ToString().Length - 2);
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string CutLast(this string s)
    {
        try
        {
            s = s.Trim();
            if (s != "Null" && s != string.Empty && s.Length > 1)
                return s.Substring(0, s.Length - 1);
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string CutLast(this object s)
    {
        try
        {
            s = s.ToString().Trim();
            if (s.ToString() != "Null" && s.ToString() != string.Empty && s.ToString().Length > 1)
                return s.ToString().Substring(0, s.ToString().Length - 2);
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToTag(this object s)
    {
        try
        {
            string[] v = s.ToString().Split(',');
            s = "";
            for (int i = 0; i < v.Length; i++)
                s += v[i].Trim() + ", ";
            return s.ToString().Substring(0, s.ToString().Length - 2);
        }
        catch (Exception) { return ""; }
    }
    public static string ToTag(this string s)
    {
        try
        {
            string[] v = s.Split(',');
            s = "";
            for (int i = 0; i < v.Length; i++)
                s += v[i].Trim() + ", ";
            return s.Substring(0, s.Length - 2);
        }
        catch (Exception) { return ""; }
    }
    public static string ToTag2(this object s)
    {
        try
        {
            string[] v = s.CutFirstLast().Split(',');
            s = "";
            for (int i = 0; i < v.Length; i++)
                s += v[i].Trim() + ", ";
            return s.ToString().Substring(0, s.ToString().Length - 2);
        }
        catch (Exception) { return ""; }
    }
    public static string ToTag2(this string s)
    {
        try
        {
            string[] v = s.CutFirstLast().Split(',');
            s = "";
            for (int i = 0; i < v.Length; i++)
                s += v[i].Trim() + ", ";
            return s.Substring(0, s.Length - 2);
        }
        catch (Exception) { return ""; }
    }
    public static string[] ToTagList(this string s)
    {
        try
        {
            string[] v = s.CutFirstLast().Split(',');
            return v;
        }
        catch (Exception) { return null; }
    }
    public static string ToDesc(this string s)
    {
        try
        {
            if (s != "Null" && s != string.Empty && s.Length > 1)
                return s.Length > 200 ? s.Substring(0, 200) : s;
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToDesc(this string s, int length)
    {
        try
        {
            if (s != "Null" && s != string.Empty && s.Length > 1)
                return s.Length > length ? s.Substring(0, length) : s;
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToDesc(this string s, int length, string chars)
    {
        try
        {
            if (s != "Null" && s != string.Empty && s.Length > 1)
                return s.Length > length ? s.Substring(0, length) + " " + chars : s;
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToDesc(this object s)
    {
        try
        {
            if (s.ToString() != "Null" && s.ToString() != string.Empty && s.ToString().Length > 1)
                return s.ToString().Length > 200 ? s.ToString().Substring(0, 200) : s.ToString();
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToDesc(this object s, int length)
    {
        try
        {
            if (s.ToString() != "Null" && s.ToString() != string.Empty && s.ToString().Length > 1)
                return s.ToString().Length > length ? s.ToString().Substring(0, length) : s.ToString();
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToDesc(this object s, int length, string chars)
    {
        try
        {
            if (s.ToString() != "Null" && s.ToString() != string.Empty && s.ToString().Length > 1)
                return s.ToString().Length > length ? s.ToString().Substring(0, length) + " " + chars : s.ToString();
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToImage(this string s)
    {
        try
        {
            if (!String.IsNullOrWhiteSpace(s))
            {
                s = s.Substring(s.IndexOf("<img"));
                return s.Substring(0, s.IndexOf("/>") + 2);
            }
            return "";
        }
        catch (Exception) { return ""; }

    }
    public static string ToImage(this object s)
    {
        try
        {
            if (!String.IsNullOrWhiteSpace(s.ToString()))
            {
                s = s.ToString().Substring(s.ToString().IndexOf("<img"));
                return s.ToString().Substring(0, s.ToString().IndexOf("/>") + 2);
            }
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToImageSrc(this string s)
    {
        try
        {
            if (s != string.Empty)
            {
                s = s.ToImage();
                s = s.Substring(s.IndexOf("src=\"") + 5);
                return s.Substring(0, s.IndexOf("\""));
            }
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToImageSrc(this object s)
    {
        try
        {
            if (s.ToString() != string.Empty)
            {
                s = s.ToString().ToImage();
                s = s.ToString().Substring(s.ToString().IndexOf("src=\"") + 5);
                return s.ToString().Substring(0, s.ToString().IndexOf("\""));
            }
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToSubString(this string s, string first, string last)
    {
        try
        {
            if (s != string.Empty)
            {
                s = s.Substring(s.IndexOf(first));
                return s.Substring(0, s.IndexOf(last));
            }
            return "";
        }
        catch (Exception) { return ""; }
    }
    public static string ToSubString(this object s, string first, string last)
    {
        try
        {
            if (!String.IsNullOrWhiteSpace(s.ToString()))
            {
                s = s.ToString().Substring(s.ToString().IndexOf(first));
                s = s.ToString().Substring(0, s.ToString().IndexOf(last));
                return s.ToString();
            }
            return "";
        }
        catch (Exception) { return ""; }
    }
}