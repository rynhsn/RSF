using CBT02200COMMON.DTO.CBT02200;
using CBT02200COMMON.DTO.CBT02210;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON
{
    public interface ICBT02210 : R_IServiceCRUDBase<CBT02210ParameterDTO>
    {
        UpdateStatusResultDTO UpdateStatus(UpdateStatusParameterDTO poParameter);
        RefreshCurrencyRateResultDTO RefreshCurrencyRate(RefreshCurrencyRateParameterDTO poParameter);
    }
}
