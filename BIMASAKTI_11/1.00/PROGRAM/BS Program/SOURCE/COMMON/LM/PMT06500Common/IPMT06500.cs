using System.Collections.Generic;
using PMT06500Common.DTOs;
using PMT06500Common.Params;
using R_CommonFrontBackAPI;

namespace PMT06500Common
{
    public interface IPMT06500 : R_IServiceCRUDBase<PMT06500InvoiceDTO>
    {
        IAsyncEnumerable<PMT06500AgreementDTO> PMT06500GetAgreementListStream();
        IAsyncEnumerable<PMT06500OvtDTO> PMT06500GetOvertimeListStream();
        IAsyncEnumerable<PMT06500ServiceDTO> PMT06500GetServiceListStream();
        IAsyncEnumerable<PMT06500UnitDTO> PMT06500GetUnitListStream();
        IAsyncEnumerable<PMT06500InvoiceDTO> PMT06500GetInvoiceListStream();
        IAsyncEnumerable<PMT06500SummaryDTO> PMT06500GetSummaryListStream();
        PMT06500SingleDTO<PMT06500PropertyDTO> PMT06500ProcessSubmit(PMT06500ProcessSubmitParam poParameter);
        PMT06500SingleDTO<PMT06500InvoiceDTO> PMT06500SavingInvoice(SavingInvoiceParamDTO<PMT06500InvoiceDTO> poParameter);
    }

}