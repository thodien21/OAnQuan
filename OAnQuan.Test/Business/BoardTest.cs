using NUnit.Framework;
using OAnQuan.Business;
using System.Collections.Generic;

namespace OAnQuan.Test.Business
{
    [TestFixture]
    public static class BoardTest
    {
        [Test]
        public static void BoardContructor_Test()
        {
            List<int> squares = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 11, 12 };

            var board = new Board(squares);

            Assert.That(board, Is.Not.Null);
            Assert.That(board.Squares.Equals(squares));
            Assert.AreEqual(squares, board.Squares);

            //Tests of default constructor
            Board board0 = new Board();
            List<int> boardSquares = board0.CreatNewBoard();
            Assert.AreEqual(boardSquares, board0.Squares);
        }
    }
}
