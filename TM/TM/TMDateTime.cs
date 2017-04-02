using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Helper
{
    public static class TMDateTime
    {
        public static DateTime LastDate(int year, int month)
        {
            return new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
        }
        public static DateTime LastDate()
        {
            return LastDate(DateTime.Now.Year, DateTime.Now.Month);
        }
        public static int DaysInMonth()
        {
            return DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        }
    }
}
