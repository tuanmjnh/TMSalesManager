using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Paging
{
    public static class Page
    {
        public static int PageNumber { get; set; }
        public static int TotalPage { get; set; }
        public static int RowIndex { get; set; }
        public static int TotalRow { get; set; }
        public static int PageSize { get; set; }
        //private List<object> listItem;
        //public static Page(IEnumerable<dynamic> query, int PageNumber = 1, int PageSize = 15)
        //{
        //    Page.PageNumber = PageNumber;
        //    Page.PageSize = PageSize;
        //    Page.TotalRow = query.Count();
        //    Page.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalRow) / Convert.ToDecimal(PageSize)));
        //    listItem = query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        //}
        //public Page(List<object> query, int PageNumber = 1, int PageSize = 15)
        //{
        //    this.PageNumber = PageNumber;
        //    this.PageSize = PageSize;
        //    this.TotalRow = query.Count();
        //    this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalRow) / Convert.ToDecimal(PageSize)));
        //    listItem = query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        //}
        public static List<object> ToPage(this IEnumerable<dynamic> query, int PageNumber, int PageSize)
        {
            TotalRow = query.Count();
            TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalRow) / Convert.ToDecimal(PageSize)));
            return query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
        public static List<object> ToPage(this IEnumerable<dynamic> query, int PageNumber)
        {
            return ToPage(query, PageNumber, PageSize);
        }
        public static List<object> ToPage(this IEnumerable<dynamic> query)
        {
            return ToPage(query, PageNumber, PageSize);
        }

        public static List<object> ToPage(this List<object> query, int PageNumber, int PageSize)
        {
            TotalRow = query.Count();
            TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalRow) / Convert.ToDecimal(PageSize)));
            return query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
        public static List<object> ToPage(this List<object> query, int PageNumber)
        {
            return ToPage(query, PageNumber, PageSize);
        }
        public static List<object> ToPage(this List<object> query)
        {
            return ToPage(query, PageNumber, PageSize);
        }

        public static void ToFirst()
        {
            PageNumber = 1;
        }
        public static void ToPrevious()
        {
            if (PageNumber > 1) PageNumber--;
        }
        public static void ToNext()
        {
            if (PageNumber < TotalPage) PageNumber++;
        }
        public static void ToLast()
        {
            PageNumber = TotalPage;
        }
        public static string getRowIndexStr(Int32 index)
        {
            index = (index + (PageNumber - 1) * PageSize);
            if (index < 10)
                return "0" + index;
            else return index + "";
        }
        public static Int32 getRowIndex(Int32 index) { return Convert.ToInt32(getRowIndexStr(index)); }
        private static string ReplacePage(string href, int page)
        {
            return href.Replace("page=0", "page=" + page.ToString());
        }
    }
    //public class Page
    //{
    //    public int PageNumber { get; set; }
    //    public int TotalPage { get; set; }
    //    public int RowIndex { get; set; }
    //    public int TotalRow { get; set; }
    //    public int PageSize;
    //    private List<object> listItem;
    //    public Page(IEnumerable<dynamic> query, int PageNumber = 1, int PageSize = 15)
    //    {
    //        this.PageNumber = PageNumber;
    //        this.PageSize = PageSize;
    //        this.TotalRow = query.Count();
    //        this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalRow) / Convert.ToDecimal(PageSize)));
    //        listItem = query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
    //    }
    //    public Page(List<object> query, int PageNumber = 1, int PageSize = 15)
    //    {
    //        this.PageNumber = PageNumber;
    //        this.PageSize = PageSize;
    //        this.TotalRow = query.Count();
    //        this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalRow) / Convert.ToDecimal(PageSize)));
    //        listItem = query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
    //    }
    //    public List<object> Get()
    //    {
    //        return listItem;
    //    }
    //    public string getRowIndexStr(Int32 index)
    //    {
    //        index = (index + (PageNumber - 1) * PageSize);
    //        if (index < 10)
    //            return "0" + index;
    //        else return index + "";
    //    }
    //    public Int32 getRowIndex(Int32 index) { return Convert.ToInt32(getRowIndexStr(index)); }
    //    private string ReplacePage(string href, int page)
    //    {
    //        return href.Replace("page=0", "page=" + page.ToString());
    //    }
    //}
}
