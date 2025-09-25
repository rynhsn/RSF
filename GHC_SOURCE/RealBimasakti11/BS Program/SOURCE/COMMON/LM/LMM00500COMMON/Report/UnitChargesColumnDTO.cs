using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00500Common.Report
{
    public class UnitChargesColumnDTO
    {
        public string COL_CHARGES_ID { get; set; } = "Charges Id";
        public string COL_CHARGES_NAME { get; set; } = "Charges Name";
        public string COL_ACTIVE { get; set; } = "Active";
        public string COL_ACCRUAL { get; set; } = "Accrual";
        public string COL_SERVICE_JRNGRP { get; set; } = "Journal Group";

        //Tax Exemption
        public string COL_TAX_EXEMPTION { get; set; } = "Tax Exemption";
        public string COL_TAX_EXEMPTION_CODE { get; set; } = "Tax Code";
        public string COL_TAX_EXEMPTION_PCT { get; set; } = "%";
        public string COL_OTHER_TAX_ID { get; set; } = "Other Tax";

        //Withholding Tax
        public string COL_WITHHOLDING_TAX { get; set; } = "Witholding Tax";
        public string COL_WITHHOLDING_TAX_TYPE { get; set; } = "Tax Type";
        public string COL_WITHHOLDING_TAX_ID { get; set; } = "Tax Id";

        //Account
        public string COL_GOA { get; set; } = "Group of Account";
        public string COL_DEPARTMENT_MODE { get; set; } = "By Dept";

        //GL Account
        public string COL_GLACCOUNT { get; set; } = "GL Account";

    }
}
