using OAnQuan.DataAccess;
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
            if (PlayerDb.IsThisPlayerExist(txbPeuso.Text, txbPassword.Password) == false)
            {
                MessageBox.Show("Pseudo or password is false, plase try again!");
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
