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
            //setup board   
            Board board = new Board();

            var myPlayers = board.PlayersList;
            var player1 = board.PlayersList[0];
            player1.Pseudo = "OurPlayer1";
            player1.Password = "OurPlayer1PassWord";

            var player2 = board.PlayersList[1];
            player2.Pseudo = "OurPlayer2";
            player2.Password = "OurPlayer2PassWord";

            var mySquares = board.SquaresList;
            Assert.AreEqual(mySquares.Count, 12);
            Assert.AreEqual(board.PlayersList.Count, 2);

            //test player
            for (int i=1; i<=5; i++)
            {
                Assert.AreEqual(mySquares[i].Player, player1);
                Assert.AreEqual(mySquares[i+6].Player, board.PlayersList[1]);
            }

            //test number of tokens in each square
            for (int i=1; i<=5; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, 5);
                Assert.AreEqual(mySquares[i+6].Tokens.Count, 5);
            }
            Assert.AreEqual(mySquares[0].Tokens.Count, 1);
            Assert.AreEqual(mySquares[6].Tokens.Count, 1);

            Assert.That(board.PlayersList[0], Is.Not.Null);
            Assert.That(board.PlayersList[0].Pseudo, Is.Not.Null);
            Assert.AreEqual(board.PlayersList[0].Pseudo,"OurPlayer1");
            Assert.AreEqual(board.PlayersList[0].Pool.Count, 0);
            Assert.AreEqual(player2.Pool.Count, 0);

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
            board.Go(player2, 8, Direction.LEFT);

            Assert.AreEqual(player1.Pool.Count, 6);
            Assert.AreEqual(player2.Pool.Count, 2);

            Assert.AreEqual(mySquares[0].Tokens.Count, 0);

            Assert.AreEqual(mySquares[1].Tokens.Count, 0);
            Assert.AreEqual(mySquares[2].Tokens.Count, 1);
            Assert.AreEqual(mySquares[3].Tokens.Count, 7);
            Assert.AreEqual(mySquares[4].Tokens.Count, 7);
            Assert.AreEqual(mySquares[5].Tokens.Count, 7);

            Assert.AreEqual(mySquares[6].Tokens.Count, 3);//2

            Assert.AreEqual(mySquares[7].Tokens.Count, 1);
            Assert.AreEqual(mySquares[8].Tokens.Count, 0);
            Assert.AreEqual(mySquares[9].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);
        }
    }
}
