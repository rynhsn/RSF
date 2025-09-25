using APT00200COMMON.DTOs.APT00220;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON
{
    public interface IAPT00220
    {
        OnCloseProcessResultDTO CloseFormProcess(OnCloseProcessParameterDTO poParameter);
    }
}
