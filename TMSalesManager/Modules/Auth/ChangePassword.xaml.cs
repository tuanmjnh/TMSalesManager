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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : UserControl
    {
        public ChangePassword()
        {
            InitializeComponent();
            lblOldPassword.Content = Common.Language.Get("oldPassword", "auth");
            lblNewPassword.Content = Common.Language.Get("newPassword", "auth");
            lblRePassword.Content = Common.Language.Get("rePassword", "auth");
            btnUpdate.ToolTip = Common.Language.Get("update");
            //btnCancel.ToolTip = Common.Language.Get("cancel");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Models.MainContext())
            {
                if (string.IsNullOrEmpty(txtOldPassword.Password) || string.IsNullOrEmpty(txtNewPassword.Password) || string.IsNullOrEmpty(txtRePassword.Password))
                {
                    Common.Message.MessageDanger(Common.Language.Get("msgInputError"));
                    return;
                }
                if (txtNewPassword.Password != txtRePassword.Password)
                {
                    Common.Message.MessageDanger(Common.Language.Get("msgRePassword", "auth"));
                    return;
                }
                if (TM.Encrypt.CryptoMD5TM(txtOldPassword.Password + Common.Auth.AuthUser.salt) != Common.Auth.AuthUser.password)
                {
                    Common.Message.MessageDanger(Common.Language.Get("msgOldPasswordError", "auth"));
                    return;
                }
                Common.Auth.AuthUser.password = TM.Encrypt.CryptoMD5TM(txtNewPassword.Password + Common.Auth.AuthUser.salt);
                db.Entry(Common.Auth.AuthUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                txtOldPassword.Password = string.Empty;
                txtNewPassword.Password = string.Empty;
                txtRePassword.Password = string.Empty;
                Common.Message.MessageSuccess(Common.Language.Get("msgUpdateSucsess"));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.listSubWindow["AuthChangePassword"].Close();
        }
    }
}
