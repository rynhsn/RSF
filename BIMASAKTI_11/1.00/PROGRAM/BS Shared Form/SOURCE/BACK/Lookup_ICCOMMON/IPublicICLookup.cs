using System.Collections.Generic;
using Lookup_ICCOMMON.DTOs.ICL00100;
using Lookup_ICCOMMON.DTOs.ICL00200;
using Lookup_ICCOMMON.DTOs.ICL00300;

namespace Lookup_ICCOMMON
{
    public interface IPublicICLookup
    {
        IAsyncEnumerable<ICL00100DTO> ICL00100RequestLookup();
        IAsyncEnumerable<ICL00200DTO> ICL00200RequestNoLookup();
        IAsyncEnumerable<ICL00300DTO> ICL00300TransactionLookup();
        ICL00300PeriodDTO ICLInitiateTransactionLookup();
    }
}