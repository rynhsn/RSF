using GSM02500COMMON.DTOs.GSM02500;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class UploadUnitParameterDTO
    {
        public SelectedPropertyDTO PropertyData { get; set; } = new SelectedPropertyDTO();
        public SelectedBuildingDTO BuildingData { get; set; } = new SelectedBuildingDTO();
        public SelectedFloorDTO FloorData { get; set; } = new SelectedFloorDTO();
    }
}
