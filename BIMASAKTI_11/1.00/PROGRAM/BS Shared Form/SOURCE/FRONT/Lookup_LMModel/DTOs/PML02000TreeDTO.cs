using R_BlazorFrontEnd.Controls.TreeView;

namespace Lookup_PMModel.DTOs
{
    public class PML02000TreeDTO : R_TreeViewItemBase
    {
        public string Note { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public int Level { get; set; }
    }
}
