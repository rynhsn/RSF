using PMB02200COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMB02200COMMON
{
    public interface IPMB02200General
    {
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<GeneralTypeDTO> GetUtilityTypeList();
        GeneralAPIResultBaseDTO<PMSystemParamDTO> GetPMSysParam();
    }
}
