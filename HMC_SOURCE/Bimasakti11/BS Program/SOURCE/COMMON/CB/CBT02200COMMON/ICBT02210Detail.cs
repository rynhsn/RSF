using CBT02200COMMON.DTO.CBT02210;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON
{
    public interface ICBT02210Detail : R_IServiceCRUDBase<CBT02210DetailParameterDTO>
    {
        IAsyncEnumerable<CBT02210DetailDTO> GetChequeEntryDetailList();
        IAsyncEnumerable<GetCenterListDTO> GetCenterList();
    }
}
