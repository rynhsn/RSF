using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using APT00200COMMON.DTOs.APT00221;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON
{
    public interface IAPT00221 : R_IServiceCRUDBase<APT00221ParameterDTO>
    {
        IAsyncEnumerable<GetProductTypeDTO> GetProductTypeList();
        APT00221ResultDTO RefreshPurchaseReturnItem(APT00221RefreshParameterDTO poParameter);
    }
}
