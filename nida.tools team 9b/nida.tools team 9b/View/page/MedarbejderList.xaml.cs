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
    
    public partial class MedarbejderList : Page
    {
        public MedarbejderList()
        {
            InitializeComponent();
            medarbejderList.ItemsSource = MedarbejderListViewModel.GetMedarbejder(this);
            MedarbejderListViewModel.ShowMedarbejderListButtons(this);
        }

        private void opretMedarbejder_Click(object sender, RoutedEventArgs e)
        {
            MedarbejderListViewModel.ShowOpretMedarbejderPage(Window.GetWindow(this) as MainWindow);
        }
    }
}
