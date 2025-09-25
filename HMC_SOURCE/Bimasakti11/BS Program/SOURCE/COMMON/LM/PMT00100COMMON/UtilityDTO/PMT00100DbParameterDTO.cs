using System;
using System.Collections.Generic;
using System.Text;

namespace PMT00100COMMON.UtilityDTO
{
    public class PMT00100DbParameterDTO : BaseDTO
    {
        public string? CPROPERTY_NAME { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public bool LAGREEMENT { get; set; }
        public string? CBUILDING_ID  { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public string? CUNIT_CATEGORY_LIST { get; set; } //CUNIT_CATEGORY_ID
        public string? CUNIT_CATEGORY_ID { get; set; }
        public string? CUNIT_CATEGORY_NAME{ get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CDEPT_CODE { get; set; }        
        public bool LOTHER_UNIT { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public string? CCALLER_ACTION { get; set; }
        public string? CTRANS_STATUS { get; set; }
    }
}
