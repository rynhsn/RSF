using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common.DTOs
{
    public class PMT04200PMSystemParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        
        public string CCUR_RATETYPE_CODE { get; set; }
        public string CCUR_RATETYPE_DESCRIPTION { get; set; }
        
        public string CTAX_RATETYPE_CODE { get; set; }
        public string CTAX_RATETYPE_DESCRIPTION { get; set; }
        
        public bool LBACKDATE { get; set; }
        public bool LGLLINK { get; set; }
        
        public string CSOFT_PERIOD { get; set; }
        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CLSOFT_END_BY { get; set; }
        public string DLSOFT_END_DATE { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public bool LPRD_END_FLAG { get; set; }
        public string CPCPRD_END_BY { get; set; }
        public string CLPRD_END_BY { get; set; }
        public DateTime DLPRD_END_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        
        public bool LALLOW_EDIT_GLLINK { get; set; }
        public string CWHT_MODE { get; set; }
        
    }
}
