using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface
{
    public interface IPMT01700LOO_UnitUtilities_Utilities : R_IServiceCRUDBase<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO>
    {
        IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE();
        IAsyncEnumerable<PMT01700LOO_UnitUtilities_UnitUtilities_UtilitiesDTO> GetUtilitiesList();
        IAsyncEnumerable<PMT01700ResponseUtilitiesCMeterNoParameterDTO> GetComboBoxDataCMETER_NO(PMT01700LOO_UnitUtilities_ParameterDTO poParameter);

    }
}
