using R_CommonFrontBackAPI.Log;

namespace PMR00400Common.DTOs
{
    public class PMR00400ReportLogKeyDTO
    {
        public PMR00400ParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}