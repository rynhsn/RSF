using PQM00500COMMON.DTO_s;
using PQM00500COMMON.DTO_s.Base;
using PQM00500COMMON.DTO_s.Helper;
using System.Collections.Generic;

namespace PQM00500COMMON.Interfaces
{
    public interface IPQM00500
    {
        IAsyncEnumerable<CompanyInfoDTO> GetList_CompanyInfo();
        IAsyncEnumerable<UserDTO> GetList_User();
        GeneralRecordAPIResultDTO<UserDTO> GetRecord_User(UserDTO poParam);
    }
}
