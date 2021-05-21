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
    /// Interaction logic for opretTeam.xaml
    /// </summary>
    public partial class opretTeam : Page
    {
        public opretTeam()
        {
            InitializeComponent();

            OpretTeamViewModel.GetTeamlederList(this);
        }

        private void OpretTeam_Click(object sender, RoutedEventArgs e)
        {
            OpretTeamViewModel.OpretTeamData(this, Window.GetWindow(this) as MainWindow);
        }

        private void anulereTeam_Click(object sender, RoutedEventArgs e)
        {
            OpretTeamViewModel.anuller(Window.GetWindow(this) as MainWindow);
        }
    }
}
