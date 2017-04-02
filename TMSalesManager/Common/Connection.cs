using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConnectionInformation
    {
        private string userId = "sa";
        private string password = "tmvnpt";
        private string database = "TMSalesManager";
        private string server = ".";
        private int timeout = 30;
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Database
        {
            get { return database; }
            set { database = value; }
        }
        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }
    }
    public class Connection
    {
        public System.Data.SqlClient.SqlConnection SqlConnection(ConnectionInformation ci)
        {
            return TM.SQLDB.Connection(ci.UserId, ci.Password, ci.Server, ci.Database, ci.Timeout);
        }
    }
}
