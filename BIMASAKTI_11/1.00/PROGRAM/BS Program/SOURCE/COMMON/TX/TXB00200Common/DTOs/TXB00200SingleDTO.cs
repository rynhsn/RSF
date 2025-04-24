using System;
using R_APICommonDTO;

namespace TXB00200Common.DTOs
{
    public class TXB00200SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

    public class TXB00200SoftClosePeriodToDoListDTO
    {
        public long ISEQ_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_DESCR { get; set; }
        public string CMODULE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_DESCR { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CSOLUTION_DESCR { get; set; }
    }

    public class TXB00200SoftClosePeriodExcelDTO
    {
        public long No { get; set; }
        public string Dept { get; set; }
        public string TransactionType { get; set; }
        public string Module { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Solution { get; set; }
    }

    public class TXB00200DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CPERIOD_YEAR { get; set; }
        public string CPERIOD_MONTH { get; set; }
        public bool LSOFT_CLOSING { get; set; }
        public string CSOFT_CLOSE_PRD_BY { get; set; }
        public DateTime DSOFT_CLOSE_DATE { get; set; }
    }
}