﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OAnQuan.Business
{
    public class BigSquare : Square
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BigSquare()
        {
            Tokens.Add(new BigToken());
            //Tokens = new List<Token>() { new BigToken() };
        }
    }
}
