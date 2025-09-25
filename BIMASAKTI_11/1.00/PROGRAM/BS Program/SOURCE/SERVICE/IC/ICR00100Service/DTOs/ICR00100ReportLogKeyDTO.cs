using ICR00100Common.DTOs.Print;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace ICR00100Service.DTOs;

public class ICR00100ReportLogKeyDTO
{
    public ICR00100ReportParam poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
    public R_ReportGlobalDTO poGlobalVar { get; set; }
}