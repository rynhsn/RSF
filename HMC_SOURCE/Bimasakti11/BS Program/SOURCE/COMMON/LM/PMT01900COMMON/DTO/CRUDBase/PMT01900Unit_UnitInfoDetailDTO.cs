using BaseAOC_BS11Common.DTO.Utilities.BaseDTO;
using System;

namespace PMT01900Common.DTO.CRUDBase
{
    public class PMT01900Unit_UnitInfoDetailDTO : BaseAOCDateTimeDTO
    {
        //PARAM
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string? COTHER_UNIT_ID { get; set; }
        public string? COTHER_UNIT_NAME { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CLOCATION { get; set; }
        public string? COTHER_UNIT_TYPE_ID { get; set; }
        public string? COTHER_UNIT_TYPE_NAME { get; set; }
        //CUNIT_TYPE_NAME
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; } = 0;
        //
        public string? CCHARGE_MODE { get; set; }
    }
}
