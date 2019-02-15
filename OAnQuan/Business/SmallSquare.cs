using System;
using System.Collections.Generic;
using System.Text;

namespace OAnQuan.Business
{
    public class SmallSquare : Square
    {
        /// <summary>
        /// Dafault constructor
        /// </summary>
        public SmallSquare()
        {            
            Tokens = new List<Token>() { new SmallToken(), new SmallToken(), new SmallToken(), new SmallToken(), new SmallToken() };
            Player = new Player("");
        }

        //Distribute the tokens
        public List<Token> Distribuable()
        {
            List<Token> movedTokens = Tokens;
            Tokens.Clear();
            return movedTokens;
        }

        //Start a turn
        public bool Startable = false;
    }
}
