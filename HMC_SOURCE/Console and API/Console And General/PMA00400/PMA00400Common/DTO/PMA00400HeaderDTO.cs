using System;

namespace PMA00400Common.DTO
{
    public class PMA00400HeaderDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CCARE_TICKET_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public DateTime? DREF_DATE { get; set; }
        public string CCONFIRMED_HO_TIME { get; set; } = string.Empty;
        public string CCONFIRMED_HO_DATE { get; set; } = string.Empty;
        public DateTime? DCONFIRMED_HO_DATE { get; set; }
        public string CCONFIRMED_HO_BY { get; set; } = string.Empty;
        public string CSCHEDULED_HO_DATE { get; set; } = string.Empty;
        public DateTime? DSCHEDULED_HO_DATE { get; set; }
        public string CSCHEDULED_HO_TIME { get; set; } = string.Empty;
        public string CSCHEDULED_HO_BY { get; set; } = string.Empty;
        public int IRESCHEDULE_COUNT { get; set; }
        public string CPROPERTY_NAME { get; set; } = string.Empty;
        public string CBUILDING_ID { get; set; } = string.Empty;
        public string CBUILDING_NAME { get; set; } = string.Empty;
        public string CUNIT_TYPE_CATEGORY_ID { get; set; } = string.Empty;
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; } = string.Empty;
        public string CTENANT_ID { get; set; } = string.Empty;
        public string CTENANT_NAME { get; set; } = string.Empty;
        public string CTENANT_PHONE_NO { get; set; } = string.Empty;
        public string CTENANT_EMAIL { get; set; } = string.Empty;
        public string CHO_ACTUAL_DATE { get; set; } = string.Empty;
        public DateTime? DHO_ACTUAL_DATE { get; set; }
        public bool LFORCED_HO { get; set; } = false;
        public bool LINCLUDE_IMAGE { get; set; } = false;
        public string CUNIT_ID { get; set; } = string.Empty;
        public string CUNIT_NAME { get; set; } = string.Empty;
        public string CFLOOR_ID { get; set; } = string.Empty;
        public string CFLOOR_NAME { get; set; } = string.Empty;
        public string CEMPLOYEE_SIGNATURE_NAME { get; set; } = string.Empty;
        public string CEMPLOYEE_SIGNATURE_STORAGE_ID { get; set; } = string.Empty;
        public string CTENANT_SIGNATURE_STORAGE_ID { get; set; } = string.Empty;
        public byte[]? OData_EMPLOYEE_SIGNATURE { get; set; }
        public byte[]? OData_TENANT_SIGNATURE { get; set; }
    }
}
