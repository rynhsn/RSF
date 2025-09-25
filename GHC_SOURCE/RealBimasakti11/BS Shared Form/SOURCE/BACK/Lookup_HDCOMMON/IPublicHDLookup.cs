using System.Collections.Generic;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using Lookup_HDCOMMON.DTOs.HDL00300;
using Lookup_HDCOMMON.DTOs.HDL00400;
using Lookup_HDCOMMON.DTOs.HDL00500;

namespace Lookup_HDCOMMON
{
    public interface IPublicHDLookup
    {
        IAsyncEnumerable<HDL00100DTO> HDL00100PriceListLookup();
        IAsyncEnumerable<HDL00100DTO> HDL00100PriceListDetailLookup();
        IAsyncEnumerable<HDL00200DTO> HDL00200PriceListItemLookup();
        IAsyncEnumerable<HDL00300DTO> HDL00300PublicLocationLookup();
        IAsyncEnumerable<HDL00400DTO> HDL00400AssetLookup();
        IAsyncEnumerable<HDL00500DTO> HDL00500TaskLookup();
    }
}