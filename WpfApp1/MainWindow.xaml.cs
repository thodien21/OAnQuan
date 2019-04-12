using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WGame
{
    public partial class MainWindow : Window
    {
        int loopCounter;
        private System.Windows.Threading.DispatcherTimer timer;
        Random rand = new Random();
        Ellipse ellipse = null;
        int TokenQty = 3;
        public MainWindow()
        {
            InitializeComponent();
            button1.Content = TokenQty;
            for(int i=0; i<TokenQty; i++)
            {
                ellipse = CreateAnEllipse(20, 20);
                PaintCanvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, rand.Next(0, 100));
                Canvas.SetTop(ellipse, rand.Next(0, 100));
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            loopCounter = 10;
            timer.Start();
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
