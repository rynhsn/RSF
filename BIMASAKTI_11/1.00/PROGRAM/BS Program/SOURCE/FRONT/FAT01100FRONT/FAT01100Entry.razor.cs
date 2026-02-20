using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using FAT01100Common.DTOs;
using FAT01100Model.VMs;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_FACommon.DTOs;
using Lookup_FAFront;
using Lookup_GSModel.ViewModel;
using Lookup_FAModel.ViewModel.FAL00200;
using System;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Interfaces;

namespace FAT01100Front
{
    public partial class FAT01100Entry : R_Page
    {
        private FAT01100EntryViewModel _VM = new();
        private R_Conductor? _conductorRef;

        private R_TabStrip _tabMain = new();
        private R_TabStripTab _tabEntry = new();

        #region Dependency Injection
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_MessageBoxService R_MessageBox { get; set; } = default!;
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Image };
        [Inject] private R_ILocalizer<FAT01100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;
        #endregion

        protected override async Task R_Init_From_Master(object? poParam)
        {
            var loEx = new R_Exception();
            try
            {
                await _VM.GetInitialProcessAsync(
                    ClientHelper.CompanyId,
                    ClientHelper.UserId,
                    ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty);
                await _VM.GetCurrencyListAsync(ClientHelper.CompanyId, ClientHelper.UserId, ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty);
                await _VM.GetDeptLookupListAsync(ClientHelper.CompanyId, ClientHelper.UserId);

                if (poParam != null && _conductorRef != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(poParam);
                    await _conductorRef.R_GetEntity(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region CRUD
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(eventArgs.Data);
                loParam.CCOMPANY_ID = ClientHelper.CompanyId;
                loParam.CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
                await _VM.GetRecordAsync(loParam);
                eventArgs.Result = _VM.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loEntity = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(eventArgs.Data);
                loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
                loEntity.CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
                if (!string.IsNullOrEmpty(loEntity.CREF_DATE) && loEntity.DREF_DATE != default)
                    loEntity.CREF_DATE = loEntity.DREF_DATE.ToString("yyyyMMdd");
                await _VM.SaveRecordAsync(loEntity, (R_eConductorMode)eventArgs.ConductorMode);
                eventArgs.Result = _VM.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loEntity = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(eventArgs.Data);
                loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
                loEntity.CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
                await _VM.DeleteRecordAsync(loEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Department Lookup
        private void BeforeOpenLookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00700ParameterDTO
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CUSER_ID = ClientHelper.UserId,
                CSEARCH_TEXT = _VM.Data.CDEPT_CODE ?? string.Empty
            };
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private async Task AfterOpenLookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = (GSL00700DTO)eventArgs.Result;
                if (loResult != null)
                {
                    _VM.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                    _VM.Data.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Asset Lookup
        private void BeforeOpenLookupAsset(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new FAL00300ParameterDTO
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CTRANS_CODE = string.Empty,
                CASSET_CODE = _VM.Data.CASSET_CODE ?? string.Empty,
                CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty
            };
            eventArgs.TargetPageType = typeof(FAL00300);
        }

        private async Task AfterOpenLookupAsset(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (FAL00300DTO)eventArgs.Result;
                if (loData != null)
                {
                    _VM.Data.CASSET_CODE = loData.CASSET_CODE;
                    _VM.Data.CASSET_NAME = loData.CASSET_NAME;
                    _VM.Data.CASSET_TRANS_SEQ_NO = loData.CASSET_TRANS_SEQ_NO ?? string.Empty;
                    await _VM.FAT01100GetAsset(ClientHelper.CompanyId, loData.CASSET_CODE, ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty);

                    // set old data from get asset data
                    _VM.Data.CINSERVICE_DATE = _VM.GetAssetData.CINSERVICE_DATE;
                    _VM.Data.CASSET_DEPT_CODE_OLD = _VM.GetAssetData.CASSET_DEPT_CODE;
                    _VM.Data.CASSET_DEPT_NAME_OLD = _VM.GetAssetData.CASSET_DEPT_NAME;
                    _VM.Data.CJRNGRP_CODE_OLD= _VM.GetAssetData.CJRNGRP_CODE;
                    _VM.Data.CJRNGRP_NAME_OLD = _VM.GetAssetData.CJRNGRP_NAME;
                    _VM.Data.CCATEGORY_ID_OLD = _VM.GetAssetData.CCATEGORY_ID;
                    _VM.Data.CCATEGORY_NAME_OLD = _VM.GetAssetData.CCATEGORY_NAME;
                    _VM.Data.CTAX_CATEGORY_ID_OLD = _VM.GetAssetData.CTAX_CATEGORY_ID;
                    _VM.Data.CTAX_CATEGORY_NAME_OLD = _VM.GetAssetData.CTAX_CATEGORY_NAME;
                    _VM.Data.IQTY_OLD = _VM.GetAssetData.IASSET_QTY;
                    _VM.Data.CASSET_UNIT_OLD = _VM.GetAssetData.CUNIT;
                    _VM.Data.CSERIAL_NO_OLD = _VM.GetAssetData.CSERIAL_NO;
                    _VM.Data.CASSET_OWNER_OLD = _VM.GetAssetData.CASSET_OWNER;
                    _VM.Data.CASSET_DESC_OLD = _VM.GetAssetData.CTRANS_DESCRIPTION;
                    _VM.Data.CLOCATION_ID_OLD = _VM.GetAssetData.CLOCATION_ID;
                    _VM.Data.CLOCATION_NAME_OLD = _VM.GetAssetData.CLOCATION_NAME;
                    _VM.Data.CPROPERTY_ID_OLD = _VM.GetAssetData.CPROPERTY_ID;
                    _VM.Data.CPROPERTY_NAME_OLD = _VM.GetAssetData.CPROPERTY_NAME;
                    _VM.Data.CBUILDING_ID_OLD = _VM.GetAssetData.CBUILDING_ID;
                    _VM.Data.CBUILDING_NAME_OLD = _VM.GetAssetData.CBUILDING_NAME;
                    _VM.Data.CFLOOR_ID_OLD = _VM.GetAssetData.CFLOOR_ID;
                    _VM.Data.CFLOOR_NAME_OLD = _VM.GetAssetData.CFLOOR_NAME;
                    _VM.Data.CSTORAGE_ID_OLD = _VM.GetAssetData.CSTORAGE_ID;
                    _VM.Data.OASSET_IMAGE_OLD= _VM.GetAssetData.OASSET_IMAGE;

                    // set new data from get asset data
                    _VM.Data.CASSET_DEPT_CODE = _VM.GetAssetData.CASSET_DEPT_CODE;
                    _VM.Data.CASSET_DEPT_NAME = _VM.GetAssetData.CASSET_DEPT_NAME;
                    _VM.Data.CJRNGRP_CODE = _VM.GetAssetData.CJRNGRP_CODE;
                    _VM.Data.CJRNGRP_NAME = _VM.GetAssetData.CJRNGRP_NAME;
                    _VM.Data.CCATEGORY_ID = _VM.GetAssetData.CCATEGORY_ID;
                    _VM.Data.CCATEGORY_NAME = _VM.GetAssetData.CCATEGORY_NAME;
                    _VM.Data.CTAX_CATEGORY_ID = _VM.GetAssetData.CTAX_CATEGORY_ID;
                    _VM.Data.CTAX_CATEGORY_NAME = _VM.GetAssetData.CTAX_CATEGORY_NAME;
                    _VM.Data.IQTY = _VM.GetAssetData.IASSET_QTY;
                    _VM.Data.CASSET_UNIT = _VM.GetAssetData.CUNIT;
                    _VM.Data.CSERIAL_NO = _VM.GetAssetData.CSERIAL_NO;
                    _VM.Data.CASSET_OWNER = _VM.GetAssetData.CASSET_OWNER;
                    _VM.Data.CASSET_DESC = _VM.GetAssetData.CTRANS_DESCRIPTION;
                    _VM.Data.CLOCATION_ID = _VM.GetAssetData.CLOCATION_ID;
                    _VM.Data.CLOCATION_NAME = _VM.GetAssetData.CLOCATION_NAME;
                    _VM.Data.CPROPERTY_ID = _VM.GetAssetData.CPROPERTY_ID;
                    _VM.Data.CPROPERTY_NAME = _VM.GetAssetData.CPROPERTY_NAME;
                    _VM.Data.CBUILDING_ID = _VM.GetAssetData.CBUILDING_ID;
                    _VM.Data.CBUILDING_NAME = _VM.GetAssetData.CBUILDING_NAME;
                    _VM.Data.CFLOOR_ID = _VM.GetAssetData.CFLOOR_ID;
                    _VM.Data.CFLOOR_NAME = _VM.GetAssetData.CFLOOR_NAME;
                    _VM.Data.CSTORAGE_ID = _VM.GetAssetData.CSTORAGE_ID;
                    _VM.Data.OASSET_IMAGE = _VM.GetAssetData.OASSET_IMAGE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Journal Group Lookup (New Asset Information)
        private void BeforeOpenLookupJournalGroup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00400ParameterDTO
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CPROPERTY_ID = "",
                CJRNGRP_TYPE = "60",
                CSEARCH_TEXT = _VM.Data.CJRNGRP_CODE ?? string.Empty
            };
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private async Task AfterOpenLookupJournalGroup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = (GSL00400DTO)eventArgs.Result;
                if (loResult != null)
                {
                    _VM.Data.CJRNGRP_CODE = loResult.CJRNGRP_CODE ?? string.Empty;
                    _VM.Data.CJRNGRP_NAME = loResult.CJRNGRP_NAME ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Asset Category Lookup (New Asset Information)
        private void BeforeOpenLookupAssetCategory(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL01800DTOParameter
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CUSER_ID = ClientHelper.UserId,
                CPROPERTY_ID = "",
                CCATEGORY_TYPE = "60",
                CSEARCH_TEXT = _VM.Data.CCATEGORY_ID ?? string.Empty
            };
            eventArgs.TargetPageType = typeof(GSL01800);
        }

        private async Task AfterOpenLookupAssetCategory(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = (GSL01800DTO)eventArgs.Result;
                if (loResult != null)
                {
                    _VM.Data.CCATEGORY_ID = loResult.CCATEGORY_ID ?? string.Empty;
                    _VM.Data.CCATEGORY_NAME = loResult.CCATEGORY_NAME ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Asset Tax Category Lookup (New Asset Information)
        private void BeforeOpenLookupAssetTaxCategory(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new FAL00200ParameterDTO
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CSTATUS = "ACTIVE",
                CTAX_CATEGORY_ID = _VM.Data.CTAX_CATEGORY_ID ?? string.Empty,
                CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? "en"
            };
            eventArgs.TargetPageType = typeof(FAL00200);
        }

        private async Task AfterOpenLookupAssetTaxCategory(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = (FAL00200DTO)eventArgs.Result;
                if (loResult != null)
                {
                    _VM.Data.CTAX_CATEGORY_ID = loResult.CTAX_CATEGORY_ID ?? string.Empty;
                    _VM.Data.CTAX_CATEGORY_NAME = loResult.CTAX_CATEGORY_NAME ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Location Lookup (New Asset Information)
        private void BeforeOpenLookupLocation(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL03800ParameterDTO
            {
                CPROPERTY_ID = _VM.Data.CPROPERTY_ID ?? "",
                CACTIVE_TYPE = "ACTIVE",
                CLOCATION_ID = _VM.Data.CLOCATION_ID ?? "",
                CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(GSL03800);
        }

        private async Task AfterOpenLookupLocation(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = (GSL03800DTO)eventArgs.Result;
                if (loResult != null)
                {
                    _VM.Data.CLOCATION_ID = loResult.CLOCATION_ID ?? string.Empty;
                    _VM.Data.CLOCATION_NAME = loResult.CLOCATION_NAME ?? string.Empty;
                    _VM.Data.CPROPERTY_ID = loResult.CPROPERTY_ID ?? string.Empty;
                    _VM.Data.CPROPERTY_NAME = loResult.CPROPERTY_ID ?? string.Empty;
                    _VM.Data.CBUILDING_ID = loResult.CBUILDING_ID ?? string.Empty;
                    _VM.Data.CBUILDING_NAME = loResult.CBUILDING_NAME ?? string.Empty;
                    _VM.Data.CFLOOR_ID = loResult.CFLOOR_ID ?? string.Empty;
                    _VM.Data.CFLOOR_NAME = loResult.CFLOOR_NAME ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Submit / Redraft / Next
        private async Task OnClickSubmit()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(_VM.Data.CREC_ID))
                {
                    return;
                }
                await _VM.SubmitTransAsync(new FAT01100SubmitTransParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CREC_ID = _VM.Data.CREC_ID
                });
                await R_MessageBox.Show("", "Transaction submitted successfully.", R_eMessageBoxButtonType.OK);
                if (_conductorRef != null)
                    await _conductorRef.R_GetEntity(_VM.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnClickRedraft()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(_VM.Data.CREC_ID))
                {
                    return;
                }
                var leMsg = await R_MessageBox.Show("", "Redraft this transaction?", R_eMessageBoxButtonType.YesNo);
                if (leMsg == R_eMessageBoxResult.No)
                    return;
                await _VM.UpdateTransHdStatusAsync(new FAT01100UpdateTransHdStatusParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CREC_ID = _VM.Data.CREC_ID,
                    CNEW_STATUS = "00"
                });
                await R_MessageBox.Show("", "Transaction redrafted successfully.", R_eMessageBoxButtonType.OK);
                if (_conductorRef != null)
                    await _conductorRef.R_GetEntity(_VM.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnClickNextAssetInfo()
        {
            await CloseProgramAsync();
        }
        private async Task OnClickNextDepreciation()
        {
            await CloseProgramAsync();
        }

        private Task OnClickPreviousButton() => Task.CompletedTask;
        #endregion

        /// <summary>
        /// Format audit date/time for display (e.g. 10-Mar-2023 16:25:23).
        /// </summary>
        protected string FormatAuditDateTime(DateTime ldDate)
        {
            if (ldDate == default)
                return string.Empty;
            return ldDate.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        private void OnChangeTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
        }

        private Task OnClickUploadImage() => Task.CompletedTask;
        private Task OnClickResetImage()
        {
            if (_VM?.Data != null)
                _VM.Data.OASSET_IMAGE = Array.Empty<byte>();
            return Task.CompletedTask;
        }

        #region Asset Info Conductor (Asset Info tab)
        private Task ConductorAssetInfo_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_Display(R_DisplayEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_AfterAdd(R_AfterAddEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_Saving(R_SavingEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_ServiceSave(R_ServiceSaveEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_Validation(R_ValidationEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_BeforeCancel(R_BeforeCancelEventArgs eventArgs) => Task.CompletedTask;
        private Task AssetInfoAndDepreciationInfo_SetOther(R_SetEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_CheckAdd(R_CheckAddEventArgs eventArgs) => Task.CompletedTask;
        private Task ConductorAssetInfo_R_CheckEdit(R_CheckEditEventArgs eventArgs) => Task.CompletedTask;
        #endregion
    }
}
