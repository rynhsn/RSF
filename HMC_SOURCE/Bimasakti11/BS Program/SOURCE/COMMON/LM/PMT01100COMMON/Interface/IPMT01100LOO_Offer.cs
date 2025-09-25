using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Request;
using PMT01100Common.Utilities.Response;
using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace PMT01100Common.Interface
{
    public interface IPMT01100LOO_Offer : R_IServiceCRUDBase<PMT01100LOO_Offer_SelectedOfferDTO>
    {
        PMT01100VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE();
        IAsyncEnumerable<PMT01100ResponseTenantCategoryDTO> GetComboBoxDataTenantCategory(PMT01100RequestTenantCategoryDTO poParam);
        IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataTaxType();
        IAsyncEnumerable<PMT01100ComboBoxDTO> GetComboBoxDataIDType();
    }
}
