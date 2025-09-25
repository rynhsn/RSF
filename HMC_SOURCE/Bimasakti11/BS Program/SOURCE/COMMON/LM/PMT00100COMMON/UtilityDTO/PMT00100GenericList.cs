using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT00100COMMON.UtilityDTO
{
    public class PMT00100GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}
