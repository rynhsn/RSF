using APT00200COMMON.DTOs.APT00200;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON
{
    public interface IAPT00200
    {
        GetAPSystemParamResultDTO GetAPSystemParam();
        GetPeriodYearRangeResultDTO GetPeriodYearRange();
        GetCompanyInfoResultDTO GetCompanyInfo();
        GetGLSystemParamResultDTO GetGLSystemParam();
        GetTransCodeInfoResultDTO GetTransCodeInfo();
        IAsyncEnumerable<APT00200DetailDTO> GetPurchaseReturnList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
    }
}
