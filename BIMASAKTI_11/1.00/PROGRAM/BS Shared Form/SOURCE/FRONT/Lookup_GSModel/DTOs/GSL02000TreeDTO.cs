using R_BlazorFrontEnd.Controls.TreeView;

namespace Lookup_GSModel
{
    public class GSL02000TreeDTO : R_TreeViewItemBase
    {
        public string DisplayTree { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
    }
}
