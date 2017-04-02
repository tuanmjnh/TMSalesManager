using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    //public class AuthUser
    //{
    //    public static Guid id { get; set; }
    //    public static string username { get; set; }
    //    public static string password { get; set; }
    //    public static string salt { get; set; }
    //    public static string full_name { get; set; }
    //    public static string mobile { get; set; }
    //    public static string email { get; set; }
    //    public static string address { get; set; }
    //    public static string roles { get; set; }
    //    public static string created_by { get; set; }
    //    public static DateTime? created_at { get; set; }
    //    public static string updated_by { get; set; }
    //    public static DateTime? updated_at { get; set; }
    //    public static DateTime? last_login { get; set; }
    //    public static Guid staff_id { get; set; }
    //    public static int flag { get; set; }
    //    public static string extras { get; set; }
    //}
    public class Auth
    {
        public static bool isAuth { set; get; }
        public static VinaphoneBill.Models.user AuthUser = null;
        public static void setAuth(VinaphoneBill.Models.user user)
        {
            AuthUser = user;
            isAuth = true;
            //id = user.id;
            //username = user.username;
            //password = user.password;
            //salt = user.salt;
            //full_name = user.full_name;
            //mobile = user.mobile;
            //email = user.email;
            //address = user.address;
            //roles = user.roles;
            //created_by = user.created_by;
            //created_at = user.created_at;
            //updated_by = user.updated_by;
            //updated_at = user.updated_at;
            //last_login = user.last_login;
            //staff_id = user.staff_id.HasValue ? user.staff_id.Value : Guid.Empty;
            //flag = user.flag.HasValue ? user.flag.Value : 0;
            //extras = user.extras;
        }
    }

    public class AuthStatic
    {
        public static bool isAuthStatic(string username, string password)
        {
            VinaphoneBill.Models.user user = new VinaphoneBill.Models.user();
            user.id = Guid.Parse("f4191f70-2c4a-442e-a62d-b4b6833b33f4");
            user.username = "tuanmjnh";
            user.password = "aa2de065c899d53d7031b0975c56062f";//"Tuanmjnh@tm"; //"fc44d0279133a2f46178134ce9bf2167";//tuanmjnh@123
            user.salt = "1c114c58-69d9-41e6-bd3e-363906e04e50";
            user.full_name = "SuperAdmin";
            user.mobile = "0123456789";
            user.email = "SuperAdmin@SuperAdmin.com";
            user.address = "SuperAdmin";
            user.roles = Common.roles.superadmin;
            user.created_by = "f4191f70-2c4a-442e-a62d-b4b6833b33f4";
            user.created_at = DateTime.Now;
            user.updated_by = "f4191f70-2c4a-442e-a62d-b4b6833b33f4";
            user.updated_at = DateTime.Now;
            user.last_login = DateTime.Now;
            user.staff_id = Guid.NewGuid();
            user.flag = 1;
            user.extras = null;
            if (user.username == username && user.password == TM.Encrypt.CryptoMD5TM(password + user.salt))
            {
                Auth.setAuth(user);
                return true;
            }
            return false;
        }
    }
    public class roles
    {
        public const string superadmin = "187eb627-0a7b-44a8-83c4-ceb4829709a3";
        public const string admin = "ee82e7f1-592c-4f5c-95fa-7cad30b14a2d";
        public const string mod = "238391cd-990d-4f3b-8d0c-0300416f9263";
        public const string director = "121ab8e5-1ad2-4b78-8ff2-4d953c9b71a8";
        public const string leader = "d0443498-09c4-4267-a7c9-2a20dde8e925";
        public const string staff = "dc67601d-ad74-4813-8293-8d4a07db1d31";
    }
}
