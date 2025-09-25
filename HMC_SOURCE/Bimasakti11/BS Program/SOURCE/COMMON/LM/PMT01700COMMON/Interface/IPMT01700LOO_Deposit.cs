using PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface
{
    public interface IPMT01700LOO_Deposit : R_IServiceCRUDBase<PMT01700LOO_Deposit_DepositDetailDTO>
    {
        PMT01700LOO_Deposit_DepositHeaderDTO GetDepositHeader(PMT01700LOO_UnitUtilities_ParameterDTO poParameter);
        IAsyncEnumerable<PMT01700LOO_Deposit_DepositListDTO> GetDepositList();
        IAsyncEnumerable<PMT01700ResponseCurrencyParameterDTO> GetComboBoxDataCurrency();
    }
}
