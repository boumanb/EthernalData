using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EthernalData.Models.ManageViewModels
{
    public class UploadViewModel
    {
        [Required]
        public string ETHAddress { get; set; }
    }
}
