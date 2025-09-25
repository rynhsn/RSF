using PMT01700COMMON.DTO._3._LOC._2._LOC;
using PMT01700COMMON.DTO.Utilities;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.Interface.LOC
{
    public interface IPMT01700LOC_LOC : R_IServiceCRUDBase<PMT010700_LOC_LOC_SelectedLOCDTO>
    {
        PMT01700VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE();
    }
}
