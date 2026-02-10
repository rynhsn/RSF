using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using FAT00300FrontResources;
using FAT00300Model;
using FAT00300Common;
using R_BlazorFrontEnd.Exceptions;
using FAT00300Common.Requests;
using R_BlazorFrontEnd.Controls.Events;
using FAT00300Model.VMs;
using R_BlazorFrontEnd.Controls.DataControls;
using BlazorClientHelper;
using R_BlazorFrontEnd.Helpers;
using FAT00300Common.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls.MessageBox;
using Lookup_FAFront;
using Lookup_FAModel;
using Lookup_FACommon.DTOs;
using Lookup_FAModel.ViewModel.FAL00200;

namespace FAT00300Front
{
    public partial class FAT00300 : R_Page
    {
        [Inject] private R_ILocalizer<Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        private R_Grid<FAT00300GetTransListResultDTO> gridTransList;
        private FAT00300ViewModel viewModelFAT00300 = new FAT00300ViewModel();
        private R_ConductorGrid conGridTrans;

        private FAT00300GetInitialProcessResultDTO loInitProcess = new FAT00300GetInitialProcessResultDTO();
        private R_TextBox txtDeptCode;
        private R_TextBox txtAstCode;

        public string VAR_TRANS_CODE = "210020";

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                //ClientHelper.Set_CompanyId("HGRBH");
                //ClientHelper.Set_UserId("FK");

                viewModelFAT00300.SetDefaultValue();

                // Set Up Initial Process
                var loParamGetCompanyInfo = new FAT00300GetCompanyInfoParameterDTO();
                var loParamGetSystemParam = new FAT00300GetSystemParamParameterDTO();
                var loParamGetTransCodeInfo = new FAT00300GetTransCodeInfoParamDTO();
                var loParamGetPeriodInfo = new FAT00300GetPeriodInfoParamDTO();
                var loParamGetPeriodRange = new FAT00300GetPeriodRangeParamDTO();

