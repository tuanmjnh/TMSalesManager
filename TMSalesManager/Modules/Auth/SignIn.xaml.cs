using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMSalesManager.Modules.Auth
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class SignIn : UserControl
    {
        public SignIn()
        {
            InitializeComponent();
            lblLanguage.Content = Common.Language.Get("language", "main");
            lblUser.Content = Common.Language.Get("username", "auth");
            lblPassword.Content = Common.Language.Get("password", "auth");
            btnLogin.ToolTip = Common.Language.Get("btnSigin", "auth");
            txtUsername.Focusable = true;
            txtUsername.Focus();
            Common.Message.MessageRemove();

            //Language
            cbLanguage.ItemsSource = Common.Language.GetLanguageList();
            cbLanguage.SelectedValuePath = "Key";
            cbLanguage.DisplayMemberPath = "Value";
            //var lang = Common.Config.Value(Common.AppKey.SettingGlobal, Common.AppKey.Language);
            if (cbLanguage.Items.Count > 0)
                cbLanguage.SelectedValue = Common.Language.lang;
        }
        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLanguage.SelectedValue != null)
            {
                var setting = new Models.setting();
                var SettingLanguage = Common.Config.Setting(Common.AppKey.SettingGlobal, Common.AppKey.Language);//.Where(d => d.module_key == Common.Object.SettingGlobal && d.sub_key == Common.Object.Language).FirstOrDefault();
                if (SettingLanguage == null)
                {
                    setting.module_key = Common.AppKey.SettingGlobal;
                    setting.sub_key = Common.AppKey.Language;
                }
                else
                    setting.id = SettingLanguage.id;
                setting.value = cbLanguage.SelectedValue.ToString();
                setting.sub_value = ((KeyValuePair<string, string>)cbLanguage.SelectedItem).Value;
                Common.Config.SetSetting(setting);
                Common.Config.LoadSetting();
                Common.Language.lang = cbLanguage.SelectedValue.ToString();
                MainWindow.LoadMenu();
                //
                MainWindow.listSubWindow["AuthSignIn"].stackContent.Children.Clear();
                MainWindow.listSubWindow["AuthSignIn"].stackContent.Children.Add(new SignIn());
                MainWindow.listSubWindow["AuthSignIn"].Focus();
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                Common.Message.MessageDanger(Common.Language.Get("msgInputError"));
                return;
            }
            //Connect to AuthStatic
            if (!Common.AuthStatic.isAuthStatic(txtUsername.Text, txtPassword.Password))
            {
                //Connect db to Auth
                using (var db = new Models.MainContext())
                {
                    var user = db.users.Where(d => d.username == txtUsername.Text).FirstOrDefault();
                    if (user == null)
                    {
                        Common.Message.MessageDanger(Common.Language.Get("msgExistError", "auth"));
                        return;
                    }

                    var password = TM.Encrypt.CryptoMD5TM(txtPassword.Password + user.salt);
                    user = db.users.Where(d => d.username == txtUsername.Text && d.password == password).FirstOrDefault();
                    if (user == null)
                    {
                        Common.Message.MessageDanger(Common.Language.Get("msgPasswordError", "auth"));
                        return;
                    }

                    if (user.flag < 1)
                    {
                        Common.Message.MessageDanger(Common.Language.Get("msgLocked", "auth"));
                        return;
                    }

                    user.last_login = DateTime.Now;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Common.Auth.setAuth(user);
                }
            }
            MainWindow.IsClose = false;
            //MessageBox.Show("Đăng nhập thành công!");
            MainWindow.listSubWindow["AuthSignIn"].Hide();
            Application.Current.MainWindow.Show();
        }
    }
}
