using System;
using System.Collections.Generic;
using System.Text;

namespace PMM00100COMMON.DTO_s
{
    public class HoUtilBuildingMappingDTO : HoUtilBuildingMappingParamDTO
    {
        public string CBUILDING_NAME { get; set; }
        public string CBUILDING_DISPLAY => $"{CBUILDING_ID} - {CBUILDING_NAME}";
        
        public string CELECTRICITY_CHARGES_NAME { get; set; }
        public bool LELECTRICITY_CHARGES_TAXABLE { get; set; } = false;
        public string CELECTRICITY_TAX_NAME { get; set; }
        
        public string CCHILLER_CHARGES_NAME { get; set; }
        public bool LCHILLER_CHARGES_TAXABLE { get; set; } = false;
        public string CCHILLER_TAX_NAME { get; set; }

        public string CGAS_CHARGES_NAME { get; set; }
        public bool LGAS_CHARGES_TAXABLE { get; set; } = false;
        public string CGAS_TAX_NAME { get; set; }
        
        public string CWATER_CHARGES_NAME { get; set; }
        public bool LWATER_CHARGES_TAXABLE { get; set; } = false;
        public string CWATER_TAX_NAME { get; set; }
    }
}
