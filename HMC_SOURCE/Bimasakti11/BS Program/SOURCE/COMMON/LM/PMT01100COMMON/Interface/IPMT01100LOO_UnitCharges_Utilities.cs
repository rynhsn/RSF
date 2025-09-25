using PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._2._LOO___Unit___Charges___Utilities;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMT01100Common.Interface
{
    public interface IPMT01100LOO_UnitCharges_Utilities : R_IServiceCRUDBase<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO>
    {
        IAsyncEnumerable<PMT01100LOO_UnitCharges_Utilities_UtilitiesListDTO> GetUtilitiesList();
        IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE();
        IAsyncEnumerable<PMT01100ResponseUtilitiesCMeterNoParameterDTO> GetComboBoxDataCMETER_NO(PMT01100RequestUtilitiesCMeterNoParameterDTO poParameter);
    }
}
