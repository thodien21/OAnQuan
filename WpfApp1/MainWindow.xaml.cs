using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

            TextBlock.Text = "Turn of " + board.Turn;
            Storyboard story = new Storyboard();
            foreach (var item in btnList)
            {
                // Animate the button background color when it's clicked.
                item.Click += delegate (object sender, RoutedEventArgs args)
                {
                    if ((board.SquaresList[btnList.IndexOf(item)].PlayerNumber != board.Turn || board.SquaresList[btnList.IndexOf(item)].TokenQty == 0) && board.ClickedSquares.Count == 0)
                    {
                        MessageBox.Show("La case choisie doit être non vide et se situer dans la rangée de joueur qui a le tour");
                    }
                    else
                    {
                        TextBlock.Text = "Turn of " + board.Turn;
                        board.ClickedSquares.Add(btnList.IndexOf(item));
                        AnimateWhenClicked(item);
                        if (board.ClickedSquares.Count == 1)
                        {
                            AnimateWhenClickedFirstSquare(btnList[(btnList.IndexOf(item) + 1)%12], story);
                            AnimateWhenClickedFirstSquare(btnList[(btnList.IndexOf(item) + 11)%12], story);
                        }
                        if (board.ClickedSquares.Count == 2)
                        {
                            Go();
                            story.Stop();//stop animation of neighbor squares
                            story.Children.Clear();//clear storyboard of neighbor squares
                        }
                    }
                };
            }
        }
        
        /// <summary>
        /// Button will have a green and thicker border when clicked
        /// </summary>
        /// <param name="button"></param>
        public void AnimateWhenClicked(Button button)
        {
            Storyboard story = new Storyboard();

            ColorAnimation anim = new ColorAnimation();
            anim.From = Colors.Gray;
            anim.To = Colors.Green;
            anim.BeginTime = TimeSpan.FromSeconds(0);
            anim.Duration = new Duration(TimeSpan.FromSeconds(1));
            anim.AutoReverse = true;
            story.Children.Add(anim);
            Storyboard.SetTarget(anim, button);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(Button.BorderBrush).(SolidColorBrush.Color)"));

            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
            thicknessAnimation.From = new Thickness(1);
            thicknessAnimation.To = new Thickness(5);
            thicknessAnimation.BeginTime = TimeSpan.FromSeconds(0);
            thicknessAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            thicknessAnimation.AutoReverse = true;
            story.Children.Add(thicknessAnimation);
            Storyboard.SetTarget(thicknessAnimation, button);
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("(Button.BorderThickness)"));

            story.Begin();
        }

        /// <summary>
        /// Animate the neighbor squares still one of them is chosen
        /// </summary>
        /// <param name="button">button</param>
        /// <param name="story">storyboard</param>
        public void AnimateWhenClickedFirstSquare(Button button, Storyboard story)
        {
            ColorAnimation anim = new ColorAnimation();
            anim.From = Colors.Gray;
            anim.To = Colors.Green;
            story.Children.Add(anim);
            Storyboard.SetTarget(anim, button);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(Button.BorderBrush).(SolidColorBrush.Color)"));

            ThicknessAnimation thicknessAnimation = new ThicknessAnimation();
            thicknessAnimation.From = new Thickness(1);
            thicknessAnimation.To = new Thickness(5);
            story.Children.Add(thicknessAnimation);
            Storyboard.SetTarget(thicknessAnimation, button);
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("(Button.BorderThickness)"));

            story.Begin();
        }
        
        /// <summary>
        /// Visualize the board
        /// </summary>
        private void SetBoard()
        {
            //clear the canvas before beginning the game
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

        private void UpdateBoard()
        {
            
            int n = 0;
            for (int k = 0; k < 2; k++)
            {
                //Update big squares
                int diffBigSquare = canList[n].Children.Count - board.SquaresList[n].TokenQty;
                if (board.SquaresList[n].TokenQty ==0 )
                    canList[n].Children.RemoveRange(1, canList[n].Children.Count);
                else
                {
                    if (board.SquaresList[n].Tokens.FirstOrDefault(s => s.GetType().Equals(typeof(BigToken))) == null)
                    {
                        Diff(diffBigSquare, n);
                    }
                    else
                    {
                        Diff(diffBigSquare - 1, n);//because there is one big token
                    }
                }
                
                //Update small squares
                for (int i=1+n; i<6+n; i++)
                {
                    if (board.SquaresList[i].TokenQty == 0)
                        canList[i].Children.RemoveRange(1, canList[i].Children.Count);
                    else
                    {
                        int diffSmallSquare = canList[i + n].Children.Count - board.SquaresList[i + n].TokenQty;
                        Diff(diffSmallSquare, i);
                    }
                }
                n = n + 6;
            }

            //Display the quantity of tokens in each square(button)
            for (int i = 0; i < 12; i++)
            {
                btnList[i].Content = board.SquaresList[i].TokenQty;
            }
        }

        public void Diff(int diff, int n)
        {
            if (diff > 0)
            {
                for (int j = 0; j < diff; j++)
                {
                    canList[n].Children.Remove(ellipse);//remove small tokens
                }
            }
            else if (diff < 0)
            {
                if(n==0 || n==6)
                {

                }
                for (int j = 0; j < -diff; j++)
                {
                    canList[n].Children.Add(ellipse);//add small tokens
                }
            }
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
                    Canvas.SetLeft(ellipse, rand.Next(u * (12 - i) + 50, u * (12 - i) + u - 50));
                    Canvas.SetTop(ellipse, rand.Next(50, u - 50));
                    canList[i].Children.Add(ellipse);
                }
            }
        }

        /// <summary>
        /// Diffuse the tokens
        /// </summary>
        public void Go()
        {
            if (board.ClickedSquares[0] != 0 & board.ClickedSquares[0] != 6)
            {
                if(board.ClickedSquares[0]==11 && board.ClickedSquares[1]==0)
                {
                    board.SmallGo(board.Turn, board.ClickedSquares[0], Direction.RIGHT);
                    TextBlock2.Text = "Chosen square is " + board.ClickedSquares[0] + "\nDirection is right\n" + "next player turn " + board.Turn;
                }
                else if (board.ClickedSquares[0] - board.ClickedSquares[1] == -1)
                {
                    board.SmallGo(board.Turn, board.ClickedSquares[0], Direction.RIGHT);
                    TextBlock2.Text = "Chosen square is " + board.ClickedSquares[0] + "\nDirection is right\n" + "next player turn " + board.Turn;
                }
                else if (board.ClickedSquares[0] - board.ClickedSquares[1] == 1)
                {
                    board.SmallGo(board.Turn, board.ClickedSquares[0], Direction.LEFT);
                    TextBlock2.Text = "Chosen square is " + board.ClickedSquares[0] + "\nDirection is left\n" + "next player turn " + board.Turn;
                }
                else MessageBox.Show("Vous ne pouvez choisir que 2 cases de suite");
                UpdateBoard();
            }
            board.ClickedSquares.Clear();//Clear the list of clicked squares after each turn
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
        //private void button0_Click(object sender, RoutedEventArgs e)
        //{
        //    if(board.ClickedSquares.Count == 0)
        //    {
        //        MessageBox.Show("Il faut d'abord choisir une case de votre rangé !");
        //    }
        //    else
        //    {
        //        board.ClickedSquares.Add(0);
        //        Go();
        //    }
        //}

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(1);
        //    if(board.ClickedSquares.Count==2)
        //    {
        //        Go();
        //    }
        //}

        //private void button2_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(2);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}

        //private void button3_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(3);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    };
        //}

        //private void button4_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(4);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}

        //private void button5_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(5);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}

        //private void button6_Click(object sender, RoutedEventArgs e)
        //{
        //    if (board.ClickedSquares.Count == 0)
        //    {
        //        MessageBox.Show("Il faut d'abord choisir une case de votre rangé !");
        //    }
        //    else
        //    {
        //        board.ClickedSquares.Add(6);
        //        Go();
        //    }
        //}

        //private void button7_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(7);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}

        //private void button8_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(8);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}

        //private void button9_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(9);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}

        //private void button10_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(10);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}

        //private void button11_Click(object sender, RoutedEventArgs e)
        //{
        //    board.ClickedSquares.Add(11);
        //    if (board.ClickedSquares.Count == 2)
        //    {
        //        Go();
        //    }
        //}
        #endregion
    }
}
