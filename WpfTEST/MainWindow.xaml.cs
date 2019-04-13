using OAnQuan;
using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfTEST
{
    /// <summary>
    /// Logique d'interaction pour WindowGame.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int loopCounter;
        private System.Windows.Threading.DispatcherTimer timer;
        Random rand = new Random();
        Ellipse ellipse = null;
        List<Ellipse> ellList = new List<Ellipse>();
        public const int u = 160;
        Board board = new Board();
        List<Button> buttons = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
            
            buttons.Add(new Button { ButtonContent = board.SquaresList[0].TokenQty.ToString(), EllQty = board.SquaresList[0].TokenQty , ButtonID = "0", X = 0, Y = 0, Height = u*2, Width = u, Radius=50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[1].TokenQty.ToString(), EllQty = board.SquaresList[1].TokenQty, ButtonID = "1", X = u, Y = 0, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[2].TokenQty.ToString(), EllQty = board.SquaresList[2].TokenQty, ButtonID = "2", X = u*2, Y = 0, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[3].TokenQty.ToString(), EllQty = board.SquaresList[3].TokenQty, ButtonID = "3", X = u*3, Y = 0, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[4].TokenQty.ToString(), EllQty = board.SquaresList[4].TokenQty, ButtonID = "4", X = u*4, Y = 0, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[5].TokenQty.ToString(), EllQty = board.SquaresList[5].TokenQty, ButtonID = "5", X = u*5, Y = 0, Height = u, Width = u, Radius = 50 });

            buttons.Add(new Button { ButtonContent = board.SquaresList[6].TokenQty.ToString(), EllQty = board.SquaresList[6].TokenQty, ButtonID = "6", X = u*6, Y = 0, Height = u*2, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[7].TokenQty.ToString(), EllQty = board.SquaresList[7].TokenQty, ButtonID = "7", X = u*5, Y = u, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[8].TokenQty.ToString(), EllQty = board.SquaresList[8].TokenQty, ButtonID = "8", X = u*4, Y = u, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[9].TokenQty.ToString(), EllQty = board.SquaresList[9].TokenQty, ButtonID = "9", X = u*3, Y = u, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[10].TokenQty.ToString(), EllQty = board.SquaresList[10].TokenQty, ButtonID = "10", X = u*2, Y = u, Height = u, Width = u, Radius = 50 });
            buttons.Add(new Button { ButtonContent = board.SquaresList[11].TokenQty.ToString(), EllQty = board.SquaresList[11].TokenQty, ButtonID = "11", X = u, Y = u, Height = u, Width = u, Radius = 50 });
            
            ic.ItemsSource = buttons;

            //Set the new board
            SetNewBoard();

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); //Set the interval period here.
            timer.Tick += timer1_Tick;
        }

        /// <summary>
        /// Set the new board
        /// </summary>
        private void SetNewBoard()
        {
            int n = 0;
            for (int k = 0; k < 2; k++)
            {
                //Set big squares n = 0 and 6
                ellipse = CreateAnEllipse(u / 2, u / 4);
                Canvas.SetLeft(ellipse, rand.Next(buttons[n].X + 60, buttons[n].X + u - 100));
                Canvas.SetTop(ellipse, rand.Next(buttons[n].Y + 120, buttons[n].Y + 2 * u - 160));
                PaintCanvas.Children.Add(ellipse);
                buttons[n].EllList.Add(ellipse);

                //Set small squares
                for (int i = n + 1; i < n + 6; i++)
                {
                    for (int j = 0; j < board.SquaresList[i].TokenQty; j++)
                    {
                        ellipse = CreateAnEllipse(20, 20);
                        Canvas.SetLeft(ellipse, rand.Next(buttons[i].X + 50, buttons[i].X + u - 50));
                        Canvas.SetTop(ellipse, rand.Next(buttons[i].Y + 50, buttons[i].Y + u - 50));
                        PaintCanvas.Children.Add(ellipse);
                        buttons[i].EllList.Add(ellipse);
                    }
                    ellList = buttons[i].EllList;
                }
                n = n + 6;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Remove the previous ellipse from the paint canvas.
            PaintCanvas.Children.Remove(ellipse);
        }

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    loopCounter = 10;
        //    timer.Start();
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (--loopCounter == 0)
                timer.Stop();

            //Add the ellipse to the canvas
            ellipse = CreateAnEllipse(20, 20);
            PaintCanvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, rand.Next(0, 100));
            Canvas.SetTop(ellipse, rand.Next(0, 100));
        }

        // Customize your ellipse in this method
        public Ellipse CreateAnEllipse(int height, int width)
        {
            SolidColorBrush fillBrush = new SolidColorBrush() { Color = Colors.Red };
            SolidColorBrush borderBrush = new SolidColorBrush() { Color = Colors.Black };

            return new Ellipse()
            {
                Height = height,
                Width = width,
                StrokeThickness = 1,
                Stroke = borderBrush,
                Fill = fillBrush
            };
        }
    }

    class Button
    {
        public string ButtonContent { get; set; }
        public string ButtonID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Radius { get; set; }
        public int EllQty { get; set; }
        public List<Ellipse> EllList => new List<Ellipse>();
    }
}
