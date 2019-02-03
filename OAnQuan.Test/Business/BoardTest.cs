using NUnit.Framework;
using OAnQuan.Business;
using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Test.Business
{
    [TestFixture]
    public static class BoardTest
    {
        [Test]
        public static void BoardContructor_Test()
        {
            var board = new Board();

            Assert.That(board, Is.Not.Null);
            Assert.That(board.Squares, Is.Not.Null);
            Assert.That(board.Squares.Count == 12);
            Assert.That(board.Squares.All(s => s == 5));
        }
    }
}
