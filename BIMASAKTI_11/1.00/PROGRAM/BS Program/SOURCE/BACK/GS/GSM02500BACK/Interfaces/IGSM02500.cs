using GSM02500COMMON.DTOs.GSM02500;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500BACK
{
    public interface IGSM02500
    {
        IAsyncEnumerable<GetUnitTypeDTO> GetUnitTypeList();
        IAsyncEnumerable<GetUnitViewDTO> GetUnitViewList();
        IAsyncEnumerable<GetUnitCategoryDTO> GetUnitCategoryList();
        Task<SelectedPropertyResultDTO> GetSelectedProperty(SelectedPropertyParameterDTO poParam);
        Task<SelectedBuildingResultDTO> GetSelectedBuilding(SelectedBuildingParameterDTO poParam);
        Task<SelectedFloorResultDTO> GetSelectedFloor(SelectedFloorParameterDTO poParam);
        Task<SelectedUnitResultDTO> GetSelectedUnit(SelectedUnitParameterDTO poParam);
        Task<SelectedOtherUnitResultDTO> GetSelectedOtherUnit(SelectedOtherUnitParameterDTO poParam);
        Task<SelectedUnitTypeResultDTO> GetSelectedUnitType(SelectedUnitTypeParameterDTO poParam);
        Task<GetCUOMFromPropertyResultDTO> GetCUOMFromProperty(GetCUOMFromPropertyParameterDTO poParam);
    }
}
