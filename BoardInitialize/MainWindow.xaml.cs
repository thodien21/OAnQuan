using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoardInitialize
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Button> buttons = new List<Button>();
            buttons.Add(new Button { ButtonContent = "b0", ButtonID = "0", X = 0, Y = 0, Height = Button.u * 2, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b1", ButtonID = "1", X = Button.u, Y = 0, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b2", ButtonID = "2", X = Button.u * 2, Y = 0, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b3", ButtonID = "3", X = Button.u * 3, Y = 0, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b4", ButtonID = "4", X = Button.u * 4, Y = 0, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b5", ButtonID = "5", X = Button.u * 5, Y = 0, Height = Button.u, Width = Button.u });

            buttons.Add(new Button { ButtonContent = "b6", ButtonID = "6", X = Button.u * 6, Y = 0, Height = Button.u * 2, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b7", ButtonID = "7", X = Button.u * 5, Y = Button.u, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b8", ButtonID = "8", X = Button.u * 4, Y = Button.u, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b9", ButtonID = "9", X = Button.u * 3, Y = Button.u, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b10", ButtonID = "10", X = Button.u * 2, Y = Button.u, Height = Button.u, Width = Button.u });
            buttons.Add(new Button { ButtonContent = "b11", ButtonID = "11", X = Button.u, Y = Button.u, Height = Button.u, Width = Button.u });

            ic.ItemsSource = buttons;
        }
    }
    class Button
    {
        public const int u = 160;
        public string ButtonContent { get; set; }
        public string ButtonID { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
    }
}
