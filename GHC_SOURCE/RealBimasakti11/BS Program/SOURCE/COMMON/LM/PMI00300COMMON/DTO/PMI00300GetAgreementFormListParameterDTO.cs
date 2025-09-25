using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300GetAgreementFormListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CBUILDING_ID { get; set; } = string.Empty;
        public string CFLOOR_ID { get; set; } = string.Empty;
        public string CUNIT_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public int IPERIOD_YEAR { get; set; } = 0;
    }
}
