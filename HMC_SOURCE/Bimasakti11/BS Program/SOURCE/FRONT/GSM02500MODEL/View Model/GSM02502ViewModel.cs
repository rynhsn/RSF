using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02502;
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
    public class GSM02502ViewModel : R_ViewModel<GSM02502DetailDTO>
    {
        private GSM02502Model loModel = new GSM02502Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02502DetailDTO loUnitTypeCategoryDetail = null;

        public ObservableCollection<GSM02502DTO> loUnitTypeCategoryList = new ObservableCollection<GSM02502DTO>();

        public GSM02502ListDTO loRtn = null;

        public List<PropertyTypeDTO> loPropertyTypeList = null;

        public PropertyTypeResultDTO loPropertyTypeRtn = null;  

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
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                SelectedProperty = await loSharedModel.GetSelectedPropertyAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void UnitTypeCategoryValidation(GSM02502DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_TYPE_CATEGORY_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V007"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_TYPE_CATEGORY_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V008"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUnitTypeCategoryListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loRtn = await loModel.GetUnitTypeCategoryListStreamAsync();
                loUnitTypeCategoryList = new ObservableCollection<GSM02502DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyTypeRtn = await loModel.GetPropertyTypeListStreamAsync();
                loPropertyTypeList = loPropertyTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitTypeCategoryAsync(GSM02502DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02502ParameterDTO loParam = null;
            GSM02502ParameterDTO loResult = null;

            try
            {
                loParam = new GSM02502ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUnitTypeCategoryDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUnitTypeCategoryAsync(GSM02502DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02502ParameterDTO loParam = null;
            GSM02502ParameterDTO loResult = null;
            try
            {
                loParam = new GSM02502ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUnitTypeCategoryDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteUnitTypeCategoryAsync(GSM02502DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02502ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02502ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
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
                    CUNIT_TYPE_CATEGORY_ID = loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_UNIT_TYPE_CATEGORY_ID_CONTEXT, loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02502_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }



        #region Template
        public async Task<TemplateUnitTypeCategoryDTO> DownloadTemplateUnitTypeCategoryAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitTypeCategoryDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateUnitTypeCategoryAsync();
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
