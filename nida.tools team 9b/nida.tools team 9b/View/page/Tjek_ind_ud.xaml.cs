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
    /// Interaction logic for Tjek_ind_ud.xaml
    /// </summary>
    public partial class Tjek_ind_ud : Page
    {
        public Tjek_ind_ud()
        {
            InitializeComponent();
            Tjek_Ind_UdViewModel.ShowStempleSystem(this);

        }

        private void StempleButton_Click(object sender, RoutedEventArgs e)
        {
            Tjek_Ind_UdViewModel.TjekIndUd(this);
        }
    }
}
