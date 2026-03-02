using R_BackEnd;
using R_CommonFrontBackAPI.Log;
using FAT00700Service.DTOs;

namespace FAT00700Service.DTOs
{
    public class FAT00700ReportLogKeyDTO
    {
        public FAT00700ReportParam poParam { get; set; } = new FAT00700ReportParam();
        public R_NetCoreLogKeyDTO poLogKey { get; set; } = new R_NetCoreLogKeyDTO();
        public R_ReportGlobalDTO poGlobalVar { get; set; } = new R_ReportGlobalDTO();
    }
}

