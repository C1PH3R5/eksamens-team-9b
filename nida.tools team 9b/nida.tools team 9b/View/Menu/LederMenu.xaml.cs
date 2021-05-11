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
using nida.tools_team_9b;

namespace nida.tools_team_9b.View.Menu
{
    /// <summary>
    /// Interaction logic for LederMenu.xaml hej er
    /// </summary>
    public partial class LederMenu : Page
    {
        public LederMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuViewModel.MenuButtonOnClick(sender, Window.GetWindow(this) as MainWindow);
        }
    }
}
