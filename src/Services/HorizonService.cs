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
        public Horizon_data.KeyPair Create_Stellar_account() {
            Horizon_data.KeyPair data_kp = new Horizon_data.KeyPair();
            var keypair = KeyPair.Random();
            data_kp.Public_Key = keypair.AccountId;
            data_kp.Secret_Key = keypair.SecretSeed;
            return data_kp;
        }
    }
}
