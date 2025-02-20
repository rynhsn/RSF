using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMM01500COMMON
{
    public interface IPMM01500 : R_IServiceCRUDBase<PMM01500DTO>
    {
        IAsyncEnumerable<PMM01500DTOPropety> GetProperty();

        IAsyncEnumerable<PMM01501DTO> GetInvoiceGrpList();
        
        IAsyncEnumerable<PMM01500DTOInvTemplate> GetInvoiceTemplate();
        IAsyncEnumerable<PMM01500StampRateDTO> GetStampRateList();

        PMM01500SingleResult<PMM01500DTO> PMM01500ActiveInactive(PMM01500DTO poParam);

        IAsyncEnumerable<PMM01502DTO> PMM01500LookupBank();

        PMM01500SingleResult<bool> CheckDataTabTemplateBank(PMM01500DTO poParam);

        IAsyncEnumerable<PMM01500UniversalDTO> GetAllUniversalList();

    }

}
