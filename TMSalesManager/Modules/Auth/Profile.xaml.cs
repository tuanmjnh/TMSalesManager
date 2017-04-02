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
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
            gbGeneral.Header = Common.Language.Get("aboutMain");
            gbDetails.Header = Common.Language.Get("aboutDetails");
            if (!Common.Auth.isAuth)
                return;
            lblUsername.Content = Common.Language.Get("username", "auth") + ": " + Common.Auth.AuthUser.username;
            lblFullName.Content = Common.Language.Get("fullname", "auth") + ": ";
            txtFullName.Text = Common.Auth.AuthUser.full_name;
            lblMobile.Content = Common.Language.Get("mobile", "auth") + ": ";
            txtMobile.Text = Common.Auth.AuthUser.mobile;
            lblEmail.Content = Common.Language.Get("email", "auth") + ": ";
            txtEmail.Text = Common.Auth.AuthUser.email;
            lblAddress.Content = Common.Language.Get("address", "auth") + ": ";
            txtAddress.Text = Common.Auth.AuthUser.address;
            lblRoles.Content = Common.Language.Get("roles", "auth") + ": ";
            if (Common.Auth.AuthUser.roles == Common.roles.superadmin)
                lblRoles.Content += "Super Admin";
            else if (Common.Auth.AuthUser.roles == Common.roles.admin)
                lblRoles.Content += "Admin";
            else if (Common.Auth.AuthUser.roles == Common.roles.mod)
                lblRoles.Content += "Mod";
            else if (Common.Auth.AuthUser.roles == Common.roles.director)
                lblRoles.Content += "Giám đốc";
            else if (Common.Auth.AuthUser.roles == Common.roles.leader)
                lblRoles.Content += "Trưởng phòng";
            else if (Common.Auth.AuthUser.roles == Common.roles.staff)
                lblRoles.Content += "Nhân viên";
            lblFlag.Content = Common.Language.Get("status") + ": " + (Common.Auth.AuthUser.flag == 1 ? Common.Language.Get("activity") : Common.Language.Get("locked"));
            lblCreatedBy.Content = Common.Language.Get("createdBy") + ": " + Common.Context.GetUser(Common.Auth.AuthUser.created_by);
            lblCreatedAt.Content = Common.Language.Get("createdAt") + ": " + Common.Auth.AuthUser.created_at;
            lblUpdatedBy.Content = Common.Language.Get("updatedBy") + ": " + Common.Context.GetUser(Common.Auth.AuthUser.updated_by);
            lblUpdatedAt.Content = Common.Language.Get("updatedAt") + ": " + Common.Auth.AuthUser.updated_at;
            btnUpdate.ToolTip = Common.Language.Get("update");
            //btnCancel.ToolTip = Common.Language.Get("cancel");
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new Models.MainContext())
                {
                    var user = db.users.Find(Common.Auth.AuthUser.id);
                    user.full_name = txtFullName.Text;
                    user.mobile = txtMobile.Text;
                    user.email = txtEmail.Text;
                    user.address = txtAddress.Text;
                    user.updated_by = Common.Auth.AuthUser.id.ToString();
                    user.updated_at = DateTime.Now;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Common.Auth.setAuth(user);
                    Common.Message.MessageSuccess(Common.Language.Get("msgUpdateSucsess"));
                }
            }
            catch (Exception)
            {
                Common.Message.MessageDanger(Common.Language.Get("msgUpdateError"));
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.listSubWindow["AuthProfile"].Close();
        }
    }
}
