using PMT50600COMMON.DTOs.PMT50620;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON
{
    public interface IPMT50620
    {
        OnCloseProcessResultDTO CloseFormProcess(OnCloseProcessParameterDTO poParameter);
    }
}
