using System;
using System.Text;
using System.Data.OleDb;
using System.Collections.Generic;
using ExcelApp = Microsoft.Office.Interop.Excel;
namespace TM.Interop
{
    public class ExcelStatic
    {
        //Declare
        static ExcelApp.Application App = new ExcelApp.Application();
        static ExcelApp.Workbook Book = null;
        static ExcelApp.Worksheet Sheet = null;
        static ExcelApp.Range Range = null;
        public static string DataSource { get; set; }
        public static ExcelApp.Sheets ToSheet(string DataSource)
        {
            try
            {
                //New Application
                //App = new ExcelApp.Application();
                App.Visible = false;
                Book = App.Workbooks.Open(DataSource,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                //Each Worksheet
                //for (int i = 1; i <= Book.Sheets.Count; i++)
                //{
                //    Sheet = (ExcelApp.Worksheet)Book.Sheets[i];
                //    //int lastRow = Sheet.Rows.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
                //    //int lastUsedRow = Sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                //    //Excel.Range lastCell = Sheet.UsedRange.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                //    Range = Sheet.UsedRange;
                //    //Range.ClearFormats();
                //    //Range.Clear();
                //    rs.Add((Object[,])Range.Cells.Value2);
                //    //int row = Sheet.Rows.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Row;
                //    //System.Array values = (System.Array)Range.Cells.Value;

                //    //totalRecords = Range.Rows.Count - 1;
                //}
                return Book.Sheets;
            }
            catch (Exception) { throw; }
        }
        public static ExcelApp.Sheets ToSheet()
        {
            return ToSheet(DataSource);
        }
        public static int getRows()
        {
            try
            {
                //return Object[,].GetUpperBound(0);
                return Sheet.UsedRange.Rows.Count;
            }
            catch (Exception) { return 0; }
        }
        public static int getColumns()
        {
            try
            {
                return Sheet.UsedRange.Columns.Count;
            }
            catch (Exception) { return 0; }
        }
        public static bool InsertColumn(string DataSource, int IndexSheet, string ColumnName, string desfile)
        {
            try
            {
                //New Application
                App = new ExcelApp.Application();
                App.Visible = false;
                Book = App.Workbooks.Open(DataSource);
                //Each Worksheet
                ExcelApp.Sheets s = Book.Sheets;
                ExcelApp.Worksheet Sheet = (ExcelApp.Worksheet)s[IndexSheet];
                ExcelApp.Range Range = Sheet.UsedRange;

                int colCount = Range.Columns.Count;
                //int rowCount = Range.Rows.Count;
                //Range = (ExcelApp.Range)Sheet.Cells[rowCount, colCount];
                //ExcelApp.Range column = Range.EntireColumn;
                //Range.EntireColumn.Insert(ExcelApp.XlInsertShiftDirection.xlShiftToRight,
                //                        ExcelApp.XlInsertFormatOrigin.xlFormatFromRightOrBelow);
                //column.Insert(ExcelApp.XlInsertShiftDirection.xlShiftToRight, false);
                Sheet.Cells[1, colCount + 1] = ColumnName;
                App.DisplayAlerts = false;
                Book.SaveAs(desfile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    ExcelApp.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception) { return false; }
            finally
            {
                //Clean up
                CleanUp(App, Book, Sheet, Range);
            }
        }
        public static bool InsertColumn(int IndexSheet, string ColumnName, string desfile)
        {
            return InsertColumn(DataSource, IndexSheet, ColumnName, desfile);
        }
        public static bool InsertColumn(string DataSource, string ColumnName, string desfile)
        {
            return InsertColumn(DataSource, 1, ColumnName, desfile);
        }
        public static bool InsertColumn(string ColumnName, string desfile)
        {
            return InsertColumn(DataSource, 1, ColumnName, desfile);
        }
        public static bool InsertColumn(string ColumnName)
        {
            return InsertColumn(DataSource, 1, ColumnName, DataSource);
        }
        public static object[,] ToObject(string DataSource, int IndexSheet)
        {
            try
            {
                //Each Worksheet
                ExcelApp.Sheets s = ToSheet(DataSource);
                ExcelApp.Worksheet Sheet = (ExcelApp.Worksheet)s[IndexSheet];
                ExcelApp.Range Range = Sheet.UsedRange;
                return (object[,])Range.Cells.Value2;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally
            {
                //Clean up
                CleanUp(App, Book, Sheet, Range);
            }
        }
        public static object[,] ToObject(string DataSource)
        {
            return ToObject(DataSource, 1);
        }
        public static object[,] ToObject()
        {
            return ToObject(DataSource, 1);
        }
        public static List<object[,]> ToListObject(string DataSource)
        {
            try
            {
                //Return List
                List<object[,]> rs = new List<object[,]>();
                //Each Worksheet
                ExcelApp.Sheets s = ToSheet(DataSource);
                for (int i = 1; i <= s.Count; i++)
                {
                    ExcelApp.Worksheet Sheet = (ExcelApp.Worksheet)s[i];
                    ExcelApp.Range Range = Sheet.UsedRange;
                    rs.Add(Range.Cells.Value2);
                }
                return rs;
            }
            catch (Exception) { throw; }
            finally
            {
                //Clean up
                CleanUp(App, Book, Sheet, Range);
            }
        }
        public static List<object[,]> ToListObject()
        {
            return ToListObject(DataSource);
        }
        public static List<object[]> ToList(object[,] obj)
        {
            try
            {
                //Declare table
                var list = new List<object[]>();
                //var obj = ToObject(DataSource, IndexSheet);
                if (obj != null)
                {
                    var Rows = obj.GetLength(0);
                    var Columns = obj.GetLength(1);
                    //Add Rows
                    for (int i = 1; i <= Rows; i++)
                    {
                        var tmp = new object[Columns];
                        for (int j = 0; j < Columns; j++)
                            tmp[j] = obj[i, j + 1];
                        list.Add(tmp);
                    }
                }
                return list;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public static List<object[]> ToList(string DataSource, int IndexSheet)
        {
            return ToList(ToObject(DataSource, IndexSheet));
        }
        public static List<object[]> ToList(string DataSource)
        {
            return ToList(DataSource, 1);
        }
        public static List<object[]> ToList()
        {
            return ToList(DataSource, 1);
        }
        public static System.Data.DataTable ToDataTable(object[,] obj)
        {
            try
            {
                //Declare table
                var dt = new System.Data.DataTable();
                //var obj = ToObject(DataSource, IndexSheet);
                if (obj != null)
                {
                    var Rows = obj.GetLength(0);
                    var Columns = obj.GetLength(1);

                    //Add Columns
                    for (int i = 1; i <= Columns; i++)
                        dt.Columns.Add(obj[1, i].ToString());

                    //Add Rows
                    for (int i = 2; i <= Rows; i++)
                    {
                        var r = dt.NewRow();
                        for (int j = 0; j < Columns; j++)
                            r[j] = obj[i, j + 1];

                        dt.Rows.Add(r);
                    }
                }
                return dt;
            }
            catch (Exception) { throw; }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, int IndexSheet)
        {
            return ToDataTable(ToObject(DataSource, IndexSheet));
        }
        public static System.Data.DataTable ToDataTable(string DataSource)
        {
            return ToDataTable(DataSource, 1);
        }
        public static System.Data.DataTable ToDataTable()
        {
            return ToDataTable(DataSource, 1);
        }
        public static System.Data.DataSet ToDataSet(string DataSource)
        {
            try
            {
                var ds = new System.Data.DataSet();
                //Each Worksheet
                //ExcelApp.Sheets s = ToSheet(DataSource);
                List<object[,]> list = ToListObject(DataSource);
                for (int i = 1; i <= list.Count; i++)
                {
                    //ExcelApp.Worksheet Sheet = (ExcelApp.Worksheet)s[i];
                    //ExcelApp.Range Range = Sheet.UsedRange;
                    //var obj = (object[,])Range.Cells.Value2;
                    //Declare table

                    //var dt = new System.Data.DataTable();
                    //if (obj != null)
                    //{
                    //    var Rows = obj.GetLength(0);
                    //    var Columns = obj.GetLength(1);

                    //    //Add Columns
                    //    for (int j = 1; j <= Columns; j++)
                    //        dt.Columns.Add(obj[1, j].ToString());

                    //    //Add Rows
                    //    for (int j = 2; j <= Rows; j++)
                    //    {
                    //        var r = dt.NewRow();
                    //        for (int k = 0; k < Columns; k++)
                    //            r[k] = obj[j, k + 1];
                    //        dt.Rows.Add(r);
                    //    }
                    //}
                    //ds.Tables.Add(dt);
                    ds.Tables.Add(ToDataTable(list[i]));
                }
                return ds;
            }
            catch (Exception) { throw; }
            finally
            {
                //Clean up
                CleanUp(App, Book, Sheet, Range);
            }
        }
        public static System.Data.DataSet ToDataSet()
        {
            return ToDataSet(DataSource);
        }
        public static void CleanUp(ExcelApp.Application App, ExcelApp.Workbook Book, ExcelApp.Worksheet Sheet, ExcelApp.Range Range)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ////
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Range);
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Sheet);
            ////
            //Book.Close(Type.Missing, Type.Missing, Type.Missing);
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Book);
            ////
            App.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(App);
            App = null;
            //
            TM.ProcessMethod.Kill("EXCEL");
            //int hWnd = ExcelApp.Application.Hwnd;
            //TM.ProcessMethod.TryKillProcessByMainWindowHwnd(hWnd);
        }
    }
    public class Excel
    {
        //Personally, I ran these exact steps:
        //* Install Interop Assemblies: you can install from Microsoft's website https://www.microsoft.com/en-us/download/details.aspx?id=3508&tduid=(09cd06700e5e2553aa540650ec905f71)(256380)(2459594)(TnL5HPStwNw-yuTjfb1FeDiXvvZxhh.R.Q)() 
        //* Check assemblies version: check the version of the assemblies on development and production machines.The assemblies will be in the GAC, in widows 7 this folder is %windir%\assembly.
        //* Create a Desktop folder: the service uses the desktop folder under systemprofile so you will need to create this folder if not there, here is the location of the folder: For 64 bit applications : C:\Windows\SysWOW64\config\systemprofile\Desktop For 32 bit applications : C:\Windows\System32\config\systemprofile\Desktop
        //* Add DCOM user permissions:
        //---start the run window and type 'dcomcnfg'.
        //---Expand Component Services –> Computers –> My Computer –> DCOM Config.
        //---Look for Microsoft Excel Application.Right click on it and select properties, then select the Security tab.
        //---Select the Customize radio button under 'Launch and Activation Permissions' and 'Access Permission' and click the Edit button for both to add users as follows.
        //---------Click the Add button and users 'IIS_IUSRS' and 'NETWORK SERVICE' and give them full privileges.
        //---Go to the Identity tab and select "The interactive user" option.
        //---Click Apply and OK.
        ExcelApp.Application App = new ExcelApp.Application();
        ExcelApp.Workbook Book = null;
        ExcelApp.Sheets Sheets = null;
        ExcelApp.Worksheet Sheet = null;
        ExcelApp.Range Range = null;
        string DataSource = "";
        public Excel(string DataSource)
        {
            try
            {
                //var App = new ExcelApp.Application();
                this.DataSource = DataSource;
                App.Visible = false;
                Book = App.Workbooks.Open(DataSource,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Sheets = Book.Sheets;
            }
            catch (Exception) { throw; }
        }
        public ExcelApp.Sheets ToSheet(string DataSource)
        {
            return Sheets;
        }
        public int getRows(int IndexSheet)
        {
            try
            {
                Sheet = (ExcelApp.Worksheet)Sheets[IndexSheet];
                Range = Sheet.UsedRange;
                return Sheet.UsedRange.Rows.Count;
            }
            catch (Exception) { return 0; }
        }
        public int getRows()
        {
            return getRows(1);
        }
        public int getColumns(int IndexSheet)
        {
            try
            {
                Sheet = (ExcelApp.Worksheet)Sheets[IndexSheet];
                Range = Sheet.UsedRange;
                return Sheet.UsedRange.Columns.Count;
            }
            catch (Exception) { return 0; }
        }
        public int getColumns()
        {
            return getColumns(1);
        }
        public object[,] ToObject(string DataSource, int IndexSheet)
        {
            try
            {
                //Each Worksheet
                Sheet = (ExcelApp.Worksheet)Sheets[IndexSheet];
                Range = Sheet.UsedRange;
                return (object[,])Range.Cells.Value2;
            }
            catch (Exception) { throw; }
            finally
            {
                //Clean up
                //CleanUp(App, Book, Sheet, Range);
                App.Quit();
            }
        }
        public object[,] ToObject(string DataSource)
        {
            return ToObject(DataSource, 1);
        }
        public object[,] ToObject()
        {
            return ToObject(DataSource, 1);
        }
        public List<object[,]> ToListObject(string DataSource)
        {
            try
            {
                //Return List
                List<object[,]> rs = new List<object[,]>();
                //Each Worksheet
                Sheets = ToSheet(DataSource);
                for (int i = 1; i <= Sheets.Count; i++)
                {
                    Sheet = (ExcelApp.Worksheet)Sheets[i];
                    Range = Sheet.UsedRange;
                    rs.Add(Range.Cells.Value2);
                }
                return rs;
            }
            catch (Exception) { throw; }
            finally
            {
                //Clean up
                //CleanUp(App, Book, Sheet, Range);
                App.Quit();
            }
        }
        public List<object[,]> ToListObject()
        {
            return ToListObject(DataSource);
        }
        public List<object[]> ToList(object[,] obj)
        {
            try
            {
                //Declare table
                var list = new List<object[]>();
                //var obj = ToObject(DataSource, IndexSheet);
                if (obj != null)
                {
                    var Rows = obj.GetLength(0);
                    var Columns = obj.GetLength(1);
                    //Add Rows
                    for (int i = 1; i <= Rows; i++)
                    {
                        var tmp = new object[Columns];
                        for (int j = 0; j < Columns; j++)
                            tmp[j] = obj[i, j + 1];
                        list.Add(tmp);
                    }
                }
                return list;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public List<object[]> ToList(string DataSource, int IndexSheet)
        {
            return ToList(ToObject(DataSource, IndexSheet));
        }
        public List<object[]> ToList(string DataSource)
        {
            return ToList(DataSource, 1);
        }
        public List<object[]> ToList()
        {
            return ToList(DataSource, 1);
        }
        public void CleanUp(ExcelApp.Application App, ExcelApp.Workbook Book, ExcelApp.Worksheet Sheet, ExcelApp.Range Range)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ////
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Range);
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Sheet);
            ////
            //Book.Close(Type.Missing, Type.Missing, Type.Missing);
            //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Book);
            ////
            App.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(App);
            App = null;
            //
            TM.ProcessMethod.Kill("EXCEL");
            //int hWnd = ExcelApp.Application.Hwnd;
            //TM.ProcessMethod.TryKillProcessByMainWindowHwnd(hWnd);
        }
    }
}
namespace TM
{
    public class OleExcel
    {
        public static string DataSource { get; set; }
        public static string GetConnectionString(string DataSource, bool isXlSX)
        {
            System.Collections.Generic.Dictionary<string, string> props = new System.Collections.Generic.Dictionary<string, string>();
            if (isXlSX)
            {
                // XLSX - Excel 2007, 2010, 2012, 2013
                props["Provider"] = "Microsoft.ACE.OLEDB.12.0";
                props["Extended Properties"] = "Excel 12.0 XML";
                props["Data Source"] = DataSource;
                //"Mode=ReadWrite;Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;\""
            }
            else
            {
                // XLS - Excel 2003 and Older
                props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                props["Extended Properties"] = "Excel 8.0";
                props["Data Source"] = DataSource;
            }

            StringBuilder sb = new StringBuilder();

            foreach (System.Collections.Generic.KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }
        public static string GetConnectionString(string DataSource)
        {
            return GetConnectionString(DataSource, true);
        }
        public static string GetConnectionString()
        {
            return GetConnectionString(DataSource, true);
        }
        public static OleDbConnection Connection(string DataSource, bool isXlSX)
        {
            try
            {
                //using (OleDbConnection con = new OleDbConnection(GetConnectionString(DataSource, isXlSX)))
                //{
                OleDbConnection con = new OleDbConnection(GetConnectionString(DataSource, isXlSX));
                if (con.State == System.Data.ConnectionState.Open) con.Close();
                con.Open();
                return con;
                //}
            }
            catch (Exception) { throw; }
        }
        public static OleDbConnection Connection(string DataSource)
        {
            return Connection(DataSource, true);
        }
        public static OleDbConnection Connection()
        {
            return Connection(DataSource, true);
        }
        public static void ConnectionOpen(string DataSource, bool isXlSX)
        {
            try
            {
                if (Connection(DataSource, isXlSX) != null && Connection(DataSource, isXlSX).State == System.Data.ConnectionState.Closed) Connection(DataSource, isXlSX).Open();
            }
            catch (Exception) { throw; }
        }
        public static void ConnectionOpen(string DataSource)
        {
            ConnectionOpen(DataSource, true);
        }
        public static void ConnectionOpen()
        {
            ConnectionOpen(DataSource, true);
        }
        public static void ConnectionClose(string DataSource, bool isXlSX)
        {
            try
            {
                if (Connection(DataSource, isXlSX) != null && Connection(DataSource, isXlSX).State == System.Data.ConnectionState.Open) { Connection(DataSource, isXlSX).Close(); }//Connection().Dispose(); } }
            }
            catch (Exception) { throw; }
        }
        public static void ConnectionClose(string DataSource)
        {
            ConnectionClose(DataSource, true);
        }
        public static void ConnectionClose()
        {
            ConnectionClose(DataSource, true);
        }
        public OleDbCommand ToCommand(string DataSource, bool isXlSX, string query)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, isXlSX))
                {
                    using (OleDbCommand cmd = new OleDbCommand(query))
                    {
                        cmd.Connection = con;
                        return cmd;
                    }
                }
            }
            catch (Exception) { throw; }
            finally { }
        }
        public OleDbCommand ToCommand(string DataSource, string query)
        {
            return ToCommand(DataSource, true, query);
        }
        public OleDbCommand ToCommand(string query)
        {
            return ToCommand(DataSource, true, query);
        }
        public static bool Execute(OleDbConnection con, OleDbCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                //using (OleDbConnection con = Connection(DataSource, isXlSX))
                //{
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                return true;
                //}
            }
            catch (Exception) { return false; }
            finally { }
        }
        public static bool Execute(string DataSource, bool isXlSX, OleDbCommand cmd)
        {
            //ClearCache();
            //Connection().Open();
            using (OleDbConnection con = Connection(DataSource, isXlSX))
            {
                return Execute(con, cmd);
            }
        }
        public static bool Execute(string DataSource, OleDbCommand cmd)
        {
            return Execute(DataSource, true, cmd);
        }
        public static bool Execute(OleDbCommand cmd)
        {
            return Execute(DataSource, true, cmd);
        }
        public static bool Execute(OleDbConnection con, string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                //using (OleDbConnection con = Connection(DataSource, isXlSX))
                //{
                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                //}
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public static bool Execute(string DataSource, bool isXlSX, string sql)
        {
            using (OleDbConnection con = Connection(DataSource, isXlSX))
            {
                return Execute(con, sql);
            }
        }
        //public static bool Execute(string DataSource, bool isXlSX, string sql)
        //{
        //    try
        //    {
        //        //ClearCache();
        //        //Connection().Open();
        //        using (OleDbConnection con = Connection(DataSource, isXlSX))
        //        {
        //            using (OleDbCommand cmd = new OleDbCommand(sql, con))
        //            {
        //                cmd.Execute();
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception) { return false; }
        //    finally { }
        //}
        public static bool Execute(string DataSource, string sql)
        {
            return Execute(DataSource, true, sql);
        }
        public static bool Execute(string sql)
        {
            return Execute(DataSource, true, sql);
        }
        public static OleDbDataReader ToDataReader(string DataSource, bool isXlSX, OleDbCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, isXlSX))
                {
                    cmd.Connection = con;
                    using (OleDbDataReader datareader = cmd.ExecuteReader())
                    {
                        return cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        public static OleDbDataReader ToDataReader(string DataSource, OleDbCommand cmd)
        {
            return ToDataReader(DataSource, true, cmd);
        }
        public static OleDbDataReader ToDataReader(OleDbCommand cmd)
        {
            return ToDataReader(DataSource, true, cmd);
        }
        public static OleDbDataReader ToDataReader(string DataSource, bool isXlSX, string sql)
        {
            try
            {
                //ClearCache();
                using (OleDbConnection con = Connection(DataSource, isXlSX))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            return reader;
                        }
                    }
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        public static OleDbDataReader ToDataReader(string DataSource, string sql)
        {
            return ToDataReader(DataSource, true, sql);
        }
        public static OleDbDataReader ToDataReader(string sql)
        {
            return ToDataReader(DataSource, true, sql);
        }
        public static System.Data.DataTable ToDataTable(OleDbConnection con, OleDbCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                //using (OleDbConnection con = Connection(DataSource, isXlSX))
                //{
                using (cmd.Connection = con)
                {
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        using (System.Data.DataTable dt = new System.Data.DataTable())
                        {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
                //}
            }
            catch (Exception) { throw; }
            finally { }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, bool isXlSX, OleDbCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, isXlSX))
                {
                    return ToDataTable(con, cmd);
                }
            }
            catch (Exception) { throw; }
            finally { }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, OleDbCommand cmd)
        {
            return ToDataTable(DataSource, true, cmd);
        }
        public static System.Data.DataTable ToDataTable(OleDbCommand cmd)
        {
            return ToDataTable(DataSource, true, cmd);
        }
        public static System.Data.DataTable ToDataTable(OleDbConnection con, string sql)
        {
            try
            {
                //using (OleDbConnection con = Connection(DataSource, isXlSX))
                //{
                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        using (System.Data.DataTable dt = new System.Data.DataTable())
                        {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
                //}
            }
            catch (Exception) { throw; }
            finally { }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, bool isXlSX, string sql)
        {
            try
            {
                using (OleDbConnection con = Connection(DataSource, isXlSX))
                {
                    return ToDataTable(con, sql);
                }
            }
            catch (Exception) { throw; }
            finally { }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, string sql)
        {
            return ToDataTable(DataSource, true, sql);
        }
        public static System.Data.DataTable ToDataTable(string sql)
        {
            return ToDataTable(DataSource, true, sql);
        }
        public static System.Data.DataSet ToDataSet(string DataSource, bool isXlSX)
        {
            try
            {
                using (System.Data.DataSet ds = new System.Data.DataSet())
                {
                    // Get all Sheets in Excel File
                    foreach (System.Data.DataRow dr in GetSchemaTable(DataSource, isXlSX).Rows)
                    {
                        string sheetName = dr["TABLE_NAME"].ToString();
                        if (!sheetName.EndsWith("$"))
                            continue;
                        // Get all rows from the Sheet
                        ds.Tables.Add(ToDataTable(DataSource, isXlSX, "SELECT * FROM [" + sheetName + "]"));
                    }
                    return ds;
                }
            }
            catch (Exception) { throw; }
            finally { }
        }
        public static System.Data.DataSet ToDataSet(string DataSource)
        {
            return ToDataSet(DataSource, true);
        }
        public static System.Data.DataSet ToDataSet()
        {
            return ToDataSet(DataSource, true);
        }
        public static System.Data.DataTable Select(string DataSource, bool isXlSX, string table, string where)
        {
            return ToDataTable(DataSource, isXlSX, "SELECT * FROM [" + table + "]" +
                (!String.IsNullOrWhiteSpace(where) || where != null ? " WHERE " + where : ""));
        }
        public static System.Data.DataTable Select(string DataSource, string table, string where)
        {
            return Select(DataSource, true, table, where);
        }
        public static System.Data.DataTable Select(string DataSource, string table)
        {
            return Select(DataSource, true, table, null);
        }
        public static System.Data.DataTable Select(string table)
        {
            return Select(DataSource, true, table, null);
        }
        public static System.Data.DataTable SelectWhere(string table, string where)
        {
            return Select(DataSource, true, table, where);
        }
        public static System.Data.DataTable GetSchemaTable(string DataSource, bool isXlSX, object[] restrictions)
        {
            try
            {
                return Connection(DataSource, isXlSX).GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
            }
            catch (Exception) { throw; }
        }
        public static System.Data.DataTable GetSchemaTable(string DataSource, object[] restrictions)
        {
            return GetSchemaTable(DataSource, true, restrictions);
        }
        public static System.Data.DataTable GetSchemaTable(string DataSource, bool isXlSX)
        {
            return GetSchemaTable(DataSource, isXlSX, null);
        }
        public static System.Data.DataTable GetSchemaTable(string DataSource)
        {
            return GetSchemaTable(DataSource, true, null);
        }
        public static System.Data.DataTable GetSchemaTable()
        {
            return GetSchemaTable(DataSource, true, null);
        }
        public static string[] GetTableName(string DataSource, bool isXlSX, object[] restrictions)
        {
            try
            {
                string rs = "";
                foreach (System.Data.DataRow dr in GetSchemaTable(DataSource, isXlSX, restrictions).Rows)
                    rs += dr["TABLE_NAME"].ToString() + ",";
                return rs.Substring(0, rs.Length - 1).Split(',');
            }
            catch (Exception) { throw; }
        }
        public static string[] GetTableName(string DataSource, bool isXlSX)
        {
            return GetTableName(DataSource, isXlSX, null);
        }
        public static string[] GetTableName(string DataSource, object[] restrictions)
        {
            return GetTableName(DataSource, true, restrictions);
        }
        public static string[] GetTableName(string DataSource)
        {
            return GetTableName(DataSource, true, null);
        }
        public static string[] GetTableName()
        {
            return GetTableName(DataSource, true, null);
        }
        public static System.Collections.Generic.List<string> GetTableNameList(string DataSource, bool isXlSX, object[] restrictions)
        {
            try
            {
                System.Collections.Generic.List<string> lst = new System.Collections.Generic.List<string>();
                foreach (System.Data.DataRow dr in GetSchemaTable(DataSource, isXlSX, restrictions).Rows)
                    lst.Add(dr["TABLE_NAME"].ToString());
                return lst;
            }
            catch (Exception) { throw; }
        }
        public static System.Collections.Generic.List<string> GetTableNameList(string DataSource, bool isXlSX)
        {
            return GetTableNameList(DataSource, isXlSX, null);
        }
        public static System.Collections.Generic.List<string> GetTableNameList(string DataSource, object[] restrictions)
        {
            return GetTableNameList(DataSource, true, restrictions);
        }
        public static System.Collections.Generic.List<string> GetTableNameList(string DataSource)
        {
            return GetTableNameList(DataSource, true, null);
        }
        public static System.Collections.Generic.List<string> GetTableNameList()
        {
            return GetTableNameList(DataSource, true, null);
        }
    }
}
public class TMDB
{
    //public static OleDbConnection con { get; set; }
    public static string select { get; set; }
    public static string table { get; set; }
    public static string where { get; set; }
    public static string query { get; set; }
}
public static class TMDBS
{
    public static void Dispose()
    {
        TMDB.select = null;
        TMDB.table = null;
        TMDB.where = null;
        TMDB.query = null;
    }
    public static OleDbConnection TMDBSelect(this OleDbConnection con, string select)
    {
        TMDB.select = select;
        return con;
    }
    public static OleDbConnection TMDBTable(this OleDbConnection con, string table)
    {
        TMDB.table = table;
        return con;
    }
    public static OleDbConnection TMDBWhere(this OleDbConnection con, string where)
    {
        TMDB.where += " WHERE " + where;
        return con;
    }
    public static System.Data.DataTable TMDBGet(this OleDbConnection con)
    {
        var query = TMDB.select != null ? TMDB.select : "SELECT * FROM [" + TMDB.table + "]" + TMDB.where;
        var dt = TM.OleExcel.ToDataTable(con, query);
        Dispose();
        return dt;
    }
    public static bool TMDBDelete(this OleDbConnection con, string table)
    {
        var rs = TM.OleExcel.Execute(con, "DELETE * FROM [" + table + "]" + TMDB.where);
        Dispose();
        return rs;
    }
    public static bool TMDBDelete(this OleDbConnection con)
    {
        return TMDBDelete(con, TMDB.table);
    }
    public static bool TMDBInsert(this OleDbConnection con, string table, string[] values)
    {
        var rs = TM.OleExcel.Execute(con, "INSERT INTO [" + table + "] VALUES(" + string.Join(",", values) + ")");
        Dispose();
        return rs;
    }
    public static bool TMDBInsert(this OleDbConnection con, string[] values)
    {
        var rs = TM.OleExcel.Execute(con, "INSERT INTO [" + TMDB.table + "] VALUES(" + string.Join(",", values) + ")");
        Dispose();
        return rs;
    }
}