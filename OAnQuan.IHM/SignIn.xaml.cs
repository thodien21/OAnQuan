﻿using OAnQuan.DataAccess;
using System.Windows;
using OAnQuan.Business;

namespace OAnQuan.IHM
{
    /// <summary>
    /// Logique d'interaction pour SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }
        
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Verify if this pseudo already exists
            if (PlayerDb.GetPlayer(txbPeuso.Text, txbPassword.Password) == null)
            {
                MessageBox.Show("Pseudo ou Mot de passe ne sont pas corrects. Réessayez !");
                this.Hide();
                SignIn signIn = new SignIn();
                signIn.ShowDialog();
            }
            else
            {
                this.Hide();
                Home click = new Home();
                click.ShowDialog();
            }
        }
    }
}