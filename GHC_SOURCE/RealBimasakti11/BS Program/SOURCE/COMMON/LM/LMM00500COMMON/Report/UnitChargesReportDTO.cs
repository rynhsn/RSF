using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00500Common.Report
{
    public class UnitChargesHeaderDTO
    {
        //HEADER
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
    }

    public class UnitChargersDetail1DTO
    {
        //DETAIL
        public string CCHARGES_TYPE_DESCR { get; set; }
        public List<UnitChargesDetail2DTO> UnitChargesDetail2 { get; set; }
    }

    public class UnitChargesDetail2DTO
    {
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CTAX_EXEMPTION_CODE { get; set; }
        public decimal NTAX_EXEMPTION_PCT { get; set; } 
        public string COTHER_TAX_ID { get; set; }
        public string CWITHHOLDING_TAX_TYPE { get; set; }
        public string CWITHHOLDING_TAX_ID { get; set; }
        public bool LACCRUAL { get; set; }
        public string CSERVICE_JRNGRP_CODE { get; set; }
        public string CSERVICE_JRNGRP_NAME { get; set; }
        public List<UnitChargesDetail3DTO> UnitChargesDetail3 { get; set; }
    }

    public class UnitChargesDetail3DTO
    {
        public bool LDEPARTMENT_MODE { get; set; }
        public string CGOA_CODE { get; set; }
        public string CGOA_NAME { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
    }
}
