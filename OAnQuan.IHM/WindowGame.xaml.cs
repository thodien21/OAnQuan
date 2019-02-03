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
    public partial class WindowGame : Window
    {
        Board board = new Board();
        Random ran = new Random();
        public WindowGame()
        {
            InitializeComponent();    
        }

        private void BtnSquare_Click(int i)
        {
            List<int> b = board.CreatNewBoard();


            /*b = board.Go(i, Direction.LEFT);
            RefreshContentButton(b);
            
            //IA joue:
            int ranSquare = 7;

            //la case choisie ne doit pas être vide
            while (b[ranSquare] == 0)
            {
                ranSquare = ran.Next(7, 12);
            }           
            
            Direction ranDirection= (ran.Next(Direction.LEFT, 1) == 1) ? true:false;
            b = board.Go(ranSquare, ranDirection);
            
            RefreshContentButton(b);*/
        }

        private void RefreshContentButton(List<int> b)
        {
            
            btn0.Content = b[0];
            
            //Thread.Sleep(100);
            btn1.Content = b[1];           
            btn1.BorderBrush = Brushes.GreenYellow;
            
            //Thread.Sleep(100);
            btn2.Content = b[2];
            //Thread.Sleep(100);
            btn3.Content = b[3];
            //Thread.Sleep(100);
            btn4.Content = b[4];
            //Thread.Sleep(100);
            btn5.Content = b[5];
            //Thread.Sleep(100);
            btn6.Content = b[6];
            //Thread.Sleep(100);
            btn7.Content = b[7];
            //Thread.Sleep(100);
            btn8.Content = b[8];
            //Thread.Sleep(100);
            btn9.Content = b[9];
            btn10.Content = b[10];
            //Thread.Sleep(100);
            btn11.Content = b[11];
        }
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            BtnSquare_Click(1);
        }
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            BtnSquare_Click(2);
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            BtnSquare_Click(3);
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            BtnSquare_Click(4);
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            BtnSquare_Click(5);
        }
    }
}
