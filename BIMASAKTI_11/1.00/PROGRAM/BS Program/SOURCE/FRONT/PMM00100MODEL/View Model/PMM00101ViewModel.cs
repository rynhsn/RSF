using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.General;
using PMM00100COMMON.DTO_s.Helper;
using PMM00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMM00100MODEL.View_Model
{
    public class PMM00101ViewModel : R_ViewModel<HoUtilBuildingMappingDTO>
    {
        //var
        private PMM00102Model _model = new PMM00102Model();
        public ObservableCollection<HoUtilBuildingMappingDTO> HoUtilBuildingMappings { get; set; } = new ObservableCollection<HoUtilBuildingMappingDTO>();
        public SystemParamDetailDTO SystemParamDetails { get; set; } = new SystemParamDetailDTO();
        public HoUtilBuildingMappingDTO HoUtilBuildingMapping { get; set; } = new HoUtilBuildingMappingDTO();
        public string CurrentBuildingId { get; set; } = "";
        public bool IsBuildingMappingFound { get; set; } = true;

        //methods
        public async Task SetCurrentProeprty(SystemParamDetailDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                SystemParamDetails = poParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetBuildingListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CPROPERTY_ID, SystemParamDetails.CPROPERTY_ID);
                var loResult = await _model.GetBuildingListAsync();
                HoUtilBuildingMappings = new ObservableCollection<HoUtilBuildingMappingDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUtillMapping_BuildingAsync(HoUtilBuildingMappingDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (SystemParamDetails.LALL_BUILDING)
                {
                    poParam.CBUILDING_ID = "";
                }
                var loData = await _model.R_ServiceGetRecordAsync(poParam);
                IsBuildingMappingFound = loData != null;
                HoUtilBuildingMapping = loData ?? new HoUtilBuildingMappingDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveHoUtilBuildingAsync(HoUtilBuildingMappingDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CACTION = poCRUDMode switch
                {
                    eCRUDMode.AddMode => "ADD",
                    eCRUDMode.EditMode => "EDIT",
                    _ => "",
                };
                var loResult = await _model.R_ServiceSaveAsync(poParam, poCRUDMode);
                IsBuildingMappingFound = loResult != null;
                HoUtilBuildingMapping = loResult ?? new HoUtilBuildingMappingDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
