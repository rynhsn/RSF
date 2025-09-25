using CBB00300COMMON.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBB00300COMMON
{
    public interface ICBB00300
    {
        CBB00300ResultDTO GetCashflowInfo();
        GenerateCashflowResultDTO GenerateCashflowProcess(GenerateCashflowParameterDTO poParameter);
    }
}
