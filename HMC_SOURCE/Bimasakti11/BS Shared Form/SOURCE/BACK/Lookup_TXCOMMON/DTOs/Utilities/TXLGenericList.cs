using R_APICommonDTO;
using System.Collections.Generic;

namespace Lookup_TXCOMMON.DTOs.Utilities
{
    public class TXLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}
