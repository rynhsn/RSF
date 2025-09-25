using PMT01100Common.DTO._2._LOO._4._LOO___Deposit;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMT01100Common.Interface
{
    public interface IPMT01100LOO_Deposit : R_IServiceCRUDBase<PMT01100LOO_Deposit_DepositDetailDTO>
    {
        PMT01100LOO_Deposit_DepositHeaderDTO GetDepositHeader(PMT01100LOO_Deposit_RequestDepositHeaderDTO poParameter);
        IAsyncEnumerable<PMT01100LOO_Deposit_DepositListDTO> GetDepositList();
        IAsyncEnumerable<PMT01100ResponseUtilitiesCurrencyParameterDTO> GetComboBoxDataCurrency();
    }
}
