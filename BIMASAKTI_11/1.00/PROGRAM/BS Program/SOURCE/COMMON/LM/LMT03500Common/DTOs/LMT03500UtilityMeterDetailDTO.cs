using System;
using R_APICommonDTO;

namespace LMT03500Common.DTOs
{
    public class LMT03500UtilityMeterDetailDTO:R_APIResultBaseDTO
    {
        public string CFROM_METER_NO { get; set; }
        public string CMETER_END { get; set; }
        public string CTO_METER_NO { get; set; }
        public string CMETER_START_NO { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CAGREEMENT_UNIT_LIST { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CMETER_START { get; set; }

        //--//

        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CUTILITY_TYPE { get; set; }
        public string CUTILITY_TYPE_NAME { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CMETER_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CTAX_CODE { get; set; }
        public string CTAX_NAME { get; set; }
        public string CSTATUS { get; set; }
        public string CINV_PRD { get; set; }
        public string CSTART_INV_PRD { get; set; }
        public string CINV_GRP_CODE { get; set; }
        public string CINV_GRP_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}