using PMT50600COMMON.DTOs.PMT50600;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON
{
    public interface IPMT50600
    {
        GetPMSystemParamResultDTO GetPMSystemParam(GetPMSystemParamParameterDTO poParameter);
        GetPeriodYearRangeResultDTO GetPeriodYearRange();
        GetCompanyInfoResultDTO GetCompanyInfo();
        GetGLSystemParamResultDTO GetGLSystemParam();
        GetTransCodeInfoResultDTO GetTransCodeInfo();
        IAsyncEnumerable<PMT50600DetailDTO> GetInvoiceList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
    }
}
