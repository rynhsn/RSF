using System;

namespace ICI00200Common.DTOs
{
    public class ICI00200LastInfoDetailDTO
    {
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
        public string CLAST_PO_DATE { get; set; }
        public DateTime? DLAST_PO_DATE { get; set; }
        public decimal NLAST_PO_AMT { get; set; }
        public string CLAST_PURCHASE_DATE { get; set; }
        public DateTime? DLAST_PURCHASE_DATE { get; set; }
        public decimal NLAST_PURCHASE_AMT { get; set; }
        public string CLAST_RECEIPT_DATE { get; set; }
        public DateTime? DLAST_RECEIPT_DATE { get; set; }
        public decimal NLAST_RECEIPT_AMT { get; set; }
        public string CLAST_ISSUE_DATE { get; set; }
        public DateTime? DLAST_ISSUE_DATE { get; set; }
        public decimal NLAST_ISSUE_AMT { get; set; }
        public string CLAST_MOVE_DATE { get; set; }
        public DateTime? DLAST_MOVE_DATE { get; set; }
        public decimal NLAST_MOVE_AMT { get; set; }
        public string CLAST_MOVE_TYPE { get; set; }
        public string CLAST_STOCK_DATE { get; set; }
        public DateTime? DLAST_STOCK_DATE { get; set; }
        public decimal NLAST_STOCK_AMT { get; set; }
        public string CLAST_RECALC_DATE { get; set; }
        public DateTime? DLAST_RECALC_DATE { get; set; }
        public decimal NLAST_RECALC_AMT { get; set; }
        public string CLAST_REPLACE_DATE { get; set; }
        public DateTime? DREPLACEMENT_DATE { get; set; }
        public decimal NREPLACEMENT_COST { get; set; }
    }
}