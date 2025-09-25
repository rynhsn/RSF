using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges
{
    public class PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO
    {
        //PARAM
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string? COTHER_UNIT_ID { get; set; }
        public string? COTHER_UNIT_NAME { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CLOCATION { get; set; }
        public string? COTHER_UNIT_TYPE_ID { get; set; }
        public string? COTHER_UNIT_TYPE_NAME { get; set; }
        //CUNIT_TYPE_NAME
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; } = 0;
        //
        public string? CCHARGE_MODE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}
