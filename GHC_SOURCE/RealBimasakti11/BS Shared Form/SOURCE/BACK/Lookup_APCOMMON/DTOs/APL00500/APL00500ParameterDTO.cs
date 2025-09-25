namespace Lookup_APCOMMON.DTOs.APL00500
{
    public class APL00500ParameterDTO 
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CPERIOD { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CTRANS_NAME { get; set; } = "";
        public bool LHAS_REMAINING { get; set; } = false;
        public bool LNO_REMAINING { get; set; } = true;
        public string CLANGUAGE_ID { get; set; } = "";
        
        public string CYEAR { get; set; } = "";
        public string CMODE { get; set; } = "";
        public string CSEARCH_CODE { get; set; } = "";
    }
}