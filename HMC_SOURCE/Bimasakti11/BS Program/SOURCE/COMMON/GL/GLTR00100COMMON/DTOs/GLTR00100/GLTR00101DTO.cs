using System;
using System.Collections.Generic;

namespace GLTR00100COMMON
{
    public class GLTR00101DTO
    {
        public string CTRANSACTION_NAME { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public DateTime DDOC_DATE { get; set; }
        public string CREVERSE_DATE { get; set; }
        public DateTime DREVERSE_DATE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public string CSTATUS_NAME { get; set; }

        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
