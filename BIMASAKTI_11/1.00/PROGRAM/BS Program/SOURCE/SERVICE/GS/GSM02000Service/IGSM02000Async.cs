using GSM02000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM02000Service;

public interface IGSM02000Async : R_IServiceCRUDAsyncBase<GSM02000DTO>
{
    IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream();
    IAsyncEnumerable<GSM02000DeductionGridDTO> GetAllDeductionStream();
    Task<GSM02000ListDTO<GSM02000RoundingDTO>> GetAllRounding();
    Task<GSM02000ListDTO<GSM02000PropertyDTO>> GetAllProperty();
    Task<GSM02000ActiveInactiveDTO> SetActiveInactive(GSM02000ActiveInactiveParamsDTO poParams);
}

public interface IGSM02000TaxAsync : R_IServiceCRUDAsyncBase<GSM02000TaxDTO>
{
    IAsyncEnumerable<GSM02000TaxSalesDTO> GSM02000GetAllSalesTaxListStream();
    IAsyncEnumerable<GSM02000TaxDTO> GSM02000GetAllTaxListStream();
}