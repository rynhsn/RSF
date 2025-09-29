using System;
using System.Collections.Generic;
using System.Text;

namespace PMB02200COMMON.DTO_s
{
    public class UtilityChargesDbDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get;set;}
        public string CDEPT_CODE { get;set;}
        public string CTRANS_CODE { get;set;}
        public string CREF_NO { get;set;}
        public string CSEQ_NO { get;set;}
        public string CUNIT_ID { get;set;}
        public string CCHARGES_ID { get;set;}
        public string CNEW_CHARGES_ID { get;set;}
        public string CTAX_ID { get;set;}
        public string CNEW_TAX_ID { get; set; } = "";
    }
}
