using EthernalData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;

namespace EthernalData.Services
{
    public class EtherScanAPIService : IEtherScanAPIService
    {

       public async Task<List<Transaction>> GetTransactionsAsync()
        {
            List<Transaction> transactions = new List<Transaction>();
            string uri = "http://api-ropsten.etherscan.io/api?module=account&action=txlist&address=0xe47FFd7fD16519Bc532F4F73E8993FFfFe0191BE&startblock=0&endblock=99999999&sort=asc&apikey=EFVIQ2QEQT6NDRH5TF3UKSUVAIKMYF11FV";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string result = await response.Content.ReadAsStringAsync();
                    RootObject root = JsonConvert.DeserializeObject<RootObject>(result);
                    Debug.WriteLine(root.transactions[0].blockNumber);
                    Debug.WriteLine(root.transactions[1].input);
                    Debug.WriteLine(root.transactions[1].HexString2B64String(root.transactions[1].input));
                }
                catch (JsonSerializationException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (JsonReaderException x)
                {
                    Debug.WriteLine(x.Message);
                }
            }
            return transactions;
        }
    }
}
