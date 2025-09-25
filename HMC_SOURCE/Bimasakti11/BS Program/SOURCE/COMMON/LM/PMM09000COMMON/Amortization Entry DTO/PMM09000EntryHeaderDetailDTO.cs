using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.Amortization_Entry_DTO
{
    public class PMM09000EntryHeaderDetailDTO : R_APIResultBaseDTO
    {
        public PMM09000EntryHeaderDTO? Header { get; set; }
        public List<PMM09000AmortizationChargesDTO>? Details { get; set; }
        //Constructor
        public PMM09000EntryHeaderDetailDTO()
        {
            Header = new PMM09000EntryHeaderDTO();
            Details = new List<PMM09000AmortizationChargesDTO>();
        }
    }
}
