using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WGame
{
    //class Button
    //{
    //    public string ButtonContent { get; set; }
    //    public string ButtonID { get; set; }
    //    public int X { get; set; }
    //    public int Y { get; set; }
    //    public int Height { get; set; }
    //    public int Width { get; set; }
    //    public int Radius { get; set; }
    //    public int EllQty { get; set; }
    //    public List<Ellipse> EllList => new List<Ellipse>();
    //    public string CanvasName { get; set; }
    //}
    public partial class MainWindow : Window
    {
        int loopCounter;
        private System.Windows.Threading.DispatcherTimer timer;
        Random rand = new Random();
        Ellipse ellipse = null;
        List<Button> btnList = new List<Button>();
        List<Canvas> canList = new List<Canvas>();
        Board board = new Board();
        public const int u = 160;
        public MainWindow()
        {
            InitializeComponent();
            btnList.Add(button0);
            btnList.Add(button1);
            btnList.Add(button2);
            btnList.Add(button3);
            btnList.Add(button4);
            btnList.Add(button5);
            btnList.Add(button6);
            btnList.Add(button7);
            btnList.Add(button8);
            btnList.Add(button9);
            btnList.Add(button10);
            btnList.Add(button11);

            canList.Add(canvas0);
            canList.Add(canvas1);
            canList.Add(canvas2);
            canList.Add(canvas3);
            canList.Add(canvas4);
            canList.Add(canvas5);
            canList.Add(canvas6);
            canList.Add(canvas7);
            canList.Add(canvas8);
            canList.Add(canvas9);
            canList.Add(canvas10);
            canList.Add(canvas11);

            SetBoard();
            TextBlock.Text = "Turn of "+board.Turn;
        }

        /// <summary>
        /// Visualize the board
        /// </summary>
        private void SetBoard()
        {
            //clear the canvas before set up
            foreach(var item in canList)
            {
                item.Children.RemoveRange(1, item.Children.Count);
            }

            //Set up big squares
            int n = 0;
            int bigTokenQty = 0;
            for (int k = 0; k < 2; k++)
            {
                if (board.SquaresList[n].Tokens.FirstOrDefault(s=>s.GetType().Equals(typeof(BigToken))) != null)
                {
                    bigTokenQty = 1;
                    ellipse = CreateAnEllipse(u / 2, u / 4);
                    Canvas.SetLeft(ellipse, rand.Next(60 + n * u, 60 + n * u));
                    Canvas.SetTop(ellipse, rand.Next(120, 2 * u - 160));
                    canList[n].Children.Add(ellipse);
                    btnList[n].Content = board.SquaresList[n].TokenQty;
                }
                //Set big squares with small tokens
                for(int j=0; j< board.SquaresList[n].TokenQty - bigTokenQty; j++)
                {
                    ellipse = CreateAnEllipse(20, 20);
                    Canvas.SetLeft(ellipse, rand.Next(60 + n * u, 60 + n * u));
                    Canvas.SetTop(ellipse, rand.Next(120, 2 * u - 160));
                    canList[n].Children.Add(ellipse);
                }
                n = n + 6;
            }

            //Set up small squares
            for (int i = 1; i < 6; i++)
            {
                for (int j = 0; j < board.SquaresList[i].TokenQty; j++)
                {
                    ellipse = CreateAnEllipse(20, 20);
                    Canvas.SetLeft(ellipse, rand.Next(u * i + 50, u * i + u - 50));
                    Canvas.SetTop(ellipse, rand.Next(u + 50, 2 * u - 50));
                    canList[i].Children.Add(ellipse);
                }
            }
            for (int i = 7; i < 12; i++)
            {
                for (int j = 0; j < board.SquaresList[i].TokenQty; j++)
                {
                    ellipse = CreateAnEllipse(20, 20);
                    Canvas.SetLeft(ellipse, rand.Next(u * (12-i) + 50, u * (12 - i) + u - 50));
                    Canvas.SetTop(ellipse, rand.Next(50, u - 50));
                    canList[i].Children.Add(ellipse);
                }
            }

            //Display the quantity of tokens in each square(button)
            for(int i=0; i<12; i++)
            {
                btnList[i].Content = board.SquaresList[i].TokenQty;
            }
        }

        public void Go()
        {
            if (board.ClickedSquares[0] != 0 & board.ClickedSquares[0] != 6)
            {
                if (board.ClickedSquares[0] < board.ClickedSquares[1])
                {
                    board.Go(board.Turn, board.ClickedSquares[0], Direction.RIGHT);
                    TextBlock2.Text = "Chosen square is "+ board.ClickedSquares[0]+ "\nDirection is right\n" + "next player turn " + board.Turn;
                }
                else
                {
                    board.Go(board.Turn, board.ClickedSquares[0], Direction.LEFT);
                    TextBlock2.Text = "Chosen square is " + board.ClickedSquares[0] + "\nDirection is left\n" + "next player turn " + board.Turn;
                }
                SetBoard();
            }
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

#region button click
        private void button0_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(0);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(1);
            if(board.ClickedSquares.Count==2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(2);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(3);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            };
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(4);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(5);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(6);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(7);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(8);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(9);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(10);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            board.ClickedSquares.Add(11);
            if (board.ClickedSquares.Count == 2)
            {
                Go();
                board.ClickedSquares.Clear();
            }
        }
        #endregion
    }
}
