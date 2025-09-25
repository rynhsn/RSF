using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500MODEL.View_Model;
using GSM02500COMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSCOMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using R_BlazorFrontEnd.Helpers;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02504;
using GSM02500COMMON.DTOs.GSM02503;

namespace GSM02500FRONT
{
    public partial class GSM0250x : R_Page
    {
        private R_TabStrip UnitTypeTabStripRef;

        //General Info

        private GSM02501ViewModel loGeneralInfoViewModel = new();

        private R_Conductor _conductorGeneralInfoRef;

        private R_Grid<GSM02501PropertyDTO> _gridGeneralInfoRef;

        private string loGeneralInfoLabel = "";

        //Unit Type Category

        private GSM02502ViewModel loUnitTypeCategoryViewModel = new();

        private R_Conductor _conductorUnitTypeCategoryRef;

        private R_Grid<GSM02502DTO> _gridUnitTypeCategoryRef;

        private string loUnitTypeCategoryLabel = "";

        //Image

        public GSM02503ImageViewModel loImageViewModel = new();

        private R_Conductor _conductorImageRef;

        private R_Grid<GSM02503ImageDTO> _gridImageRef;

        //Unit Type

        private GSM02503UnitTypeViewModel loUnitTypeViewModel = new();
        private R_Conductor _conductorUnitTypeRef;
        private R_Grid<GSM02503UnitTypeDTO> _gridUnitTypeRef;
        private string loUnitTypeLabel = "";
        private R_eConductorMode ConductorMode;
        private SelectedUnitTypeCategoryDTO _selectedUnitTypeCategory = new SelectedUnitTypeCategoryDTO();

        //Unit Type

        public GSM02504ViewModel loUnitViewViewModel = new();

        private R_ConductorGrid _conGridUnitViewRef;

