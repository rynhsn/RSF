using PQM00500COMMON.DTO_s;
using System.Collections.Generic;

namespace PQM00500COMMON.Interfaces
{
    public interface IPQM00510
    {
        IAsyncEnumerable<MenuDTO> GetList_Menu();
    }
}
