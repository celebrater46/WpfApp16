﻿using System.Windows.Input;

namespace WpfApp16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool IsExistsSaveConfigPath()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.SavingConfigPath) ||
                System.IO.File.Exists(Properties.Settings.Default.SavingConfigPath) == false)
            {
                return false; // No file or no setting
            }
            else
            {
                return true; // The file and setting exist 
            }
        }
    }
}