using System.Collections.Generic;
using PMT01500Common.DTO._5._Invoice_Plan;
using PMT01500Common.Utilities;

namespace PMT01500Common.Interface
{
    public interface IPMT01500InvoicePlan
    {
        PMT01500InvoicePlanHeaderDTO GetInvoicePlanHeader(PMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT01500InvoicePlanChargesListDTO> GetInvoicePlanChargeList();
        IAsyncEnumerable<PMT01500InvoicePlanListDTO> GetInvoicePlanList();
        
    }
}