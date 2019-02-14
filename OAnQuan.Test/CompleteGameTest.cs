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
            Player player1 = new Player("Player1", "Player1PassWord");
            
            Player player2 = new Player("Player2", "Player2PassWord");
            BigSquare bigSquare = new BigSquare();

            //setup board
            Board board = new Board();
            
            var mySquares = board.SquaresList;
            for(int i=1; i<=5; i++)
            {
                mySquares[i+6].Player = player2;
                mySquares[i].Player = player1;
            }
            board.FirstPlayer = player1;
            board.SecondPlayer = player2;
            Assert.AreEqual(mySquares.Count, 12);
            Assert.That(mySquares[2].Player, Is.Not.Null);
            Assert.That(mySquares[2].Player.Equals(player1));

            for (int i=1; i<=5; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, 5);
                Assert.AreEqual(mySquares[i+6].Tokens.Count, 5);
            }
            Assert.AreEqual(mySquares[0].Tokens.Count, 1);
            Assert.AreEqual(mySquares[6].Tokens.Count, 1);

            Assert.AreEqual(player1.Pool.Count, 0);
            Assert.That(mySquares[2].Player.Equals(player1));

            // First turn
            board.Go(player1, 1, Direction.RIGHT);
            
            Assert.AreEqual(player1.Pool.Count, 6);           
            Assert.AreEqual(player2.Pool.Count, 0);

            Assert.AreEqual(mySquares[0].Tokens.Count, 2);

            Assert.AreEqual(mySquares[1].Tokens.Count, 0);
            Assert.AreEqual(mySquares[2].Tokens.Count, 0);
            Assert.AreEqual(mySquares[3].Tokens.Count, 6);
            Assert.AreEqual(mySquares[4].Tokens.Count, 6);
            Assert.AreEqual(mySquares[5].Tokens.Count, 6);

            Assert.AreEqual(mySquares[6].Tokens.Count, 2);

            Assert.AreEqual(mySquares[7].Tokens.Count, 0);
            Assert.AreEqual(mySquares[8].Tokens.Count, 6);
            Assert.AreEqual(mySquares[9].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);

            // Second Turn
            board.Go(player2, 2, Direction.LEFT);

            Assert.AreEqual(player1.Pool.Count, 6);
            Assert.AreEqual(player2.Pool.Count, 2);

            Assert.AreEqual(mySquares[0].Tokens.Count, 0);

            Assert.AreEqual(mySquares[1].Tokens.Count, 0);
            Assert.AreEqual(mySquares[2].Tokens.Count, 1);
            Assert.AreEqual(mySquares[3].Tokens.Count, 7);
            Assert.AreEqual(mySquares[4].Tokens.Count, 7);
            Assert.AreEqual(mySquares[5].Tokens.Count, 7);

            Assert.AreEqual(mySquares[6].Tokens.Count, 2);

            Assert.AreEqual(mySquares[7].Tokens.Count, 1);
            Assert.AreEqual(mySquares[8].Tokens.Count, 0);
            Assert.AreEqual(mySquares[9].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);
        }
    }
}
