using OAnQuan.Business;
using OAnQuan.DataAccess;

namespace OAnQuan.Services
{
    public class Services
    {
        public Player Player { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public Services(string pass, string pseudo)
        {
            Player = PlayerDb.GetPlayer(pass, pseudo)
        }
    }
}
