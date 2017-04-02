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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace TMSalesManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //readonly IDisposable disposable;
        public static MainWindow Window = null;
        //public static SubWindow subWindow = null;
        public static Dictionary<string, SubWindow> listSubWindow = new Dictionary<string, SubWindow>();
        public static Dictionary<string, UserControl> listUserControl = new Dictionary<string, UserControl>();
        public static bool IsClose = false;
        public static Guid _id;
        public MainWindow()
        {
            if (!Common.Context.CheckConnection())
            {
                MessageBox.Show(Common.Language.Get("msgConnectionError"), Common.Language.Get("msgErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            Common.Language.lang = Common.Config.Value(Common.AppKey.SettingGlobal, Common.AppKey.Language) ?? Common.Language.lang;
            InitializeComponent();
            var DisableMoving = new TM.DisableMovingWindows(this);
            this.SourceInitialized += DisableMoving.Window_SourceInitialized;
            TM.WindowForm.setWindowSize(this, SystemParameters.FullPrimaryScreenWidth, SystemParameters.FullPrimaryScreenHeight + 23, ResizeMode.CanMinimize, 1);
            Window = this;
            Loaded += Window_Loaded;
            Closed += Window_Closed;
            Closing += Window_Closing;
            //PreviewKeyDown += Window_PreviewKeyDown;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Maximize
            //Application.Current.MainWindow.WindowState = WindowState.Maximized;
            //Application.Current.MainWindow.WindowState = WindowState.Minimized;
            //this.Title = "TM Sales Manager 1.0" + Common.Language.Get("title", "main");
            //this.Icon = FontAwesome.WPF.ImageAwesome.CreateImageSource(FontAwesome.WPF.FontAwesomeIcon.SnowflakeOutline, ColorBrush.Brush("FF0E99CD"));

            //
            LoadMenu();
            //Auth();
        }
        //public async MessageDialogResult ShowCustomDialogAsync(string message, string title)
        //{
        //    var mySettings = new MetroDialogSettings()
        //    {
        //        AffirmativeButtonText = Common.Language.Get("ok"),
        //        NegativeButtonText = Common.Language.Get("cancel"),
        //        FirstAuxiliaryButtonText = "Cancel",
        //        ColorScheme = MetroDialogColorScheme.Theme
        //    };
        //    var msg = await this.ShowMessageAsync(Common.Language.Get("notification"), Common.Language.Get("msgWarningRequired"), MessageDialogStyle.AffirmativeAndNegative, mySettings);
        //    return msg;

        //var metroDialogSettings = new MetroDialogSettings()
        //{
        //    AffirmativeButtonText = "OK",
        //    NegativeButtonText = "CANCEL",
        //    AnimateHide = true,
        //    AnimateShow = true,
        //    ColorScheme = MetroDialogColorScheme.Accented,
        //};

        //var dialog = new CustomInputDialog(View, metroDialogSettings)
        //{
        //    Message = message,
        //    Title = title,
        //    Input = metroDialogSettings.DefaultText
        //};

        //return await InvokeOnCurrentDispatcher(async () =>
        //{
        //    await View.ShowMetroDialogAsync(dialog, metroDialogSettings);

        //    await dialog.WaitForButtonPressAsync().ContinueWith((m) =>
        //    {
        //        InvokeOnCurrentDispatcher(() => View.HideMetroDialogAsync(dialog));
        //    });

        //    return dialog.Input;
        //});

        //}
        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = Common.Language.Get("agree"),
                NegativeButtonText = Common.Language.Get("cancel"),
                FirstAuxiliaryButtonText = "Cancel",
                ColorScheme = MetroDialogColorScheme.Accented
            };
            var msg = await this.ShowMessageAsync(
                Common.Language.Get("notification"),
                Common.Language.Get("msgCloseApp"),
                MessageDialogStyle.AffirmativeAndNegative,
                mySettings);
            if (msg == MessageDialogResult.Affirmative)
                Application.Current.Shutdown();

            //if (MessageBox.Show("Dừng chương trình?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //    e.Cancel = true;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Common.Auth.isAuth = false;
            //Dispose();
            //base.OnClosed(e);
            Application.Current.Shutdown();
        }

        //private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Escape) this.Close();
        //}
        public static void LoadMenu()
        {
            Window.MenuConfigCommon.Header = Common.Language.Get("MenuConfigCommon", "main");
            Window.MenuConfigAboutCompany.Header = Common.Language.Get("ConfigAboutCompany", "main");
            Window.MenuConfigSetting.Header = Common.Language.Get("ConfigSetting", "main");
            Window.MenuConfigShutdown.Header = Common.Language.Get("ConfigShutdown", "main");
        }
        #region ModuleAuth
        private void Auth()
        {
            this.Hide();
            //if (subWindow != null) subWindow = null;
            if (!Common.Auth.isAuth)
            {
                IsClose = true;
                var subWindow = new SubWindow();
                var userControl = new Modules.Auth.SignIn();
                listSubWindow.Add("AuthSignIn", subWindow);
                TM.WindowForm.setWindowSize(subWindow, 500, 250, ResizeMode.CanMinimize, 1);
                subWindow.stackContent.Children.Clear();
                subWindow.stackContent.Children.Add(userControl);
                subWindow.Title = Common.Language.Get("titleSignIn", "auth");
                subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.SignIn);//FontAwesome.WPF.ImageAwesome.CreateImageSource(FontAwesome.WPF.FontAwesomeIcon.SignIn, ColorBrush.Brush("FF0E99CD"));
                subWindow.Show();
                subWindow.Focus();
            }
        }
        private void MenuAuthProfile_Click(object sender, RoutedEventArgs e)
        {
            //if (subWindow != null) subWindow = null;
            if (listSubWindow.ContainsKey("AuthProfile")) listSubWindow["AuthProfile"].Focus();
            else
            {
                var subWindow = new SubWindow();
                var userControl = new Modules.Auth.Profile();
                listSubWindow.Add("AuthProfile", subWindow);
                TM.WindowForm.setWindowSize(subWindow, userControl.Width + 30, userControl.Height + 50, ResizeMode.NoResize, 1);
                subWindow.Title = Common.Language.Get("titleAbout", "auth");
                subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.AddressCard);
                subWindow.stackContent.Children.Clear();
                subWindow.stackContent.Children.Add(userControl);
                subWindow.Owner = this;
                subWindow.Show();
                subWindow.Focus();
            }
        }
        private void MenuAuthChangePassword_Click(object sender, RoutedEventArgs e)
        {
            //if (subWindow != null) subWindow = null;
            if (listSubWindow.ContainsKey("AuthChangePassword")) listSubWindow["AuthChangePassword"].Focus();
            else
            {
                var subWindow = new SubWindow();
                var userControl = new Modules.Auth.ChangePassword();
                listSubWindow.Add("AuthChangePassword", subWindow);
                TM.WindowForm.setWindowSize(subWindow, userControl.Width + 30, userControl.Height + 50, ResizeMode.NoResize, 1);
                subWindow.Title = Common.Language.Get("titleChangePassword", "auth");
                subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.Key);
                subWindow.stackContent.Children.Clear();
                subWindow.stackContent.Children.Add(userControl);
                subWindow.Owner = this;
                subWindow.Show();
                subWindow.Focus();
            }
        }
        private void MenuAuthSetting_Click(object sender, RoutedEventArgs e)
        {
            //if (subWindow != null) subWindow = null;
            if (listSubWindow.ContainsKey("AuthSetting")) listSubWindow["AuthSetting"].Focus();
            else
            {
                var subWindow = new SubWindow();
                var userControl = new Modules.Auth.Setting();
                listSubWindow.Add("AuthSetting", subWindow);
                TM.WindowForm.setWindowSize(subWindow, userControl.Width + 30, userControl.Height + 50, ResizeMode.NoResize, 1);
                subWindow.Title = Common.Language.Get("accountSetting", "auth");
                subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.Cog);
                subWindow.stackContent.Children.Clear();
                subWindow.stackContent.Children.Add(userControl);
                subWindow.Owner = this;
                subWindow.Show();
                subWindow.Focus();
            }
        }
        private void MenuAuthLogout_Click(object sender, RoutedEventArgs e)
        {
            Common.Auth.isAuth = false;
            Common.Auth.AuthUser = null;
            Auth();
        }
        #endregion
        #region Config
        private void MenuConfigAboutCompany_Click(object sender, RoutedEventArgs e)
        {
            var userControl = new Modules.Config.Company();
            this.stackContent.Children.Clear();
            this.stackContent.Children.Add(userControl);
            //subWindow.setWindowSize(userControl.Width + 30, userControl.Height + 50, 1);
        }
        private void MenuConfigSetting_Click(object sender, RoutedEventArgs e)
        {
            var userControl = new Modules.Config.Setting();
            this.stackContent.Children.Clear();
            this.stackContent.Children.Add(userControl);
            //subWindow.setWindowSize(userControl.Width + 30, userControl.Height + 50, 1);
        }
        private void MenuConfigShutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #region Bill
        private void MenuBillCreate_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuBillList_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region Product
        private void MenuProductList_Click(object sender, RoutedEventArgs e)
        {
            var userControl = new Modules.Product.ProductList();
            this.stackContent.Children.Clear();
            this.stackContent.Children.Add(userControl);
        }
        private void MenuProductAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listSubWindow.ContainsKey("ProductAdd")) listSubWindow["ProductAdd"].Focus();
            else
            {
                var subWindow = new SubWindow();
                var userControl = new Modules.Product.ProductAdd();
                listSubWindow.Add("ProductAdd", subWindow);
                TM.WindowForm.setWindowSize(subWindow, userControl.Width, userControl.Height + 40, ResizeMode.NoResize, 1);
                subWindow.Title = Common.Language.Get("productTitleAdd", "product");
                subWindow.stackContent.Children.Clear();
                subWindow.stackContent.Children.Add(userControl);
                subWindow.Show();
                subWindow.Focus();
            }
        }
        public void CategoryList()
        {
            var userControl = new Modules.Product.CategoryList();
            this.stackContent.Children.Clear();
            this.stackContent.Children.Add(userControl);
            lblTitle.Content = Common.Language.Get("categoryTitleList", "product");
        }
        private void MenuCategoryList_Click(object sender, RoutedEventArgs e)
        {
            CategoryList();
        }
        private void MenuCategoryAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listSubWindow.ContainsKey("ProductCategoryAdd")) listSubWindow["ProductCategoryAdd"].Focus();
            else
            {
                var subWindow = new SubWindow();
                var userControl = new Modules.Product.CategoryAdd();
                listSubWindow.Add("ProductCategoryAdd", subWindow);
                TM.WindowForm.setWindowSize(subWindow, userControl.Width, userControl.Height + 40, ResizeMode.NoResize, 1);
                subWindow.Title = Common.Language.Get("categoryTitleAdd", "product");
                subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.PlusCircle);
                subWindow.stackContent.Children.Clear();
                subWindow.stackContent.Children.Add(userControl);
                subWindow.ShowDialog();
            }
        }
        private void MenuProductOption_Click(object sender, RoutedEventArgs e)
        {
            var userControl = new Modules.Product.ProductOption();
            this.stackContent.Children.Clear();
            this.stackContent.Children.Add(userControl);
        }
        #endregion
        #region Report
        private void MenuReportMoney_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuReportInventory_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region Account
        private void MenuAccountList_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuAccountAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region About
        private void MenuAboutCommon_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuAboutRegister_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuAboutContact_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        //private void Dispose()
        //{
        //    subWindow = null;
        //}
        //public void Dispose()
        //{
        //    disposable.Dispose();
        //}
    }
}
