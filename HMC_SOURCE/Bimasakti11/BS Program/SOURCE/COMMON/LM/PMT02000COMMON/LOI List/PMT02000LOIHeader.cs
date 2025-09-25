using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.LOI_List
{
    public class PMT02000LOIHeader : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CTRANS_CODE { get; set; } = "";
        public string? CPROPERTY_NAME { get; set; } = "";
        public string? CLANG_ID { get; set; } = "";
        public string? CBUILDING_ID { get; set; } = "";
        //public string? CFLOOR_ID { get; set; } = "";
        //public string? CUNIT_ID { get; set; } = "";
       // public string? CFLOOR_NAME { get; set; } = "";
        //CR02 --20-02-2024
        public string? CUNIT_DESCRIPTION { get; set; }
        // END CR 02
        public string? CSAVEMODE { get; set; } = "";

        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CHO_REF_NO { get; set; }
        public string? CHO_REF_DATE { get; set; }
        public DateTime? DHO_REF_DATE { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public string? CHAND_OVER_DATE { get; set; }
        public DateTime? DHAND_OVER_DATE { get; set; }
        public string? CHO_ACTUAL_DATE { get; set; }
        public DateTime? DHO_ACTUAL_DATE { get; set; }
        public string? CHO_PLAN_START_DATE { get; set; }
        public DateTime? DHO_PLAN_START_DATE { get; set; }
        public string? CHO_PLAN_END_DATE { get; set; }
        public DateTime? DHO_PLAN_END_DATE { get; set; }
        public decimal NHO_ACTUAL_SIZE { get; set; }

        //for antoher program
        public string CPROGRAM_ID { get; set; } = "";
        public string? CALLER_ACTION { get; set; } = "";
    }
}
