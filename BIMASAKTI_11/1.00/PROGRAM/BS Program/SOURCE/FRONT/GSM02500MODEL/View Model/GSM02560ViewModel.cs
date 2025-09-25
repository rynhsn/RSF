using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02560;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs;
using R_BlazorFrontEnd.Helpers;
using GSM02500FrontResources;

namespace GSM02500MODEL.View_Model
{
    public class GSM02560ViewModel : R_ViewModel<GSM02560DTO>
    {
        private GSM02560Model loModel = new GSM02560Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02560DTO loDepartmentDetail = null;

        public ObservableCollection<GSM02560DTO> loDepartmentList = new ObservableCollection<GSM02560DTO>();

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public GSM02560ResultDTO loRtn = null;

        public void DepartmentValidation(GSM02560DTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V029"));
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


        public async Task GetDepartmentListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02560_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtn = await loModel.GetDepartmentListStreamAsync();
                loDepartmentList = new ObservableCollection<GSM02560DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetDepartmentAsync(GSM02560DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02560ParameterDTO loResult = null;
            GSM02560ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02560ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };

                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loDepartmentDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveDepartmentAsync(GSM02560DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02560ParameterDTO loResult = null;
            GSM02560ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02560ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loDepartmentDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteDepartmentAsync(GSM02560DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02560ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02560ParameterDTO()
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
    }
}
