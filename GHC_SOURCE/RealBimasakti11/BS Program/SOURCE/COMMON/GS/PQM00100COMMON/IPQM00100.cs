using PQM00100COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PQM00100COMMON
{
    public interface IPQM00100 : R_IServiceCRUDBase<ServiceGridDTO>
    {
        IAsyncEnumerable<ServiceGridDTO> GetList_Service();
    }
}