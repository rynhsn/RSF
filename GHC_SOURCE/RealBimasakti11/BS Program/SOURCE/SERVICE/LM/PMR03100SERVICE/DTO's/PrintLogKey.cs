using PMR03100COMMON.DTO_s.Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace PMR03100SERVICE.DTO_s
{
    public class PrintLogKey
    {
        public ParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO? poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
