using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace EthernalData.Services
{
    public class NethereumWeb3Service : INethereumWeb3Service
    {
        private Web3 Web3;

        public NethereumWeb3Service()
        {
            Web3 = new Web3("https://ropsten.infura.io/8WSOwn09LvBKuJTt4EL");
        }
      

        public LinkedList<Transaction> GetTransactionsByAddress(string address, int fromBlock, int tillBlock)
        {
            var transactionList = new LinkedList<Transaction>();

            for (int i = fromBlock; i < tillBlock; i++)
            {
            
                var blockWithTransactionsTask = Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new BlockParameter((ulong)i));

                blockWithTransactionsTask.Wait();

                if (blockWithTransactionsTask != null)
                {
                    foreach (Transaction transaction in blockWithTransactionsTask.Result.Transactions)
                    {
                        if (transaction.From == address || transaction.To == address)
                        {
                            transactionList.AddLast(transaction);
                        }
                    }
                }
            }
            
            return transactionList;
        }
    }
}
