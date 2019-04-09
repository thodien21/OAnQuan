﻿using OAnQuan.Business;
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
            ThisPlayer.FullName= nametextBox.Text;
            ThisPlayer.IsEnabled = (cobEnabled.Text == "Oui")? 1: 0;
            ThisPlayer.IsAdmin = (cobAdmin.Text == "Oui")? 1: 0;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            PlayerDb.UpdatePlayerDb(ThisPlayer);
            PlayerInfo.dataGrid.ItemsSource = Services.PlayerListWithRanking;
        }
    }
}
