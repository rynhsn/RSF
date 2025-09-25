using System.Collections.Generic;
using PMT01500Common.DTO._4._Charges_Info;
using PMT01500Common.Utilities;
using R_CommonFrontBackAPI;

namespace PMT01500Common.Interface
{
    public interface IPMT01500ChargesInfo : R_IServiceCRUDBase<PMT01500ChargesInfoDetailDTO>
    {
        PMT01500ChargesInfoHeaderDTO GetChargesInfoHeader(PMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT01500ChargesInfoListDTO> GetChargesInfoList();
        IAsyncEnumerable<PMT01500FrontChargesInfo_FeeCalculationDetailDTO> GetChargesInfoCalUnitList();
        IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCFEE_METHOD();
        IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCINVOICE_PERIOD();
        PMT01500ChargesInfoResultActiveDTO ProcessChangeStatusChargesInfoActive(PMT01500ChargesInfoParameterActiveDTO poParameter);
    }
}