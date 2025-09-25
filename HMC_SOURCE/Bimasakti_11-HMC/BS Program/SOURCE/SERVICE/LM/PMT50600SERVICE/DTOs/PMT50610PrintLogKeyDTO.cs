using PMT50600COMMON.DTOs.PMT50610Print;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600SERVICE.DTOs
{
    public class PMT50610PrintLogKeyDTO
    {
        public PMT50610PrintReportParameterDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
