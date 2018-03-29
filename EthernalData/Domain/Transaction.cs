using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



namespace EthernalData.Domain
{
    public class Transaction : Nethereum.RPC.Eth.DTOs.Transaction
    {
        public string base64;



        public void setBase64(string base64)
        {
            this.base64 = base64;
        }
   
    }
}
