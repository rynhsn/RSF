using Global_PMCOMMON.DTOs.Response.Property;
using Global_PMModel.ViewModel;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.Upload;
using PMM10000COMMON.UtilityDTO;
using PMM10000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000MODEL.ViewModel
{
    public class PMM10000CallTypeViewModel
    {
      
        private PMM10000ListModel _GetListModel = new PMM10000ListModel();
        private PMM10000CRUDCallTypeModel _CRUDCallTypeModel = new PMM10000CRUDCallTypeModel();
        public PMM10000TemplateModel _TemplateModel = new PMM10000TemplateModel();
        

        public ObservableCollection<PMM10000SLACallTypeDTO> _CallTypeList =          new ObservableCollection<PMM10000SLACallTypeDTO>();
        public PMM10000SLACallTypeDTO _CallTypeData = new PMM10000SLACallTypeDTO();
       
     

        public bool _enabledTabCallType = true;
        public PMM10000DbParameterDTO Parameter = new PMM10000DbParameterDTO();


        public async Task GetSLACallTypeList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, Parameter.CPROPERTY_ID);

                var loResult = await _GetListModel.GetCallTypeListAsyncModel();
                _CallTypeList = new ObservableCollection<PMM10000SLACallTypeDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #region CRUD

        public async Task<PMM10000SLACallTypeDTO> GetEntity(PMM10000SLACallTypeDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            PMM10000SLACallTypeDTO loResult = new PMM10000SLACallTypeDTO();
            try
            {
                loResult = await _CRUDCallTypeModel.R_ServiceGetRecordAsync(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task ServiceDelete(PMM10000SLACallTypeDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                await _CRUDCallTypeModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMM10000SLACallTypeDTO poNewEntity, R_eConductorMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CRUDCallTypeModel.R_ServiceSaveAsync(poNewEntity, (eCRUDMode)peCRUDMode);
                _CallTypeData = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Validation
        public void ValidationFieldEmpty(PMM10000SLACallTypeDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(poEntity.CCALL_TYPE_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM10000_Class), "ValidationCallTypeID");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CCALL_TYPE_NAME))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM10000_Class), "ValidationCallTypeName");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CCATEGORY_NAME))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM10000_Class), "ValidationCategoryID");
                    loEx.Add(loErr);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Template
        public async Task<PMM10000TemplateDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            PMM10000TemplateDTO loResult = new PMM10000TemplateDTO();
            try
            {
                loResult = await _TemplateModel.DownloadTemplateFileAsync();
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
