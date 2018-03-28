using EthernalData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Services
{
    public interface IEtherScanService
    {
        Task<List<Transaction>> GetTransactionsAsync();
    }
}
