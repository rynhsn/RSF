using System.Collections.Generic;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;

namespace Lookup_CBCOMMON
{
    public interface IPublicCBLookup
    {
        CBL00100DTO CBL00100InitialProcessLookup();
        CBL00100DTO InitialProcessCBL00100Month();
        IAsyncEnumerable<CBL00100DTO> CBL00100ReceiptFromCustomerLookup();
        IAsyncEnumerable<CBL00200DTO> CBL00200JournalLookup();
    }
}