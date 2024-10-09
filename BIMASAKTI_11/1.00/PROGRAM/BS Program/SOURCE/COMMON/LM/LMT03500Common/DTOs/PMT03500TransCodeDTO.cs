using System;

namespace PMT03500Common.DTOs
{
    public class PMT03500TransCodeDTO
    {
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CMODULE_ID { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CDEPT_DELIMITER { get; set; }
        public bool LTRANSACTION_MODE { get; set; }
        public string CTRANSACTION_DELIMITER { get; set; }
        public string CPERIOD_MODE { get; set; }
        public string CPERIOD_DELIMITER { get; set; }
        public string CYEAR_FORMAT { get; set; }
        public int INUMBER_LENGTH { get; set; }
        public string CNUMBER_DELIMITER { get; set; }
        public string CPREFIX { get; set; }
        public string CPREFIX_DELIMITER { get; set; }
        public string CSUFFIX { get; set; }
        public string CSEQUENCE01 { get; set; }
        public string CSEQUENCE02 { get; set; }
        public string CSEQUENCE03 { get; set; }
        public string CSEQUENCE04 { get; set; }
        public bool LAPPROVAL_FLAG { get; set; }
        public bool LUSE_THIRD_PARTY { get; set; }
        public string CAPPROVAL_MODE { get; set; }
        public string CAPPROVAL_MODE_DESCR { get; set; }
        public bool LAPPROVAL_DEPT { get; set; }
        public string CTABLE_NAME { get; set; }
        public string CAFTER_APPROVAL_STATUS { get; set; }
        public string CPROGRAM_ID { get; set; }
        public string CREPORT_ID { get; set; }
        public string CTHIRD_PARTY_VIEW_URL { get; set; }
        public string CTHIRD_PARTY_API_URL { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class PMT03500RateWGListDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public int IUP_TO_USAGE { get; set; }
        public string CUSAGE_DESC { get; set; }
        public decimal NUSAGE_CHARGE { get; set; }
        public decimal NBUY_USAGE_CHARGE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

        public int IMIN_USAGE { get; set; }
        public decimal NUSAGE { get; set; }
        public decimal NSUB_TOTAL_ROW { get; set; }
    }

    public class PMT03500SystemParamDTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CCUR_RATETYPE_CODE { get; set; } = "";
        public string CCUR_RATETYPE_DESCRIPTION { get; set; } = "";
        public string CTAX_RATETYPE_CODE { get; set; } = "";
        public string CTAX_RATETYPE_DESCRIPTION { get; set; } = "";
        public bool LBACKDATE { get; set; }
        public bool LGLLINK { get; set; }
        public string CSOFT_PERIOD { get; set; } = "";
        public string CSOFT_PERIOD_YY { get; set; } = "";
        public string CSOFT_PERIOD_MM { get; set; } = "";
        public string CLSOFT_END_BY { get; set; } = "";
        public DateTime? DLSOFT_END_DATE { get; set; }
        public string CCURRENT_PERIOD { get; set; } = "";
        public string CCURRENT_PERIOD_YY { get; set; } = "";
        public string CCURRENT_PERIOD_MM { get; set; } = "";
        public bool LPRD_END_FLAG { get; set; }
        public string CPCPRD_END_BY { get; set; } = "";
        public string CLPRD_END_BY { get; set; } = "";
        public DateTime? DLPRD_END_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public bool LALLOW_EDIT_GLLINK { get; set; }
        public string CWHT_MODE { get; set; } = "";
        public decimal NRECEIPT_CR_ADJ_AMT { get; set; }
        public decimal NRECEIPT_DB_ADJ_AMT { get; set; }
        public string CRECEIPT_CR_ADJ_CHARGES_ID { get; set; } = "";
        public string CRECEIPT_DB_ADJ_CHARGES_ID { get; set; } = "";
        public bool LINV_PROCESS_FLAG { get; set; }
        public string CELECTRIC_PERIOD { get; set; } = "";
        public string CELECTRIC_DATE { get; set; } = "";
        public bool LELECTRIC_END_MONTH { get; set; }
        public string CWATER_PERIOD { get; set; } = "";
        public string CWATER_DATE { get; set; } = "";
        public bool LWATER_END_MONTH { get; set; }
        public string CGAS_PERIOD { get; set; } = "";
        public string CGAS_DATE { get; set; } = "";
        public bool LGAS_END_MONTH { get; set; }
        public string CCURRENCY { get; set; } = "";
    }
}