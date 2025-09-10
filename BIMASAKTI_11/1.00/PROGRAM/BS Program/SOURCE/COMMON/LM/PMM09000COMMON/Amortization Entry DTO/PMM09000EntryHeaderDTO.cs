using System;
using PMM09000COMMON.UtiliyDTO;
using R_APICommonDTO;

namespace PMM09000COMMON.Amortization_Entry_DTO
{
    public class PMM09000EntryHeaderDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
      //  public string? CBUILDING_CODE { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string CDESCRIPTION { get; set; } = "";
        public string? CTRANS_DEPT_NAME { get; set; }
        public string CAMORTIZATION_NO { get; set; } = "";
        public string? CTRANS_DEPT_CODE { get; set; }
        public bool LCUT_OFF_PRD { get; set; }
        public string? CPERIOD_YEAR { get; set; }
        public int IPERIOD_YEAR { get; set; }
        public string? CPERIOD_MONTH { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string CCHARGES_ID { get; set; } = "";
        public string? CCHARGES_NAME { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
        public string? CSTATUS { get; set; }

        //For Parameter
        public string? CUNIT_OPTION { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CTRANS_TYPE { get; set; }
        public string? CAGREEMENT_NO { get; set; }
        public string? CCUT_OF_PRD { get; set; }
        public string? CCHARGE_ACCRUAL { get; set; }

        public string? CTRANS_CODE{ get; set; }
    }
}
