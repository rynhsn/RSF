using System.Collections.Generic;
using GSM04500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM04500Common
{
    public interface IGSM04500AccountSettingDept : R_IServiceCRUDBase<GSM04500AccountSettingDeptDTO>
    {
        IAsyncEnumerable<GSM04500AccountSettingDeptDTO> GSM04500GetAllAccountSettingDeptListStream();
    }
}