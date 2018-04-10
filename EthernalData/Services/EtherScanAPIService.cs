using EthernalData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using EthernalData.Converters;

namespace EthernalData.Services
{
    public class EtherScanAPIService : IEtherScanAPIService
    {

        private string APIKEY = "EFVIQ2QEQT6NDRH5TF3UKSUVAIKMYF11FV";

        public async Task<IQueryable<Transaction>> GetTransactionsAsync(string address, int fromBlock, int tillBlock, Sort sort)
        {            
            List<Domain.Transaction> transactions = new List<Transaction>();
            string uri = "http://api-ropsten.etherscan.io/api?module=account&action=txlist&address=" + address + "&startblock=" + fromBlock + "&endblock=" + tillBlock + "&sort=" + sort.ToString() + "&apikey=" + APIKEY;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string result = await response.Content.ReadAsStringAsync();
                    ESTransactionsResult eSTransactionsResult = JsonConvert.DeserializeObject<ESTransactionsResult>(result);
                    //Debug.WriteLine(eSTransactionsResult.transactions[0].BlockNumber);
                    //Debug.WriteLine(eSTransactionsResult.transactions[1].Input);
                    //Debug.WriteLine(eSTransactionsResult.transactions[1].HexString2B64String(eSTransactionsResult.transactions[1].Input));
                    foreach (Domain.Transaction t in eSTransactionsResult.transactions)
                    {
                        string base64 = RequestInputConverter.HexString2B64String(t.Input);
                        Debug.WriteLine(base64);
                        t.setBase64(base64);
                     //   t.setBase64(t.HexString2B64String(t.Input)); <-- dit werkt niet? 
                        transactions.Add(t);
                    }
                }
                catch (JsonSerializationException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (JsonReaderException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return transactions.AsQueryable();
        }      
    }
}
