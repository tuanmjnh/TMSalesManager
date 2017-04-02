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
using FontAwesome.WPF;

namespace TMSalesManager.Modules.Config
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();

            MainWindow.Window.Title = Common.Config.ApplicationName + " - " + Common.Language.Get("ConfigSetting", "main", true);
            gbGeneral.Header = Common.Language.Get("settingGeneral", "main");
            lblLanguage.Content = Common.Language.Get("language", "main");
            //Combobox Language
            cbLanguage.ItemsSource = Common.Language.GetLanguageList();
            cbLanguage.SelectedValuePath = "Key";
            cbLanguage.DisplayMemberPath = "Value";
            cbLanguage.SelectedValue = Common.Config.Value(Common.AppKey.SettingGlobal, Common.AppKey.Language);
        }
        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLanguage.SelectedValue != null)
            {
                var setting = new Models.setting();
                var SettingLanguage = Common.Config.Setting(Common.AppKey.SettingGlobal, Common.AppKey.Language);//.Where(d => d.module_key == Common.Object.SettingGlobal && d.sub_key == Common.Object.Language).FirstOrDefault();
                if (SettingLanguage == null)
                {
                    setting.module_key = Common.AppKey.SettingGlobal;
                    setting.sub_key = Common.AppKey.Language;
                }
                else
                    setting.id = SettingLanguage.id;
                setting.value = cbLanguage.SelectedValue.ToString();
                setting.sub_value = ((KeyValuePair<string, string>)cbLanguage.SelectedItem).Value;
                Common.Config.SetSetting(setting);
                Common.Config.LoadSetting();
                Common.Language.lang = cbLanguage.SelectedValue.ToString();

                //
                var userControl = new Setting();
                MainWindow.Window.stackContent.Children.Clear();
                MainWindow.Window.stackContent.Children.Add(userControl);
                MainWindow.LoadMenu();
            }
        }
    }
}
