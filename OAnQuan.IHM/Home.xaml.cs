using OAnQuan.Business;
using OAnQuan.DataAccess;
using System;
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
            icBestPlayerList.ItemsSource= PlayerDb.GetRanking(5);
            txbWelcome.Text = "Bienvenu "+ Services.Player.Pseudo + " !";

            //Display info of player
            tbiPlayer.DataContext = Services.Player;
            lblOwnRanking.Content = Services.Player.Ranking + "/" + Services.PlayerQty;
            lblIsAdmin.Content = (Services.Player.IsAdmin == 1) ? "Oui" : "Non";
            lblIsDisabled.Content = (Services.Player.IsDisabled == 1) ? "Désactivé" : "Activé";

            //Administration
            if(Services.Player.IsAdmin != 1)
            {
                btnAdmin.Visibility = Visibility;//Hide this tab since player is not admin
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if(Services.Player.IsDisabled == 1)
            {
                MessageBox.Show("Votre compte est désactivé, vous ne pouvez plus jouer...");
            }
            else
            {
                this.Hide();
                PlayGame game = new PlayGame();
                game.ShowDialog();
            }
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.Show();
        }
    }
}
