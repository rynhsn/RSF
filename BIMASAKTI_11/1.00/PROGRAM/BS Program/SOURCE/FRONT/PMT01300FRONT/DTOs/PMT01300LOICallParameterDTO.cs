using PMT01300COMMON;
using System;

namespace PMT01300FRONT
{
    public class PMT01300LOICallParameterDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CALLER_ACTION { get; set; }
        public bool LPOP_UP_MODE { get; set; }
        public bool LIS_ADD_DATA_LOI { get; set; }
        public bool LCLOSE_ONLY { get; set; }
    }

    public class PMT01300LOICallBackParameterDTO
    {
        public bool CRUD_MODE { get; set; }
        public bool TO_INVOICE_TAB { get; set; }
        public PMT01300DTO SELECTED_DATA_TAB_LOI { get; set; }
        public bool LIS_ADD_DATA_LOI { get; set; }
        public PMT01310DTO SELECTED_DATA_TAB_UNIT { get; set; }
        public bool LIS_LOI_UNIT_HAS_DATA { get; set; }
        public PMT01330DTO SELECTED_DATA_TAB_CHARGES { get; set; }
    }

    public class PMT01300LOIParameterInvoicePlanDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CCHARGE_MODE { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public PMT01330DTO SELECTED_DATA_TAB_CHARGES { get; set; }
    }
}
