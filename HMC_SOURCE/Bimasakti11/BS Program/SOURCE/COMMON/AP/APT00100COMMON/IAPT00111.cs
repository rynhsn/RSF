using APT00100COMMON.DTOs.APT00111;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON
{
    public interface IAPT00111
    {
        APT00111DetailResultDTO GetDetailInfo(APT00111DetailParameterDTO poParameter);
        APT00111HeaderResultDTO GetHeaderInfo(APT00111HeaderParameterDTO poParameter);
        IAsyncEnumerable<APT00111ListDTO> GetInvoiceItemList();
    }
}
