using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
    /// Interaction logic for ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : UserControl
    {
        //System.Collections.ObjectModel.ObservableCollection<Models.sub_item> ObSubItem = new System.Collections.ObjectModel.ObservableCollection<Models.sub_item>();
        public ProductAdd()
        {
            InitializeComponent();
            Loaded += UserControl_Loaded;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //
            gbDetailsMain.Header = Common.Language.Get("detailsMain", "product");
            lblTitle.Content = Common.Language.Get("productTitle", "product");
            lblCodeKey.Content = Common.Language.Get("productCodeKey", "product");
            lblQuantity.Content = Common.Language.Get("productQuantity", "product");
            lblPriceOld.Content = Common.Language.Get("productPriceOld", "product");
            lblPrice.Content = Common.Language.Get("productPrice", "product");
            //
            gbDetailsOption.Header = Common.Language.Get("detailsOption");
            lblFlag.Content = Common.Language.Get("status");
            rbTrue.Content = Common.Language.Get("show");
            rbFalse.Content = Common.Language.Get("draft");
            //
            gbDetailsImages.Header = Common.Language.Get("detailsImages");
            //
            gbDetailsAttributes.Header = Common.Language.Get("detailsAttributes");
            //
            gbDesc.Header = Common.Language.Get("desc");
            //
            getDefault();
            //DataContext = ObSubItem;
        }
        private void getDefault()
        {
            txtCodeKey.Text = "";
            txtTitle.Text = "";
            txtQuantity.Text = "1";
            txtPriceOld.Text = "";
            txtPrice.Text = "";
            txtDesc.Text = "";
            rbTrue.IsChecked = true;
            ImageFile.Source = null;
            loadOption();
            txtCodeKey.Focus();
        }
        public void loadOption()
        {
            var ls = new List<Models.sub_item>();
            var sub_item = new Models.sub_item();
            sub_item.id = Guid.NewGuid();
            sub_item.main_key = "abc";
            sub_item.value = "45+61";
            ls.Add(sub_item);
            gdAttributes.ItemsSource = ls;
            //ObSubItem.Add(sub_item);
            //var sub_item = new Models.sub_item();
            //lsOption.ItemsSource = ls;
        }
        private bool Validate()
        {
            if (string.IsNullOrEmpty(txtCodeKey.Text))
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = Common.Language.Get("ok"),
                    NegativeButtonText = Common.Language.Get("cancel"),
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                var msg = MainWindow.listSubWindow["ProductAdd"].ShowMessageAsync(
                    Common.Language.Get("notification"),
                    Common.Language.Get("msgWarningRequired"),
                    MessageDialogStyle.Affirmative,
                    mySettings);
                txtCodeKey.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = Common.Language.Get("ok"),
                    NegativeButtonText = Common.Language.Get("cancel"),
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                var msg = MainWindow.listSubWindow["ProductAdd"].ShowMessageAsync(
                    Common.Language.Get("notification"),
                    Common.Language.Get("msgWarningRequired"),
                    MessageDialogStyle.Affirmative,
                    mySettings);
                txtTitle.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = Common.Language.Get("ok"),
                    NegativeButtonText = Common.Language.Get("cancel"),
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                var msg = MainWindow.listSubWindow["ProductAdd"].ShowMessageAsync(
                    Common.Language.Get("notification"),
                    Common.Language.Get("msgWarningRequired"),
                    MessageDialogStyle.Affirmative,
                    mySettings);
                txtQuantity.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPriceOld.Text))
            {
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = Common.Language.Get("ok"),
                    NegativeButtonText = Common.Language.Get("cancel"),
                    ColorScheme = MetroDialogColorScheme.Theme
                };
                var msg = MainWindow.listSubWindow["ProductAdd"].ShowMessageAsync(
                    Common.Language.Get("notification"),
                    Common.Language.Get("msgWarningRequired"),
                    MessageDialogStyle.Affirmative,
                    mySettings);
                txtPriceOld.Focus();
                return false;
            }
            return true;
        }
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                    using (var db = new Models.MainContext())
                    {
                        var item = new Models.item();
                        item.id = Guid.NewGuid();
                        item.app_key = Common.AppKey.product;
                        item.code_key = txtCodeKey.Text;
                        item.title = txtTitle.Text;
                        item.quantity = long.Parse(txtQuantity.Text);
                        item.quantity_total = item.quantity;
                        item.price_old = decimal.Parse(txtPriceOld.Text);
                        item.price = string.IsNullOrEmpty(txtPrice.Text) ? item.price_old : decimal.Parse(txtPrice.Text);
                        var images = (BitmapImage)ImageFile.Source;
                        item.image = images != null ? TM.IO.BitmapToByte(images) : null;
                        item.desc = txtDesc.Text;
                        item.flag = rbTrue.IsChecked == true ? 1 : 0;
                        db.items.Add(item);
                        //
                        var attributes = gdAttributes.Items;
                        for (int i = 0; i < attributes.Count - 1; i++)
                        {
                            var sub_item = (Models.sub_item)attributes[i];
                            sub_item.id = Guid.NewGuid();
                            sub_item.app_key = Common.AppKey.product;
                            sub_item.item_id = item.id;
                            sub_item.flag = 1;
                            db.sub_item.Add(sub_item);
                        }
                        db.SaveChanges();
                        //
                        var mySettings = new MetroDialogSettings()
                        {
                            AffirmativeButtonText = Common.Language.Get("ok"),
                            ColorScheme = MetroDialogColorScheme.Theme
                        };
                        var msg = await MainWindow.listSubWindow["ProductAdd"].ShowMessageAsync(
                            Common.Language.Get("notification"), Common.Language.Get("msgCreateSucsess"),
                            MessageDialogStyle.Affirmative,
                            mySettings);
                        getDefault();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private System.Drawing.Bitmap compressImage(System.Drawing.Bitmap image)
        {
            System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(@"c:\TestPhoto.jpg");
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"c:\TestPhotoQualityFifty.jpg", jgpEncoder, myEncoderParameters);
            return bmp1;
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = TM.IO.FileDialogFilterImages();
            //fileDialog.Multiselect = true;
            //openFileDialog.InitialDirectory = @"c:\temp\";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //if (openFileDialog.ShowDialog() == true)
            //    txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
            if (fileDialog.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage(new Uri(fileDialog.FileName));
                ImageFile.Source = img;
            }

            //byte[] binaryData = Convert.FromBase64String(bgImage64);

            //BitmapImage bi = new BitmapImage();
            //bi.BeginInit();
            //bi.StreamSource = new MemoryStream(binaryData);
            //bi.EndInit();

            //Image img = new Image();
            //img.Source = bi;
        }

        private void btnAttributesSave_Click(object sender, RoutedEventArgs e)
        {

            //var subWindow = new SubWindow();
            //var userControl = new Modules.Product.OptionAdd();
            //MainWindow.listSubWindow.Add("ProductOptionAdd", subWindow);
            //TM.WindowForm.setWindowSize(subWindow, userControl.Width, userControl.Height + 40, ResizeMode.NoResize, 1);
            //subWindow.Title = Common.Language.Get("OptionTitleAdd", "product");
            //subWindow.stackContent.Children.Clear();
            //subWindow.stackContent.Children.Add(userControl);
            //subWindow.ShowDialog();
            //subWindow.Focus();
        }
    }
}
