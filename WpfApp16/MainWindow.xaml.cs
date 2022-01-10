using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using Microsoft.Win32;

namespace WpfApp16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string showTime;
        private Dictionary<string, string> timeAndMsgs;
        private DispatcherTimer timer;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitProc()
        {
            timeAndMsgs = new Dictionary<string, string>();
            showTime = "";
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += new EventHandler(timerTick);
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

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listBox.Items.Count > 0)
                {
                    if (IsExistsSaveConfigPath())
                    {
                        SaveConfigFile();
                    }
                    else
                    {
                        ShowSaveDialogToConfigFile();
                    }
                }

                Properties.Settings.Default.AlarmEnabled = alarmOn.IsChecked == true;
                Properties.Settings.Default.Save();
            }
            catch(Exception ex)
            {
                ErrMsg(ex.Message);
            }
            
        }

        private void Window_OnClosing(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveConfigFile()
        {
            using (var sw = new StreamWriter(Properties.Settings.Default.SavingConfigPath, append: false,
                       encoding: Encoding.UTF8))
            {
                // Output the list file
                foreach (string item in listBox.Items)
                {
                    // It Writes one line
                    sw.WriteLine(item);
                }
            }
        }

        private void ShowSaveDialogToConfigFile()
        {
            // Generate the dialog
            var dlg = new SaveFileDialog();
            dlg.Title = "Save the config";
            dlg.FileName = Properties.Resources.AppTitle + "_config.dat";
            dlg.Filter = "Config File|*.dat|All files|*.*";
            
            // Show the dialog of saving
            if (dlg.ShowDialog() == true)
            {
                Properties.Settings.Default.SavingConfigPath = dlg.FileName;
                SaveConfigFile();
            }
        }
    }
}