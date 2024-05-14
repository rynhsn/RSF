using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL.View_Model
{
    public class GSM02541ViewModel : R_ViewModel<GSM02541DetailDTO>
    {
        private GSM02541Model loModel = new GSM02541Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02541DetailDTO loUnitPromotionDetail = null;

        public GSM02541DTO loCurrentUnitPromotion = null;

        public ObservableCollection<GSM02541DTO> loUnitPromotionList = new ObservableCollection<GSM02541DTO>();

        public List<BuildingDTO> loBuildingList = null;

        public List<FloorDTO> loFloorList = null;

        public List<UnitPromotionTypeDTO> loUnitPromotionTypeList = null;

        public GSM02541ResultDTO loRtn = null;

        public BuildingResultDTO loBuildingRtn = null;

        public FloorResultDTO loFloorRtn = null;

        public UnitPromotionTypeResultDTO loUnitPromotionTypeRtn = null;

        public bool SelectedActiveInactiveLACTIVE;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public string SelectedBuildingId;

        public void UnitPromotionValidation(GSM02541DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_PROMOTION_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V024"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_PROMOTION_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V025"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_PROMOTION_TYPE_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V026"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CLOCATION);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V027"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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

        public async Task GetUnitPromotionListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtn = await loModel.GetUnitPromotionListStreamAsync();
                loUnitPromotionList = new ObservableCollection<GSM02541DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loBuildingRtn = await loModel.GetBuildingListStreamAsync();
                loBuildingList = loBuildingRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetFloorListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_BUILDING_ID_STREAMING_CONTEXT, SelectedBuildingId);
                loFloorRtn = await loModel.GetFloorListStreamAsync();
                loFloorList = loFloorRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitPromotionTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loUnitPromotionTypeRtn = await loModel.GetUnitPromotionTypeListStreamAsync();
                loUnitPromotionTypeList = loUnitPromotionTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitPromotionAsync(GSM02541DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02541ParameterDTO loResult = null;
            GSM02541ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02541ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUnitPromotionDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUnitPromotionAsync(GSM02541DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02541ParameterDTO loResult = null;
            GSM02541ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02541ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUnitPromotionDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteUnitPromotionAsync(GSM02541DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02541ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02541ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
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
                    CUNIT_PROMOTION_ID = loUnitPromotionDetail.CUNIT_PROMOTION_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_CUNIT_PROMOTION_ID_CONTEXT, loUnitPromotionDetail.CUNIT_PROMOTION_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateUnitPromotionDTO> DownloadTemplateUnitPromotionAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitPromotionDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateUnitPromotionAsync();
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
