using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
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
        
        public async Task<LinkedList<Transaction>> GetTransactionsByAddress(string address, int fromBlock, int tillBlock)
        {
            var transactionList = new LinkedList<Transaction>();

            for (int i = fromBlock; i < tillBlock; i++)
            {
            
                var blockWithTransactions = await Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new BlockParameter((ulong)i));
                

                if (blockWithTransactions != null)
                {
                    foreach (Transaction transaction in blockWithTransactions.Transactions)
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

        public async Task<Transaction> GetTransactionByHashAsync(string txHash)
        {
            Transaction transaction = new Transaction();
            try
            {
                transaction = await Web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(txHash);
            } catch (RpcResponseException ex)
            {                

            }
            
            return transaction;
                
        }
    }
}
