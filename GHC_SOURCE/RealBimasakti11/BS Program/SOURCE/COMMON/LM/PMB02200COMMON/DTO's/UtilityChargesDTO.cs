using System;
using System.Collections.Generic;
using System.Text;

namespace PMB02200COMMON.DTO_s
{
    public class UtilityChargesDTO :UtilityChargesDbDTO
    {
        public bool LSELECTED { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CREF_DATE { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CMETER_NO { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CNEW_CHARGES_ID { get; set; }
    }
}
