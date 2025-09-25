using PMT03000COMMON.DTO_s.Helper;
using System;

namespace PMT03000COMMON.DTO_s
{
    public class UnitTypeCtgFacilityDTO: GeneralParamDTO
    {
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CFACILITY_TYPE { get; set; }
        public string CFACILITY_TYPE_DESCR { get; set; }
        public bool LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
