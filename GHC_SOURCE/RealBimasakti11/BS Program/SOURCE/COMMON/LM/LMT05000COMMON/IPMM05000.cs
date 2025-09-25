using PMT05000COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON
{
    public interface IPMM05000
    {
        IAsyncEnumerable<AgreementChrgDiscDetailDTO> GetAgreementChargesDiscountList();
        AgreementChrgDiscResultDTO ProcessAgreementChargeDiscount(AgreementChrgDiscParamDTO poParam);

    }
}
