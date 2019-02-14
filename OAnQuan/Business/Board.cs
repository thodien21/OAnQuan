using OAnQuan.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAnQuan.Business
{
    public class Board
    {
        SmallToken smallToken = new SmallToken();
        BigSquare bigSquare1 = new BigSquare();
        BigSquare bigSquare2 = new BigSquare();
        List<SmallSquare> SubListSquares1 = new List<SmallSquare> { new SmallSquare(), new SmallSquare(), new SmallSquare(), new SmallSquare(), new SmallSquare() };
        List<SmallSquare> SubListSquares2 = new List<SmallSquare> { new SmallSquare(), new SmallSquare(), new SmallSquare(), new SmallSquare(), new SmallSquare() };

        //public List<SmallSquare> SubListSquares { get; set; }
        public List<Square> SquaresList { get; set; }

        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }

        public Board()
        {         
            for(int i=0; i<5; i++)
            {
                SubListSquares1[i].Id = i+1;
                SubListSquares1[i].Player = FirstPlayer;
                SubListSquares2[i].Id = i+1;
                SubListSquares1[i].Player = SecondPlayer;
            }
            SquaresList = new List<Square> { bigSquare1 };
            SquaresList.AddRange(SubListSquares1);
            SquaresList.Add(bigSquare2);
            SquaresList.AddRange(SubListSquares2);
        }

        /// <summary>
        /// play turn
        /// </summary>
        /// <param name="player">player having turn</param>
        /// <param name="squareId">square identifier (1-5) </param>
        /// <param name="direction">direction to share the tokens</param>
        /// <returns></returns>
        public void Go(Player player, int squareId, Direction direction)
        {
            var _listSquare = (player == FirstPlayer) ? SubListSquares1 : SubListSquares2;
            List<Square> _subListSquares = new List<Square>();
            for (int i = 0; i < 5; i++)
            {
                _subListSquares.Add(_listSquare[i]);
            }
            var _selectedSquare = _subListSquares.FirstOrDefault(s => s.Id == squareId);
            List<Token> _eatenTokens = new List<Token>();
            Square _eatenSquare = new Square();
            Square _providerSquare = new Square();

            //the value of index of selected square in SquaresList depends on player
            int _squareIndex = (player == FirstPlayer) ? SquaresList.IndexOf(_selectedSquare) : SquaresList.IndexOf(_selectedSquare) + 6;
            var _tokenQty = SquaresList[_squareIndex].Tokens.Count;

            //Check if the selected square is authorized and the qty of provider square is not null:
            if (_selectedSquare.Player != player || _tokenQty == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(squareId), "The selected square should correspond to {0} and not be empty", player.Pseudo);
            }

            //While the provider square is not empty, it provide its tokens to next squares
            while (_tokenQty != 0)
            {
                _providerSquare = SquaresList[_squareIndex];//save the tokens in a new object
                SquaresList[_squareIndex].Tokens.Clear();//the provider square is emptied by distributing the tokens for its followed squares.

                for (int i = 1; i <= _tokenQty; i++)
                {
                    _squareIndex = (direction == Direction.RIGHT) ? (_squareIndex + 1) % 12 : (_squareIndex + 11) % 12;
                    SquaresList[_squareIndex].Tokens.Add(smallToken);//the next square has 1 token in plus
                }
                _squareIndex = (direction == Direction.RIGHT) ? (_squareIndex + 1) % 12 : (_squareIndex + 11) % 12;//next square
                _tokenQty = SquaresList[_squareIndex].Tokens.Count;//the quantity of tokens in the next square
            }

            //Some eaten tokens?
            while (_tokenQty == 0 )
            {
                _squareIndex = (direction == Direction.RIGHT) ? (_squareIndex + 1) % 12 : (_squareIndex + 11) % 12;//next square
                _eatenTokens = SquaresList[_squareIndex].Eaten();
                foreach(var item in _eatenTokens)
                {
                    player.Pool.Add(item);
                }
                _squareIndex = (direction == Direction.RIGHT) ? (_squareIndex + 1) % 12 : (_squareIndex + 11) % 12;//next square
                _tokenQty = SquaresList[_squareIndex].Tokens.Count;//the quantity of tokens in the next square
            }
        }
    }
}    