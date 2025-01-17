﻿using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Services
{
    public interface INethereumWeb3Service
    {
        Task<LinkedList<Transaction>> GetTransactionsByAddress(string address, int fromBlock, int tillBlock);
        Task<Transaction> GetTransactionByHashAsync(string txHash);
    }
}
