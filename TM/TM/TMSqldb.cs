using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace TM
{
    //This is collection and modify by tuanmjnh - TM
    public class SQLDB
    {
        public static string ConstantConnectionString = "MainContext";
        //public static string _Connection { get { return ConstantConnectionString; } set { ConstantConnectionString = value; } }
        public static SqlConnection Connection(string userId, string password, string server, string database, int timeout)
        {
            try
            {
                SqlConnection con = new SqlConnection("user id=" + userId + ";" +
                                       "password=" + password + ";server=" + server + ";" +
                                       "Trusted_Connection=yes;" +
                                       "database=" + database + "; " +
                                       "connection timeout=" + timeout.ToString());
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                return con;
            }
            catch (Exception) { return null; }
        }
        public static SqlConnection Connection(string ConnectionString)
        {
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                return con;
            }
            catch (Exception) { return null; }
        }
        public static SqlConnection Connection()
        {
            //return Connection("sa", "tmvnpt", ".", "TMSalesManager", 30);
            return Connection(ConstantConnectionString);
        }

        public static void ConnectionOpen()
        {
            if (Connection() != null && Connection().State == ConnectionState.Closed) Connection().Open();
        }
        public static void ConnectionClose()
        {
            if (Connection() != null && Connection().State == ConnectionState.Open) { Connection().Close(); }//Connection().Dispose(); } }
        }

        #region DB Access Functions
        public SqlCommand getCommand(string query)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        return cmd;
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetCommand", ex); }
            finally { }
        }
        public static DataTable ToDataTable(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (cmd.Connection = con)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableCommand", ex); }
            finally { }
        }
        public static DataTable ToDataTable(string sql)
        {
            try
            {
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                cmd.CommandTimeout = 0;
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableQuery", ex); }
            finally { }
        }
        public static DataTable ToDataTable(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableStoredProcedureName", ex); }
            finally { }
        }
        public static DataSet ToDataSet(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataTableQuery", ex); }
            finally { }
        }
        public static DataSet ToDataSet(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataSetQuery", ex); }
            finally { }
        }
        public static DataSet ToDataSet(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                da.Fill(ds);
                                return ds;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("GetDataSetStoredProcedureName", ex); }
            finally { }
        }
        public static bool Execute(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception) { return false; }
            finally { }
        }
        public static bool Execute(string sql, string extraEX)
        {
            try
            {
                extraEX = (extraEX != null || !String.IsNullOrWhiteSpace(extraEX)) ? extraEX + ": " : "";
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(extraEX + ex.Message); }
            finally { }
        }
        public static bool Execute(string sql)
        {
            return Execute(sql, "");
        }
        public static bool ExecuteNonQuery(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception) { return false; }
            finally { }
        }
        public static bool Execute(string[] StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            //ClearCache();
            //Connection().Open();
            using (SqlConnection con = Connection())
            {
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = tran;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (ArrayParam != null)
                                foreach (SqlParameter param in ArrayParam)
                                    cmd.Parameters.Add(param);
                            for (int i = 0; i < StoredProcedureName.Length; i++)
                                cmd.CommandText = StoredProcedureName[i];
                            cmd.ExecuteNonQuery();
                            tran.Commit();
                            return true;
                        }
                    }
                    catch (Exception) { tran.Rollback(); return false; }
                    finally { }
                }
            }
        }
        public static int ExecuteScalar(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    var rs = cmd.ExecuteScalar();
                    return rs != null ? Convert.ToInt32(rs) : 0;
                }
            }
            catch (Exception) { return 0; }
            finally { }
        }
        public static int ExecuteScalar(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? Convert.ToInt32(rs) : 0;
                    }
                }
            }
            catch (Exception) { return 0; }
            finally { }
        }
        public static int ExecuteScalar(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? Convert.ToInt32(rs) : 0;
                    }
                }
            }
            catch (Exception) { return 0; }
            finally { }
        }
        public static int ExecuteScalar(string[] StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            //ClearCache();
            //Connection().Open();
            using (SqlConnection con = Connection())
            {
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = tran;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (ArrayParam != null)
                                foreach (SqlParameter param in ArrayParam)
                                    cmd.Parameters.Add(param);
                            for (int i = 0; i < StoredProcedureName.Length; i++)
                                cmd.CommandText = StoredProcedureName[i];
                            var rs = cmd.ExecuteScalar();
                            return rs != null ? Convert.ToInt32(rs) : 0;
                        }
                    }
                    catch (Exception) { return 0; }
                    finally { }
                }
            }
        }
        public static string ExecuteScalarStr(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    var rs = cmd.ExecuteScalar();
                    return rs != null ? rs.ToString() : string.Empty;
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        public static string ExecuteScalarStr(string sql)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sql;
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? rs.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        public static string ExecuteScalarStr(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        var rs = cmd.ExecuteScalar();
                        return rs != null ? rs.ToString() : string.Empty;
                    }
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        public static SqlDataReader GetDataReader(SqlCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    cmd.Connection = con;
                    using (SqlDataReader datareader = cmd.ExecuteReader())
                    {
                        return cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        public static SqlDataReader ToDataReader(string sql)
        {
            try
            {
                //ClearCache();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader datareader = cmd.ExecuteReader())
                        {
                            return cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        public static SqlDataReader ToDataReader(string StoredProcedureName, params SqlParameter[] ArrayParam)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (SqlConnection con = Connection())
                {
                    using (SqlCommand cmd = new SqlCommand(StoredProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (ArrayParam != null)
                            foreach (SqlParameter param in ArrayParam)
                                cmd.Parameters.Add(param);
                        using (SqlDataReader datareader = cmd.ExecuteReader())
                        {
                            return cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception) { return null; }
            finally { }
        }
        #endregion
    }
}