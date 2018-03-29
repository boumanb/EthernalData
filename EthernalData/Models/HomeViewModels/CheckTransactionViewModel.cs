using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Models.HomeViewModels
{
    public class CheckTransactionViewModel
    {
        public string StatusMessage { get; set; }

        [DefaultValue(false)]
        public Boolean HasTransaction { get; set; }

        [Required]
        public string TXHash { get; set; }        

        public Nethereum.RPC.Eth.DTOs.Transaction Transaction { get; set; }

        public static byte[] StringToByteArray(string hex)
        {
            string newHex = hex.Remove(0, 2);
            return Enumerable.Range(0, newHex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(newHex.Substring(x, 2), 16))
                             .ToArray();
        }

        public string HexString2B64String(string input)
        {
            string result = "";

            try
            {
                result = System.Convert.ToBase64String(StringToByteArray(input));

            }
            catch (System.FormatException x)
            {
                
            }
            return result;

        }
    }
}
