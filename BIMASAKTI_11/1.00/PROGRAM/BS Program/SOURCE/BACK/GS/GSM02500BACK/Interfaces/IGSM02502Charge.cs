using GSM02500COMMON.DTOs.GSM02502Charge;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02502Charge : R_IServiceCRUDAsyncBase<GSM02502ChargeParameterDTO>
    {
        IAsyncEnumerable<GSM02502ChargeDTO> GetChargeList();
        IAsyncEnumerable<GSM02502ChargeComboboxDTO> GetChargeComboBoxList();
    }
}
