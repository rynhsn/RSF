using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100Email
{
    public class PMT02100ParameterRequestEmailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
    }
}
