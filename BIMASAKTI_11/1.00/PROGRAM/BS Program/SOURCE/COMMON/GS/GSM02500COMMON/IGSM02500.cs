using GSM02500COMMON.DTOs.GSM02500;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON
{
    public interface IGSM02500
    {
        IAsyncEnumerable<GetUnitTypeDTO> GetUnitTypeList();
        IAsyncEnumerable<GetUnitViewDTO> GetUnitViewList();
        IAsyncEnumerable<GetUnitCategoryDTO> GetUnitCategoryList();
        SelectedPropertyResultDTO GetSelectedProperty(SelectedPropertyParameterDTO poParam);
        SelectedBuildingResultDTO GetSelectedBuilding(SelectedBuildingParameterDTO poParam);
        SelectedFloorResultDTO GetSelectedFloor(SelectedFloorParameterDTO poParam);
        SelectedUnitResultDTO GetSelectedUnit(SelectedUnitParameterDTO poParam);
        SelectedUnitTypeResultDTO GetSelectedUnitType(SelectedUnitTypeParameterDTO poParam);
        GetCUOMFromPropertyResultDTO GetCUOMFromProperty(GetCUOMFromPropertyParameterDTO poParam);
    }
}
