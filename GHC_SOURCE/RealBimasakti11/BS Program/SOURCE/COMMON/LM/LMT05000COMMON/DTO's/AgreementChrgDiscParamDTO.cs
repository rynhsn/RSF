using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON.DTO_s
{
    public class AgreementChrgDiscParamDTO : AgreementChrgDiscHeaderDTO
    {
        public string CBUILDING_NAME { get; set; }
        public string CDISCOUNT_CODE { get; set; }
        public string CDISCOUNT_TYPE { get; set; }
        public List<AgreementChrgDiscDetailBulkProcessDTO> AgreementChrgDiscDetail { get; set; }

    }
}
