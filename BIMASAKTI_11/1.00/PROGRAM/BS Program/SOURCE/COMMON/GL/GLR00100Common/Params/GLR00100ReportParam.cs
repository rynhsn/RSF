namespace GLR00100Common.Params
{
    public class GLR00100ReportParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CREPORT_TYPE { get; set; }
        public string CFROM_DEPT_CODE { get; set; } = "";
        public string CFROM_DEPT_NAME { get; set; }
        public string CTO_DEPT_CODE { get; set; } = "";
        public string CTO_DEPT_NAME { get; set; }
        public string CPERIOD_TYPE { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CTRANS_CODE { get; set; } = "";
        public string CTRANSACTION_NAME { get; set; }
        public string CFROM_REF_NO { get; set; } = "";
        public string CTO_REF_NO { get; set; } = "";
        public string CSORT_BY { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CCURRENCY_TYPE_NAME { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CREPORT_CULTURE { get; set; }
        
        public bool LTOTAL_BY_REF_NO { get; set; } = true;
        public bool LTOTAL_BY_DEPT { get; set; } = true;
        
        
        public string CREPORT_FILETYPE { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
        public bool LIS_PRINT { get; set; } = true;
    }
}