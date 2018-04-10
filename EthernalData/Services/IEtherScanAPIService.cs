using EthernalData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Services
{
    public interface IEtherScanAPIService
    {
        Task<IQueryable<Transaction>> GetTransactionsAsync(string address, int fromBlock, int tillBlock, Sort sort);
    }

    public sealed class Sort
    {

        private readonly String name;
        private readonly int value;

        public static readonly Sort ASC = new Sort(1, "asc");
        public static readonly Sort DESC = new Sort(2, "desc");

        private Sort(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }

    }
}
