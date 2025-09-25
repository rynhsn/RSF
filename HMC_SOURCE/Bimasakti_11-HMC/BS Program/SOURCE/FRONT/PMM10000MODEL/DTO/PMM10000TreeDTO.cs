using R_BlazorFrontEnd.Controls.TreeView;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000MODEL.DTO
{
    public class PMM10000TreeDTO : R_TreeViewItemBase
    {
        public string? CCATEGORY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? ParentName { get; set; }
        public string? CNOTES { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }
    }
}
