﻿using System;
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

namespace VinaphoneBill.Modules.Config
{
    /// <summary>
    /// Interaction logic for Company.xaml
    /// </summary>
    public partial class Company : UserControl
    {
        public Company()
        {
            InitializeComponent();
            MainWindow.Window.Title = Common.Config.ApplicationName + " - " + Common.Language.Get("ConfigAboutCompany", "main", true);
        }
    }
}
