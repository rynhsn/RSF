using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02502Utility;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL.View_Model
{
    public class GSM02502UtilityViewModel : R_ViewModel<GSM02502UtilityDTO>
    {
        private GSM02502UtilityModel loModel = new GSM02502UtilityModel();

        public GSM02502UtilityDTO loUtility = null;

        public ObservableCollection<GSM02502UtilityDTO> loUtilityList = new ObservableCollection<GSM02502UtilityDTO>();

        public GSM02502UtilityResultDTO loRtn = null;

        public string SelectedProperty;
        
        public string SelectedUnitTypeCategory;

        public async Task GetUtilityListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT, SelectedUnitTypeCategory);
                loRtn = await loModel.GetUtilityListStreamAsync();
                loUtilityList = new ObservableCollection<GSM02502UtilityDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUtilityAsync(GSM02502UtilityDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02502UtilityParameterDTO loParam = null;
            GSM02502UtilityParameterDTO loResult = null;
            try
            {
                loParam = new GSM02502UtilityParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty,
                    CSELECTED_UNIT_TYPE_CATEGORY_ID = SelectedUnitTypeCategory
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_CONTEXT, SelectedProperty);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUtility = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUtilityAsync(GSM02502UtilityDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02502UtilityParameterDTO loParam = null;
            GSM02502UtilityParameterDTO loResult = null;
            try
            {
                loParam = new GSM02502UtilityParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty,
                    CSELECTED_UNIT_TYPE_CATEGORY_ID = SelectedUnitTypeCategory
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_CONTEXT, SelectedProperty);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory);
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUtility = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
/*
        public async Task DeleteUtilityAsync(GSM02502UtilityDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM02502_UTILITY_PROPERTY_ID_CONTEXT, SelectedProperty);
                R_FrontContext.R_SetContext(ContextConstant.GSM02502_UTILITY_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory);
                await loModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }*/
    }
}
