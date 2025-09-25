using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02500;
using R_BlazorFrontEnd.Helpers;
using GSM02500FrontResources;

namespace GSM02500MODEL.View_Model
{
    public class GSM02510ViewModel : R_ViewModel<GSM02510DetailDTO>
    {

        private GSM02510Model loModel = new GSM02510Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02510DetailDTO loBuildingCategoryDetail = new GSM02510DetailDTO();

        public ObservableCollection<GSM02510DTO> loBuildingCategoryList = new ObservableCollection<GSM02510DTO>();

        public GSM02510ListDTO loRtn = null;

        public bool SelectedActiveInactiveLACTIVE;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

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


        public void BuildingValidation(GSM02510DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CBUILDING_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V012"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CBUILDING_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V013"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02510_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtn = await loModel.GetBuildingListStreamAsync();
                loBuildingCategoryList = new ObservableCollection<GSM02510DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetBuildingAsync(GSM02510DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02510ParameterDTO loResult = null;
            GSM02510ParameterDTO loParam = null;
            try
            {
                loParam = new GSM02510ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loBuildingCategoryDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveBuildingAsync(GSM02510DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02510ParameterDTO loResult = null;
            GSM02510ParameterDTO loParam = null;
            try
            {
                loParam = new GSM02510ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loBuildingCategoryDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteBuildingAsync(GSM02510DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02510ParameterDTO loParam = null;
            try
            {
                loParam = new GSM02510ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
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
                    CBUILDING_ID = loBuildingCategoryDetail.CBUILDING_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };

                await loModel.RSP_GS_ACTIVE_INACTIVE_BUILIDNGMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
