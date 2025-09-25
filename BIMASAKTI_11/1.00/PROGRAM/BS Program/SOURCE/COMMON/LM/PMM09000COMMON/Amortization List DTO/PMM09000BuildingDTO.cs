using PMM09000COMMON.UtiliyDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.Amortization_List_DTO
{
    public class PMM09000BuildingDTO : BaseDTO
    {
        public string CBUILDING_ID { get; set; } = "";
        public string? CBUILDING_NAME { get; set; }
    }
}
