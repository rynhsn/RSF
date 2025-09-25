using System.Collections.Generic;

namespace PMT02000COMMON.LOI_List
{
    public class PMT02000LOIHandOverUnitDTO
    {
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public decimal NGROSS_SIZE { get; set; }
        public decimal NNET_SIZE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        //public List<PMT02000LOIHandoverUtilityDTO>? ListUtility { get; set; }

    }
}
