using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._2._LOO___Offer
{
    public class PMT01700LOO_Offer_SelectedOtherDataUnitListDTO
    {
        //Ini HardCoded
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        //Data Add
        public string? COTHER_UNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; }
        public string? CUSER_ID { get; set; }
    }
}
