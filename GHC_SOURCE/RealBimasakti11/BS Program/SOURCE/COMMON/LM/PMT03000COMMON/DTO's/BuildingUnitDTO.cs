using System;
using System.Collections.Generic;
using System.Text;

namespace PMT03000COMMON
{
    public class BuildingUnitDTO : BuildingDTO
    {
        public string CFLOOR_ID { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; }
        public string CUNIT_VIEW_ID { get; set; }
        public string CUNIT_VIEW_NAME { get; set; }
        public string CUNIT_CATEGORY_ID { get; set; }
        public string CUNIT_CATEGORY_NAME { get; set; }
        public string CUNIT_TYPE_ID { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public string CLEASE_STATUS { get; set; }
        public string CLEASE_STATUS_DESC { get; set; }
        public string CLEASE_DEPT_CODE { get; set; }
        public string CLEASE_TRANS_CODE { get; set; }
        public string CLEASE_REF_NO { get; set; }
        public string CLEASE_REC_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public int ITOTAL_LOO { get; set; }
        public string CAGREEMENT_ID { get; set; }
        public string CLEASE_TRX_STATUS { get; set; }
        public string CSTRATA_STATUS { get; set; }
        public string CSTRATA_STATUS_DESC { get; set; }
        public string CSTRATA_DEPT_CODE { get; set; }
        public string CSTRATA_TRANS_CODE { get; set; }
        public string CSTRATA_REF_NO { get; set; }
        public string CSTRATA_REC_ID { get; set; }
        public string CSTRATA_TENANT_ID { get; set; }
        public string CSTRATA_TENANT_NAME { get; set; }
        public int IREINVITATION_COUNT { get; set; }
        public string CSTRATA_TRX_STATUS { get; set; }
        public string CSTRATA_HO_ACTUAL_DATE { get; set; }
    }
}
