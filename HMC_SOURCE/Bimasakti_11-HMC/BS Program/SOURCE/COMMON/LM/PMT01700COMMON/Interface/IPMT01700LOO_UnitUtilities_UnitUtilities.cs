using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface
{
    public interface IPMT01700LOO_UnitUtilities_UnitUtilities : R_IServiceCRUDBase<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>
    {
        PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO GetUnitChargesHeader(PMT01700LOO_UnitUtilities_ParameterDTO poParameter);
        IAsyncEnumerable<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> GetUnitInfoList();
    }
}
