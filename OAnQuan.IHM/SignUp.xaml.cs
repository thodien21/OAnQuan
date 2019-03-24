using OAnQuan.Business;
using OAnQuan.DataAccess;
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
            List<Player> allPlayer = PlayerDb.GetAllPlayer(2);
            var Player1 = allPlayer.FirstOrDefault(s => s.Pseudo == txbPeuso.Text);
            if (Player1 != null)
            {
                MessageBox.Show("Ce pseudo existe déjà, veuillez choisir un autre :");
                this.Hide();
                SignUp signUp = new SignUp();
                signUp.ShowDialog();
            }
            else
            {
                this.Hide();
                PlayerDb.InsertPlayer(txbPeuso.Text, txbPassword.Password, txbFullName.Text);
                Home click = new Home();
                click.ShowDialog();
            }
        }
    }
}
