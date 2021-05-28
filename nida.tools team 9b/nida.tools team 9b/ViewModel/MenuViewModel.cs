using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using nida.tools_team_9b.Model;
using nida.tools_team_9b.View.window;



namespace nida.tools_team_9b.ViewModel
{
    public class MenuViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public static void GetMenu(MainWindow mainWindow)
        {
            mainWindow.menuFrame.Source = new Uri("/View/Menu/" + GetMenuName(Convert.ToInt32(Application.Current.Properties["Global_userRole"])) + ".xaml", UriKind.Relative);
        }
        private static string GetMenuName(int role)
        {
            string menuName = "";
            switch (role)
            {
                case 1:
                    menuName = "AdminMenu";
                    break;
                case 2:
                    menuName = "LederMenu";
                    break;
                case 3:
                    menuName = "MedarbejderMenu";
                    break;
            }
            return menuName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mainWindow"></param>
        public static void MenuButtonOnClick(object sender, MainWindow mainWindow)
        {
            Button btn = (Button)sender;
            string buttonName = btn.Name.ToString();

            if (buttonName == "LogUd")
            {
                Application.Current.Properties["Global_userId"] = null;
                Application.Current.Properties["Global_userRole"] = null;
                LogIndWindow newLogindWindow = new LogIndWindow();
                newLogindWindow.Show();
                mainWindow.Close();


                // MainWindow mainWindow = new MainWindow();
                //list newlist = new list();
            }else
            {
                mainWindow.contentHolder.Source = new Uri("/View/page/" + buttonName + ".xaml", UriKind.Relative);
            }

        }
       
    }
}
