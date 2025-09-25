using APT00200COMMON.DTOs.APT00210Print;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00200SERVICE.DTOs
{
    public class APT00210PrintLogKeyDTO
    {
        public APT00210PrintReportParameterDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
