using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
