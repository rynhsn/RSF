using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON.DTOs.GLT00200
{
    public class GLT00200HeaderDTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public string CTRANS_DESC { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NLBASE_RATE { get; set; } = 0;
        public decimal NLCURRENCY_RATE { get; set; } = 0;
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public decimal NBBASE_RATE { get; set; } = 0;
        public decimal NBCURRENCY_RATE { get; set; } = 0;
        public string CBASE_CURRENCY_CODE { get; set; } = "";
        public decimal NTOTAL_DEBIT { get; set; } = 0;
        public decimal NTOTAL_CREDIT { get; set; } = 0;
        public void Clear()
        {
            CDEPT_CODE = "";
            CREF_NO = "";
            CREF_DATE = "";
            CDOC_NO = "";
            CDOC_DATE = "";
            CTRANS_DESC = "";
            CCURRENCY_CODE = "";
            NLBASE_RATE = 0;
            NLCURRENCY_RATE = 0;
            CLOCAL_CURRENCY_CODE = "";
            NBBASE_RATE = 0;
            NBCURRENCY_RATE = 0;
            CBASE_CURRENCY_CODE = "";
            NTOTAL_DEBIT = 0;
            NTOTAL_CREDIT = 0;
        }
    }
}
