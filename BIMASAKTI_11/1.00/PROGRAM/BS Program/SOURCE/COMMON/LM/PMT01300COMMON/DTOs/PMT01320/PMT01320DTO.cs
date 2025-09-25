using System;

namespace PMT01300COMMON
{
    public class PMT01320DTO
    {
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CUTILITY_TYPE_DESCR { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CCHARGES_ID_NAME { get; set; }
        public string CSEQ_NO { get; set; }
        public bool LTAXABLE { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public string CMETER_NO { get; set; }
        public int IMETER_START { get; set; }
        public int IMETER_MAX_RESET { get; set; }
        public string CSTART_INV_PRD { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_DESCR { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public decimal NCAPACITY { get; set; }

        public string CACTIVE_BY { get; set; }
        public DateTime? DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime? DINACTIVE_DATE { get; set; }


        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        //Detail
        public string CAGREEMENT_UNIT_LIST { get; set; }
        public string CINV_PRD { get; set; }
        public string CINV_GRP_NAME { get; set; }
        public string CINV_GRP_CODE { get; set; }
        public string CSTART_DATE { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CYEAR_PERIOD { get; set; }
        public int IBLOCK2_END { get; set; }
        public int IBLOCK1_END { get; set; }
        public int IMETER_END { get; set; }
        public int IBLOCK2_START { get; set; }
        public int IBLOCK1_START { get; set; }
    }
}
