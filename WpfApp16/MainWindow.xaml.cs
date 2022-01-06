using System;
using System.Windows;
using System.Windows.Input;

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

        private void InfoMsg(string msg)
        {
            MessageBox.Show(msg, Properties.Resources.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ErrMsg(string msg)
        {
            MessageBox.Show(msg, Properties.Resources.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AddBtClick(object sender, RoutedEventArgs e)
        {
            DateTime d;

            if (System.Text.RegularExpressions.Regex.IsMatch(timeText.Text, "^[0-9]{2}:[0-9]{2}$") &&
                DateTime.TryParse("2000/01/01 " + timeText.Text, out d))
            {
                // whether the inputted text exists
                if (string.IsNullOrWhiteSpace(msgText.Text))
                {
                    // No message
                    ErrMsg("Type a message.");
                    msgText.Focus();
                }
                else
                {
                    // The time and the message both exist
                    listBox.Items.Add(timeText.Text + "\t" + msgText.Text);
                    
                    timeText.Clear();
                    msgText.Clear();
                    timeText.Focus();
                }
            }
            else
            {
                // The message is not correct as time
                ErrMsg("Type a date time correctly.");
                timeText.Focus();
            }
        }
    }
}