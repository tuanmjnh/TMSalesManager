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

namespace VinaphoneBill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Window = null;
        //public static SubWindow subWindow = null;
        public static Dictionary<string, SubWindow> listSubWindow = new Dictionary<string, SubWindow>();
        public static bool IsClose = false;
        public MainWindow()
        {
            InitializeComponent();
            if (!Common.Context.CheckConnection())
            {
                MessageBox.Show(Common.Language.Get("msgConnectionError"), Common.Language.Get("msgErrorTitle"), MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            Common.Language.lang = Common.Config.Value(Common.AppKey.SettingGlobal, Common.AppKey.Language) ?? Common.Language.lang;
            InitializeComponent();
            var DisableMoving = new TM.DisableMovingWindows(this);
            this.SourceInitialized += DisableMoving.Window_SourceInitialized;
            TM.WindowForm.setWindowSize(this, SystemParameters.FullPrimaryScreenWidth, SystemParameters.FullPrimaryScreenHeight + 22, ResizeMode.CanMinimize, 1);
            Window = this;
            Loaded += Window_Loaded;
            Closed += Window_Closed;
            Closing += Window_Closing;
            //lblTitle.Content = "";
            //lblMessage.Content = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMenu();
            //Auth();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Dừng chương trình?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                e.Cancel = true;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Common.Auth.isAuth = false;
            //Dispose();
            //base.OnClosed(e);
            Application.Current.Shutdown();
        }
        public static void LoadMenu()
        {
            Window.MenuConfigCommon.Header = Common.Language.Get("MenuConfigCommon", "main");
            Window.MenuConfigSetting.Header = Common.Language.Get("ConfigSetting", "main");
            Window.MenuConfigShutdown.Header = Common.Language.Get("ConfigShutdown", "main");
        }

        #region Config
        private void MenuConfigSetting_Click(object sender, RoutedEventArgs e)
        {
            this.stackContent.Children.Clear();
            this.stackContent.Children.Add(new Modules.Config.Setting());
            //subWindow.setWindowSize(userControl.Width + 30, userControl.Height + 50, 1);
        }
        private void MenuConfigShutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #region Internet
        //SELECT * FROM hddd1016 o INNER JOIN (SELECT ma_cq , COUNT(*) AS dupeCount FROM hddd1016 GROUP BY ma_cq,ma_dvi HAVING COUNT(*) > 1) oc ON o.ma_cq=oc.ma_cq ORDER BY o.ma_cq
        private void MenuFiber_Click(object sender, RoutedEventArgs e)
        {
            //if (subWindow != null) subWindow = null;
            //if (listSubWindow.ContainsKey("Fiber")) listSubWindow["Fiber"].Focus();
            //else
            //{

            //    var subWindow = new SubWindow();
            //    var userControl = new Modules.Internet.Fiber();
            //    listSubWindow.Add("Fiber", subWindow);
            //    TM.WindowForm.setWindowSize(subWindow, userControl.Width+50, userControl.Height + 50, ResizeMode.CanResize, 1);
            //    subWindow.Title = "Xử lý FiberVNN";
            //    subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.Fire);
            //    subWindow.stackContent.Children.Clear();
            //    subWindow.stackContent.Children.Add(userControl);
            //    subWindow.Owner = this;
            //    subWindow.ShowInTaskbar = false;
            //    subWindow.ShowDialog();
            //    subWindow.Focus();
            //}
            this.stackContent.Children.Clear();
            this.stackContent.Children.Add(new Modules.Internet.Fiber());
        }
        private void MenuMega_Click(object sender, RoutedEventArgs e)
        {
            //if (subWindow != null) subWindow = null;
            if (listSubWindow.ContainsKey("Mega")) listSubWindow["Mega"].Focus();
            else
            {
                var subWindow = new SubWindow();
                var userControl = new Modules.Internet.Mega();
                listSubWindow.Add("Mega", subWindow);
                TM.WindowForm.setWindowSize(subWindow, userControl.Width, userControl.Height + 50, ResizeMode.CanResize, 1);
                subWindow.Title = "Xử lý MegaVNN";
                subWindow.Icon = Common.Config.ImageSource(FontAwesome.WPF.FontAwesomeIcon.Mercury);
                subWindow.stackContent.Children.Clear();
                subWindow.stackContent.Children.Add(userControl);
                subWindow.Owner = this;
                subWindow.ShowInTaskbar = false;
                subWindow.ShowDialog();
                subWindow.Focus();
                
            }
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
    }
}
