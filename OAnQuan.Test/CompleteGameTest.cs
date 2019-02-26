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
            var player1 = board.PlayersList[0];
            player1.Pseudo = "OurPlayer1";

            var player2 = board.PlayersList[1];
            player2.Pseudo = "OurPlayer2";

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
            var list1 = new List<int> { 2, 0, 0, 6, 6, 6, 2, 0, 6, 6, 6, 6 };
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
            var list2 = new List<int> { 0, 0, 1, 7, 7, 7, 3, 1, 0, 6, 6, 6 };
            Assert.AreEqual(player1.Pool.Count, 6);
            Assert.AreEqual(player2.Pool.Count, 2);

            Assert.AreEqual(mySquares[0].Tokens.Count, 0);

            Assert.AreEqual(mySquares[1].Tokens.Count, 0);
            Assert.AreEqual(mySquares[2].Tokens.Count, 1);
            Assert.AreEqual(mySquares[3].Tokens.Count, 7);
            Assert.AreEqual(mySquares[4].Tokens.Count, 7);
            Assert.AreEqual(mySquares[5].Tokens.Count, 7);

            Assert.AreEqual(mySquares[6].Tokens.Count, 3);

            Assert.AreEqual(mySquares[7].Tokens.Count, 1);
            Assert.AreEqual(mySquares[8].Tokens.Count, 0);
            Assert.AreEqual(mySquares[9].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);
            Assert.AreEqual(mySquares[10].Tokens.Count, 6);

            //3rd turn
            board.Go(player1, 3, Direction.LEFT);
            List<int> list3 = new List<int> { 0, 0, 5, 3, 10, 2 , 6, 2, 3, 1, 0, 9};
            Assert.AreEqual(player1.Pool.Count, 9);
            Assert.AreEqual(player2.Pool.Count, 2);
            for (int i=0; i<12; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, list3[i]);
            }

            //4th
            board.Go(player2, 7, Direction.LEFT);
            List<int> list4 = new List<int> { 2, 1, 8, 6, 2, 1, 9, 2, 5, 3, 2, 0 };
            Assert.AreEqual(player1.Pool.Count, 9);
            Assert.AreEqual(player2.Pool.Count, 2);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, list4[i]);
            }

            //5th
            board.Go(player1, 4, Direction.RIGHT);
            List<int> list5 = new List<int> { 3, 0, 9, 0, 1, 3, 11, 1, 7, 5, 0, 0 };
            Assert.AreEqual(player1.Pool.Count, 10);
            Assert.AreEqual(player2.Pool.Count, 2);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, list5[i]);
            }

            //6th
            board.Go(player2, 9, Direction.RIGHT);
            List<int> list6 = new List<int> { 4, 1, 10, 0, 0, 3, 11, 1, 7, 0, 1, 1 };
            Assert.AreEqual(player1.Pool.Count, 10);
            Assert.AreEqual(player2.Pool.Count, 3);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, list6[i]);
            }

            //7th
            board.Go(player1, 5, Direction.LEFT);
            List<int> list7 = new List<int> { 5, 0, 11, 1, 1, 0, 11, 1, 0, 0, 2, 0 };
            Assert.AreEqual(player1.Pool.Count, 17);
            Assert.AreEqual(player2.Pool.Count, 3);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, list7[i]);
            }

            //8th
            board.Go(player2, 7, Direction.RIGHT);
            List<int> list8 = new List<int> { 0, 0, 0, 1, 1, 0, 11, 0, 1, 0, 0, 0 };
            Assert.AreEqual(player1.Pool.Count, 17);
            Assert.AreEqual(player2.Pool.Count, 21);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, list8[i]);
            }

            //9th
            board.Go(player1, 3, Direction.RIGHT);
            List<int> list9 = new List<int> { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 };
            Assert.AreEqual(player1.Pool.Count, 29);
            Assert.AreEqual(player2.Pool.Count, 21);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(mySquares[i].Tokens.Count, list9[i]);
            }


            //END OF GAME
            Assert.AreEqual(player1.GetScore(), 33);
            Assert.AreEqual(player2.GetScore(), 25);
            Assert.AreEqual(player1.GamesNb, 1);

            //2nd game
            Board board2 = new Board();
            player1 = board2.PlayersList[0];
            Assert.AreEqual(board2.SquaresList[1].Player, player1);
            player2 = board2.PlayersList[1];
            Turn(board2, player1, 1, Direction.RIGHT, list1, 6);
            Turn(board2, player2, 8, Direction.LEFT, list2, 2);
            Turn(board2, player1, 3, Direction.LEFT, list3, 9);
            Turn(board2, player2, 7, Direction.LEFT, list4, 2);
            Turn(board2, player1, 4, Direction.RIGHT, list5, 10);
            Turn(board2, player2, 9, Direction.RIGHT, list6, 3);
            Turn(board2, player1, 5, Direction.LEFT, list7, 17);
            Turn(board2, player2, 7, Direction.RIGHT, list8, 21);
            Turn(board2, player1, 3, Direction.RIGHT, list9, 29);

            //END OF GAME
            Assert.AreEqual(player1.GetScore(), 33);
            Assert.AreEqual(player2.GetScore(), 25);
            Assert.AreEqual(player1.GamesNb, 1);
        }
        static void Turn(Board board, Player player, int squareId, Direction direction, List<int> listGotten, int score)
        {
            board.Go(player, squareId, direction);
            Assert.AreEqual(player.Pool.Count, score);
            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(board.SquaresList[i].Tokens.Count, listGotten[i]);
            }
        }
    }
}
