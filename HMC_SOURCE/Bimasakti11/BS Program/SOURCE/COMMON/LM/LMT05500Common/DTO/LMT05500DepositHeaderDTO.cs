using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace PMT05500COMMON.DTO
{
    public class LMT05500DepositHeaderDTO : R_APIResultBaseDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUNIT_NAME_LIST { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CCURRENCY_CODE { get; set; }
    }
}
