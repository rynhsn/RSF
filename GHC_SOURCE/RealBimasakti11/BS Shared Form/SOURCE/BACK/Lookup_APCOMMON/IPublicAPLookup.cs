using System;
using System.Collections.Generic;
using System.Text;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APCOMMON.DTOs.APL00600;
using Lookup_APCOMMON.DTOs.APL00700;

namespace Lookup_APCOMMON
{
    public interface IPublicAPLookup
    {
        IAsyncEnumerable<APL00100DTO> APL00100SupplierLookUp();
        IAsyncEnumerable<APL00110DTO> APL00110SupplierInfoLookUp();
        IAsyncEnumerable<APL00200DTO> APL00200ExpenditureLookUp();
        IAsyncEnumerable<APL00300DTO> APL00300ProductLookUp();
        IAsyncEnumerable<APL00400DTO> APL00400ProductAllocationLookUp();
        IAsyncEnumerable<APL00500DTO> APL00500TransactionLookup();
        APL00500PeriodDTO APLInitiateTransactionLookup();
        IAsyncEnumerable<APL00600DTO> APL00600ApSchedulePaymentLookup();
        IAsyncEnumerable<APL00600DTO> APL00600ApInvoiceListLookup();
        IAsyncEnumerable<APL00700DTO> APL00700CancelPaymentToSupplierLookup();
        APL00700DTO APL00700InitialProcess();

    }
}
