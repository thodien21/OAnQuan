using NUnit.Framework;
using OAnQuan.Business;
using OAnQuan.DataAccess;
using System.Collections.Generic;

namespace OAnQuan.Test.DataAccess
{
    [TestFixture]
    public static class PlayerDbTest
    {
        [Test]
        public static void GetAllPlayer_Test()
        {
            List<Player> playerList = PlayerDb.GetAllPlayer();
            Assert.AreEqual(playerList.Count, 4);

            Assert.AreEqual(playerList[0].Pseudo, "");
            Assert.AreEqual(playerList[1].Pseudo, "a");
            Assert.AreEqual(playerList[2].Pseudo, "s");
            Assert.AreEqual(playerList[3].Pseudo, "c");
        }
    }
}
