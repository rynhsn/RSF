namespace ICI00200Common.DTOs
{
    public class ICI00200ProductDetailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_NAME { get; set; }
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CUNIT1 { get; set; }
        public string CUNIT2 { get; set; }
        public string CUNIT3 { get; set; }
        public string CPURCHASE_UNIT { get; set; }
        public decimal NCONV_FACTOR1 { get; set; }
        public decimal NCONV_FACTOR2 { get; set; }
        // public decimal NUNIT_COST { get; set; }
        public int ILEAD_TIME { get; set; }
        public string CPRODUCT_DESC { get; set; }
        public string CSTORAGE_ID { get; set; }
        public byte[]? OIMAGE { get; set; }
    }
}