using GSM04000Common;
using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace GSM04000Service;

public class GSM04000ReportLogKeyDTO
{
    public DepartmentDTO poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
    public R_ReportGlobalDTO poGlobalVar { get; set; }
}