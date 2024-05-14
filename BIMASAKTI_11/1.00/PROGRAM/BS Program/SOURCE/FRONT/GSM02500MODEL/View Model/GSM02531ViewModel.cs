using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs.GSM02531;
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
    public class GSM02531ViewModel : R_ViewModel<GSM02531UtilityDetailDTO>
    {
        private GSM02531Model loModel = new GSM02531Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02531UtilityDetailDTO loUtilityDetail = new GSM02531UtilityDetailDTO();

        public GSM02531UtilityTypeDTO loUtilityType = new GSM02531UtilityTypeDTO();

        public ObservableCollection<GSM02531UtilityDTO> loUtilityList = new ObservableCollection<GSM02531UtilityDTO>();

        public ObservableCollection<GSM02531UtilityTypeDTO> loUtilityTypeList = new ObservableCollection<GSM02531UtilityTypeDTO>();

        public GSM02531UtilityResultDTO loRtn = null;

        public GSM02531UtilityTypeResultDTO loRtnUtilityType = null;

        public bool SelectedActiveInactiveLACTIVE;
        
        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public UploadUnitUtilityParameterDTO loUploadUnitUtilityParameter = null;

        public SelectedUnitDTO SelectedUnit = new SelectedUnitDTO();


        public void UtilitiesValidation(GSM02531UtilityDetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CMETER_NO);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V021"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSelectedUnitAsync()
        {
            R_Exception loException = new R_Exception();
            SelectedUnitParameterDTO loParam = null;
            try
            {
                loParam = new SelectedUnitParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CSELECTED_FLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID,
                    Data = new SelectedUnitDTO()
                    {
                        CUNIT_ID = loTabParameter.CSELECTED_UNIT_ID
                    }
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_UNIT_ID_CONTEXT, loTabParameter.CSELECTED_UNIT_ID);
                SelectedUnit = await loSharedModel.GetSelectedUnitAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUtilityTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loRtnUtilityType = await loModel.GetUtilityTypeListStreamAsync();
                loUtilityTypeList = new ObservableCollection<GSM02531UtilityTypeDTO>(loRtnUtilityType.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUtilitiesListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_BUILDING_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_FLOOR_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_UNIT_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_UNIT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02530_UTILITIES_TYPE_ID_STREAMING_CONTEXT, loUtilityType.CCODE);

                loRtn = await loModel.GetUtilitiesListStreamAsync();
                loUtilityList = new ObservableCollection<GSM02531UtilityDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUtilityAsync(GSM02531UtilityDetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02531UtilityParameterDTO loResult = null;
            GSM02531UtilityParameterDTO loParam = null;

            try
            {
                loParam = new GSM02531UtilityParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CSELECTED_FLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID,
                    CSELECTED_UNIT_ID = loTabParameter.CSELECTED_UNIT_ID,
                    CSELECTED_UTILITIES_TYPE_ID = loUtilityType.CCODE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UNIT_ID_CONTEXT, loTabParameter.CSELECTED_UNIT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT, loUtilityType.CCODE);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUtilityDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUtilityAsync(GSM02531UtilityDetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02531UtilityParameterDTO loResult = null;
            GSM02531UtilityParameterDTO loParam = null;

            try
            {
                loParam = new GSM02531UtilityParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CSELECTED_FLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID,
                    CSELECTED_UNIT_ID = loTabParameter.CSELECTED_UNIT_ID,
                    CSELECTED_UTILITIES_TYPE_ID = loUtilityType.CCODE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UNIT_ID_CONTEXT, loTabParameter.CSELECTED_UNIT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT, loUtilityType.CCODE);

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUtilityDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteUtilityAsync(GSM02531UtilityDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02531UtilityParameterDTO loParam = null;

            try
            {
                loParam = new GSM02531UtilityParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_BUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CSELECTED_FLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID,
                    CSELECTED_UNIT_ID = loTabParameter.CSELECTED_UNIT_ID,
                    CSELECTED_UTILITIES_TYPE_ID = loUtilityType.CCODE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UNIT_ID_CONTEXT, loTabParameter.CSELECTED_UNIT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT, loUtilityType.CCODE);
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
                    CPROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CBUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID,
                    CFLOOR_ID = loTabParameter.CSELECTED_FLOOR_ID,
                    CUNIT_ID = loTabParameter.CSELECTED_UNIT_ID,
                    CUTILITIES_TYPE = loUtilityType.CCODE,
                    CSEQUENCE = loUtilityDetail.CSEQUENCE,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_BUILDING_ID_CONTEXT, loTabParameter.CSELECTED_BUILDING_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_FLOOR_ID_CONTEXT, loTabParameter.CSELECTED_FLOOR_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UNIT_ID_CONTEXT, loTabParameter.CSELECTED_UNIT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_UTILITIES_TYPE_CONTEXT, loUtilityType.CCODE);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02530_SEQUENCE_CONTEXT, loUtilityDetail.CSEQUENCE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_BUILDING_UNIT_UTILITIESMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        #region Template
        public async Task<TemplateUnitUtilityDTO> DownloadTemplateUnitUtilityAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitUtilityDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateUnitUtilityAsync();
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
