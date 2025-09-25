using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace Lookup_APCOMMON.DTOs.APL00700
{
    public class APL00700DTO  : R_APIResultBaseDTO
    {
        public string CSCH_PAYMENT_DATE { get; set; }
        public DateTime? DSCH_PAYMENT_DATE { get; set; }
        public int IYEAR { get; set; }
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREC_ID { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public string CCHEQUE_STATUS { get; set; }
        public string CCHEQUE_STATUS_NAME { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CDUE_DATE { get; set; }
        public string CPAYMENT_TYPE { get; set; }
        public string CPAYMENT_TYPE_NAME { get; set; }
        public string CCB_CODE { get; set; }
        public string CCB_NAME { get; set; }
        public string CCB_ACCOUNT_NO { get; set; }
        public string CCB_ACCOUNT_NAME { get; set; }
        public string CSCH_PAYMENT_ID { get; set; } = "";
        public string CSCH_PAYMENT_NO { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public string Month { get; set; }
    }

    public class APL00700ListDTO
    {
        public List<APL00700DTO> Data { get; set; }
    }
}