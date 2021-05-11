using System;
using System.Collections.Generic;
using System.Text;

namespace nida.tools_team_9b.Model
{
    class Menu
    {
        private string _Text;

        public string text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        private string _ImageUrl;

        public string imageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        private string _Action;

        public string action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        private int _Order;

        public int order
        {
            get { return _Order; }
            set { _Order = value; }
        }


    }
}
