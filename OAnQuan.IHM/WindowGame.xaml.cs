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
        Player player1 = new Player("thodien");
        Player player2 = new Player("maytinh");

        public WindowGame()
        {
            InitializeComponent();
        }

        private void BtnSquare_Click(int i)
        {            
            var listSquare = board.Go(board.PlayersList[0], i, Direction.LEFT);
            board.PlayersList[0]=player1;
            board.PlayersList[1]=player2;

            //IA joue:
            int ranSquare = 7;

            //la case choisie ne doit pas être vide
            while (listSquare[ranSquare].Tokens.Count == 0)
            {
                ranSquare = ran.Next(7, 12);
            }           
            
            Direction ranDirection= (ran.Next(0, 2) == 1) ? Direction.RIGHT:Direction.LEFT;
            listSquare = board.Go(player1, ranSquare, ranDirection);
            
            RefreshContentButton(listSquare);
        }

        private void RefreshContentButton(List<Square> listSquare)
        {
            
            btn0.Content = listSquare[0];
            
            //Thread.Sleep(100);
            btn1.Content = listSquare[1];           
            btn1.BorderBrush = Brushes.GreenYellow;
            
            //Thread.Sleep(100);
            btn2.Content = listSquare[2];
            //Thread.Sleep(100);
            btn3.Content = listSquare[3];
            //Thread.Sleep(100);
            btn4.Content = listSquare[4];
            //Thread.Sleep(100);
            btn5.Content = listSquare[5];
            //Thread.Sleep(100);
            btn6.Content = listSquare[6];
            //Thread.Sleep(100);
            btn7.Content = listSquare[7];
            //Thread.Sleep(100);
            btn8.Content = listSquare[8];
            //Thread.Sleep(100);
            btn9.Content = listSquare[9];
            btn10.Content = listSquare[10];
            //Thread.Sleep(100);
            btn11.Content = listSquare[11];
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
