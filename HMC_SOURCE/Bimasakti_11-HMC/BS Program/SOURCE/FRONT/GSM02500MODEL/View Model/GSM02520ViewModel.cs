using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02520;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using GSM02500COMMON.DTOs.GSM02500;
using R_BlazorFrontEnd.Helpers;
using GSM02500FrontResources;

namespace GSM02500MODEL.View_Model
{
    public class GSM02520ViewModel : R_ViewModel<GSM02520DetailDTO>
    {
        private GSM02520Model loModel = new GSM02520Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02520DetailDTO loFloorDetail = null;

        public ObservableCollection<GSM02520DTO> loFloorList = new ObservableCollection<GSM02520DTO>();

        public GSM02520ListDTO loRtn = null;

        public bool SelectedActiveInactiveLACTIVE;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public SelectedBuildingDTO SelectedBuilding = new SelectedBuildingDTO();

        public GetCUOMFromPropertyDTO loCUOM = new GetCUOMFromPropertyDTO();

        public List<GetUnitTypeDTO> loUnitTypeList = null;

        public GetUnitTypeListDTO loRtnUnitType = null;

        public List<GetUnitCategoryDTO> loUnitCategoryList = null;

        public GetUnitCategoryListDTO loRtnUnitCategory = null;


        public void FloorValidation(GSM02520DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CFLOOR_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V014"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CFLOOR_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V015"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCUOMFromPropertyAsync()
        {
            R_Exception loException = new R_Exception();
            GetCUOMFromPropertyParameterDTO loParam = null;
            try
            {
                loParam = new GetCUOMFromPropertyParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loCUOM = await loSharedModel.GetCUOMFromPropertyAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetSelectedPropertyAsync()
        {
            R_Exception loException = new R_Exception();
            SelectedPropertyParameterDTO loParam = null;
            try
            {
                loParam = new SelectedPropertyParameterDTO()
                {
                    Data = SelectedProperty
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                SelectedProperty = await loSharedModel.GetSelectedPropertyAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetSelectedBuildingAsync()
        {
            R_Exception loException = new R_Exception();
            SelectedBuildingParameterDTO loParam = null;
            try
            {
                loParam = new SelectedBuildingParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    Data = new SelectedBuildingDTO()
                    {
                        CBUILDING_ID = SelectedBuilding.CBUILDING_ID
                    }
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_BUILDING_ID_CONTEXT, SelectedBuilding.CBUILDING_ID);
                SelectedBuilding = await loSharedModel.GetSelectedBuildingAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        public async Task GetUnitCategoryListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loRtnUnitCategory = await loSharedModel.GetUnitCategoryListStreamAsync();
                loUnitCategoryList = loRtnUnitCategory.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtnUnitType = await loSharedModel.GetUnitTypeListStreamAsync();
                loUnitTypeList = loRtnUnitType.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetFloowListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02520_BUILDING_ID_STREAMING_CONTEXT, SelectedBuilding.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02520_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtn = await loModel.GetFloorListStreamAsync();
                loFloorList = new ObservableCollection<GSM02520DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetFloorAsync(GSM02520DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02520ParameterDTO loParam = null;
            GSM02520ParameterDTO loResult = null;
            try
            {
                loParam = new GSM02520ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CBUILDING_ID = SelectedBuilding.CBUILDING_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_BUILDING_ID_CONTEXT, SelectedBuilding.CBUILDING_ID);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loFloorDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveFloorAsync(GSM02520DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02520ParameterDTO loParam = null;
            GSM02520ParameterDTO loResult = null;
            try
            {
                loParam = new GSM02520ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CBUILDING_ID = SelectedBuilding.CBUILDING_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_BUILDING_ID_CONTEXT, SelectedBuilding.CBUILDING_ID);
                
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loFloorDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteFloorAsync(GSM02520DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02520ParameterDTO loParam = null;
            try
            {
                loParam = new GSM02520ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CBUILDING_ID = SelectedBuilding.CBUILDING_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_BUILDING_ID_CONTEXT, SelectedBuilding.CBUILDING_ID);
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync()
        {
            R_Exception loException = new R_Exception();
            GSM02500ActiveInactiveParameterDTO loParam = null;

            try
            {
                loParam = new GSM02500ActiveInactiveParameterDTO()
                {
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CBUILDING_ID = SelectedBuilding.CBUILDING_ID,
                    CFLOOR_ID = loFloorDetail.CFLOOR_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_BUILDING_ID_CONTEXT, SelectedBuilding.CBUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_FLOOR_ID_CONTEXT, loFloorDetail.CFLOOR_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02520_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_FLOORMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateFloorDTO> DownloadTemplateFloorAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateFloorDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateFloorAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion
    }
}
