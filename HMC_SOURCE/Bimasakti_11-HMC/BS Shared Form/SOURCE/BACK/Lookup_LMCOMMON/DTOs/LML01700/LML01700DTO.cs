using Lookup_PMCOMMON.DTOs.UtilityDTO;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01700
{
    public class LML01700DTO : BaseDTO
    {
        //DTO display
        public string? CPROPERTY_NAME { get; set; }
        public string? CREC_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CPAYMENT_TYPE_NAME { get; set; }
        public string? CCB_CODE { get; set; }
        public string? CCB_NAME { get; set; }
        public string? CCB_ACCOUNT_NO { get; set; }
        public string? CCB_ACCOUNT_NAME { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CLOI_AGRMT_ID { get; set; }
        public string? CLOI_AGRMT_NO { get; set; }
        //DTO from output SP
        public string? CTRANS_CODE { get; set; }
        public string? CREF_PRD { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }    
        public string? CTRANS_STATUS { get; set; }
        public string? CTRANS_STATUS_NAME { get; set; }
        public string? CCHEQUE_STATUS { get; set; }
        public string? CCHEQUE_STATUS_NAME { get; set; }
        public string? CTRANS_DESC { get; set; }
        public string? CDUE_DATE { get; set; }
        public string? CPAYMENT_TYPE { get; set; }  
    }
    public class LML01700InitalProcessDTO : LML00900InitialProcessDTO
    {
    }
    public class LML01700GetMonthDTO
    {
        public string? Id { get; set; }
    }
}
