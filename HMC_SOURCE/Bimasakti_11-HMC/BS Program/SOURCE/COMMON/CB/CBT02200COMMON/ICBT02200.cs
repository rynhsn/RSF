using CBT02200COMMON.DTO.CBT02200;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON
{
    public interface ICBT02200
    {
        InitialProcessResultDTO InitialProcess();
        IAsyncEnumerable<GetDeptLookupListDTO> GetDeptLookupList();
        IAsyncEnumerable<GetStatusDTO> GetStatusList();
        IAsyncEnumerable<CBT02200GridDTO> GetChequeHeaderList();
        IAsyncEnumerable<CBT02200GridDetailDTO> GetChequeDetailList();
        UpdateStatusResultDTO UpdateStatus(UpdateStatusParameterDTO poParameter);
    }
}
