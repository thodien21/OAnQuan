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
    }
}
