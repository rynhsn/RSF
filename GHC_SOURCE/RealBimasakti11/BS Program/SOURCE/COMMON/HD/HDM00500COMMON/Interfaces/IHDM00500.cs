using HDM00500COMMON.DTO.General;
using HDM00500COMMON.DTO_s;
using HDM00500COMMON.DTO_s.Helper;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace HDM00500COMMON.Interfaces
{
    public interface IHDM00500 : R_IServiceCRUDBase<TaskchecklistDTO>
    {
        IAsyncEnumerable<PropertyDTO> GetList_Property();
        IAsyncEnumerable<TaskchecklistDTO> GetList_Taskchecklist();
        GeneralAPIResultBaseDTO<object> ActiveInactive_Taskchecklist(TaskchecklistDTO poParam);
    }
}
