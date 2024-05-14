using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02540;
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
    public class GSM02540ViewModel : R_ViewModel<GSM02540DetailDTO>
    {
        private GSM02540Model loModel = new GSM02540Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02540DetailDTO loUnitPromotionTypeDetail = null;

        public ObservableCollection<GSM02540DTO> loUnitPromotionTypeList = new ObservableCollection<GSM02540DTO>();

        public GSM02540DTO loCurrentUnitPromotionType = null;

        public GSM02540ResultDTO loRtn = null;

        public bool SelectedActiveInactiveLACTIVE;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public GetCUOMFromPropertyDTO loCUOM = new GetCUOMFromPropertyDTO();


        public void UnitPromotionTypeValidation(GSM02540DetailDTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_PROMOTION_TYPE_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V022"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CUNIT_PROMOTION_TYPE_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V023"));
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
                //R_FrontContext.R_SetContext(ContextConstant.GSM02500_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loCUOM = await loSharedModel.GetCUOMFromPropertyAsync(loParam);
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
                loRtn = await loModel.GetUnitPromotionTypeListStreamAsync();
                loUnitPromotionTypeList = new ObservableCollection<GSM02540DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetUnitPromotionTypeAsync(GSM02540DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            GSM02540ParameterDTO loResult = null;
            GSM02540ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02540ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loUnitPromotionTypeDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveUnitPromotionTypeAsync(GSM02540DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02540ParameterDTO loResult = null;
            GSM02540ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02540ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loUnitPromotionTypeDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteUnitPromotionTypeAsync(GSM02540DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02540ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02540ParameterDTO()
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
                    CUNIT_PROMOTION_TYPE_ID = loUnitPromotionTypeDetail.CUNIT_PROMOTION_TYPE_ID,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_PROPERTY_ID_CONTEXT, SelectedProperty.CPROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_CUNIT_PROMOTION_TYPE_ID_CONTEXT, loUnitPromotionTypeDetail.CUNIT_PROMOTION_TYPE_ID);
                //R_FrontContext.R_SetContext(ContextConstant.GSM02540_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTION_TYPEMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateUnitPromotionTypeDTO> DownloadTemplateUnitPromotionTypeAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitPromotionTypeDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateUnitPromotionTypeAsync();
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
