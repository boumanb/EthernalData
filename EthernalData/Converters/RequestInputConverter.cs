﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Converters
{
    public class RequestInputConverter
    {
        public static byte[] StringToByteArray(string hex)
        {
            string newHex = hex.Remove(0, 2);
            return Enumerable.Range(0, newHex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(newHex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string HexString2B64String(string input)
        {
            string result = "";

            try
            {
                result = System.Convert.ToBase64String(StringToByteArray(input));

            }
            catch (System.FormatException x)
            {
                Debug.WriteLine(x.Message);
            }
            return result;

        }

    }
}
