using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02530;
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
        
        private GSM02530Model loUnitModel = new GSM02530Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02541DetailDTO loOtherUnitDetail = null;

        public GSM02541DTO loCurrentOtherUnit = null;

        public GetCUOMFromPropertyDTO loCUOM = new GetCUOMFromPropertyDTO();

        public List<GetUnitViewDTO> loOtherUnitViewList = null;

        public ObservableCollection<GSM02541DTO> loOtherUnitList = new ObservableCollection<GSM02541DTO>();

        public List<BuildingDTO> loBuildingList = null;

        public List<FloorDTO> loFloorList = null;

        public List<OtherUnitTypeDTO> loOtherUnitTypeList = null;

        public GSM02541ResultDTO loRtn = null;

        public BuildingResultDTO loBuildingRtn = null;

        public FloorResultDTO loFloorRtn = null;

        public OtherUnitTypeResultDTO loOtherUnitTypeRtn = null;

        public List<GetStrataLeaseDTO> loLeaseList = null;

        public bool SelectedActiveInactiveLACTIVE;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public string SelectedBuildingId;

        public async Task GetStrataLeaseListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetStrataLeaseParameterDTO loParam = new GetStrataLeaseParameterDTO();
            GetStrataLeaseResultDTO loResult = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_STRATA_LEASE_STREAMING_CONTEXT, new GetStrataLeaseParameterDTO()
                {
                    CLASS_ID = "_BS_LEASE_STATUS",
                    REC_ID_LIST = "01,05,06"
                });
                loResult = await loUnitModel.GetStrataLeaseListStreamAsync();
                loLeaseList = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
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
                
                loCUOM = await loSharedModel.GetCUOMFromPropertyAsync(loParam);
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
            GetUnitViewResultDTO loRtnUnitView = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02500_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtnUnitView = await loSharedModel.GetUnitViewListStreamAsync();
                loOtherUnitViewList = loRtnUnitView.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void OtherUnitValidation(GSM02541DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.COTHER_UNIT_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V024"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.COTHER_UNIT_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V025"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.COTHER_UNIT_TYPE_ID);
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
                
                SelectedProperty = await loSharedModel.GetSelectedPropertyAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetOtherUnitListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtn = await loModel.GetOtherUnitListStreamAsync();
                loOtherUnitList = new ObservableCollection<GSM02541DTO>(loRtn.Data);
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

        public async Task GetOtherUnitTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loOtherUnitTypeRtn = await loModel.GetOtherUnitTypeListStreamAsync();
                loOtherUnitTypeList = loOtherUnitTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetOtherUnitAsync(GSM02541DetailDTO poEntity)
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
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loOtherUnitDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveOtherUnitAsync(GSM02541DetailDTO poEntity, eCRUDMode peCRUDMode)
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

                if (string.IsNullOrWhiteSpace(loParam.Data.CFLOOR_ID))
                {
                    loParam.Data.CFLOOR_ID = "";
                }
                if (string.IsNullOrWhiteSpace(loParam.Data.CBUILDING_ID))
                {
                    loParam.Data.CBUILDING_ID = "";
                }
                if (string.IsNullOrWhiteSpace(loParam.Data.COTHER_UNIT_TYPE_ID))
                {
                    loParam.Data.COTHER_UNIT_TYPE_ID = "";
                }
                if (string.IsNullOrWhiteSpace(loParam.Data.CLEASE_STATUS))
                {
                    loParam.Data.CLEASE_STATUS = "";
                }

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loOtherUnitDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteOtherUnitAsync(GSM02541DetailDTO poEntity)
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
                    COTHER_UNIT_ID = loOtherUnitDetail.COTHER_UNIT_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };

                await loModel.RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateOtherUnitDTO> DownloadTemplateOtherUnitAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateOtherUnitDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateOtherUnitAsync();
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
