using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SQLite;
using OAnQuan;
using OAnQuan.Business;
using OAnQuan.DataAccess;

namespace OAnQuan.IHM
{
    /// <summary>
    /// Logique d'interaction pour WindowSignUp.xaml
    /// </summary>
    public partial class WindowSignUp : Window
    {
        public WindowSignUp()
        {
            InitializeComponent();
        }

        private void BtnCreatAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PlayerDb d = new PlayerDb();
            /*List<string> displayListPlayer = new List<string>();
            displayListPlayer = d.InsertPlayer(txbPeuso.Text, txbPassword.Text);
            displayListPlayer = d.DisplayPlayerList();
            foreach (var item in displayListPlayer)
            {
                MessageBox.Show("Votre nouveau compte a été créé!");
            }*/
            d.InsertPlayer(txbPeuso.Text, txbPassword.Password);
            WindowMainMenu click = new WindowMainMenu();
            click.ShowDialog();
        }
    }
}
