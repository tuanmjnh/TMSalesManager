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
using MahApps.Metro.Controls.Dialogs;

namespace TMSalesManager.Modules.Product
{
    /// <summary>
    /// Interaction logic for CategoryAdd.xaml
    /// </summary>
    public partial class CategoryAdd : UserControl
    {
        public CategoryAdd()
        {
            InitializeComponent();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lblParent.Content = Common.Language.Get("categoryParent", "product");
            lblTitle.Content = Common.Language.GetRequired("categoryTitle", "product");
            gbDesc.Header = Common.Language.Get("desc");
            btnAdd.ToolTip = Common.Language.Get("addNew");
            //btnCancel.ToolTip = Common.Language.Get("cancel");
            gbOption.Header = Common.Language.Get("detailsOption");
            lblFlag.Content = Common.Language.Get("status");
            rbTrue.Content = Common.Language.Get("show");
            rbFalse.Content = Common.Language.Get("draft");
            getDefault();
        }
        private void getDefault()
        {
            var group = new Models.group();
            var groups = new List<Models.group>();
            group.id = Guid.Empty;
            group.title = "-- " + Common.Language.Get("categoryParent", "product") + " --";
            groups.Add(group);
            groups.AddRange(Common.Context.getGroup(Common.AppKey.product, Guid.Empty.ToString(), Guid.Empty));
            cbCategory.ItemsSource = groups;
            cbCategory.DisplayMemberPath = "title";
            cbCategory.SelectedValuePath = "id";
            if (cbCategory.Items.Count > 0)
                cbCategory.SelectedIndex = 0;
            txtTitle.Text = "";
            txtDesc.Text = "";
            rbTrue.IsChecked = true;
            txtTitle.Focus();
        }
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = Common.Language.Get("ok"),
                    NegativeButtonText = Common.Language.Get("cancel"),
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                var msg = await MainWindow.listSubWindow["ProductCategoryAdd"].ShowMessageAsync(
                    Common.Language.Get("notification"),
                    Common.Language.Get("msgWarningRequired"),
                    MessageDialogStyle.Affirmative,
                    mySettings);
                txtTitle.Focus();
                //Common.Message.MessageDanger(Common.Language.Get("msgInputError"));
                //MessageBox.Show(Common.Language.Get("msgWarningRequired"), Common.Language.Get("notification"), MessageBoxButton.);
                return;
            }
            try
            {
                using (var db = new Models.MainContext())
                {
                    var group = new Models.group();
                    group.id = Guid.NewGuid();
                    group.title = txtTitle.Text;
                    group.desc = txtDesc.Text;
                    //Parents_ID
                    group.parent_id = cbCategory.SelectedValue.ToString();
                    group.parents_id = ",";
                    group.level = 0;
                    if (group.parent_id != Guid.Empty.ToString())
                    {
                        var parent_id = Guid.Parse(group.parent_id);
                        var tmp = db.groups.Where(d => d.id == parent_id && d.flag > 0).FirstOrDefault();
                        group.parents_id = tmp.parents_id + parent_id + ",";
                        group.level = tmp.level + 1;
                    }
                    group.app_key = Common.AppKey.product;
                    group.created_by = "8bb25bd4-37b1-4a48-96e6-0b049d12435f";//Common.Auth.AuthUser.id.ToString();
                    group.created_at = DateTime.Now;
                    group.flag = rbTrue.IsChecked == true ? 1 : 0;
                    db.groups.Add(group);
                    db.SaveChanges();
                    //
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = Common.Language.Get("ok"),
                        ColorScheme = MetroDialogColorScheme.Theme
                    };
                    var msg = await MainWindow.listSubWindow["ProductCategoryAdd"].ShowMessageAsync(
                        Common.Language.Get("notification"), Common.Language.Get("msgCreateSucsess"),
                        MessageDialogStyle.Affirmative,
                        mySettings);
                    getDefault();
                }
            }
            catch (Exception)
            {
                Common.Message.MessageDanger(Common.Language.Get("msgCreateError"));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.listSubWindow["ProductCategoryAdd"].Close();
        }
    }
}
