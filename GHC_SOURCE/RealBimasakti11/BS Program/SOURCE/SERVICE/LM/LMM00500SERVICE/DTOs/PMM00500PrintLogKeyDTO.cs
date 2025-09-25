using PMM00500Common.DTOs;
using R_CommonFrontBackAPI.Log;

namespace PMM00500Service.DTOs
{
    public class PMM00500PrintLogKeyDTO
    {
        public PrintParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}