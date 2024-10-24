using GLR00100Common.Params;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace GLR00100Service.DTOs;

public class GLR00100ReportLogKeyDTO
{
    public GLR00100ReportParam poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
    public R_ReportGlobalDTO poGlobalVar { get; set; }
}