using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04000Common
{
    public interface IGSM04100 : R_IServiceCRUDBase<UserDepartmentDTO>
    {
        IAsyncEnumerable<UserDepartmentDTO> GetUserDeptList();
        IAsyncEnumerable<UserDepartmentDTO> GetUserList();
    }
}
