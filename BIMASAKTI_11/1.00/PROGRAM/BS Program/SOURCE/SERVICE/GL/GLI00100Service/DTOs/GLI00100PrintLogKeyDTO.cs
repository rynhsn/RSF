using GLI00100Common.DTOs;
using R_CommonFrontBackAPI.Log;

namespace GLI00100Service.DTOs;

public class GLI00100PrintLogKeyDTO
{
    public GLI00100PopupParamsDTO poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
}