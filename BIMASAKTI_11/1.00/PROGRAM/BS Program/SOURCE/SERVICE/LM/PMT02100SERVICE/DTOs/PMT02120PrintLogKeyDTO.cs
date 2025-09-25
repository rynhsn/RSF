using PMT02100COMMON.DTOs.PMT02120Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100SERVICE.DTOs
{
    public class PMT02120PrintLogKeyDTO
    {
        public PMT02120PrintReportParameterDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
