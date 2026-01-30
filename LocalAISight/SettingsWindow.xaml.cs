using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LocalAISight
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DefaultPromptTextBox.Text = Properties.Settings.Default.DefaultPrompt;
            SystemMessageTextBox.Text = Properties.Settings.Default.SystemPrompt;
            OCRPromptTextBox.Text = Properties.Settings.Default.OCRPrompt;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DefaultPrompt = DefaultPromptTextBox.Text;
            Properties.Settings.Default.OCRPrompt = OCRPromptTextBox.Text;
            Properties.Settings.Default.SystemPrompt = SystemMessageTextBox.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
