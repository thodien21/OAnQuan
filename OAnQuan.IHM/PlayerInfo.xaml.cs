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
    /// Interaction logic for PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : Window
    {
        public PlayerInfo()
        {
            InitializeComponent();
            icAllPlayerList.ItemsSource = Services.PlayerListWithRanking;
            //var playerChosenForInfo = Services.PlayerChosenForInfo;
            //grdPlayer.DataContext = playerChosenForInfo;
            //lblOwnRanking.Content = playerChosenForInfo.Ranking + "/" + PlayerDb.CountPlayer();
            //lblIsAdmin.Content = (playerChosenForInfo.IsAdmin == 1) ? "Oui" : "Non";
            //lblIsDisabled.Content = (playerChosenForInfo.IsDisabled == 1) ? "Désactivé" : "Activé";
        }
    }
}
