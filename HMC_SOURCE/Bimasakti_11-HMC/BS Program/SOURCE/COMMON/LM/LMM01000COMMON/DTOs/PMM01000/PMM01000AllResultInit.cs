using R_APICommonDTO;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01000AllResultInit : R_APIResultBaseDTO
    {
        public List<PMM01000DTOPropety> PropertyList { get; set; }
        public List<PMM01000UniversalDTO> TaxExemptionCodeList { get; set; }
        public List<PMM01000UniversalDTO> WithholdingTaxTypeList { get; set; }
        public List<PMM01000UniversalDTO> AccrualMethodTypeList { get; set; }
    }

}