                await viewModelFAT00300.GetCompanyInfoAsync(loParamGetCompanyInfo);
                await viewModelFAT00300.GetSystemParamAsync(loParamGetSystemParam);
                await viewModelFAT00300.GetDeptListAsync();
                await ValidationSystemParam();
                await viewModelFAT00300.GetPeriodInfoAsync(loParamGetPeriodInfo);
                await viewModelFAT00300.GetTransCodeInfoAsync(loParamGetTransCodeInfo);
                await viewModelFAT00300.GetPeriodRangeAsync(loParamGetPeriodRange);

            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        public async Task R_ServiceGetListTransaction(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await viewModelFAT00300.GetTransListAsync();
                eventArgs.ListEntityResult = viewModelFAT00300.AllTransList;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Value Changed 
        public void DeptCodeValueChanged(string pcValue)
        {
            viewModelFAT00300.Data.CDEPT_CODE = pcValue;
        }

        public void PeriodYearFromValueChanged(int pcValue)
        {
            viewModelFAT00300.IPERIOD_FROM = pcValue;
        }

        public void PeriodYearToValueChanged(int pcValue)
        {
            viewModelFAT00300.IPERIOD_TO = pcValue;
        }

        public void PeriodMonthFromValueChanged(string pcValue)
        {
            viewModelFAT00300.CFROM_PERIOD = pcValue;
        }

        public void PeriodMonthToValueChanged(string pcValue)
        {
            viewModelFAT00300.CTO_PERIOD = pcValue;
        }

        public void AssetCodeValueChanged(string pcValue)
        {
            viewModelFAT00300.Data.CASSET_CODE = pcValue;
        }
        #endregion

        #region Refresh Button
        public async Task GetTransListData()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                viewModelFAT00300.ValidationGetTransList();

                if (!loEx.HasError)
                {
                    await gridTransList.R_RefreshGrid(null);
                }

                if (viewModelFAT00300.AllTransList.Count == 0)
                {
                    viewModelFAT00300.TranslistRecord = new FAT00300GetTransListResultDTO();
                    viewModelFAT00300.ValidationTransactionList();

                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Open Tab Entry
        public void BeforeOpenTabEntry(R_InstantiateDockEventArgs eventArgs)
        {
            var loParam = new FAT00300DTO();
            loParam.CCOMPANY_ID = viewModelFAT00300.TranslistRecord.CCOMPANY_ID;
            loParam.CREF_NO = viewModelFAT00300.TranslistRecord.CREF_NO;
            loParam.CTRANS_CODE = VAR_TRANS_CODE;
            loParam.CDEPT_CODE = viewModelFAT00300.TranslistRecord.CDEPT_CODE;
            loParam.CASSET_CODE = viewModelFAT00300.TranslistRecord.CASSET_CODE;
            loParam.CLOCAL_CURRENCY_CODE = viewModelFAT00300.CompanyInfo.CLOCAL_CURRENCY_CODE;
            loParam.CBASE_CURRENCY_CODE = viewModelFAT00300.CompanyInfo.CBASE_CURRENCY_CODE;
            loParam.LINCREMENT_FLAG = viewModelFAT00300.TransCodeInfo.LINCREMENT_FLAG;
            loParam.CSOFT_PERIOD = viewModelFAT00300.SystemParam.CSOFT_PERIOD;
            loParam.CREC_ID = viewModelFAT00300.TranslistRecord.CREC_ID;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(FAT00301);
        }

        public async Task AfterOpenTabEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await gridTransList.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        #endregion

        #region Display List 
        public void GetTransListRecord(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                viewModelFAT00300.TranslistRecord = eventArgs.Data as FAT00300GetTransListResultDTO ?? new FAT00300GetTransListResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #endregion

        #region Lookup
        public void BeforeOpenLookUpDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new GSL00700ParameterDTO();
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00700);

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void AfterOpenLookUpDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.Result != null)
                {
                    var loResult = (GSL00700DTO)eventArgs.Result;

                    viewModelFAT00300.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                    viewModelFAT00300.Data.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnLostFocusedDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new GSL00700ParameterDTO();
                var loViewModel = new LookupGSL00700ViewModel();

                if (!string.IsNullOrWhiteSpace(viewModelFAT00300.Data.CDEPT_CODE))
                {
                    loParam.CSEARCH_TEXT = viewModelFAT00300.Data.CDEPT_CODE;

                    var loTemp = await loViewModel.GetDepartment(loParam);

                    if (loTemp != null)
                    {
                        viewModelFAT00300.Data.CDEPT_CODE = loTemp.CDEPT_CODE;
                        viewModelFAT00300.Data.CDEPT_NAME = loTemp.CDEPT_NAME;
                    }
                    else
                    {
                        await R_MessageBox.Show(Localizer["ErrLookupInformation"],
                        R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrLookup01").ErrDescp);
                        await txtDeptCode.FocusAsync();
                        viewModelFAT00300.Data.CDEPT_CODE = "";
                        viewModelFAT00300.Data.CDEPT_NAME = "";

                    }
                }

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void BeforeOpenLookUpAsset(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new FAL00300ParameterDTO();
                loParam.CTRANS_CODE = VAR_TRANS_CODE;
                loParam.CASSET_CODE = "";
                loParam.CCOMPANY_ID = "";
                loParam.CLANGUAGE_ID = "";
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(FAL00300);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void AfterOpenLookUpAsset(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.Result != null)
                {
                    var loResult = (FAL00300DTO)eventArgs.Result;

                    viewModelFAT00300.Data.CASSET_CODE = loResult.CASSET_CODE;
                    viewModelFAT00300.Data.CASSET_NAME = loResult.CASSET_NAME;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnLostFocusedAsset()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new FAL00300ParameterDTO();
                var loViewModel = new LookupFAL00300ViewModel();

                if (!string.IsNullOrWhiteSpace(viewModelFAT00300.Data.CASSET_CODE))
                {
                    loParam.CASSET_CODE = viewModelFAT00300.Data.CASSET_CODE;

                    var loTemp = await loViewModel.GetTaxCategory(loParam);

                    if (loTemp != null)
                    {
                        viewModelFAT00300.Data.CASSET_CODE = loTemp.CASSET_CODE;
                        viewModelFAT00300.Data.CASSET_NAME = loTemp.CASSET_NAME;
                    }
                    else
                    {
                        await R_MessageBox.Show(Localizer["ErrLookupInformation"],
                        R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrLookup02").ErrDescp);
                        await txtAstCode.FocusAsync();
                        viewModelFAT00300.Data.CASSET_CODE = "";
                        viewModelFAT00300.Data.CASSET_CODE = "";

                    }
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Validation SystemParam
        public async Task ValidationSystemParam()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var lcTemp = viewModelFAT00300.SystemParam.CTRANS_DEPT_CODE;

                if (viewModelFAT00300.SystemParam.CCOMPANY_ID == "")
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrorSystemParam"));
                    await this.CloseProgramAsync();
                }
                else if (!viewModelFAT00300.loListDept.Any(x => x.CDEPT_CODE == lcTemp))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrorDepartment"));
                }

                if (!loEx.HasError)
                {
                    //Set Default Value Department
                    viewModelFAT00300.SetDefaultLookUpDepartment();
                    await Task.CompletedTask;
                }
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
