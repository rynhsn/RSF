using System.Collections.Generic;
using PMT01500Common.DTO._3._Unit_Info;
using PMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace PMT01500Common.Interface
{
    public interface IPMT01500UnitInfo_Utilities : R_IServiceCRUDBase<PMT01500UnitInfoUnit_UtilitiesDetailDTO>
    {
        IAsyncEnumerable<PMT01500UnitInfoUnit_UtilitiesListDTO> GetUnitInfoList();
        IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE();
        IAsyncEnumerable<PMT01500ComboBoxStartInvoicePeriodYearDTO> GetComboBoxDataCSTART_INV_PRDYear();
        IAsyncEnumerable<PMT01500ComboBoxStartInvoicePeriodMonthDTO> GetComboBoxDataCSTART_INV_PRDMonth();
        IAsyncEnumerable<PMT01500ComboBoxCMeterNoDTO> GetComboBoxDataCMETER_NO(PMT01500GetUnitInfo_UtilitiesCMeterNoParameterDTO poParameter);
    }
}