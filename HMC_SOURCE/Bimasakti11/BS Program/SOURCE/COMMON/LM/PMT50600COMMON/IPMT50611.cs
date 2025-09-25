using PMT50600COMMON.DTOs.PMT50611;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON
{
    public interface IPMT50611
    {
        PMT50611DetailResultDTO GetDetailInfo(PMT50611DetailParameterDTO poParameter);
        PMT50611HeaderResultDTO GetHeaderInfo(PMT50611HeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT50611ListDTO> GetInvoiceItemList();
    }
}
