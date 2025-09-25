using APT00200COMMON.DTOs.APT00211;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON
{
    public interface IAPT00211
    {
        APT00211DetailResultDTO GetDetailInfo(APT00211DetailParameterDTO poParameter);
        APT00211HeaderResultDTO GetHeaderInfo(APT00211HeaderParameterDTO poParameter);
        IAsyncEnumerable<APT00211ListDTO> GetPurchaseReturnItemList();
    }
}
