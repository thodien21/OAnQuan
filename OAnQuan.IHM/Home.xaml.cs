using OAnQuan.Business;
using OAnQuan.DataAccess;
using System.Collections.Generic;
using System.Windows;

namespace OAnQuan.IHM
{
    /// <summary>
    /// Logique d'interaction pour WindowMainMenu.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();

            //Display the best players
            icBestPlayerList.ItemsSource = PlayerDb.GetRanking(5);
            txbWelcom.Text = PlayerDb.GetPlayer();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PlayGame game = new PlayGame();
            game.ShowDialog();
        }
    }
}
