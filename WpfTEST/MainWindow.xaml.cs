using System;
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

        public MainWindow()
        {
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); //Set the interval period here.
            timer.Tick += timer1_Tick;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            loopCounter = 10;
            timer.Start();
        }

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
}
