using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL.View_Model
{
    public class GSM02501ViewModel : R_ViewModel<GSM02501DetailDTO>
    {
        private GSM02501Model loModel = new GSM02501Model();

        public GSM02501DetailDTO loPropertyDetail = null;

        public ObservableCollection<GSM02501PropertyDTO> loPropertyList = new ObservableCollection<GSM02501PropertyDTO>();

        public GSM02501PropertyListDTO loRtn = null;

        public bool SelectedActiveInactiveLACTIVE;

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = new ObservableCollection<GSM02501PropertyDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void GeneralInfoValidation(GSM02501DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CPROPERTY_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V002"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CPROPERTY_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V003"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CSALES_TAX_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V004"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CCURRENCY);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V005"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CUOM);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V006"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetPropertyAsync(GSM02501DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02501DetailDTO loResult = await loModel.R_ServiceGetRecordAsync(poEntity);

                loPropertyDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SavePropertyAsync(GSM02501DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02501DetailDTO loResult = await loModel.R_ServiceSaveAsync(poEntity, peCRUDMode);

                loPropertyDetail = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeletePropertyAsync(GSM02501DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await loModel.R_ServiceDeleteAsync(poEntity);
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
                    CPROPERTY_ID = loPropertyDetail.CPROPERTY_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02501_PROPERTY_ID_CONTEXT, loPropertyDetail.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02501_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_PROPERTYMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
