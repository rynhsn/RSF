using PMT50600COMMON.DTOs.PMT50631;
using PMT50600MODEL.ViewModel;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Popup;
using PMT50600COMMON.DTOs.PMT50621;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Helpers;
using R_LockingFront;

namespace PMT50600FRONT
{
    public partial class PMT50631 : R_Page
    {
        [Inject] private R_ILocalizer<PMT50600FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        [Inject] private R_PopupService PopupService { get; set; }

        private PMT50631ViewModel loViewModel = new PMT50631ViewModel();

        private R_ConductorGrid _conductorRef;

        private R_Grid<PMT50631DTO> _gridAdditionalRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private bool IsDiscountTypeEnable = false;

        private bool IsDiscountPercentEnable = false;

        private bool IsDiscountAmountEnable = false;

        private string lcLabel = "";

        [Inject] IClientHelper _clientHelper { get; set; }

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMT50631DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT50600",
                        Table_Name = "PMT_TRANS_ADD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT50600",
                        Table_Name = "PMT_TRANS_ADD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CSEQ_NO)
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

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loViewModel.loTabParam = (AdditionalParameterDTO)poParameter;
                if (loViewModel.loTabParam.CADDITIONAL_TYPE == "A")
                {
                    lcLabel = _localizer["_Addition"];
                }
                else if (loViewModel.loTabParam.CADDITIONAL_TYPE == "D")
                {
                    lcLabel = _localizer["_Deduction"];
                }
                else
                {
                    lcLabel = _localizer["_Addition"] + " / " + _localizer["_Deduction"];
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loTabParam.CREC_ID))
                {
                    await loViewModel.GetCompanyInfoAsync();
                    await loViewModel.GetHeaderInfoAsync();
                    await _gridAdditionalRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        protected override async Task R_PageClosing(R_PageClosingEventArgs eventArgs)
        {
            //await loViewModel.GetHeaderInfoAsync();

            //if (loViewModel.loTabParam.CADDITIONAL_TYPE == "A")
            //{
            //    await this.Close(true, loViewModel.loHeader.NADDITION);
            //}
            //else  
            //{
            //    await this.Close(true, loViewModel.loHeader.NDEDUCTION);
            //}
        }

        private void Additional_Display(R_DisplayEventArgs eventArgs)
        {
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetAdditionalListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loAdditionalList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Additional_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetAdditionalAsync((PMT50631DTO)eventArgs.Data);
                eventArgs.Result = loViewModel.loAdditional;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Additional_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
        }

        private async Task Additional_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_eMessageBoxResult loValidate;
            var loData = (PMT50631DTO)eventArgs.Data;
            if (loData.CTRX_TYPE == "A")
            {
                loValidate = await R_MessageBox.Show("", string.Format(_localizer["M011"], lcLabel), R_eMessageBoxButtonType.OK);
                eventArgs.Cancel = true;
            }
            else
            {
                loValidate = await R_MessageBox.Show("", string.Format(_localizer["M009"], lcLabel), R_eMessageBoxButtonType.YesNo);
                eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
            }
        }

        private void Additional_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50631DTO loData = (PMT50631DTO)eventArgs.Data;
                loViewModel.AdditionalValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Additional_AfterDelete()
        {
            await R_MessageBox.Show("", string.Format(_localizer["M010"], lcLabel), R_eMessageBoxButtonType.OK);
        }


        private void Additional_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsDiscountTypeEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private void Additional_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            IsDiscountTypeEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private async Task Additional_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveAdditionalAsync((PMT50631DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loAdditional;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Additional_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.DeleteAdditionalAsync((PMT50631DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        #region Lookup

        private async Task AdditionalOnLostFocused(R_CellLostFocusedEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.ColumnName == nameof(PMT50631DTO.CADD_DEPT_CODE))
                {
                    PMT50631DTO loGetData = (PMT50631DTO)eventArgs.CurrentRow;

                    if (string.IsNullOrWhiteSpace(loGetData.CADD_DEPT_CODE))
                    {
                        loGetData.CADD_DEPT_NAME = "";
                        goto EndBlock;
                    }

                    LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                    GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                    {
                        CPROPERTY_ID = loViewModel.loHeader.CPROPERTY_ID,
                        CSEARCH_TEXT = loGetData.CADD_DEPT_CODE
                    };

                    var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData.CADD_DEPT_CODE = "";
                        loGetData.CADD_DEPT_NAME = "";
                    }
                    else
                    {
                        loGetData.CADD_DEPT_CODE = loResult.CDEPT_CODE;
                        loGetData.CADD_DEPT_NAME = loResult.CDEPT_NAME;
                    }
                }
                else if (eventArgs.ColumnName == nameof(PMT50631DTO.CCHARGES_ID))
                {
                    PMT50631DTO loGetData = (PMT50631DTO)eventArgs.CurrentRow;

                    if (string.IsNullOrWhiteSpace(loGetData.CADD_DEPT_CODE))
                    {
                        loGetData.CCHARGES_NAME = "";
                        goto EndBlock;
                    }

                    LookupGSL01400ViewModel loLookupViewModel = new LookupGSL01400ViewModel();
                    GSL01400ParameterDTO loParam = new GSL01400ParameterDTO()
                    {
                        CPROPERTY_ID = loViewModel.loHeader.CPROPERTY_ID,
                        CCHARGES_TYPE_ID = loViewModel.loTabParam.CADDITIONAL_TYPE,
                        CSEARCH_TEXT = loGetData.CCHARGES_ID
                    };

                    var loResult = await loLookupViewModel.GetOtherCharges(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        loGetData.CCHARGES_ID = "";
                        loGetData.CCHARGES_NAME = "";
                    }
                    else
                    {
                        loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                        loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_BeforelookUp(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(PMT50631DTO.CADD_DEPT_CODE))
            {
                eventArgs.Parameter = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loHeader.CPROPERTY_ID
                };
                eventArgs.TargetPageType = typeof(GSL00710);
            }
            else if (eventArgs.ColumnName == nameof(PMT50631DTO.CCHARGES_ID))
            {
                eventArgs.Parameter = new GSL01400ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loHeader.CPROPERTY_ID,
                    CCHARGES_TYPE_ID = loViewModel.loTabParam.CADDITIONAL_TYPE
                };
                eventArgs.TargetPageType = typeof(GSL01400);
            }
        }

        private void Grid_AfterlookUp(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(PMT50631DTO.CADD_DEPT_CODE))
            {
                GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                PMT50631DTO loGetData = (PMT50631DTO)eventArgs.ColumnData;
                loGetData.CADD_DEPT_CODE = loTempResult.CDEPT_CODE;
                loGetData.CADD_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
            else if (eventArgs.ColumnName == nameof(PMT50631DTO.CCHARGES_ID))
            {
                GSL01400DTO loTempResult = (GSL01400DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                PMT50631DTO loGetData = (PMT50631DTO)eventArgs.ColumnData;
                loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
                loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
                //loGetData.CCHARGES_DESC = loTempResult.;
            }
        }
        #endregion
    }
}
