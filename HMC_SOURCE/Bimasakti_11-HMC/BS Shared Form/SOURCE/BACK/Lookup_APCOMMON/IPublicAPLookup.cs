using System;
using System.Collections.Generic;
using System.Text;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.DTOs.APL00500;

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

    }
}
