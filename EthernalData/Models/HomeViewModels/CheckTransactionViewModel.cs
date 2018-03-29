using EthernalData.Converters;
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

        public static RequestInputConverter Converter { get; set; }
    }
}
