using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit;
using PMT01700COMMON.DTO.Utilities;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface
{
    public interface IPMT01700LOO_UnitUtilities_Charges : R_IServiceCRUDBase<PMT01700LOO_UnitCharges_ChargesDetailDTO>
    {
        IAsyncEnumerable<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> GetDetailItemList();
        IAsyncEnumerable<PMT01700LOO_UnitCharges_ChargesListDTO> GetChargesList();
        IAsyncEnumerable<PMT01700ComboBoxDTO> GetFeeMethodList();
        IAsyncEnumerable<PMT01700ComboBoxDTO> GetPeriodModeList();
    }
}
