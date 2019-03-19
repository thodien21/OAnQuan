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
        /// The tokens in this square is eaten
        /// </summary>
        /// <returns></returns>
        public List<Token> Eaten()
        {
            List<Token> earnedTokens = new List<Token>();
            /* foreach(var item in Tokens)
            {
                earnedTokens.Add(item);
            }*/
            earnedTokens.AddRange(Tokens);
            Tokens.Clear();
            return earnedTokens;
        }

        /// <summary>
        /// Calculate the quantity of big token in the token list
        /// </summary>
        /// <returns></returns>
        public static int GetBigTokenQtyFromList(List<Token> tokensList)
        {
            var bigToken = new BigToken();
            var nbBigToken = 0;
            foreach (var item in tokensList)
            {
                if (item.GetType() == bigToken.GetType())
                {
                    nbBigToken++;
                }
            }
            return nbBigToken;
        }
    }
}
