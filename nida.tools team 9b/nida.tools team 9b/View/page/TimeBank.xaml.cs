﻿using System;
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
    /// Interaction logic for TimeBank.xaml
    /// </summary>
    public partial class TimeBank : UserControl
    {
        public TimeBank()
        {

            InitializeComponent();
            


        }

        private void Søg_Clicked(object sender, RoutedEventArgs e)
        {
            var til = Convert.ToDateTime(tilDato.SelectedDate).ToString("yyyy/MM/dd");
            var fra = Convert.ToDateTime(fraDato.SelectedDate).ToString("yyyy/MM/dd");
            ArbjedesTimer.ItemsSource = TimeBankViewModel.getTimeBankList(til, fra);
            //ArbjedesTimer.ItemsSource =  TimeBankViewModel.getTimeBankList(fraDato.SelectedDate.ToString(), tilDato.SelectedDate.ToString());
        }
    }
}
