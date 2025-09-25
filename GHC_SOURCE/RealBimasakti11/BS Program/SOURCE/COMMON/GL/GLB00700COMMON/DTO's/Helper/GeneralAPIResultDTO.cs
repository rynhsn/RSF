using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GLB00700COMMON.DTO_s.Helper
{
    public class GeneralAPIResultDTO<T> : R_APIResultBaseDTO
    {
        public T data { get; set; }
    }
}
