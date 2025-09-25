using GSM02500COMMON.DTOs.GSM02550;
using GSM02500COMMON.DTOs.GSM02560;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02560 : R_IServiceCRUDBase<GSM02560ParameterDTO>
    {
        IAsyncEnumerable<GSM02560DTO> GetDepartmentList();
        IAsyncEnumerable<GetDepartmentLookupListDTO> GetDepartmentLookupList();
    }
}
