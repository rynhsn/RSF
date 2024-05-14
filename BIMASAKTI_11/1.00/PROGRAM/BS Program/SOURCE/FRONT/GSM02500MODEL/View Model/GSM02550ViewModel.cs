using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500COMMON.DTOs.GSM02550;
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
    public class GSM02550ViewModel : R_ViewModel<GSM02550DTO>
    {
        private GSM02550Model loModel = new GSM02550Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02550DTO loUserPropertyDetail = null;

        public ObservableCollection<GSM02550DTO> loUserPropertyList = new ObservableCollection<GSM02550DTO>();

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public GSM02550ResultDTO loRtn = null;

        public void UserValidation(GSM02550DTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CUSER_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V028"));
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


        public async Task GetUserPropertyTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02550_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtn = await loModel.GetUserPropertyListStreamAsync();
                loUserPropertyList = new ObservableCollection<GSM02550DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetUserPropertyAsync(GSM02550DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02550ParameterDTO loResult = null;
            GSM02550ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02550ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02550_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUserPropertyDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUserPropertyAsync(GSM02550DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02550ParameterDTO loResult = null;
            GSM02550ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02550ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02550_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUserPropertyDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteUserPropertyAsync(GSM02550DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02550ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02550ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02550_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
