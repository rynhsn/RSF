using System;
using R_APICommonDTO;

namespace Lookup_ICCOMMON.DTOs.ICL00300
{
    public class ICL00300DTO
    {
        public string CPERIOD { get; set; }
        public int VAR_GSM_PERIOD { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CREC_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CSTATUS { get; set; }
        public string CSTOCKTAKE_BY { get; set; }
        public string CWAREHOUSE_ID { get; set; }
        public string CWAREHOUSE_NAME { get; set; }
        public string CTRANS_DESC { get; set; }
        public string Code { get; set; } 
        public string Desc { get; set; }
        public string RadioButton { get; set; } 
        public string Month { get; set; }
        public string CWAREHOUSE
        {
            get => CWAREHOUSE_ID + " - " + CWAREHOUSE_NAME;
        }
    }
    public class ICL00300PeriodDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}