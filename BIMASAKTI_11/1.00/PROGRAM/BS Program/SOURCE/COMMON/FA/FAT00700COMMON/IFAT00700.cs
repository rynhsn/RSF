using FAT00700Common.DTOs;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00700Common
{
    /// <summary>
    /// Interface for FAT00700 - FA Transaction operations
    /// </summary>
    public interface IFAT00700 : R_IServiceCRUDAsyncBase<FAT00700DTO>
    {
        // Non-streaming methods
        Task<FAT00700ResultDTO<GetPeriodResultDTO>> GetPeriod(GetPeriodParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetCurrencyResultDTO>> GetCurrency(GetCurrencyParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetFATransactionDataResultDTO>> GetFATransactionData(GetFATransactionDataParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetAssetInfoDataResultDTO>> GetAssetInfoData(GetAssetInfoDataParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetDateStatusResultDTO>> GetDateStatus(GetDateStatusParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetAssetInformationResultDTO>> GetAssetInformation(GetAssetInformationParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetUserRightApprovalResultDTO>> GetUserRightApproval(GetUserRightApprovalParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetUserActivityRightsResultDTO>> GetUserActivityRights(GetUserActivityRightsParameterDTO poParameter);
        Task<FAT00700ResultDTO<CheckOutstandingTransResultDTO>> CheckOutstandingTrans(CheckOutstandingTransParameterDTO poParameter);
        Task<FAT00700ResultDTO<ValidateVoidResultDTO>> ValidateVoid(ValidateVoidParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(GetApprovalPrecheckParameterDTO poParameter);
        Task<FAT00700ResultDTO<ValidateFoundDeptResultDTO>> ValidateFoundDept(ValidateFoundDeptParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetTransDateValidationResultDTO>> GetTransDateValidation(GetTransDateValidationParameterDTO poParameter);
        Task<FAT00700ResultDTO<GetGridAllocDataResultDTO>> GetGridAllocData(GetGridAllocDataParameterDTO poParameter);
        Task<FAT00700ResultDTO<FAT00700CompanyInfoResultDTO>> GetCompanyInfo(FAT00700CompanyInfoParameterDTO poParameter);
        Task<FAT00700ResultDTO<FAT00700SystemParamResultDTO>> GetSystemParam(FAT00700SystemParamParameterDTO poParameter);
        Task<FAT00700ResultDTO<FAT00700PeriodInfoResultDTO>> GetPeriodInfo(FAT00700PeriodInfoParamDTO poParameter);
        Task<FAT00700ResultDTO<FAT00700TransCodeInfoResultDTO>> GetTransCodeInfo(FAT00700TransCodeInfoParamDTO poParameter);
        Task<FAT00700ResultDTO<FAT00700PeriodRangeResultDTO>> GetPeriodRange(FAT00700PeriodRangeParamDTO poParameter);

        // Streaming methods
        IAsyncEnumerable<GetTransactionListResultDTO> GetTransactionList();
        IAsyncEnumerable<FAT00700GetDeptListResultDTO> GetAllDeptList();

        // Void methods (validation/action only)
        Task ValidateGLJournal(ValidateGLJournalParameterDTO poParameter);
        Task<FAT00700ResultDTO<FAT00700SubmitProcessParameterDTO>> SubmitButton(FAT00700SubmitProcessParameterDTO poParameter);
        Task<FAT00700ResultDTO<FAT00700DTO>> DeleteTransaction(FAT00700DTO poParameter);
        Task CloseButton(CloseButtonParameterDTO poParameter);
        Task ApproveButton(ApproveButtonParameterDTO poParameter);
        Task VoidButton(VoidButtonParameterDTO poParameter);
    }
}

