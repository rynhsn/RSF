using System.Collections.Generic;
using GSM04500Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM04500Common
{
    public interface IGSM04500AccountSetting : R_IServiceCRUDBase<GSM04500AccountSettingDTO>
    {
        IAsyncEnumerable<GSM04500AccountSettingDTO> GSM04500GetAllAccountSettingListStream();
    }
}