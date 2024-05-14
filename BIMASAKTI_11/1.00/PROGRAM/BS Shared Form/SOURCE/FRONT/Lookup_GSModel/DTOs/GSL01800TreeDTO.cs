using R_BlazorFrontEnd.Controls.TreeView;

namespace Lookup_GSModel
{
    public class GSL01800TreeDTO : R_TreeViewItemBase
    {
        public string DisplayTree { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public int Level { get; set; }
    }
}
