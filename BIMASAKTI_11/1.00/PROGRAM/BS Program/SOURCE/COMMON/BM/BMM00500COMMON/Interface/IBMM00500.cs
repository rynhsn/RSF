using BMM00500COMMON.DTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMM00500COMMON.Interface
{
    public interface IBMM00500 : R_IServiceCRUDBase<BMM00500CRUDParameterDTO>
    {
        IAsyncEnumerable<BMM00500DTO> GetMobileProgramList();
    }
}
