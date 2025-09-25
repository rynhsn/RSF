using GSM00300COMMON.DTO_s;
using GSM00300COMMON.DTO_s.Helper;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00300COMMON
{
    public interface IGSM00300 : R_IServiceCRUDBase<CompanyParamRecordDTO>
    {
        GeneralAPIResultDTO<CheckPrimaryAccountDTO> CheckIsPrimaryAccount();
        GeneralAPIResultDTO<ValidateCompanyDTO> CheckIsCompanyParamEditable();
    }
}
