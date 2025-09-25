using PMT03000COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace PMT03000COMMON
{
    public class TenantUnitFacilityDTO : UnitTypeCtgFacilityDTO
    {
        public string CTENANT_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CSEQUENCE { get; set; }
        public string CREGIST_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime? DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime? DINACTIVE_DATE { get; set; }
        public string CACTION { get; set; }
        public string CUSER_ID { get; set; }
    }
}
