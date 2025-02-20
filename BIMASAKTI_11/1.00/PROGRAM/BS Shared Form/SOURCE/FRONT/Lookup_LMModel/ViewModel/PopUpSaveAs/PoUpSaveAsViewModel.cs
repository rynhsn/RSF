using Lookup_PMCOMMON.DTOs.PopUpSaveAs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMModel.ViewModel.PopUpSaveAs
{
    public class PoUpSaveAsViewModel
    {
        public SaveAsDTO SaveAsParam = new SaveAsDTO();
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };
    }
}
