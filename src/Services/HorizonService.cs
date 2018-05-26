using System;
using System.IO;
using System.Net;
using stellar_dotnetcore_sdk;
using Stellmart.Api.Services;
using Stellmart.Api.Data.Horizon;
using Microsoft.Extensions.Options;
using Stellmart.Api.Data.Settings;
using AutoMapper;

namespace Stellmart.Services
{
    public class HorizonService : IHorizonService
    {
        private readonly Server _server;
        private readonly IOptions<HorizonSettings> _horizonSettings;
        private readonly IMapper _mapper;

        public HorizonService(IOptions<HorizonSettings> horizonSettings, IMapper mapper)
        {
            _horizonSettings = horizonSettings;
            _mapper = mapper;

            if (_horizonSettings.Value.Server.Contains("testnet"))
            {
                Network.UseTestNetwork();
            }
            else
            {
                Network.UsePublicNetwork();
            }
            
            _server = new Server(_horizonSettings.Value.Server);
        }

        public HorizonKeyPairModel CreateAccount()
        {
            var keypair = _mapper.Map<HorizonKeyPairModel>(KeyPair.Random());
	    return keypair;
        }

	public void Fund_Test_Account(string Public_Key)
	{
            UriBuilder baseUri = new UriBuilder("https://horizon-testnet.stellar.org/friendbot");
            string queryToAppend = "addr=" + Public_Key;

	     baseUri.Query = queryToAppend;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUri.ToString());
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            sr.ReadToEnd();
	}
    }
}
