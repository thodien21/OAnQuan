using System.Windows;

namespace OAnQuan.IHM
{
    /// <summary>
    /// Logique d'interaction pour WindowMainMenu.xaml
    /// </summary>
    public partial class WindowMainMenu : Window
    {
        public WindowMainMenu()
        {
            InitializeComponent();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WindowGame game = new WindowGame();
            game.ShowDialog();
        }
    }
}
