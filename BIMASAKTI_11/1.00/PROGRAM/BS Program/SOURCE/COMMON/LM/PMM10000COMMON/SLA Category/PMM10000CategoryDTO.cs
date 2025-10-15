using PMM10000COMMON.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.SLA_Category
{
    public class PMM10000CategoryDTO : BaseDTO
    {
        private string? _cCATEGORY_ID_NAME;
        private string? cPARENT_NAME;

        public int ILEVEL { get; set; }
        public string? CCATEGORY_ID { get; set; }
        public string? CCATEGORY_NAME { get; set; }
        public string? CCATEGORY_ID_NAME_DISPLAY { get; set; }
        public string? CPARENT_CATEGORY_ID { get; set; }
        public string? CCATEGORY_TYPE { get; set; }
        public string CNOTES { get; set; } = "";
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CACTION { get; set; }

        public bool LHAS_CHILD { get; set; }
        public string? CCATEGORY_TYPE_DESCR { get; set; }
        public string CPARENT { get; set; } = "";
        public string? CPARENT_NAME
        {
            get => cPARENT_NAME;
            set => cPARENT_NAME = string.IsNullOrWhiteSpace(value) ? "" : value;
        }
    }
}
