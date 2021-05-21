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
    /// Interaction logic for TeamList.xaml
    /// </summary>
    public partial class TeamList : Page
    {
        public TeamList()
        {
            InitializeComponent();
            TeamListGrid.ItemsSource = TeamListViewModel.GetTeam();
            TeamListViewModel.ShowTeamListButtons(this);
        }

        private void opretTeam_Click(object sender, RoutedEventArgs e)
        {
            TeamListViewModel.ShowOpretTeamPage(Window.GetWindow(this) as MainWindow);
        }

        private void fjernTeam_Click(object sender, RoutedEventArgs e)
        {

            TeamListViewModel.DeleteTeam(this, Window.GetWindow(this) as MainWindow);
        }
    }
}
