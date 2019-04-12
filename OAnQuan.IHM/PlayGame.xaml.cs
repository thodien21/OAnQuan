using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace OAnQuan.IHM
{
    /// <summary>
    /// Logique d'interaction pour WindowGame.xaml
    /// </summary>
    public partial class PlayGame : Window
    {
        public PlayGame()
        {
            InitializeComponent();
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button { ButtonContent = "b0", ButtonID = "0", X = 50, Y = 100, Height = 100 });
            buttons.Add(new Button { ButtonContent = "b1", ButtonID = "1", X = 100, Y = 100, Height = 50 });
            buttons.Add(new Button { ButtonContent = "b2", ButtonID = "2", X = 150, Y = 100, Height = 50 });
            buttons.Add(new Button { ButtonContent = "b3", ButtonID = "3", X = 200, Y = 100, Height = 50 });
            buttons.Add(new Button { ButtonContent = "b4", ButtonID = "4", X = 250, Y = 100, Height = 50 });
            buttons.Add(new Button { ButtonContent = "b5", ButtonID = "5", X = 300, Y = 100, Height = 50 });

            ic.ItemsSource = buttons;
        }
    }

    class Button
    {
        public string ButtonContent { get; set; }
        public string ButtonID { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public long Height { get; set; }
    }
}
