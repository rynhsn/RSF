using R_CommonFrontBackAPI;
using FAT00800Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FAT00800Common
{
    /// <summary>
    /// Interface for FAT00800 - Fixed Asset Transaction operations
    /// </summary>
    public interface IFAT00800Entry : R_IServiceCRUDAsyncBase<FAT00800DTO>
    {
        Task<FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>> FAT00800GetCompanyInfo(FAT00800GetCompanyInfoParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>> FAT00800GetGetSystemParam(FAT00800GetGetSystemParamParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>> FAT00800GetPeriodeDtInfo(FAT00800GetPeriodeDtInfoParameterDTO poParameter);
        /// <summary>
        /// Get currency list (streaming). Parameters from R_BackGlobalVar and optional streaming context. Uses RSP_GS_GET_CURRENCY_LIST.
        /// </summary>
        IAsyncEnumerable<FAT00800GetCurrencyListResultDTO> GetCurrencyList();
        /// <summary>
        /// Get department lookup list (streaming). Parameters via streaming context (CCOMPANY_ID, CUSER_ID, CPROGRAM_ID).
        /// </summary>
        IAsyncEnumerable<FAT00800GetDeptLookupListResultDTO> FAT00800GetDeptLookupList();
        Task<FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>> FAT00800GetTransCodeInfo(FAT00800GetTransCodeInfoParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>> FAT00800GetYearRange(FAT00800GetYearRangeParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetLastCurrencyRateResultDTO>> FAT00800GetLastCurrencyRate(FAT00800GetLastCurrencyRateParameterDTO poParameter);
        Task<FAT00800ResultDTO<object>> FAT00800UpdateTransHdStatus(FAT00800UpdateTransHdStatusParameterDTO poParameter);
        Task<FAT00800ResultDTO<object>> FAT00800SubmitTrans(FAT00800SubmitTransParameterDTO poParameter);
    }
}

