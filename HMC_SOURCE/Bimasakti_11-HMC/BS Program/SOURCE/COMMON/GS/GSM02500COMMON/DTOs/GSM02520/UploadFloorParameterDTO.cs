using GSM02500COMMON.DTOs.GSM02500;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class UploadFloorParameterDTO
    {
        public SelectedPropertyDTO PropertyData { get; set; } = new SelectedPropertyDTO();
        public SelectedBuildingDTO BuildingData { get; set; } = new SelectedBuildingDTO();
    }
}
