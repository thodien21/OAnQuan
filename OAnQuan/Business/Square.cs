using System;
using System.Collections.Generic;
using System.Text;

namespace OAnQuan.Business
{
    public class Square
    {
        public List<Token> Tokens { get; set; }

        public Player Player { get; set; }

        public int PlayerNumber { get; set; }

        /// <summary>
        /// To be eaten
        /// </summary>
        /// <returns></returns>
        public List<Token> Eaten()
        {
            List<Token> earnedTokens = new List<Token>();
            foreach(var item in Tokens)
            {
                earnedTokens.Add(item);
            }
            Tokens.Clear();
            return earnedTokens;
        }
    }
}
