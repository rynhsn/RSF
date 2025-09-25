using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02130
{
    public class PMT02130HandoverUnitChecklistDTO
    {
        public string CCHECKLIST_ITEM_NAME { get; set; }
        public string CNOTES { get; set; }
        public bool LSTATUS { get; set; }
        public string CSTATUS { get; set; }
        public string CCARE_REF_NO { get; set; }
        public int IBASE_QUANTITY { get; set; }
        public string CBASE_QUANTITY { get; set; }
        public int IACTUAL_QUANTITY { get; set; }
        public string CACTUAL_QUANTITY { get; set; }
        public string CUNIT { get; set; }
    }
}