using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02500;
using R_BlazorFrontEnd.Helpers;
using GSM02500FrontResources;

namespace GSM02500MODEL.View_Model
{
    public class GSM02503UnitTypeViewModel : R_ViewModel<GSM02503UnitTypeDetailDTO>
    {
        private GSM02503UnitTypeModel loModel = new GSM02503UnitTypeModel();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02503UnitTypeDetailDTO loUnitTypeDetail = null;

        public ObservableCollection<GSM02503UnitTypeDTO> loUnitTypeList = new ObservableCollection<GSM02503UnitTypeDTO>();

        public GSM02503UnitTypeListDTO loRtn = null;

        public bool SelectedActiveInactiveLACTIVE;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public SelectedUnitTypeCategoryDTO SelectedUnitTypeCategory = new SelectedUnitTypeCategoryDTO();

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

        public void UnitTypeValidation(GSM02503UnitTypeDetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_TYPE_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V009"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_TYPE_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V010"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUnitTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02503_PROPERTY_ID_STREAMING_CONTEXT, SelectedProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02503_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT, SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID);
                loRtn = await loModel.GetUnitTypeListStreamAsync();
                loUnitTypeList = new ObservableCollection<GSM02503UnitTypeDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitTypeAsync(GSM02503UnitTypeDetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02503UnitTypeParameterDTO loParam = null;
            GSM02503UnitTypeParameterDTO loResult = null;

            try
            {
                loParam = new GSM02503UnitTypeParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUnitTypeDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUnitTypeAsync(GSM02503UnitTypeDetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02503UnitTypeParameterDTO loParam = null;
            GSM02503UnitTypeParameterDTO loResult = null;

            try
            {
                loParam = new GSM02503UnitTypeParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CUNIT_TYPE_CATEGORY_ID = SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID);
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUnitTypeDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteUnitTypeAsync(GSM02503UnitTypeDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02503UnitTypeParameterDTO loParam = null;

            try
            {
                loParam = new GSM02503UnitTypeParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID,
                    CUNIT_TYPE_CATEGORY_ID = SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_UNIT_TYPE_CATEGORY_ID_CONTEXT, SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID);
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
                    CUNIT_TYPE_ID = loUnitTypeDetail.CUNIT_TYPE_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_UNIT_TYPE_ID_CONTEXT, loUnitTypeDetail.CUNIT_TYPE_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02503_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }



        #region Template
        public async Task<TemplateUnitTypeDTO> DownloadTemplateUnitTypeAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitTypeDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateUnitTypeAsync();
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
