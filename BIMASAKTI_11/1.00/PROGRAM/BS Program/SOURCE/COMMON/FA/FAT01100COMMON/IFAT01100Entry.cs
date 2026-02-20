using System.Collections.Generic;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using FAT01100Common.DTOs;

namespace FAT01100Common
{
    /// <summary>
    /// Interface for FAT01100 - Change Asset Data Transaction (CRUD)
    /// </summary>
    public interface IFAT01100Entry : R_IServiceCRUDAsyncBase<FAT01100DTO>
    {
        Task<FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO>> FAT01100GetCompanyInfo(FAT01100GetCompanyInfoParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>> FAT01100GetGetSystemParam(FAT01100GetGetSystemParamParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO>> FAT01100GetPeriodeDtInfo(FAT01100GetPeriodeDtInfoParameterDTO poParameter);
        Task<FAT01100ResultDTO<List<FAT01100GetCurrencyListResultDTO>>> GetCurrencyList(FAT01100GetCurrencyListParameterDTO poParameter);
        Task<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>> FAT01100GetDeptLookupList(FAT01100GetDeptLookupListParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO>> FAT01100GetTransCodeInfo(FAT01100GetTransCodeInfoParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>> FAT01100GetYearRange(FAT01100GetYearRangeParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO>> FAT01100GetLastCurrencyRate(FAT01100GetLastCurrencyRateParameterDTO poParameter);
        Task<FAT01100ResultDTO<object>> FAT01100UpdateTransHdStatus(FAT01100UpdateTransHdStatusParameterDTO poParameter);
        Task<FAT01100ResultDTO<object>> FAT01100SubmitTrans(FAT01100SubmitTransParameterDTO poParameter);
        Task<FAT01100ResultDTO<FAT01100GetAssetResultDTO>> FAT01100GetAsset(FAT01100GetAssetParameterDTO poParameter);
    }
}
