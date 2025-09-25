using System.Collections.Generic;
using System.Threading.Tasks;
using GSM02000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM02000Common
{
    public interface IGSM02000Tax : R_IServiceCRUDBase<GSM02000TaxDTO>
    {
        IAsyncEnumerable<GSM02000TaxSalesDTO> GSM02000GetAllSalesTaxListStream();
        IAsyncEnumerable<GSM02000TaxDTO> GSM02000GetAllTaxListStream();
    }
    public interface IGSM02000 : R_IServiceCRUDBase<GSM02000DTO>
    {
        IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream();
        IAsyncEnumerable<GSM02000DeductionGridDTO> GetAllDeductionStream();
        GSM02000ListDTO<GSM02000RoundingDTO> GetAllRounding();
        GSM02000ListDTO<GSM02000PropertyDTO> GetAllProperty();
        GSM02000ActiveInactiveDTO SetActiveInactive(GSM02000ActiveInactiveParamsDTO poParams);
    }
}