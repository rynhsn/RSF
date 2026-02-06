using Lookup_FACommon.DTOs;
using System.Collections.Generic;

namespace Lookup_FACommon
{
    public interface IPublicLookupFA
    {
        public IAsyncEnumerable<FAL00100DTO> FAL00100TaxTypeLookup();
        public IAsyncEnumerable<FAL00200DTO> FAL00200TaxCategoryLookup();
        public IAsyncEnumerable<FAL00300DTO> FAL00300AssetLookup();
    }
}
