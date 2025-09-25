using APR00100COMMON.DTO_s.Print;
using APR00100COMMON.DTO_s;
using R_CommonFrontBackAPI.Log;

namespace APR00100COMMON
{
    public class APR00100PrintLogKey
    {
        public ReportParamDTO poParamSummary { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
