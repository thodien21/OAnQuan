using System;
using System.Collections.Generic;
using System.Text;

namespace OAnQuan.Business
{
    public class Square
    {
        public List<Token> Tokens { get; set; }

        public Player Player { get; set; }

        /// <summary>
        /// Each square associate with one player
        /// </summary>        
        public int Id { get; set; }//1-5

        /// <summary>
        /// To be eaten
        /// </summary>
        /// <returns></returns>
        public List<Token> Eaten()
        {
            List<Token> _earnedTokens = new List<Token>();
            foreach(var item in Tokens)
            {
                _earnedTokens.Add(item);
            }
            Tokens.Clear();
            return _earnedTokens;
        }
    }
}
