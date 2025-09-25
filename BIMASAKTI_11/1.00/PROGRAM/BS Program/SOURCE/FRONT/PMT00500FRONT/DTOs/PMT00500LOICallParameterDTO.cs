using PMT00500COMMON;
using System;

namespace PMT00500FRONT
{
    public class PMT00500LOICallParameterDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CALLER_ACTION { get; set; }
        public bool LPOP_UP_MODE { get; set; }
        public bool LCLOSE_ONLY { get; set; }
        public bool LIS_ADD_DATA_LOI { get; set; }
        public string VAR_LINK_REF_NO { get; set; } = "";
        public string VAR_LINK_DEPT_CODE { get; set; } = "";
        public string VAR_LINK_TRANS_CODE { get; set; } = "";
        public string VAR_AGRMT_ID { get; set; } = "";
        public string VAR_TRANS_MODE { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public List<PMT00500BuildingUnitDTO> VAR_SELECTED_UNIT_LIST_DATA { get; set; }
        public string BUTTON_FROM_TAB_UNIT_LIST { get; set; } = "";
        public string CREC_ID { get; set; } = "";
    }

    public class PMT00500LOICallBackParameterDTO
    {
        public bool CRUD_MODE { get; set; }
        public bool TO_INVOICE_TAB { get; set; }
        public PMT00500DTO SELECTED_DATA_TAB_LOI { get; set; }
        public bool LIS_ADD_DATA_LOI { get; set; }
        public PMT00510DTO SELECTED_DATA_TAB_UNIT { get; set; }
        public bool LIS_LOI_UNIT_HAS_DATA { get; set; }
        public PMT00530DTO SELECTED_DATA_TAB_CHARGES { get; set; }
    }

    public class PMT00500LOIParameterInvoicePlanDTO
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
        public PMT00530DTO SELECTED_DATA_TAB_CHARGES { get; set; }
    }

    public class PMT00500LeaseAgreement
    {
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREC_ID { get; set; }
    }

    public class PMT00500UnitAgreement
    {
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public bool LVIEW_TRANSACTION { get; set; }
        public string CREF_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREC_ID { get; set; }
    }

    public class PMT00500AddDeleteGrid<T>
    {
        public T DATA { get; set; }
        public string ACTION { get; set; }
    }
}
