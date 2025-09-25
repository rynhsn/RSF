using GSM04000Common.DTO_s;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04000Common
{
    public interface IGSM04000 : R_IServiceCRUDBase<DepartmentDTO>
    {
        IAsyncEnumerable<DepartmentDTO> GetAllDeptList();
        GeneralAPIResultDTO<DepartmentDTO> ActiveInactiveDepartmentAsync(ActiveInactiveParam poParam);
        GeneralAPIResultDTO<DeptUserIsExistDTO> CheckIsUserDeptExist(DepartmentDTO poEntity);
        GeneralAPIResultDTO<DeleteAssignedUser> DeleteDeptUserWhenChangingEveryone(DepartmentDTO poEntity);
        GeneralAPIResultDTO<UploadFileDTO> DownloadUploadDeptTemplate();
    }
}
