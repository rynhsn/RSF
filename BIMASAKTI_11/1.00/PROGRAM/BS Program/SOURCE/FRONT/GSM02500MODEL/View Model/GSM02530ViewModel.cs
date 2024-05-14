using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02530;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02520;
using R_BlazorFrontEnd.Helpers;
using GSM02500FrontResources;

namespace GSM02500MODEL.View_Model
{
    public class GSM02530ViewModel : R_ViewModel<GSM02530DetailDTO>
    {
        private GSM02530Model loModel = new GSM02530Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02530DetailDTO loUnitInfoDetail = null;

        public ObservableCollection<GSM02530DTO> loUnitInfoList = new ObservableCollection<GSM02530DTO>();

        public GSM02530ResultDTO loRtn = null;

        public bool SelectedActiveInactiveLACTIVE;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public SelectedBuildingDTO SelectedBuilding = new SelectedBuildingDTO();

        public SelectedFloorDTO SelectedFloor = new SelectedFloorDTO();

        public GetCUOMFromPropertyDTO loCUOM = new GetCUOMFromPropertyDTO();

        public List<GetUnitTypeDTO> loUnitTypeList = null;

        public GetUnitTypeListDTO loRtnUnitType = null;

        public List<GetUnitViewDTO> loUnitViewList = null;

        public GetUnitViewResultDTO loRtnUnitView = null;

        public GetUnitCategoryListDTO loRtnUnitCategory = null;

        public List<GetUnitCategoryDTO> loUnitCategoryList = null;

        public TabParameterDTO loTabParameter = new TabParameterDTO();


        public void UnitInfoValidation(GSM02530DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V016"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V017"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_TYPE_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V018"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_VIEW_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V019"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_CATEGORY_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V020"));
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
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
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
                    Data = new SelectedPropertyDTO()
                    {
                        CPROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID
                    }
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
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
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    Data = new SelectedBuildingDTO()
                    {
                        CBUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID
                    }
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                SelectedBuilding = await loSharedModel.GetSelectedBuildingAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetSelectedFloorAsync()
        {
            R_Exception loException = new R_Exception();
            SelectedFloorParameterDTO loParam = null;
            try
            {
                loParam = new SelectedFloorParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    Data = new SelectedFloorDTO()
                    {
                        CFLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID
                    }
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                SelectedFloor = await loSharedModel.GetSelectedFloorAsync(loParam);
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

        public async Task GetUnitViewListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loRtnUnitView = await loSharedModel.GetUnitViewListStreamAsync();
                loUnitViewList = loRtnUnitView.Data;
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
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loRtnUnitType = await loSharedModel.GetUnitTypeListStreamAsync();
                loUnitTypeList = loRtnUnitType.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitInfoListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_BUILDING_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_FLOOR_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                loRtn = await loModel.GetUnitInfoListStreamAsync();
                loUnitInfoList = new ObservableCollection<GSM02530DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitInfoAsync(GSM02530DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02530ParameterDTO loParam = null;
            GSM02530ParameterDTO loResult = null;

            try
            {
                loParam = new GSM02530ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CSELECTED_FLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUnitInfoDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUnitInfoAsync(GSM02530DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02530ParameterDTO loParam = null;
            GSM02530ParameterDTO loResult = null;

            try
            {
                loParam = new GSM02530ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CSELECTED_FLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUnitInfoDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteUnitInfoAsync(GSM02530DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02530ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02530ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CSELECTED_FLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
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
                    CFLOOR_ID = SelectedFloor.CFLOOR_ID,
                    CUNIT_ID = loUnitInfoDetail.CUNIT_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UNIT_ID_CONTEXT, loUnitInfoDetail.CUNIT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateUnitDTO> DownloadTemplateUnitAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateUnitAsync();
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