        private R_Grid<GSM02504DTO> _gridUnitViewRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridGeneralInfoRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnParentTabChanged(R_TabStripTab eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Title == "Unit Type Category")
                {
                    loUnitTypeCategoryViewModel.SelectedProperty.CPROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
                    await loUnitTypeCategoryViewModel.GetSelectedPropertyAsync();
                    await _gridUnitTypeCategoryRef.R_RefreshGrid(null);
                }
                else if(eventArgs.Title == "Unit Type")
                {
                    if (UnitTypeTabStripRef.ActiveTabIndex == 0)
                    {
                        loUnitTypeViewModel.SelectedProperty.CPROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
                        loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_ID;
                        loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_NAME;

                        await loUnitTypeViewModel.GetSelectedPropertyAsync();
                        ConductorMode = R_eConductorMode.Normal;
                        //await loUnitTypeViewModel.GetSelectedPropertyAsync();
                        await _gridUnitTypeRef.R_RefreshGrid(null);
                    }
                    else if (UnitTypeTabStripRef.ActiveTabIndex == 1)
                    {
                        loImageViewModel.SelectedProperty.CPROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
                        loImageViewModel.SelectedUnitType.CUNIT_TYPE_ID = loUnitTypeViewModel.loUnitTypeDetail.CUNIT_TYPE_ID;
                        await loImageViewModel.GetSelectedPropertyAsync();
                        await loImageViewModel.GetSelectedUnitTypeAsync();

                        await _gridImageRef.R_RefreshGrid(null);
                    }
                }
                else if(eventArgs.Title == "Unit View")
                {
                    loUnitViewViewModel.SelectedProperty.CPROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
                    await loUnitViewViewModel.GetSelectedPropertyAsync();
                    await _gridUnitViewRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnUnitTypeTabChanged(R_TabStripTab eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Title == "Unit Type")
                {
                    loUnitTypeViewModel.SelectedProperty.CPROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
                    loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_ID;
                    loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_NAME;

                    await loUnitTypeViewModel.GetSelectedPropertyAsync();
                    ConductorMode = R_eConductorMode.Normal;
                    //await loUnitTypeViewModel.GetSelectedPropertyAsync();
                    await _gridUnitTypeRef.R_RefreshGrid(null);
                }
                else if (eventArgs.Title == "Image")
                {
                    loImageViewModel.SelectedProperty.CPROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
                    loImageViewModel.SelectedUnitType.CUNIT_TYPE_ID = loUnitTypeViewModel.loUnitTypeDetail.CUNIT_TYPE_ID;
                    await loImageViewModel.GetSelectedPropertyAsync();
                    await loImageViewModel.GetSelectedUnitTypeAsync();

                    await _gridImageRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region GeneralInfo

        private async Task Grid_DisplayGeneralInfo(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02501DetailDTO)eventArgs.Data;
                loGeneralInfoViewModel.loPropertyDetail = loParam;
                loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_NAME = loParam.CPROPERTY_NAME;
                loGeneralInfoViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loGeneralInfoLabel = "Inactive";
                    loGeneralInfoViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loGeneralInfoLabel = "Activate";
                    loGeneralInfoViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
        }

        private async Task Grid_R_ServiceGetGeneralInfoListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loGeneralInfoViewModel.GetPropertyListStreamAsync();
                eventArgs.ListEntityResult = loGeneralInfoViewModel.loPropertyList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetGeneralInfoRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02501DetailDTO loParam = new GSM02501DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02501DetailDTO>(eventArgs.Data);
                await loGeneralInfoViewModel.GetPropertyAsync(loParam);

                eventArgs.Result = loGeneralInfoViewModel.loPropertyDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveGeneralInfo(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loGeneralInfoViewModel.SavePropertyAsync(
                    (GSM02501DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loGeneralInfoViewModel.loPropertyDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteGeneralInfo(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02501DetailDTO loData = (GSM02501DetailDTO)eventArgs.Data;
                await loGeneralInfoViewModel.DeletePropertyAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Popup_ActivateInactiveGeneralInfo(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "GSM02502";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }

        private async Task R_After_Open_Popup_ActivateInactiveGeneralInfo(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await loGeneralInfoViewModel.ActiveInactiveProcessAsync();
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await _gridGeneralInfoRef.R_RefreshGrid(null);
        }

        private void R_After_Open_LookupSalesTaxId(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02501DetailDTO)_conductorGeneralInfoRef.R_GetCurrentData();
            loGetData.CSALES_TAX_ID = loTempResult.CTAX_ID;
            loGetData.CSALES_TAX_NAME = loTempResult.CTAX_NAME;
            loGetData.NTAX_PERCENTAGE = loTempResult.NTAX_PERCENTAGE;
        }

        private void R_Before_Open_LookupSalesTaxId(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00100ParameterDTO()
            {
                CCOMPANY_ID = "",
                CUSER_ID = ""
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private void R_After_Open_LookupCurrency(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02501DetailDTO)_conductorGeneralInfoRef.R_GetCurrentData();
            loGetData.CCURRENCY = loTempResult.CCURRENCY_CODE;
            loGetData.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
        }

        private void R_Before_Open_LookupCurrency(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void R_Before_OpenBuilding_Detail(R_BeforeOpenDetailEventArgs eventArgs)
        {
            eventArgs.Parameter = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(GSM02510);
        }

        private void R_After_OpenBuilding_Detail()
        {

        }
        #endregion

        #region Unit Type Category

        private void Grid_DisplayUnitTypeCategory(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02502DetailDTO)eventArgs.Data;
                loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail = loParam;
                loUnitTypeCategoryViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loUnitTypeCategoryLabel = "Inactive";
                    loUnitTypeCategoryViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loUnitTypeCategoryLabel = "Activate";
                    loUnitTypeCategoryViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
        }

        private async Task Grid_R_ServiceGetUnitTypeCategoryListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeCategoryViewModel.GetUnitTypeCategoryListStreamAsync();
                eventArgs.ListEntityResult = loUnitTypeCategoryViewModel.loUnitTypeCategoryList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordUnitTypeCategory(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02502DetailDTO loParam = new GSM02502DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02502DetailDTO>(eventArgs.Data);
                await loUnitTypeCategoryViewModel.GetUnitTypeCategoryAsync(loParam);

                eventArgs.Result = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveUnitTypeCategory(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeCategoryViewModel.SaveUnitTypeCategoryAsync(
                    (GSM02502DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteUnitTypeCategory(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02502DetailDTO loData = (GSM02502DetailDTO)eventArgs.Data;
                await loUnitTypeCategoryViewModel.DeleteUnitTypeCategoryAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Popup_UnitTypeCategoryActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "GSM02502";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }

        private async Task R_After_Open_Popup_UnitTypeCategoryActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await loUnitTypeCategoryViewModel.ActiveInactiveProcessAsync();
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await _gridUnitTypeCategoryRef.R_RefreshGrid(null);
        }
        #endregion

        #region Image

        private async Task Grid_DisplayImage(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02503ImageDTO)eventArgs.Data;
                loImageViewModel.loImageDetail = loParam;
            }
        }

        private async Task Grid_R_ServiceGetImageListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loImageViewModel.GetImageListStreamAsync();

                eventArgs.ListEntityResult = loImageViewModel.loImageList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetImageRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02503ImageDTO loParam = (GSM02503ImageDTO)eventArgs.Data;
                await loImageViewModel.GetImageAsync(loParam);

                eventArgs.Result = loImageViewModel.loImageDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveImage(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loImageViewModel.SaveImageAsync(
                    (GSM02503ImageDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loImageViewModel.loImageDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteImage(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02503ImageDTO loData = (GSM02503ImageDTO)eventArgs.Data;
                await loImageViewModel.DeleteImageAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Unit Type

        private async Task Grid_DisplayUnitType(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02503UnitTypeDetailDTO)eventArgs.Data;
                loUnitTypeViewModel.loUnitTypeDetail = loParam;
                loUnitTypeViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loUnitTypeLabel = "Inactive";
                    loUnitTypeViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loUnitTypeLabel = "Activate";
                    loUnitTypeViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
        }

        private async Task Grid_R_ServiceGetUnitTypeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeViewModel.GetUnitTypeListStreamAsync();
                eventArgs.ListEntityResult = loUnitTypeViewModel.loUnitTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUnitTypeRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02503UnitTypeDetailDTO loParam = new GSM02503UnitTypeDetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02503UnitTypeDetailDTO>(eventArgs.Data);
                await loUnitTypeViewModel.GetUnitTypeAsync(loParam);

                eventArgs.Result = loUnitTypeViewModel.loUnitTypeDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void AssignUnitTypeCategoryToTemp()
        {
            _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID;
            _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME;
        }

        private void Grid_BeforeAddUnitType(R_BeforeAddEventArgs eventArgs)
        {
            AssignUnitTypeCategoryToTemp();
            ConductorMode = R_eConductorMode.Add;
        }

        private void Grid_BeforeEditUnitType(R_BeforeEditEventArgs eventArgs)
        {
            AssignUnitTypeCategoryToTemp();
            ConductorMode = R_eConductorMode.Edit;
        }

        private void Grid_BeforeCancelUnitType(R_BeforeCancelEventArgs eventArgs)
        {
            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID;
            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = _selectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME;

            ConductorMode = R_eConductorMode.Normal;
        }

        private void Grid_AfterSaveUnitType(R_AfterSaveEventArgs eventArgs)
        {
            ConductorMode = R_eConductorMode.Normal;
        }


        private async Task Grid_ServiceSaveUnitType(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeViewModel.SaveUnitTypeAsync(
                    (GSM02503UnitTypeDetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUnitTypeViewModel.loUnitTypeDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteUnitType(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02503UnitTypeDetailDTO loData = (GSM02503UnitTypeDetailDTO)eventArgs.Data;
                await loUnitTypeViewModel.DeleteUnitTypeAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Before_Open_Popup_ActivateInactiveUnitType(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "GSM02502";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }

        private async Task R_After_Open_Popup_ActivateInactiveUnitType(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await loUnitTypeViewModel.ActiveInactiveProcessAsync();
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await _gridUnitTypeRef.R_RefreshGrid(null);
        }

        private void R_Before_Open_Lookup_UnitType_UnitTypeCategory(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00600ParameterDTO()
            {
                CCOMPANY_ID = "",
                CUSER_ID = "",
                CPROPERTY_ID = loUnitTypeViewModel.SelectedProperty.CPROPERTY_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00600);
        }

        private void R_After_Open_Lookup_UnitType_UnitTypeCategory(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            /*
                        var loGetData = (GSM02502DTO)_conductorUnitTypeRef.R_GetCurrentData();
                        loGetData.CUNIT_TYPE_CATEGORY_ID = loTempResult.CUNIT_TYPE_CATEGORY_ID;
                        loGetData.CUNIT_TYPE_CATEGORY_NAME = loTempResult.CUNIT_TYPE_CATEGORY_NAME;*/

            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_ID = loTempResult.CUNIT_TYPE_CATEGORY_ID;
            loUnitTypeViewModel.SelectedUnitTypeCategory.CUNIT_TYPE_CATEGORY_NAME = loTempResult.CUNIT_TYPE_CATEGORY_NAME;

            if (ConductorMode == R_eConductorMode.Normal)
            {
                _gridUnitTypeRef.R_RefreshGrid(null);
            }
        }
        #endregion

        #region Unit View

        private async Task Grid_DisplayUnitView(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02504DTO)eventArgs.Data;
                //await loUnitViewViewModel.GetSelectedPropertyAsync();
            }
        }

        private async Task Grid_R_ServiceGetUnitViewListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitViewViewModel.GetUnitViewListStreamAsync();
                eventArgs.ListEntityResult = loUnitViewViewModel.loUnitViewList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUnitViewRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02504DTO loParam = (GSM02504DTO)eventArgs.Data;
                await loUnitViewViewModel.GetUnitViewAsync(loParam);

                eventArgs.Result = loUnitViewViewModel.loUnitView;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveUnitView(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitViewViewModel.SaveUnitViewAsync(
                    (GSM02504DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUnitViewViewModel.loUnitView;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteUnitView(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02504DTO loData = (GSM02504DTO)eventArgs.Data;
                await loUnitViewViewModel.DeleteUnitViewAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
