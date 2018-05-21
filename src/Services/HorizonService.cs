using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk;
using Horizon_data = Stellmart.Api.Data.Horizon;

namespace Stellmart.Services
{
    public class HorizonService
    {
        static public Server server;
        public static void Horizon_Server(string network) {
            if(network.Equals("public")) {
                Network.UsePublicNetwork();
                server = new Server("https://horizon.stellar.org");
            } else {
                Network.UseTestNetwork();
                server = new Server("https://horizon-testnet.stellar.org");
            }
        }
        public static void Create_Stellar_account(ref Horizon_data.KeyPair data) {
            var keypair = KeyPair.Random();
            data.Public_Key = keypair.AccountId;
	    /* ToDo encode the SecretSeed */
            data.Secret_Key = keypair.SecretSeed;
        }
    }
}
