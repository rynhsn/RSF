using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common.DTOs
{
    public class PMT04200GLSystemParamDTO
    {
        public string CCLOSE_DEPT_CODE { get; set; }
        public string CCLOSE_DEPT_NAME { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATETYPE_DESCRIPTION { get; set; }
        public int IREVERSE_JRN_POST { get; set; }
        public bool LCOMMIT_APVJRN { get; set; }
        public bool LCOMMIT_IMPJRN { get; set; }
        public bool LCOMMIT_OTHJRN { get; set; }
        public string CSUSPENSE_ACCOUNT_NO { get; set; }
        public string CSUSPENSE_ACCOUNT_NAME { get; set; }
        public string CRETAINED_ACCOUNT_NO { get; set; }
        public string CRETAINED_ACCOUNT_NAME { get; set; }
        public string CSTART_PERIOD { get; set; }
        public string CSTART_PERIOD_YY { get; set; }
        public string CSTART_PERIOD_MM { get; set; }
        public string CSOFT_PERIOD { get; set; }
        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CLSOFT_END_BY { get; set; }
        public DateTime DLSOFT_END_DATE { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public bool LPRD_END_FLAG { get; set; }
        public string CPCPRD_END_BY { get; set; }
        public string CLPRD_END_BY { get; set; }
        public DateTime DLPRD_END_DATE { get; set; }
        public bool LALLOW_UNDO_COMMIT_JRN { get; set; }
        public bool LALLOW_CANCEL_SOFT_END { get; set; }
        public bool LALLOW_EDIT_IMPJRN_DESC { get; set; }
        public bool LALLOW_EDIT_OTHJRN_DESC { get; set; }
        public bool LALLOW_INTERCO_JRN { get; set; }
        public bool LALLOW_MULTIPLE_JRN { get; set; }
        public bool LWARNING_MULTIPLE_JRN { get; set; }
        public bool LALLOW_DIFF_INTERCO { get; set; }
        public bool LWARNING_DIFF_INTERCO { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
