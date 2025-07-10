using System;

namespace ICR00600Common.DTOs.Print
{
    public class ICR00600DataResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME  { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CSTATUS  { get; set; }
        public string CDESCRIPTION  { get; set; }
        public string CWAREHOUSE_ID { get; set; }
        public string CWAREHOUSE_NAME  { get; set; }
        public string CWAREHOUSE  { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME  { get; set; }
        public string CPRODUCT  { get; set; }
        public string CPROD_DEPT_CODE { get; set; }
        public string CPROD_DEPT_NAME  { get; set; }
        public string CPRODUCT_DEPT  { get; set; }
        public decimal NSTOCK_QTY3 {get; set;}
        public string CUNIT3 { get; set; }
        public decimal NSTOCK_QTY2 {get; set;}
        public string CUNIT2 { get; set; }
        public decimal NSTOCK_QTY1 {get; set;}
        public string CUNIT1 { get; set; }
        public decimal NWH_QTY3 {get; set;}
        public decimal NWH_QTY2 {get; set;}
        public decimal NWH_QTY1 {get; set;}
        public decimal NVAR_QTY3 {get; set;}
        public decimal NVAR_QTY2 {get; set;}
        public decimal NVAR_QTY1 {get; set;}
        public decimal NVAR_COST {get; set;}
    }
}