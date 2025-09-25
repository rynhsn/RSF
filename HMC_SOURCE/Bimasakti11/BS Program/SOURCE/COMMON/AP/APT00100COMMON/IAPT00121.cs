using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using APT00100COMMON.DTOs.APT00121;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON
{
    public interface IAPT00121 : R_IServiceCRUDBase<APT00121ParameterDTO>
    {
        IAsyncEnumerable<GetProductTypeDTO> GetProductTypeList();
        APT00121ResultDTO RefreshInvoiceItem(APT00121RefreshParameterDTO poParameter);
    }
}
