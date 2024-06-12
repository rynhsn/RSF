using System.Collections.Generic;
using PMI00500Common.DTOs;

namespace PMI00500Common
{
    public interface IPMI00500
    {
        PMI00500ListDTO<PMI00500PropertyDTO> PMI00500GetPropertyList();
        IAsyncEnumerable<PMI00500HeaderDTO> PMI00500GetHeaderListStream();
        IAsyncEnumerable<PMI00500DTAgreementDTO> PMI00500GetDTAgreementListStream();
        IAsyncEnumerable<PMI00500DTReminderDTO> PMI00500GetDTReminderListStream();
        IAsyncEnumerable<PMI00500DTInvoiceDTO> PMI00500GetDTInvoiceListStream();
    }
}