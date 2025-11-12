using ICT00900COMMON;
using ICT00900COMMON.DTO;
using ICT00900COMMON.Param;
using ICT00900COMMON.Utility_DTO;
using R_CommonFrontBackAPI;

namespace ICT00900BACK
{
    public interface IICT00900 : R_IServiceCRUDAsyncBase<ICT00900AjustmentDetailDTO>
    {
        Task<VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODE();
        Task<VarGsmCompanyInfoDTO> GetVAR_GSM_COMPANY_INFO();
        IAsyncEnumerable<PropertyDTO> PropertyList();
        IAsyncEnumerable<CurrencyDTO> CurrencyList();
        IAsyncEnumerable<ICT00900AdjustmentDTO> GetAdjustmentList();
        Task<ICT00900AdjustmentDTO> ChangeStatusAdjustment(ICT00900ParameterChangeStatusDTO poEntity);
        Task<ICT00900AdjustmentDTO> SubmitAdjustment(ICT00900ParameterChangeStatusDTO poEntity);
        Task<ICT00900AjustmentDetailDTO> GetProdBalanceInfo(ICT00900AjustmentDetailDTO poEntity);
        Task<ICT00900GenericRecord<ICSystemParameterDTO>> GetICSystemParam(BaseDTO poEntity);
        Task<ICT00900GenericRecord<LastCurrencyRateDTO>> GetLastCurrency(LastCurrencyRateDTO poEntity);
    }
}
