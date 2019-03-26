using OAnQuan.Business;
using OAnQuan.DataAccess;
using OAnQuan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace OAnQuan.IHM
{
    /// <summary>
    /// Logique d'interaction pour WindowSignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void BtnCreatAccount_Click(object sender, RoutedEventArgs e)
        {
            //Verify if this pseudo already exists
            List<Player> allPlayer = PlayerDb.GetAllPlayer();
            var player1 = allPlayer.FirstOrDefault(s => s.Pseudo == txbPeuso.Text);

            var string1 = txbPassword.Password;
            var string2 = txbPasswordConfirmed.Password;

            if (player1 != null)
            {
                MessageBox.Show("Ce pseudo existe déjà, veuillez choisir un autre :");
                this.Hide();
                SignUp signUp = new SignUp();
                signUp.ShowDialog();
            }
            else
            {
                if(txbPasswordConfirmed.Password == txbPassword.Password)
                {
                    this.Hide();
                    PlayerDb.InsertPlayer(txbPeuso.Text, txbPassword.Password, txbFullName.Text);
                    Services.Player = PlayerDb.GetPlayer(txbPeuso.Text, txbPassword.Password);
                    Home click = new Home();
                    click.ShowDialog();
                }
                else MessageBox.Show("Le mot de passe confirmé n'est pas correct");
            }
        }
    }
}
