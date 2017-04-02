using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM
{
    public static class Format
    {
        public static string Currency(this decimal d, string currency)
        {
            return d.Equals(Decimal.Truncate(d)) ? d.ToString("0 " + currency) : d.ToString("0.00 " + currency);
        }
        public static string CurrencyVN(this decimal d)
        {
            return Currency(d, "₫");
        }
        public static string CurrencyFR(this decimal d)
        {
            return Currency(d, "€");
        }
        public static System.Globalization.NumberFormatInfo NumberFormat(string CultureInfoCode)
        {
            var myNumberFormatInfo = new System.Globalization.CultureInfo(CultureInfoCode, false).NumberFormat;
            return myNumberFormatInfo;
        }
        public static System.Globalization.NumberFormatInfo NumberFormatEN()
        {
            return NumberFormat("en-US");
        }
        public static System.Globalization.NumberFormatInfo NumberFormatVN()
        {
            return NumberFormat("vi-VN");
        }
        public static string ToCurrency(this decimal d, System.Globalization.NumberFormatInfo NumberFormat, int digits)
        {
            return d.ToString("C" + digits.ToString(), NumberFormat);
        }
        public static string ToCurrency(this decimal d, System.Globalization.NumberFormatInfo NumberFormat)
        {
            return d.ToString("C", NumberFormat);
        }
        public static string ToCurrencyVN(this decimal d,int digits)
        {
            return ToCurrency(d, NumberFormatVN(), digits);
        }
        public static string ToCurrencyVN(this decimal d)
        {
            return ToCurrency(d, NumberFormatVN());
        }
        public static string ToCurrencyEN(this decimal d, int digits)
        {
            return ToCurrency(d, NumberFormatEN(), digits);
        }
        public static string ToCurrencyEN(this decimal d)
        {
            return ToCurrency(d, NumberFormatEN());
        }
    }
}

