using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface
{
    public interface IPMT01700LOO_Offer : R_IServiceCRUDBase<PMT01700LOO_Offer_SelectedOfferDTO>
    {
        PMT01700VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE();
        IAsyncEnumerable<PMT01700ResponseTenantCategoryDTO> GetComboBoxDataTenantCategory(PMT01700BaseParameterDTO poParam);
        IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataTaxType();
        IAsyncEnumerable<PMT01700ComboBoxDTO> GetComboBoxDataIDType();
        PMT01700LOO_Offer_TenantDetailDTO GetTenantDetail(PMT01700LOO_Offer_TenantParamDTO poParam);
        PMT01700LOO_Offer_SelectedOfferDTO GetAgreementDetail(PMT01700LOO_Offer_SelectedOfferDTO poParam);
    }
}
