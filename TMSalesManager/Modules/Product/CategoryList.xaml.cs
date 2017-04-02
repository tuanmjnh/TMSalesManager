using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections;
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

namespace TMSalesManager.Modules.Product
{
    /// <summary>
    /// Interaction logic for CategoryList.xaml
    /// </summary>
    public partial class CategoryList : UserControl
    {
        private int flag = 1;
        public CategoryList()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnShow.ToolTip = Common.Language.Get("show");
            btnHide.ToolTip = Common.Language.Get("draft");
            dgList.AutoGenerateColumns = false;
            TM.Paging.Page.PageNumber = 1;
            TM.Paging.Page.PageSize = 2;
            txtPageInput.Text = "1";
            loadData();
        }
        public void loadData()
        {
            try
            {
                if (flag == 0)
                {
                    btnShow.IsEnabled = true;
                    btnHide.IsEnabled = false;
                    dgList.ItemsSource = TM.Paging.Page.ToPage(Common.Context.getGroups(Common.AppKey.product, null, Guid.Empty, flag));
                    //dgList.ItemsSource = Common.Context.getGroups(Common.AppKey.product, null, Guid.Empty, flag);
                }
                else
                {
                    btnShow.IsEnabled = false;
                    btnHide.IsEnabled = true;
                    dgList.ItemsSource = TM.Paging.Page.ToPage(Common.Context.getGroups(Common.AppKey.product, Guid.Empty.ToString(), Guid.Empty, flag));
                    //dgList.ItemsSource = Common.Context.getGroups(Common.AppKey.product, Guid.Empty.ToString(), Guid.Empty, flag);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void colchkSelect1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void colchkSelect1_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            flag = 1;
            loadData();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            flag = 0;
            loadData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dynamic item = dgList.SelectedItem;
            if (item != null)
                if (MainWindow.listSubWindow.ContainsKey("ProductCategoryAdd")) MainWindow.listSubWindow["ProductCategoryAdd"].Focus();
                else
                {
                    MainWindow._id = item.id;
                    var subWindow = new SubWindow();
                    var userControl = new Modules.Product.CategoryEdit();
                    MainWindow.listSubWindow.Add("ProductCategoryEdit", subWindow);
                    TM.WindowForm.setWindowSize(subWindow, userControl.Width, userControl.Height + 40, ResizeMode.NoResize, 1);
                    subWindow.Title = Common.Language.Get("categoryTitleEdit", "product");
                    subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.PlusCircle);
                    subWindow.stackContent.Children.Clear();
                    subWindow.stackContent.Children.Add(userControl);
                    subWindow.ShowDialog();
                }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = Common.Language.Get("ok"),
                NegativeButtonText = Common.Language.Get("cancel"),
                ColorScheme = MetroDialogColorScheme.Theme
            };
            var msg = await MainWindow.Window.ShowMessageAsync(
                Common.Language.Get("notification"),
                Common.Language.Get("msgRemoveRecord"),
                MessageDialogStyle.AffirmativeAndNegative,
                mySettings);

            if (msg == MessageDialogResult.Affirmative)
                using (var db = new Models.MainContext())
                {
                    foreach (dynamic item in dgList.SelectedItems)
                    {
                        var group = db.groups.Find(item.id);
                        db.groups.Remove(group);
                    }
                    db.SaveChanges();
                }
            loadData();
        }

        private void txtPageInput_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                TM.Paging.Page.PageNumber = Int32.Parse(txtPageInput.Text);
            }
            catch (Exception)
            {
                TM.Paging.Page.ToFirst();
            }
            loadData();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            TM.Paging.Page.ToFirst();
            txtPageInput.Text = TM.Paging.Page.PageNumber.ToString();
            loadData();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            TM.Paging.Page.ToPrevious();
            txtPageInput.Text = TM.Paging.Page.PageNumber.ToString();
            loadData();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            TM.Paging.Page.ToNext();
            txtPageInput.Text = TM.Paging.Page.PageNumber.ToString();
            loadData();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            TM.Paging.Page.ToLast();
            txtPageInput.Text = TM.Paging.Page.PageNumber.ToString();
            loadData();
        }
    }
}
