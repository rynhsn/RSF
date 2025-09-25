using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print.Grouping
{
    public class ProductDTO
    {
        public string CPRODUCT_OR_CHARGE_ID { get; set; }
        public string CPRODUCT_OR_CHARGE_NAME { get; set; }
        public string CPRODUCT_DEPARTMENT_CODE { get; set; }
        public string CPRODUCT_DEPARTMENT_NAME { get; set; }
        public decimal NPRODUCT_QUANTITY { get; set; }
        public string CPRODUCT_MEASUREMENT_NAME { get; set; }
        public decimal NPRODUCT_PRICE_AMOUNT { get; set; }
        public decimal NPRODUCT_LINE_AMOUNT { get; set; }
        public decimal NPRODUCT_DISCOUNT_AMOUNT { get; set; }
        public decimal NOTHER_TAX_AMOUNT { get; set; }
        public decimal NPRODUCT_LINE_TOTAL_AMOUNT { get; set; }
    }
}
