using PQM00500COMMON.DTO_s;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PQM00500COMMON.Interfaces
{
    public interface IPQM00511 : R_IServiceCRUDBase<MenuUserDTO>
    {
        IAsyncEnumerable<MenuUserDTO> GetList_UserMenu();
    }
}
