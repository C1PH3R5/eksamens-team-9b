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
using System.Windows.Navigation;
using System.Windows.Shapes;
using nida.tools_team_9b.ViewModel;

namespace nida.tools_team_9b.View.page
{
    /// <summary>
    /// Interaction logic for opretMedarbejder.xaml
    /// </summary>
    public partial class opretMedarbejder : Page
    {
        public opretMedarbejder()
        {
            InitializeComponent();
            OpretMedarbejderViewModel.GetTeamList(this);
        }

        private void opret_Click(object sender, RoutedEventArgs e)
        {
            OpretMedarbejderViewModel.OpretMedarbejderData(this);
        }

        private void anuller_Click(object sender, RoutedEventArgs e)
        {
            OpretMedarbejderViewModel.anuller(Window.GetWindow(this) as MainWindow);
        }
    }
}


