using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON.DTOs.GLT00200
{
    public class BulkInsertParameterDTO
    {
        //Misc
        public string CPROCESS_ID { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";

        //Header
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public string CTRANS_DESC { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NLBASE_RATE { get; set; } = 0;
        public decimal NLCURRENCY_RATE { get; set; } = 0;
        public decimal NBBASE_RATE { get; set; } = 0;
        public decimal NBCURRENCY_RATE { get; set; } = 0;

        //Detail
        public int INO { get; set; } = 0;
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public string CDBCR { get; set; } = "";
        public decimal NTRANS_AMOUNT { get; set; } = 0;
        public decimal NLTRANS_AMOUNT { get; set; } = 0;
        public decimal NBTRANS_AMOUNT { get; set; } = 0;
        public string CDETAIL_DESC { get; set; } = "";
        public string CDOCUMENT_NO { get; set; } = "";
        public string CDOCUMENT_DATE { get; set; }
    }
}
