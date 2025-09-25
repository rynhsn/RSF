using APT00100COMMON.DTOs.APT00110Print;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00100SERVICE.DTOs
{
    public class APT00110PrintLogKeyDTO
    {
        public APT00110PrintReportParameterDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
