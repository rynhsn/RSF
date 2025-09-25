using APT00200COMMON.DTOs.APT00231;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON
{
    public interface IAPT00231 : R_IServiceCRUDBase<APT00231ParameterDTO>
    {
        IAsyncEnumerable<APT00231DTO> GetAdditionalList();
    }
}
