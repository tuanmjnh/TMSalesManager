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
    public partial class CategoryEdit : UserControl
    {
        public CategoryEdit()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
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
            //txtTitle.Focus();
        }
        private void getDefault()
        {
            var group = new Models.group();
            var groups = new List<Models.group>();
            group.id = Guid.Empty;
            group.title = "-- " + Common.Language.Get("categoryParent", "product") + " --";
            groups.Add(group);
            groups.AddRange(Common.Context.getGroup(Common.AppKey.product, Guid.Empty.ToString(), MainWindow._id));
            cbCategory.ItemsSource = groups;
            cbCategory.DisplayMemberPath = "title";
            cbCategory.SelectedValuePath = "id";
            //
            using (var db = new Models.MainContext())
            {
                var tmp = db.groups.Find(MainWindow._id);
                if (cbCategory.Items.Count > 0)
                    cbCategory.SelectedValue = tmp.parent_id;
                txtTitle.Text = tmp.title;
                txtDesc.Text = tmp.desc;
                if (tmp.flag > 0) rbTrue.IsChecked = true;
                else rbFalse.IsChecked = true;
                lblCreateBy.Content = Common.Language.Get("createdBy");
                lblCreateAt.Content = Common.Language.Get("createdAt");
                lblUpdateBy.Content = Common.Language.Get("updatedBy");
                lblUpdateAt.Content = Common.Language.Get("updatedAt");

                lblCreateBys.Content = Common.Context.GetUser(tmp.created_by);
                lblCreateAts.Content = tmp.created_at.HasValue ? tmp.created_at.Value.ToString("dd/MM/yyyy HH:mm") : Common.Language.Get("emptyvl");
                lblUpdateBys.Content = Common.Context.GetUser(tmp.updated_by);
                lblUpdateAts.Content = tmp.updated_at.HasValue ? tmp.updated_at.Value.ToString("dd/MM/yyyy HH:mm") : Common.Language.Get("emptyvl");
            }
        }
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = Common.Language.Get("ok"),
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                var msg = await MainWindow.listSubWindow["ProductCategoryEdit"].ShowMessageAsync(Common.Language.Get("notification"), Common.Language.Get("msgWarningRequired"), MessageDialogStyle.Affirmative, mySettings);
                txtTitle.Focus();

                //Common.Message.MessageDanger(Common.Language.Get("msgInputError"));
                //MessageBox.Show(Common.Language.Get("msgWarningRequired"), Common.Language.Get("notification"), MessageBoxButton.);
                return;
            }
            try
            {
                using (var db = new Models.MainContext())
                {
                    var group = db.groups.Find(MainWindow._id);
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
                    group.updated_by = "8bb25bd4-37b1-4a48-96e6-0b049d12435f";//Common.Auth.AuthUser.id.ToString();
                    group.updated_at = DateTime.Now;
                    group.flag = rbTrue.IsChecked == true ? 1 : 0;
                    db.Entry(group).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    //
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = Common.Language.Get("ok"),
                        NegativeButtonText = Common.Language.Get("cancel"),
                        ColorScheme = MetroDialogColorScheme.Theme
                    };
                    var msg = await MainWindow.listSubWindow["ProductCategoryEdit"].ShowMessageAsync(
                        Common.Language.Get("notification"),
                        Common.Language.Get("msgUpdateSucsess"),
                        MessageDialogStyle.Affirmative,
                        mySettings);
                    txtTitle.Focus();
                    //
                    //getDefault();
                    MainWindow.listSubWindow["ProductCategoryEdit"].Close();
                    MainWindow.Window.CategoryList();
                }
            }
            catch (Exception)
            {
                Common.Message.MessageDanger(Common.Language.Get("msgCreateError"));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.listSubWindow["ProductCategoryEdit"].Close();
        }
    }
}
