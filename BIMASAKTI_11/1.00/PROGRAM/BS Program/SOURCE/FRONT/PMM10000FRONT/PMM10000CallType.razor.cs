using PMM10000COMMON.SLA_Call_Type;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM10000MODEL.ViewModel;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_LockingFront;
using PMM10000COMMON.UtilityDTO;
using Microsoft.JSInterop;
using Global_PMCOMMON.DTOs.Response.Property;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.Tab;
using Lookup_PMCOMMON.DTOs.LML01500;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML01500;
using PMM10000COMMON.SLA_Category;
using PMM10000MODEL.DTO;

namespace PMM10000FRONT
{
    public partial class PMM10000CallType : R_Page, R_ITabPage
    {
        private PMM10000CallTypeViewModel _viewModelSLA = new();
        private R_Grid<PMM10000SLACallTypeDTO>? _gridSLACallType;
        private R_ConductorGrid? _conGridSLACallType;
        private int _pageSizeSLA = 25;
        [Inject] IJSRuntime? JS { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelSLA.Parameter = (PMM10000DbParameterDTO)poParameter;
                if (_viewModelSLA.Parameter.CPROPERTY_ID != null)
                {
                    await _gridSLACallType.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        //ini buat Dapet fungsi dari Page 1
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModelSLA.Parameter = R_FrontUtility.ConvertObjectToObject<PMM10000DbParameterDTO>(poParam);
                await _gridSLACallType!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }


            R_DisplayException(loException);

        }
        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModelSLA.GetSLACallTypeList();
                eventArgs.ListEntityResult = _viewModelSLA._CallTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMM10000SLACallTypeDTO)eventArgs.Data;
                eventArgs.Result = await _viewModelSLA.GetEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMM10000SLACallTypeDTO)eventArgs.Data;
                await _viewModelSLA.ServiceDelete(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceAfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        private void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM10000SLACallTypeDTO)eventArgs.Data;
                // loData.CPARENT = loCurrentSelectDataList.Id;
                loData.CPROPERTY_ID = _viewModelSLA.Parameter.CPROPERTY_ID!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }


        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMM10000SLACallTypeDTO)eventArgs.Data;
                await _viewModelSLA.ServiceSave(loParam, eventArgs.ConductorMode);
                eventArgs.Result = _viewModelSLA._CallTypeData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelSLA.ValidationFieldEmpty((PMM10000SLACallTypeDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            R_DisplayException(loEx);
        }
        #region LOOKUP
        //  Button LookUp GOA
        private void BeforeOpenLookSLACategory(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loParam = new LML01500ParameterDTO();
            loParam.CPROPERTY_ID = _viewModelSLA.Parameter.CPROPERTY_ID!;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML01500);

        }

        private void AfterOpenLookSLACategory(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            if (eventArgs.ColumnName == "Category")
            {
                var loResult = R_FrontUtility.ConvertObjectToObject<LML01500DTO>(eventArgs.Result);
                ((PMM10000SLACallTypeDTO)eventArgs.ColumnData).CCATEGORY_ID = loResult.CCATEGORY_ID;
                ((PMM10000SLACallTypeDTO)eventArgs.ColumnData).CCATEGORY_NAME = loResult.CCATEGORY_NAME;
            }
        }

        private async Task R_CellLostFocusedSLACategory(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMM10000SLACallTypeDTO loGetData = (PMM10000SLACallTypeDTO)eventArgs.CurrentRow;

                if (eventArgs.ColumnName == "Category")
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupLML01500ViewModel loLookupViewModel = new LookupLML01500ViewModel();
                        var param = new LML01500ParameterDTO
                        {
                            CPROPERTY_ID = _viewModelSLA.Parameter.CPROPERTY_ID!,
                            CSEARCH_TEXT = (string)eventArgs.Value,
                        };
                        var loResult = await loLookupViewModel.GetSLACategory(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            loGetData.CCATEGORY_ID = "";
                            loGetData.CCATEGORY_NAME = "";
                        }
                        else
                        {
                            loGetData.CCATEGORY_ID = loResult.CCATEGORY_ID;
                            loGetData.CCATEGORY_NAME = loResult.CCATEGORY_NAME;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Template
        private async Task TemplateBtn_OnClick()
        {
            var loEx = new R_Exception();
            string loCompanyName = clientHelper.CompanyId.ToUpper();

            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModelSLA.DownloadTemplate();

                    var saveFileName = $"SLA Call Type - {loCompanyName}.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Upload
        private void Before_Open_Upload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var param = new PMM10000DbParameterDTO()
            {
                //CCOMPANY_ID = journalGroupViewModel.JournalGroupCurrent.CCOMPANY_ID,
                //CUSER_ID = clientHelper.UserId,
                CPROPERTY_ID = _viewModelSLA.Parameter.CPROPERTY_ID,
                CPROPERTY_NAME = _viewModelSLA.Parameter.CPROPERTY_NAME,
            };

            eventArgs.Parameter = param;
              eventArgs.TargetPageType = typeof(PMM10000Upload);
        }

        private async Task After_Open_Upload(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                if ((bool)eventArgs.Result == true)
                {
                    await _gridSLACallType.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion
        #region UserLocking
        [Inject] IClientHelper? clientHelper { get; set; }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (PMM10000SLACallTypeDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {

                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM10000",
                        Table_Name = "PMM_SLA_CALL_TYPE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCALL_TYPE_ID)

                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM10000",
                        Table_Name = "PMM_SLA_CALL_TYPE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCALL_TYPE_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        #endregion
    }
}
