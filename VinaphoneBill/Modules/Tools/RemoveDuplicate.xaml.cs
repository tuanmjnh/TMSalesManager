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

namespace VinaphoneBill.Modules.Tools
{
    /// <summary>
    /// Interaction logic for RemoveDuplicate.xaml
    /// </summary>
    public partial class RemoveDuplicate : UserControl
    {
        public RemoveDuplicate()
        {
            InitializeComponent();
            btnProcessing.IsDefault = true;
            Loaded += UserControl_Loaded;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtMADVI.IsReadOnly = true;
        }
        private void btnDialog_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "DBF Files (*.dbf)|*.dbf|All files (*.*)|*.*";
            //fileDialog.Multiselect = true;
            //openFileDialog.InitialDirectory = @"c:\temp\";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //if (openFileDialog.ShowDialog() == true)
            //    txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            if (fileDialog.ShowDialog() == true)
            {
                lblFile.Content = fileDialog.FileName;
            }
        }
        private void btnProcessing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblFile.Content.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn file DBF trước khi thực hiện!");
                    return;
                }
                TM.IO.CreateDirectory(Common.Directories.tools);
                var arrayFileContent = lblFile.Content.ToString().Split('\\');
                var fileName = arrayFileContent[arrayFileContent.Length - 1];
                var fileNameFull = Common.Directories.tools + fileName;
                TM.IO.Copy(lblFile.Content.ToString(), fileNameFull);
                TM.OleDBF.DataSource = fileNameFull;
                string sql = "ALTER table " + fileName + " ADD COLUMN app_id n(10)";
                try
                {
                    TM.OleDBF.Execute(sql);
                }
                catch (Exception) { }
                sql = "ALTER table " + fileName + " ADD COLUMN dupe_flag n(2)";
                try
                {
                    TM.OleDBF.Execute(sql);
                }
                catch (Exception) { }
                sql = "UPDATE " + fileName + " SET app_id=RECNO()";
                TM.OleDBF.Execute(sql);
                sql = "UPDATE " + fileName + " SET dupe_flag=0";
                TM.OleDBF.Execute(sql);

                if (chkIsMaDvi.IsChecked == true)
                    sql = "UPDATE " + fileName + " SET dupe_flag=1 WHERE app_id in(SELECT app_id FROM " + fileName + " o INNER JOIN (SELECT " + txtAccount.Text + "," + txtMADVI.Text + ",COUNT(*) AS dupeCount FROM " + fileName + " GROUP BY " + txtAccount.Text + "," + txtMADVI.Text + " HAVING COUNT(*) > 1) oc ON o." + txtAccount.Text + "=oc." + txtAccount.Text + " WHERE o." + txtMADVI.Text + "=oc." + txtMADVI.Text + ")";
                else
                    sql = "UPDATE " + fileName + " SET dupe_flag=1 WHERE app_id in(SELECT app_id FROM " + fileName + " o INNER JOIN (SELECT " + txtAccount.Text + ",COUNT(*) AS dupeCount FROM " + fileName + " GROUP BY " + txtAccount.Text + " HAVING COUNT(*) > 1) oc ON o." + txtAccount.Text + "=oc." + txtAccount.Text + ")";
                TM.OleDBF.Execute(sql);

                if (chkIsMaDvi.IsChecked == true)
                    sql = "UPDATE " + fileName + " SET dupe_flag=2 WHERE app_id IN(SELECT MAX(app_id) FROM " + fileName + " o INNER JOIN (SELECT " + txtAccount.Text + "," + txtMADVI.Text + ",COUNT(*) AS dupeCount FROM " + fileName + " GROUP BY " + txtAccount.Text + "," + txtMADVI.Text + " HAVING COUNT(*) > 1) oc ON o." + txtAccount.Text + "=oc." + txtAccount.Text + " WHERE o." + txtMADVI.Text + "=oc." + txtMADVI.Text + " GROUP BY o." + txtAccount.Text + ",o." + txtMADVI.Text + ")";
                else
                    sql = "UPDATE " + fileName + " SET dupe_flag=2 WHERE app_id IN(SELECT MAX(app_id) FROM " + fileName + " o INNER JOIN (SELECT " + txtAccount.Text + ",COUNT(*) AS dupeCount FROM " + fileName + " GROUP BY " + txtAccount.Text + " HAVING COUNT(*) > 1) oc ON o." + txtAccount.Text + "=oc." + txtAccount.Text + " GROUP BY o." + txtAccount.Text + ")";
                TM.OleDBF.Execute(sql);

                sql = "DELETE FROM " + fileName + " WHERE dupe_flag=1";
                TM.OleDBF.Execute(sql);
                sql = "PACK " + fileName;
                TM.OleDBF.Execute(sql);

                MessageBox.Show("RemoveDuplicate Successfull! Get File in ApplicationFolder/DATA/Tools");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkIsMaDvi_Checked(object sender, RoutedEventArgs e)
        {
            txtMADVI.IsReadOnly = false;
        }

        private void chkIsMaDvi_Unchecked(object sender, RoutedEventArgs e)
        {
            txtMADVI.IsReadOnly = true;
        }
    }
}
