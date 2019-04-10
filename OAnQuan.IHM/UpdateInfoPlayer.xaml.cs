using OAnQuan.Business;
using OAnQuan.DataAccess;
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

namespace OAnQuan.IHM
{
    /// <summary>
    /// Logique d'interaction pour UpdateInfoPlayer.xaml
    /// </summary>
    public partial class UpdateInfoPlayer : Window
    {
        Player ThisPlayer;
        public UpdateInfoPlayer(Player player)
        {
            InitializeComponent();
            ThisPlayer = player;
            tblPlayer.Text = ThisPlayer.Pseudo;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ThisPlayer.FullName = txbFullName.Text;
            ThisPlayer.IsEnabled = (cobEnabled.Text == "Oui") ? 1 : 0;
            ThisPlayer.IsAdmin = (cobAdmin.Text == "Oui") ? 1 : 0;
            
            PlayerDb.UpdatePlayerDb(ThisPlayer);
            PlayerInfo.dataGrid.ItemsSource = Services.PlayerListWithRanking;//to save in data base
            MessageBox.Show("save!");
        }
    }
}
