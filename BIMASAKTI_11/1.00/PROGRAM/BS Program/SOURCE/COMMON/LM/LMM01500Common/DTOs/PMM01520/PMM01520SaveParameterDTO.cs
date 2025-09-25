using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;

namespace PMM01500COMMON
{
    public class PMM01520SaveParameterDTO<T>
    {
        public T poData { get; set; }
        public eCRUDMode poCRUDMode { get; set; }
    }
}
