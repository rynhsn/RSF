using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600COMMON.DTOs.PMT50621;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON
{
    public interface IPMT50621 : R_IServiceCRUDBase<PMT50621ParameterDTO>
    {
        IAsyncEnumerable<GetProductTypeDTO> GetProductTypeList();
        PMT50621ResultDTO RefreshInvoiceItem(PMT50621RefreshParameterDTO poParameter);
    }
}
