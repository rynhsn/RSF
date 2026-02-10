using System.Collections.Generic;
using System.Threading.Tasks;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using R_CommonFrontBackAPI;

namespace FAT00300Common
{
    public interface IFAT00300 : R_IServiceCRUDAsyncBase<FAT00300DTO>
    {
        // Non-Streaming methods with SP
        Task<FAT00300ResultDTO<FAT00300GetCompanyInfoResultDTO>> GetCompanyInfo(FAT00300GetCompanyInfoParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetSystemParamResultDTO>> GetSystemParam(FAT00300GetSystemParamParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetPeriodInfoResultDTO>> GetPeriodInfo(FAT00300GetPeriodInfoParamDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetTransCodeInfoResultDTO>> GetTransCodeInfo (FAT00300GetTransCodeInfoParamDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetPeriodRangeResultDTO>> GetPeriodRange (FAT00300GetPeriodRangeParamDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300DTO>> DeleteTransaction(FAT00300DTO poParameter);

        // Non-streaming methods
        Task<FAT00300ResultDTO<FAT00300GetValidationDataResultDTO>> GetValidationData(FAT00300GetValidationDataParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetInitialProcessResultDTO>> GetInitialProcess(FAT00300GetInitialProcessParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetAssetInformationTABResultDTO>> GetAssetInformationTAB(FAT00300GetAssetInformationTABParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT00300ValidateDeptCodeParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300ValidateGLJournalAccountResultDTO>> ValidateGLJournalAccount(FAT00300ValidateGLJournalAccountParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetUserCanApproveResultDTO>> GetUserCanApprove(FAT00300GetUserCanApproveParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetUserCanCloseResultDTO>> GetUserCanClose(FAT00300GetUserCanCloseParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(FAT00300GetApprovalPrecheckParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetValidateVoidResultDTO>> GetValidateVoid(FAT00300GetValidateVoidParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetValidateTransDateResultDTO>> GetValidateTransDate(FAT00300GetValidateTransDateParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetValidateOutstandTransResultDTO>> GetValidateOutstandTrans(FAT00300GetValidateOutstandTransParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300SubmitProcessResultDTO>> SubmitProcess(FAT00300SubmitProcessParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300ApproveProcessResultDTO>> ApproveProcess(FAT00300ApproveProcessParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300VoidProcessResultDTO>> VoidProcess(FAT00300VoidProcessParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300CloseProcessResultDTO>> CloseProcess(FAT00300CloseProcessParameterDTO poParameter);
        Task<FAT00300ResultDTO<FAT00300GetAssetResultDTO>> GetAsset(FAT00300GetAssetParameterDTO poParameter);

        // Streaming method
        IAsyncEnumerable<FAT00300GetAllocationExpenseListResultDTO> GetAllocationExpenseList();
        IAsyncEnumerable<FAT00300GetTransListResultDTO> GetAllTransList();
        IAsyncEnumerable<FAT00300GetDeptListResultDTO> GetAllDeptList();
    }
}







