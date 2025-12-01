using R_CommonFrontBackAPI;
using FAT00100Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00100Common
{
    /// <summary>
    /// Interface for FAT00100 - Fixed Asset Transaction operations
    /// </summary>
    public interface IFAT00100 : R_IServiceCRUDAsyncBase<FAT00100DTO>
    {
        // Non-streaming methods
        Task<FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>> GetDeptLookUpValidation(FAT00100GetDeptLookUpValidationParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>> GetInitialProcess(FAT00100GetInitialProcessParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>> GetPeriodYear(FAT00100GetPeriodYearParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT00100ValidateDeptCodeParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>> GetPeriodDT(FAT00100GetPeriodDTParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>> RSP_GET_CURRENCY_RATE(FAT00100RSP_GET_CURRENCY_RATEParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>> SubmitProcess(FAT00100SubmitProcessParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100CloseProcessResultDTO>> CloseProcess(FAT00100CloseProcessParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>> ApproveProcess(FAT00100ApproveProcessParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>> ValidationAssetCode(FAT00100ValidationAssetCodeParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>> RunApprovalPrecheck(FAT00100RunApprovalPrecheckParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100VoidProcessResultDTO>> VoidProcess(FAT00100VoidProcessParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>> ValidationBeforeSubmit(FAT00100ValidationBeforeSubmitParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>> ValidationBeforeClose(FAT00100ValidationBeforeCloseParameterDTO poParameter);
        Task<FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>> ValidatePJTrans(FAT00100ValidatePJTransParameterDTO poParameter);

        // Streaming methods
        IAsyncEnumerable<FAT00100GetComboPeriodMonthResultDTO> GetComboPeriodMonth();
        IAsyncEnumerable<FAT00100GetAssetListResultDTO> GetAssetList();
        IAsyncEnumerable<FAT00100GetDataGridResultDTO> GetDataGrid();
        IAsyncEnumerable<FAT00100GetGSM_SUPPLIER_INFOResultDTO> GetGSM_SUPPLIER_INFO();
        IAsyncEnumerable<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO> GetGSM_SUPPLIER_CONTACT();
    }
}

