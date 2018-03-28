using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Domain
{
    public class Transaction
    {
        public int blockNumber;
        public string input;

        public static byte[] HexStringToHex(string input)
        {
            var resultArray = new byte[input.Length / 2];
            try
            {

                for (var i = 0; i < resultArray.Length; i++)
                {
                    resultArray[i] = System.Convert.ToByte(input.Substring(i * 2, 2), 16);
                }
            }
            catch (System.FormatException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return resultArray;
        }

        public string HexString2B64String(string input)
        {
            string result = "";
            try
            {
                result = System.Convert.ToBase64String(HexStringToHex(input));

            }
            catch (System.FormatException x)
            {
                Debug.WriteLine(x.Message);
            }
            return result;

        }
    }
}
