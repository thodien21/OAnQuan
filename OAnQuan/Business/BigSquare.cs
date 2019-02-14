using System;
using System.Collections.Generic;
using System.Text;

namespace OAnQuan.Business
{
    public class BigSquare : Square
    {
        public BigSquare()
        {
            Tokens = new List<Token>() { new BigToken() };
        }
    }
}
