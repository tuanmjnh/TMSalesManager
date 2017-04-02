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

namespace VinaphoneBill.Modules.Internet
{
    /// <summary>
    /// Interaction logic for Mega.xaml
    /// </summary>
    public partial class Mega : UserControl
    {
        public Mega()
        {
            InitializeComponent();
            btnProcessing.IsDefault = true;
            Loaded += UserControl_Loaded;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtTime.Focus();
        }
        private void btnProcessing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Declare
                TM.OleDBF.DataSource = Common.Directories.data + txtTime.Text;
                var ext = ".dbf";
                var mega = "mega";
                var ck = "ck" + txtTime.Text;
                var mega_ct = "mega_ct" + txtTime.Text;
                var mega_th = "mega_th" + txtTime.Text;

                //Create new file
                TM.IO.Delete(TM.OleDBF.DataSource + "/" + mega + ext);
                TM.IO.Copy(TM.OleDBF.DataSource + "/" + mega_th + ext, TM.OleDBF.DataSource + "/" + mega + ext);

                //Processing
                //Update giam_tru
                TM.OleDBF.Execute("UPDATE " + mega_th + " SET giam_tru=giam_tru*130/150 WHERE toc_do='BASIC' AND cuoc_tb=0 AND cuoc_sd<=" + txtCuocSD.Text);
                //Update cuoc_sdkh
                TM.OleDBF.Execute("UPDATE " + mega_th + " SET cuoc_sdkh=cuoc_sdkh*130/150 WHERE toc_do='BASIC' AND cuoc_tb=0 AND cuoc_sd<=" + txtCuocSD.Text);
                //Update Tiền
                TM.OleDBF.Execute("UPDATE " + mega_th + " SET tien=cuoc_sdkh-giam_tru WHERE toc_do='BASIC' AND cuoc_tb=0 AND cuoc_sd<=" + txtCuocSD.Text);
                //Update VAT
                TM.OleDBF.Execute("UPDATE " + mega_th + " SET vat=ROUND(tien*0.1,0)");

                //Delete BAK
                TM.IO.DeleteExt(TM.OleDBF.DataSource, new[] { ".BAK" });
                MessageBox.Show(Common.Language.Get("msgSucsess"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.subWindow.Close();
            MainWindow.Window.Focus();
        }
    }
}
