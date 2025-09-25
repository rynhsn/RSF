using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace CBR00600SERVICE
{
    public class CBR00600PrintLogKeyDTO<T>
    {
        public T poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
        public R_ReportGlobalDTO poReportGlobal { get; set; }
    }
}
