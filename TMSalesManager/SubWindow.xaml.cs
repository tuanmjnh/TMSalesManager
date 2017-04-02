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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace TMSalesManager
{
    /// <summary>
    /// Interaction logic for SubWindow.xaml
    /// </summary>
    public partial class SubWindow : MetroWindow
    {
        public SubWindow()
        {
            InitializeComponent();
            PreviewKeyDown += Window_PreviewKeyDown;
            Closed += Window_Closed;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (MainWindow.IsClose)
                Application.Current.Shutdown();

            foreach (var item in MainWindow.listSubWindow)
                if (item.Value == this)
                {
                    MainWindow.listSubWindow.Remove(item.Key);
                    break;
                }

            if (MainWindow.listSubWindow.Count > 0)
                MainWindow.listSubWindow.FirstOrDefault().Value.Focus();
            else MainWindow.Window.Focus();
            MainWindow._id = Guid.Empty;
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
            //if (e.Key == Key.Escape)
            //    foreach (var item in MainWindow.listSubWindow)
            //        if (item.Value == this)
            //        {
            //            MainWindow.listSubWindow[item.Key].Close();
            //            break;
            //        }
        }
    }
}
