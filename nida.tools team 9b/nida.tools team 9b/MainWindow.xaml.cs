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
using nida.tools_team_9b.View.page;
using nida.tools_team_9b.ViewModel;

namespace nida.tools_team_9b
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Temple_Ind_Ud_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Tjek_Ind_Ud();
        }

        private void Tamplate_MødtMedarbejder_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new MødtMedarbejder();
        }

        private void Tamplate_OpretMedarbejder_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new OpretMedarbejder();
        }

        private void tamplate_MedarbejderList_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new MedarbejderList();
        }

        private void Tamplate_TeamList_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TeamList();
        }

        private void Tamplate_TimeBank_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TimeBank();
        }

        private void LogUd_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
