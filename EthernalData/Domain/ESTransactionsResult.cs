using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Domain
{
    public class ESTransactionsResult
    {
        public int status;
        public string message;
        [JsonProperty("result")]
        public Transaction[] transactions;
    }
}
