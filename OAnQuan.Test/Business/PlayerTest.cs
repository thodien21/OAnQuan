using NUnit.Framework;
using OAnQuan.Business;

namespace OAnQuan.Test.Business
{
    [TestFixture]
    public static class PlayerTest
    {
		[Test]
		public static void Constructor_Test()
        {
            string pseudo = "PseudoTest";
            string password = "PasswordTest";

            var player = new Player(pseudo, password);
			
            Assert.That(player, Is.Not.Null);
            Assert.That(player.Pseudo.Equals(pseudo));
            Assert.That(player.Password.Equals(password));
            Assert.AreEqual(player.Pool.Count, 0);
        }
    }
}
