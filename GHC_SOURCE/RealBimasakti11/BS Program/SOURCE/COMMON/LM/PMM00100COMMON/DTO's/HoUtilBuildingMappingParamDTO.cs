using PMM00100COMMON.DTO_s.Helper;
using System;

namespace PMM00100COMMON.DTO_s
{
    public class HoUtilBuildingMappingParamDTO : GeneralParamDTO
    {
        public string CBUILDING_ID { get; set; }
        public bool LALL_BUILDING { get; set; }
        public bool LELECTRICITY { get; set; } = false;
        public string CELECTRICITY_CHARGES_ID { get; set; }
        public string CELECTRICITY_TAX_ID { get; set; }
        public bool LCHILLER { get; set; }
        public string CCHILLER_CHARGES_ID { get; set; }
        public string CCHILLER_TAX_ID { get; set; }
        public bool LGAS { get; set; } = false;
        public string CGAS_CHARGES_ID { get; set; }
        public string CGAS_TAX_ID { get; set; }
        public bool LWATER { get; set; } = false;
        public string CWATER_CHARGES_ID { get; set; }
        public string CWATER_TAX_ID { get; set; }
        public string CUPDATE_BY { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public DateTime? DCREATE_DATE { get; set; }

    }
}
