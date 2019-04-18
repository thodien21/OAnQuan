using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WGame
{
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

            //Add buttons in a list
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

            //Add canlists in a list
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

            //Initialize the board at the beginning of the game
            SetBoard();
            
            TextBlock.Text = "Turn of " + board.Turn;
            Storyboard story = new Storyboard();
            foreach (var item in btnList)
            {
                
                // Animate the color and thickness of button's border when it's clicked.
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
                            board.ClickedSquares.Clear();//Clear the list of clicked squares after each go
                            
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
                    AddBigToken(n);
                }
                //Set big squares with small tokens
                AddSmallTokenInBigSquare(n, board.SquaresList[n].TokenQty, bigTokenQty);
                n = n + 6;
            }

            //Set up small squares
            for (int i = 1; i < 6; i++)
            {
                AddTokensInFirstRang(i, board.SquaresList[i].TokenQty);
            }
            for (int i = 7; i < 12; i++)
            {
                AddTokensInSecondRang(i, board.SquaresList[i].TokenQty);
            }

            //Display the quantity of tokens in each square(button)
            for(int i=0; i<12; i++)
            {
                btnList[i].Content = board.SquaresList[i].TokenQty;
            }
        }

        public void AddBigToken(int squareIndex)
        {
            //loopCounter = 10;
            //timer.Start();
            ellipse = CreateAnEllipse(u / 2, u / 4);
            Canvas.SetLeft(ellipse, rand.Next(60 + squareIndex * u, 60 + squareIndex * u));
            Canvas.SetTop(ellipse, rand.Next(120, 2 * u - 160));
            canList[squareIndex].Children.Add(ellipse);
            btnList[squareIndex].Content = board.SquaresList[squareIndex].TokenQty;
            
        }

        public void AddSmallTokenInBigSquare(int squareIndex, int smallTokenQty, int bigTokenQty)
        {
            for (int j = 0; j < smallTokenQty - bigTokenQty; j++)
            {
                ellipse = CreateAnEllipse(20, 20);
                Canvas.SetLeft(ellipse, rand.Next(60 + squareIndex * u, 60 + squareIndex * u));
                Canvas.SetTop(ellipse, rand.Next(120, 2 * u - 160));
                canList[squareIndex].Children.Add(ellipse);
            }
        }

        /// <summary>
        /// Where squareIndex=1-5
        /// </summary>
        /// <param name="squareIndex"></param>
        public void AddTokensInFirstRang(int squareIndex, int tokenQty)
        {
            for (int j = 0; j < tokenQty; j++)
            {
                ellipse = CreateAnEllipse(20, 20);
                Canvas.SetLeft(ellipse, rand.Next(u * squareIndex + 50, u * squareIndex + u - 50));
                Canvas.SetTop(ellipse, rand.Next(u + 50, 2 * u - 50));
                canList[squareIndex].Children.Add(ellipse);
            }
        }

        /// <summary>
        /// Where squareIndex=7-11
        /// </summary>
        /// <param name="squareIndex"></param>
        public void AddTokensInSecondRang(int squareIndex, int tokenQty)
        {
            for (int j = 0; j < tokenQty; j++)
            {
                ellipse = CreateAnEllipse(20, 20);
                Canvas.SetLeft(ellipse, rand.Next(u * (12 - squareIndex) + 50, u * (12 - squareIndex) + u - 50));
                Canvas.SetTop(ellipse, rand.Next(50, u - 50));
                canList[squareIndex].Children.Add(ellipse);
            }
        }

        private void UpdateBoard()
        {
            int n = 0;
            for (int k = 0; k < 2; k++)
            {
                //Update big squares
                int diffTokenQtyInBigSquare = canList[n].Children.Count - board.SquaresList[n].TokenQty;
                if (board.SquaresList[n].TokenQty ==0 )
                    canList[n].Children.RemoveRange(1, canList[n].Children.Count);
                else
                {
                    if (board.SquaresList[n].Tokens.FirstOrDefault(s => s.GetType().Equals(typeof(BigToken))) == null)
                    {
                        DiffTokenQty(n, diffTokenQtyInBigSquare);
                    }
                    else
                    {
                        DiffTokenQty(n, diffTokenQtyInBigSquare - 1);//because there is one big token
                    }
                }
                
                //Update small squares
                for (int i=1+n; i<6+n; i++)
                {
                    if (board.SquaresList[i].TokenQty == 0)
                        canList[i].Children.RemoveRange(1, canList[i].Children.Count);
                    else
                    {
                        int diffSmallSquare = canList[i].Children.Count - board.SquaresList[i].TokenQty;
                        DiffTokenQty(i, diffSmallSquare);
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

        /// <summary>
        /// diffTokenQty = canList[n].Children.Count - board.SquaresList[n].TokenQty
        /// </summary>
        /// <param name="diff"></param>
        /// <param name="squareIndex"></param>
        public void DiffTokenQty(int squareIndex, int diffTokenQty)
        {
            if (diffTokenQty > 0)
            {
                for (int j = 0; j < diffTokenQty; j++)
                {
                    canList[squareIndex].Children.Remove(ellipse);//remove small tokens
                }
            }
            else if (diffTokenQty < 0)
            {
                if (squareIndex == 0 || squareIndex == 6)
                {
                    AddSmallTokenInBigSquare(squareIndex, diffTokenQty, 0);
                }
                else if (squareIndex >= 1 && squareIndex <= 5)
                    AddTokensInFirstRang(squareIndex, -diffTokenQty);
                else if (squareIndex >= 7 && squareIndex <= 11)
                    AddTokensInSecondRang(squareIndex, -diffTokenQty);
            }
            btnList[squareIndex].Content = board.SquaresList[squareIndex].TokenQty;
        }

        /// <summary>
        /// Diffuse the tokens
        /// </summary>
        public void Go()
        {
            int squareId = board.ClickedSquares[0];
            int nextSquareId = board.ClickedSquares[1];
            Direction direction = Direction.UNKNOW;
            while (squareId != 0 & squareId != 6 && board.SquaresList[squareId].TokenQty != 0)
            {
                if ((squareId == 11 && nextSquareId == 0) || (squareId - nextSquareId == -1))
                    direction = Direction.RIGHT;
                else if (squareId - nextSquareId == 1)
                    direction = Direction.LEFT;
                else
                {
                    MessageBox.Show("Vous ne pouvez choisir que 2 cases de suite");
                    break;
                }
                if(direction == Direction.LEFT || direction == Direction.RIGHT)
                {
                    squareId = board.SmallStep(board.Turn, squareId, direction);//play small step and get squareId of next small step
                    TextBlock2.Text = "Chosen square is " + squareId + "\nDirection is " + direction + "\nNext player turn " + board.Turn;
                    UpdateBoard();//Update the board after each small step
                    nextSquareId = (direction == Direction.RIGHT) ? (squareId + 1) % 12 : squareId - 1;
                }
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
    }
}
