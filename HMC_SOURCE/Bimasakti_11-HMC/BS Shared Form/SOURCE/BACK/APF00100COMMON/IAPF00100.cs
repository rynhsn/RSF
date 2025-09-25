using APF00100COMMON.DTOs.APF00100;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON
{
    public interface IAPF00100
    {
        GetCompanyInfoResultDTO GetCompanyInfo();
        GetGLSystemParamResultDTO GetGLSystemParam();
        GetCallerTrxInfoResultDTO GetCallerTrxInfo(GetCallerTrxInfoParameterDTO poParam);
        GetPeriodResultDTO GetPeriod(GetPeriodParameterDTO poParam);
        GetTransactionFlagResultDTO GetTransactionFlag(GetTransactionFlagParameterDTO poParam);
        APF00100HeaderResultDTO GetHeader(APF00100HeaderParameterDTO poParam);
        IAsyncEnumerable<APF00100ListDTO> GetAllocationList();
    }
}
