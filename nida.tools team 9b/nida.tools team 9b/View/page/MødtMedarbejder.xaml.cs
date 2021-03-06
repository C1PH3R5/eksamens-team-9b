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
    /// Interaction logic for MødtMedarbejder.xaml
    /// </summary>
    public partial class MødtMedarbejder : Page
    {
        public MødtMedarbejder()
        {
            InitializeComponent();
            mødtMedarbejder.ItemsSource = MødtMedarbejderViewModel.LoadMedarbjderDataSet();
        }
    }
}
