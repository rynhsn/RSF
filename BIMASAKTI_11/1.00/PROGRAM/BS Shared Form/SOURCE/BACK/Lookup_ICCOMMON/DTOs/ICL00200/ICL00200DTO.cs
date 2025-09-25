using System;
using R_APICommonDTO;

namespace Lookup_ICCOMMON.DTOs.ICL00200
{
    public class ICL00200DTO : R_APIResultBaseDTO
    {
        public string CREQ_WAREHOUSE_DESCR { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_DESCR { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTYPE { get; set; }
        public string CTYPE_DESCR { get; set; }
        public string CREQUEST_CODE { get; set; }
        public string CREQUEST_NAME { get; set; }
        public string CREQ_WAREHOUSE_ID { get; set; }
        public string CREQ_WAREHOUSE_NAME { get; set; }
        public string CALLOC_ID { get; set; }
        public string CALLOC_NAME { get; set; }
        public string CREQ_ALLOC_ID { get; set; }
        public string CREQ_ALLOC_NAME { get; set; }
        public string CPROV_DEPT_CODE { get; set; }
        public string CPROV_DEPT_NAME { get; set; }
        public string CPROV_WAREHOUSE_ID { get; set; }
        public string CPROV_WAREHOUSE_NAME { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}