using System;
using R_APICommonDTO;

namespace Lookup_CBCOMMON.DTOs.CBL00100
{
    public class CBL00100DTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
        public string CPERIOD { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public int  VAR_GSM_PERIOD { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_ID_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public string CTRANS_DESC { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CPAYMENT_TYPE { get; set; }
        public string CPAYMENT_TYPE_NAME { get; set; }
        public string CREC_ID { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public string Month { get; set; }
        public string RadioButton { get; set; }
        
        // public string GLACCOUNT { get => CGLACCOUNT_NO + " - " + CGLACCOUNT_NAME; }
    }
}