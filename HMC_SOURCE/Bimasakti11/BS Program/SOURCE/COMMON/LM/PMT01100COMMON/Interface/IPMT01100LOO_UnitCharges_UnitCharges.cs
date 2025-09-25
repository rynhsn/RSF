using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges;
using PMT01100Common.Utilities.Request;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMT01100Common.Interface
{
    public interface IPMT01100LOO_UnitCharges_UnitCharges : R_IServiceCRUDBase<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO>
    {
        PMT01100LOO_UnitCharges_UnitCharges_UnitChargesHeaderDTO GetUnitChargesHeader(PMT01100LOO_UnitCharges_UnitCharges_RequestBodyUnitChargesHeaderDTO poParameter);
        IAsyncEnumerable<PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO> GetUnitInfoList();
        IAsyncEnumerable<PMT01100LOO_UnitCharges_UnitCharges_ChargesListDTO> GetChargesList();
    }
}
