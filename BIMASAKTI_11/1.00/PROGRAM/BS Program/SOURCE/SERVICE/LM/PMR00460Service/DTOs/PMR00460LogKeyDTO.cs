using PMR00460Common.DTOs.Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace PMR00460Service.DTOs;


public class PMR00460ReportLogKeyDTO
{
    public PMR00460ReportParam poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
    public R_ReportGlobalDTO poGlobalVar { get; set; }
}