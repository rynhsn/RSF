using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using PMM00500Common.DTOs;
using R_CommonFrontBackAPI;

namespace PMM00500Common
{
    public interface IPMM00500 : R_IServiceCRUDBase<PMM00500DTO>
    {
        IAsyncEnumerable<ChargesTypeDTO> GetAllChargesType();
    }
}
