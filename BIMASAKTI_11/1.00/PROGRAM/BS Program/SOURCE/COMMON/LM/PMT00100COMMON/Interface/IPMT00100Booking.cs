using PMT00100COMMON.Booking;
using PMT00100COMMON.UnitList;
using PMT00100COMMON.UtilityDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT00100COMMON.Interface
{
    public interface IPMT00100Booking : R_IServiceCRUDBase<PMT00100BookingDTO>
    {
        VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE();
        PMT00100BookingDTO GetAgreementDetail(PMT00100BookingDTO poParam);
        AgreementProcessDTO UpdateAgreement(AgreementProcessDTO poEntity);
    }
}
