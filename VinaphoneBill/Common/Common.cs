using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Common
{
    public class Context
    {
        //public VinaphoneBill.Models.MainContext db = new VinaphoneBill.Models.MainContext();
        public static bool CheckConnection()
        {
            try
            {
                using (var db = new VinaphoneBill.Models.MainContext())
                {
                    db.Database.Connection.Open();
                }
                return true;
            }
            catch (Exception) { return false; }
        }
        public static string GetUser(string id)
        {
            using (var db = new VinaphoneBill.Models.MainContext())
            {
                if (id != null)
                {
                    var rs = db.users.Find(Guid.Parse(id));
                    if (rs != null)
                        if (!String.IsNullOrEmpty(rs.full_name))
                            return rs.full_name;
                        else return rs.username;
                    else return Language.Get("emptyvl");
                }
                else return Language.Get("emptyvl");
            }
        }
        public static List<VinaphoneBill.Models.group> getGroup(string AppKey, string parent, Guid id)
        {
            try
            {
                var list = new List<VinaphoneBill.Models.group>();
                var db = new VinaphoneBill.Models.MainContext();
                var rs = new List<VinaphoneBill.Models.group>();

                if (id == Guid.Empty)
                    rs = db.groups.Where(d => d.app_key == AppKey && d.parent_id == parent && d.flag > 0).ToList();
                else
                    rs = db.groups.Where(d => d.id != id && d.app_key == AppKey && d.parent_id == parent && d.flag > 0).ToList();

                foreach (var item in rs)
                {
                    list.Add(item);
                    list.AddRange(getGroup(AppKey, item.id.ToString(), id));
                }
                return list;
            }
            catch (Exception) { throw; }
        }
        //protected override void Dispose(bool disposing)
        //{
        //    //if (RoleManager != null) RoleManager.Dispose();
        //    //if (UserManager != null) UserManager.Dispose();
        //    if (db != null) db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
    public class Config
    {
        public const string ApplicationName = "TM Sales Manager";
        public static System.Windows.Media.SolidColorBrush ColorIcon()
        {
            return TM.ColorBrush.Brush("00a9ec");
        }
        public static System.Windows.Media.ImageSource ImageSource(FontAwesome.WPF.FontAwesomeIcon FontAwesomeIcon, System.Windows.Media.SolidColorBrush Color)
        {
            return FontAwesome.WPF.ImageAwesome.CreateImageSource(FontAwesomeIcon, Color);
        }
        public static System.Windows.Media.ImageSource ImageSource(FontAwesome.WPF.FontAwesomeIcon FontAwesomeIcon, string Color)
        {
            return ImageSource(FontAwesomeIcon, TM.ColorBrush.Brush(Color));
        }
        public static System.Windows.Media.ImageSource ImageSource(FontAwesome.WPF.FontAwesomeIcon FontAwesomeIcon)
        {
            return ImageSource(FontAwesomeIcon, ColorIcon());
        }
        public static System.Windows.Media.ImageSource ImageSource()
        {
            return ImageSource(FontAwesome.WPF.FontAwesomeIcon.FontAwesome, ColorIcon());
        }
        public static List<VinaphoneBill.Models.setting> AllSetting { get; set; } //#00a9ec
        bool setting = InitializeSetting();
        public static void SetSetting(VinaphoneBill.Models.setting setting)
        {
            using (var db = new VinaphoneBill.Models.MainContext())
            {
                if (setting.id == Guid.Empty)
                {
                    setting.id = Guid.NewGuid();
                    db.settings.Add(setting);
                }
                else
                {
                    var tmp = db.settings.Find(setting.id);
                    tmp.value = setting.value ?? setting.value;
                    tmp.sub_value = setting.sub_value ?? setting.sub_value;
                    tmp.desc = setting.desc ?? setting.desc;
                    tmp.extra = setting.extra ?? setting.extra;
                    db.Entry(tmp).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public static List<VinaphoneBill.Models.setting> Settings(string module_key)
        {
            try
            {
                using (var db = new VinaphoneBill.Models.MainContext())
                {
                    return db.settings.Where(d => d.module_key == module_key).ToList();
                }
            }
            catch (Exception) { return null; }
        }
        public static List<VinaphoneBill.Models.setting> Settings(string module_key, string sub_key)
        {
            try
            {
                using (var db = new VinaphoneBill.Models.MainContext())
                {
                    return db.settings.Where(d => d.module_key == module_key && d.sub_key == sub_key).ToList();
                }
            }
            catch (Exception) { return null; }
        }
        public static VinaphoneBill.Models.setting Setting(string module_key, string sub_key, string value)
        {
            try
            {
                using (var db = new VinaphoneBill.Models.MainContext())
                {
                    return db.settings.Where(d => d.module_key == module_key && d.sub_key == sub_key && d.value == value).FirstOrDefault();
                }
            }
            catch (Exception) { return null; }
        }
        public static VinaphoneBill.Models.setting Setting(string module_key, string sub_key)
        {
            try
            {
                using (var db = new VinaphoneBill.Models.MainContext())
                {
                    return db.settings.Where(d => d.module_key == module_key && d.sub_key == sub_key).FirstOrDefault();
                }
            }
            catch (Exception) { return null; }
        }
        //public static List<dynamic> Settings(string module_key)
        //{
        //    try
        //    {
        //        //return AllSetting.Where(s => s.module_key.Equals("module_key")).ToList();
        //        return TM.SQLDB.Connection().Query("SELECT * FROM settings WHERE module_key=@module_key",
        //            new { module_key = module_key }).ToList();
        //    }
        //    catch (Exception) { return null; }
        //}
        //public static List<dynamic> Settings(string module_key, string sub_key)
        //{
        //    try
        //    {
        //        //return AllSetting.Where(s => s.module_key.Equals(module_key) && s.sub_key.Equals(sub_key)).ToList();
        //        return TM.SQLDB.Connection().Query("SELECT * FROM settings WHERE module_key=@module_key AND sub_key=@sub_key",
        //        new { module_key = module_key, sub_key = sub_key }).ToList();
        //    }
        //    catch (Exception) { return null; }
        //}
        //public static dynamic Setting(string module_key, string sub_key, string value)
        //{
        //    try
        //    {
        //        //return AllSetting.Where(s => s.module_key.Equals(module_key) && s.sub_key.Equals(sub_key) && s.value.Equals(value)).FirstOrDefault();
        //        return TM.SQLDB.Connection().Query("SELECT * FROM settings WHERE module_key=@module_key AND sub_key=@sub_key AND value=@value",
        //        new { module_key = module_key, sub_key = sub_key, value = value }).First();
        //    }
        //    catch (Exception) { return null; }
        //}
        public static string Value(string module_key, string sub_key)
        {
            try
            {
                return Setting(module_key, sub_key).value;
            }
            catch (Exception) { return null; }
        }
        public static string SubValue(string module_key, string sub_key, string value)
        {
            try
            {
                return Setting(module_key, sub_key, value).sub_value;
            }
            catch (Exception) { return null; }
        }
        public static bool InitializeSetting()
        {
            try
            {
                if (AllSetting == null) LoadSetting();
                return true;
            }
            catch (Exception) { return false; }
        }
        public static void LoadSetting()
        {
            try
            {
                using (var db = new VinaphoneBill.Models.MainContext())
                {
                    AllSetting = db.settings.ToList(); //TM.SQLDB.Connection().Query("SELECT * FROM settings").ToList();
                }
            }
            catch (Exception) { }
            //using (var dbs = new Models.PortalContext())
            //{
            //    AllSetting = dbs.settings.ToList();
            //}
        }
        public static System.Windows.Controls.TextBlock GetRequired(string text)
        {
            var tb = new System.Windows.Controls.TextBlock();
            tb.Inlines.Add(text + " (");
            tb.Inlines.Add(new System.Windows.Documents.Run("*") { Foreground = System.Windows.Media.Brushes.Red });
            tb.Inlines.Add(")");
            return tb;
        }
    }
    public class Message
    {
        //public static void MessageRemove()
        //{
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Content = string.Empty;
        //}
        public static void MessageBoxDefault(string msg, string caption, System.Windows.MessageBoxButton button, System.Windows.MessageBoxImage icon)
        {
            System.Windows.MessageBox.Show(msg, caption, button, icon, System.Windows.MessageBoxResult.OK, System.Windows.MessageBoxOptions.None);
        }
        //public static void MessageDefault(string msg,string caption)
        //{
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Content = msg;
        //}
        //public static void MessageSuccess(string msg)
        //{
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Foreground = TM.ColorBrush.Brush("3c763d");
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Content = msg;
        //}
        //public static void MessageInfo(string msg)
        //{
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Foreground = TM.ColorBrush.Brush("31708f");
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Content = msg;
        //}
        //public static void MessageWarning(string msg)
        //{
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Foreground = TM.ColorBrush.Brush("8a6d3b");
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Content = msg;
        //}
        //public static void MessageDanger(string msg)
        //{
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Foreground = TM.ColorBrush.Brush("a94442");
        //    VinaphoneBill.MainWindow.subWindow.lblMessage.Content = msg;
        //}
    }
    public class Language
    {
        public static string lang = "vi-vn.ini";
        const string Section = "global";
        public Language(string lang = "vi-vn.ini")
        {

        }
        public static Dictionary<string, string> GetLanguageList()
        {
            var language = new Dictionary<string, string>();
            foreach (var item in TM.IO.Files("Language"))
                language.Add(item.Name, Get(item.Name, "languageName", "global"));
            return language;
        }
        public static string Get(string lang, string key, string Section, bool RemoveAlt)
        {
            try
            {
                var ini = new TM.INIFile("Language/" + lang);
                if (RemoveAlt)
                    return ini.Read(key, Section).Replace("_", "");
                else
                    return ini.Read(key, Section);
            }
            catch (Exception) { return null; }
        }
        public static string Get(string key, string Section, bool RemoveAlt)
        {
            return Get(lang, key, Section, RemoveAlt);
        }
        public static string Get(string lang, string key, string Section)
        {
            return Get(lang, key, Section, false);
        }
        public static string Get(string key, string Section)
        {
            return Get(lang, key, Section);
        }
        public static string Get(string key, bool RemoveAlt = false)
        {
            return Get(lang, key, Section, RemoveAlt);
        }
        public static string Get(string key)
        {
            return Get(lang, key, Section, false);
        }
        public static System.Windows.Controls.TextBlock GetRequired(string lang, string key, string Section, bool RemoveAlt)
        {
            return Common.Config.GetRequired(Get(lang, key, Section, RemoveAlt));
        }
        public static System.Windows.Controls.TextBlock GetRequired(string key, string Section, bool RemoveAlt)
        {
            return Common.Config.GetRequired(Get(lang, key, Section, RemoveAlt));
        }
        public static System.Windows.Controls.TextBlock GetRequired(string key, bool RemoveAlt)
        {
            return Common.Config.GetRequired(Get(lang, key, Section, RemoveAlt));
        }
        public static System.Windows.Controls.TextBlock GetRequired(string key, string Section)
        {
            return Common.Config.GetRequired(Get(lang, key, Section, false));
        }
        public static System.Windows.Controls.TextBlock GetRequired(string key)
        {
            return Common.Config.GetRequired(Get(lang, key, Section, false));
        }
    }
}
