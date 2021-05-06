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
using nida.tools_team_9b.ViewModel;


namespace nida.tools_team_9b.View.window
{
    /// <summary>
    /// Interaction logic for LogIndWindow.xaml
    /// </summary>
    public partial class LogIndWindow : Window
    {
        public LogIndWindow()
        {
            InitializeComponent();
        }

        private void LogIndButton_Click(object sender, RoutedEventArgs e)
        {
            LogindViewModel.LogIndServices(textUserid.Text, passwordBox.Password.ToString(), this);
        }
    }
}
