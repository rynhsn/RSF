using PMR00100Common.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Back
{
    public interface IPMR00100 : R_IServiceCRUDAsyncBase<PMR00100DTO>
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        Task<PeriodYearRangeDTO> GetPeriodYear();
        Task<PeriodDT_DataDTO> GetPeriodDTList(PMR00100ParamDTO poData);
        Task<LOOStatusDataDTO> GetLOOStatusList();
        IAsyncEnumerable<PMR00100DTO> GetLOOPrintList(PrintParamDTO poData);

    }
}
