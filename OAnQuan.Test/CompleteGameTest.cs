using NUnit.Framework;
using OAnQuan.Business;
using System;
using System.Collections.Generic;

namespace OAnQuan.Test
{
    [TestFixture]
    public static class CompleteGameTest
    {
        [Test]
        public static void SimpleGame_Test()
        {
            // Setup game
            List<int> mySquares = new List<int>() { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            Board board = new Board();
            board.CreatNewBoard();
            Assert.AreEqual(board.Squares, mySquares);

            // First turn
            int gainedTokens = board.Go(2, Direction.RIGHT);
            Assert.AreEqual(gainedTokens, 6);
            List<int> mySquares2RIGHT = new List<int>() { 6, 6, 0, 0, 6, 6, 6, 6, 0, 6, 6, 6 };
            Assert.AreEqual(board.Squares, mySquares2RIGHT);
           
            // Second Turn
            //Assert.That(board.Go(3, Direction.LEFT), Is.Not.Null);
        }

        [Test]
        public static void ExceptionTest()
        {
            //var expectedMessage = "The square selecteed is not valide: it should not be the big square or empty";
            var expectedMessage = "It should not be the big square or empty";
            Board board = new Board();
            board.CreatNewBoard();
            List<int> squares = board.Squares; //= { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5};
            Assert.AreEqual(squares, new List<int>() { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 });


            //Exception null  beacause squares[2] != 0:
            string exception = null;
            try
            {
                board.Go(2, Direction.RIGHT);
            }
            catch(ArgumentOutOfRangeException e)
            {
                exception = e.Message;
            }
            Assert.That(exception, Is.Null);
            squares = board.Squares;
            Assert.AreEqual(squares, new List<int>() { 6, 6, 0, 0, 6, 6, 6, 6, 0, 6, 6, 6 });


            //Exception not null with squaredId = 0 or 6:
            try
            {
                board.Go(0, Direction.RIGHT);
            }
            catch (ArgumentOutOfRangeException e)
            {
                exception = e.Message;
            }
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Contains(expectedMessage));

            squares = board.Squares;
            Assert.AreEqual(squares, new List<int>() { 6, 6, 0, 0, 6, 6, 6, 6, 0, 6, 6, 6 });//squares don't change because of exception


            //Exception not null with squared is empty:
            try
            {
                board.Go(6, Direction.RIGHT);//test with 2, 3, 6
            }
            catch (ArgumentOutOfRangeException e)
            {
                exception = e.Message;
            }
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Contains(expectedMessage));
            squares = board.Squares;
            Assert.AreEqual(squares, new List<int>() { 6, 6, 0, 0, 6, 6, 6, 6, 0, 6, 6, 6 });//squares don't change because of exception
        }
    }
}
