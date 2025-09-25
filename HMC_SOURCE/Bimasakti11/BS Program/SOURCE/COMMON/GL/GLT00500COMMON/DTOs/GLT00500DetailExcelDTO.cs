using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON.DTOs
{
    public class GLT00500DetailExcelDTO
    {
        public string Account_No { get; set; } = "";
        public string Account_Name { get; set; } = "";
        public string Center { get; set; } = "";
        public string Center_Code { get; set; } = "";
        public string Db_Cr { get; set; } = "";
        public decimal Amount { get; set; } = 0;
        public decimal Debit_Amount { get; set; } = 0;
        public decimal Credit_Amount { get; set; } = 0;
        public string Description { get; set; } = "";
        public string Voucher_No { get; set; } = "";
        public DateTime Voucher_Date { get; set; }
    }
}
