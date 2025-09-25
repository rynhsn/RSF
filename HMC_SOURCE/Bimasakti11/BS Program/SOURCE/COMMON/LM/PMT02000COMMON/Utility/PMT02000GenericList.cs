using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Utility
{
    public class PMT02000GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }

    }
}
