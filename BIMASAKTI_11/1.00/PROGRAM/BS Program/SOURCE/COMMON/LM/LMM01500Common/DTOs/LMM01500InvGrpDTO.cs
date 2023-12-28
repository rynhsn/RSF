using System;

namespace LMM01500Common.DTOs
{
    public class LMM01500InvGrpDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CINVGRP_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CSEQUENCE { get; set; }
        public string CINVOICE_TEMPLATE { get; set; }
        public string CINVOICE_DUE_MODE { get; set; }
        public string CINVOICE_GROUP_MODE { get; set; }
        public int IDUE_DAYS { get; set; }
        public int IFIXED_DUE_DATE { get; set; }
        public int ILIMIT_INVOICE_DATE { get; set; }
        public int IBEFORE_LIMIT_INVOICE_DATE { get; set; }
        public int IAFTER_LIMIT_INVOICE_DATE { get; set; }
        public bool LDUE_DATE_TOLERANCE_HOLIDAY { get; set; }
        public bool LDUE_DATE_TOLERANCE_SATURDAY { get; set; }
        public bool LDUE_DATE_TOLERANCE_SUNDAY { get; set; }
        public bool LUSE_STAMP { get; set; }
        public string CSTAMP_ADD_ID { get; set; }
        public string CDESCRIPTION { get; set; }
        public bool LBY_DEPARTMENT { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CBANK_CODE { get; set; }
        public string CBANK_ACCOUNT { get; set; }
        public bool LPENALTY { get; set; }
        public string CPENALTY_ADD_ID { get; set; }
        public string CPENALTY_TYPE { get; set; }
        public decimal NPENALTY_TYPE_VALUE { get; set; }
        public string CPENALTY_TYPE_CALC_BASEON { get; set; }
        public int IROUNDED { get; set; }
        public string CCUTOFDATE_BY { get; set; }
        public int IGRACE_PERIOD { get; set; }
        public string CPENALTY_FEE_START_FROM { get; set; }
        public bool LEXCLUDE_SPECIAL_DAY_HOLIDAY { get; set; }
        public bool LEXCLUDE_SPECIAL_DAY_SATURDAY { get; set; }
        public bool LEXCLUDE_SPECIAL_DAY_SUNDAY { get; set; }
        public decimal NMIN_PENALTY_AMOUNT { get; set; }
        public decimal NMAX_PENALTY_AMOUNT { get; set; }
        public string CSTORAGE_ID { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

        public string CUSER_ID { get; set; }
    }
}