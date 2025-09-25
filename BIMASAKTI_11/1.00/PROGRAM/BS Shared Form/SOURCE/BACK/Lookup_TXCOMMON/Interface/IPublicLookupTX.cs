using Lookup_TXCOMMON.DTOs.TXL00100;
using Lookup_TXCOMMON.DTOs.TXL00200;
using System.Collections.Generic;

namespace Lookup_TXCOMMON.Interface
{
    public interface IPublicLookupTX
    {
        IAsyncEnumerable<TXL00100DTO> TXL00100BranchLookUp();
        IAsyncEnumerable<TXL00200DTO> TXL00200TaxNoLookUp();
    }
}
