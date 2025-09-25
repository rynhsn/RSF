using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.LOI_List
{
    public class PMT02000LOIHeader_DetailDTO
    {
        //For Param
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CTRANS_CODE { get; set; } = "";
        public string? CPROPERTY_NAME { get; set; } = "";
        public string? CLANG_ID { get; set; } = "";
        public string? VAR_LOI_TRANS_CODE { get; set; } = "";
        public string? VAR_TRANS_CODE { get; set; } = "";
        public string? CSAVEMODE { get; set; } = "";
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_ID { get; set; } = "";
        public string? CBUILDING_NAME { get; set; }
        public string CHO_REF_NO { get; set; } = "";
        public string? CREF_NO { get; set; }
        public string? CHO_REF_DATE { get; set; }
        public DateTime? DHO_REF_DATE { get; set; }
        public string? CHAND_OVER_DATE { get; set; }
        public DateTime? DHAND_OVER_DATE { get; set; }
        public string? CHO_ACTUAL_DATE { get; set; }
        public DateTime? DHO_ACTUAL_DATE { get; set; }
        public string? CHO_PLAN_START_DATE { get; set; }
        public DateTime? DHO_PLAN_START_DATE { get; set; }
        public string? CHO_PLAN_END_DATE { get; set; }
        public DateTime? DHO_PLAN_END_DATE { get; set; }
        //CR02
        public string? CUNIT_DESCRIPTION { get; set; }
        public List<PMT02000LOIHandOverUnitDTO>? ListUnit { get; set; }
        public List<PMT02000LOIHandoverUtilityDTO>? ListUtility { get; set; }

    }
}
