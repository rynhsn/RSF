using R_APICommonDTO;
using System.Collections.Generic;

namespace PMT01100Common.Utilities
{
    public class PMT01100UtilitiesListDTO<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}
