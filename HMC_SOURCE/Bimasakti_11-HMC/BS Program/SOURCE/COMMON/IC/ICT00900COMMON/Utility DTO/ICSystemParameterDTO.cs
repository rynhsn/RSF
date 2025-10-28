using System;

namespace ICT00900COMMON.Utility_DTO
{
    public class ICSystemParameterDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATETYPE_DESCRIPTION { get; set; }
        public string CIC_LINK_DATE { get; set; }
        public DateTime? DIC_LINK_DATE { get; set; }
        public bool LAUTO_CLOSE { get; set; }
        public string CPRD_END_CLOSING_BY { get; set; }
        public string CSOFT_PERIOD { get; set; }
        public bool LSOFT_CLOSING_FLAG { get; set; }
        public bool LGLLINK { get; set; }
        public string CSOFT_CLOSING_BY { get; set; }
        public string CSOFT_PERIOD_YY { get; set; }
        public int CSOFT_PERIOD_YY_INT { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CLSOFT_END_BY { get; set; }
        public DateTime? DLSOFT_END_DATE { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public int CCURRENT_PERIOD_YY_INT { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public bool LPRD_END_FLAG { get; set; }
        public string CLPRD_END_BY { get; set; }
        public bool LRECALCULATE { get; set; }
        public string CRECALCULATE_BY { get; set; }
        public DateTime? DLPRD_END_DATE { get; set; }
        public string CLAST_RECALCULATE_BY { get; set; }
        public string CLAST_RECALCULATE_DATE { get; set; }
        public DateTime? DLAST_RECALCULATE_DATE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
