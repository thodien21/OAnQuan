using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OAnQuan.Business
{
    public class Square
    {
        public List<Token> Tokens { get; set; }

        public Player Player { get; set; }

        public int PlayerNumber { get; set; }

        public int TokenQty => Tokens.Count;
    }
}
