using System.Collections.Generic;
using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;

namespace Lookup_GLCOMMON
{
    public interface IPublicLookupGL
    {
        IAsyncEnumerable<GLL00100DTO> GLL00100ReferenceNoLookUp();
        IAsyncEnumerable<GLL00110DTO> GLL00110ReferenceNoLookUpByPeriod();
    }
}