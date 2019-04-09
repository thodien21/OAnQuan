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

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(icAllPlayerList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Pseudo");
            view.GroupDescriptions.Add(groupDescription);
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.Show();
        }
    }
}
