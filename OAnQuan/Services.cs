using OAnQuan.Business;
using OAnQuan.DataAccess;

namespace OAnQuan
{
    public static class Services
    {
        public static Player Player { get; set; }

        public static void GetPlayer(string pseudo, string pass)
        {
            Player = PlayerDb.GetPlayer(pseudo, pass);
        }
    }
}
