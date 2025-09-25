using APT00100COMMON.DTOs.APT00131;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON
{
    public interface IAPT00131 : R_IServiceCRUDBase<APT00131ParameterDTO>
    {
        IAsyncEnumerable<APT00131DTO> GetAdditionalList();
    }
}
