using APT00100COMMON.DTOs.APT00120;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON
{
    public interface IAPT00120
    {
        OnCloseProcessResultDTO CloseFormProcess(OnCloseProcessParameterDTO poParameter);
    }
}
