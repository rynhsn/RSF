using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.PopUpSaveAs
{
    public class SaveAsDTO
    {
        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILETYPE { get; set; } = "";
    }
}
