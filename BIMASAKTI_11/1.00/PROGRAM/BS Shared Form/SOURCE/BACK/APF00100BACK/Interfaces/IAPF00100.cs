using APF00100COMMON.DTOs.APF00100;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100BACK
{
    public interface IAPF00100
    {
        Task<GetCompanyInfoResultDTO> GetCompanyInfo();
        Task<GetGLSystemParamResultDTO> GetGLSystemParam();
        Task<GetCallerTrxInfoResultDTO> GetCallerTrxInfo(GetCallerTrxInfoParameterDTO poParam);
        Task<GetPeriodResultDTO> GetPeriod(GetPeriodParameterDTO poParam);
        Task<GetTransactionFlagResultDTO> GetTransactionFlag(GetTransactionFlagParameterDTO poParam);
        Task<APF00100HeaderResultDTO> GetHeader(APF00100HeaderParameterDTO poParam);
        Task<APF00100HeaderResultDTO> GetCAWTCustReceipt(APF00100HeaderParameterDTO poParam);
        Task<APF00100HeaderResultDTO> GetCQCustReceipt(APF00100HeaderParameterDTO poParam);
        IAsyncEnumerable<APF00100ListDTO> GetAllocationList();
    }
}
