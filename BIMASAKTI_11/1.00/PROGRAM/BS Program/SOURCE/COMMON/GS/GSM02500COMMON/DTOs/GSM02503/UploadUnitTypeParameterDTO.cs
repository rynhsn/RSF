using GSM02500COMMON.DTOs.GSM02500;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class UploadUnitTypeParameterDTO
    {
        public SelectedPropertyDTO PropertyData { get; set; } = new SelectedPropertyDTO();
        public string SelectedUnitTypeCategory { get; set; } = "";
    }
}
