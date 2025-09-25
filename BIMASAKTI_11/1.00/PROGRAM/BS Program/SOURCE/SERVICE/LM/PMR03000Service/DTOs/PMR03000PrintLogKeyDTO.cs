using R_BackEnd;
using R_CommonFrontBackAPI.Log;

namespace PMR03000Service.DTOs;

public class PMR03000PrintLogKeyDTO<T>
{
    public T poParam { get; set; }
    public R_NetCoreLogKeyDTO poLogKey { get; set; }
    public R_ReportGlobalDTO poReportGlobal { get; set; }
}