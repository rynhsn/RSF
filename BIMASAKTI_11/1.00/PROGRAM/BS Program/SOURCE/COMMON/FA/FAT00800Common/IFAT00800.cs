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
    public interface IFAT00800 : R_IServiceCRUDAsyncBase<FAT00800DTO>
    {
        // Initial Process Methods
        Task<FAT00800ResultDTO<FAT00800GetPeriodResultDTO>> GetPeriod(FAT00800GetPeriodParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>> GetLocalBaseCurr(FAT00800GetLocalBaseCurrParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>> GetTransTypeDesc(FAT00800GetTransTypeDescParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>> GetUserRightApproval(FAT00800GetUserRightApprovalParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>> GetUserActivityRights(FAT00800GetUserActivityRightsParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>> GetValidateDepartment(FAT00800GetValidateDepartmentParameterDTO poParameter);

        // Page 1 Validation Methods
        Task<FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>> GetValidateTransDate(FAT00800GetValidateTransDateParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>> GetValidateOutstandTrans(FAT00800GetValidateOutstandTransParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>> GetValidateVoid(FAT00800GetValidateVoidParameterDTO poParameter);

        // Page 1 Button Methods
        Task<FAT00800ResultDTO<FAT00800DoSubmitResultDTO>> DoSubmit(FAT00800DoSubmitParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800DoCloseResultDTO>> DoClose(FAT00800DoCloseParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetValidateGLResultDTO>> GetValidateGL(FAT00800GetValidateGLParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800DoApproveResultDTO>> DoApprove(FAT00800DoApproveParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800DoVoidResultDTO>> DoVoid(FAT00800DoVoidParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(FAT00800GetApprovalPrecheckParameterDTO poParameter);

        // Page 1 Display Methods
        Task<FAT00800ResultDTO<FAT00800GetBookValueResultDTO>> GetBookValue(FAT00800GetBookValueParameterDTO poParameter);
        Task<FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>> GetCurrency(FAT00800GetCurrencyParameterDTO poParameter);

        // Page 2 Methods
        Task<FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>> GetAssetInfo(FAT00800GetAssetInfoParameterDTO poParameter);

        // Streaming methods
        IAsyncEnumerable<FAT00800GetGridAllocResultDTO> GetGridAlloc();
    }
}

