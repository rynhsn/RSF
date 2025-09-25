using System.Collections.Generic;
using R_APICommonDTO;

namespace Lookup_GLCOMMON.DTOs
{
    public class GLLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}