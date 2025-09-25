using APT00100COMMON.DTOs.APT00100;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON
{
    public interface IAPT00100
    {
        GetAPSystemParamResultDTO GetAPSystemParam();
        GetPeriodYearRangeResultDTO GetPeriodYearRange();
        GetCompanyInfoResultDTO GetCompanyInfo();
        GetGLSystemParamResultDTO GetGLSystemParam();
        GetTransCodeInfoResultDTO GetTransCodeInfo();
        IAsyncEnumerable<APT00100DetailDTO> GetInvoiceList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
    }
}
