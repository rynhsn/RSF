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
using FAT01100FrontResources;
using R_CommonFrontBackAPI;
using Microsoft.AspNetCore.Components.Forms;

namespace FAT01100Front
{
    public partial class FAT01100Entry : R_Page
    {
        private FAT01100EntryViewModel _VM = new();
        private R_Conductor? _conductorRef;

        private R_TabStrip _tabMain = new();
        private R_TabStripTab _tabEntry = new();
        private R_TabStripTab? _tabExpenseAllocation;
        private R_TabPage? _tabPageExpenseAllocation;

        #region Dependency Injection
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_MessageBoxService R_MessageBox { get; set; } = default!;
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Image };
        [Inject] private R_ILocalizer<FAT01100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;
        #endregion

        private bool IsSuccess { get; set; } = false;
        private bool IsErrorEmptyFile = false;

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
                await _VM.GetGsbCodeListAsync();

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
        //private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        //{
        //    var loEx = new R_Exception();
        //    try
        //    {
        //        var loParam = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(eventArgs.Data);
        //        loParam.CCOMPANY_ID = ClientHelper.CompanyId;
        //        loParam.CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
        //        await _VM.GetRecordAsync(loParam);
        //        eventArgs.Result = _VM.Entity;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}

        //private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        //{
        //    var loEx = new R_Exception();
        //    try
        //    {
        //        var loEntity = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(eventArgs.Data);
        //        loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
        //        loEntity.CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
        //        if (!string.IsNullOrEmpty(loEntity.CREF_DATE) && loEntity.DREF_DATE != default)
        //            loEntity.CREF_DATE = loEntity.DREF_DATE.Value.ToString("yyyyMMdd");
        //        await _VM.SaveRecordAsync(loEntity, (R_eConductorMode)eventArgs.ConductorMode);
        //        eventArgs.Result = _VM.Entity;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}

        //private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        //{
        //    var loEx = new R_Exception();
        //    try
        //    {
        //        var loEntity = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(eventArgs.Data);
        //        loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
        //        loEntity.CLANG_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
        //        await _VM.DeleteRecordAsync(loEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}
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
                    if (_conductorRef?.R_ConductorMode == R_eConductorMode.Add)
                    {
                        await _VM.FAT01100GetAsset(ClientHelper.CompanyId, loData.CASSET_CODE, ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty);

                        // set old data from get asset data
                        _VM.Data.CINSERVICE_DATE = _VM.GetAssetData.CINSERVICE_DATE;
                        _VM.Data.DINSERVICE_DATE = DateTime.ParseExact(_VM.GetAssetData.CINSERVICE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        _VM.Data.CASSET_DEPT_CODE_OLD = _VM.GetAssetData.CASSET_DEPT_CODE;
                        _VM.Data.CASSET_DEPT_NAME_OLD = _VM.GetAssetData.CASSET_DEPT_NAME;
                        _VM.Data.CJRNGRP_CODE_OLD = _VM.GetAssetData.CJRNGRP_CODE;
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
                        _VM.Data.OASSET_IMAGE_OLD = _VM.GetAssetData.OASSET_IMAGE;

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


                        // tab depreciation info
                        _VM.Data.CCURRENCY_CODE = _VM.GetAssetData.CCURRENCY_CODE;
                        _VM.Data.CCURRENCY_NAME = _VM.GetAssetData.CCURRENCY_NAME;

                        _VM.Data.NLBASE_RATE = _VM.GetAssetData.NLBASE_RATE;
                        _VM.Data.CCURRENCY_CODE = _VM.GetAssetData.CCURRENCY_CODE;
                        _VM.Data.NLCURRENCY_RATE = _VM.GetAssetData.NLCURRENCY_RATE;

                        _VM.Data.NBBASE_RATE = _VM.GetAssetData.NBBASE_RATE;
                        _VM.Data.NBCURRENCY_RATE = _VM.GetAssetData.NBCURRENCY_RATE;
                        _VM.Data.CBASE_CURRENCY_CODE = _VM.GetAssetData.CBASE_CURRENCY_CODE;

                        // set old depreciation info
                        _VM.Data.CDEPR_METHOD_OLD = _VM.GetAssetData.CDEPR_METHOD;
                        _VM.Data.CSTART_DATE_OLD = _VM.GetAssetData.CSTART_DATE;
                        _VM.Data.DSTART_DATE_OLD = DateTime.ParseExact(_VM.GetAssetData.CSTART_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        _VM.Data.NBOOK_VALUE_OLD = _VM.GetAssetData.NBOOK_VALUE;
                        _VM.Data.NLBOOK_VALUE_OLD = _VM.GetAssetData.NLBOOK_VALUE;
                        _VM.Data.NBBOOK_VALUE_OLD = _VM.GetAssetData.NBBOOK_VALUE;
                        _VM.Data.NRESIDUAL_VALUE_OLD = _VM.GetAssetData.NRESIDUAL_VALUE;
                        _VM.Data.NLRESIDUAL_VALUE_OLD = _VM.GetAssetData.NLRESIDUAL_VALUE;
                        _VM.Data.NBRESIDUAL_VALUE_OLD = _VM.GetAssetData.NBRESIDUAL_VALUE;
                        _VM.Data.IUSEFUL_LIFE_YY_OLD = _VM.GetAssetData.IUSEFUL_LIFE_YY;
                        _VM.Data.IUSEFUL_LIFE_MM_OLD = _VM.GetAssetData.IUSEFUL_LIFE_MM;
                        _VM.Data.NYEAR_DEPR_PCT_OLD = _VM.GetAssetData.NYEAR_DEPR_PCT;
                        _VM.Data.NYEAR_DEPR_OLD = _VM.GetAssetData.NYEAR_DEPR;
                        _VM.Data.NLYEAR_DEPR_OLD = _VM.GetAssetData.NLYEAR_DEPR;
                        _VM.Data.NBYEAR_DEPR_OLD = _VM.GetAssetData.NBYEAR_DEPR;

                        // set new depreciation info
                        _VM.Data.CDEPR_METHOD = _VM.GetAssetData.CDEPR_METHOD;
                        _VM.Data.CSTART_DATE = _VM.GetAssetData.CSTART_DATE;
                        _VM.Data.DSTART_DATE = DateTime.ParseExact(_VM.GetAssetData.CSTART_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        _VM.Data.NBOOK_VALUE = _VM.GetAssetData.NBOOK_VALUE;
                        _VM.Data.NLBOOK_VALUE = _VM.GetAssetData.NLBOOK_VALUE;
                        _VM.Data.NBBOOK_VALUE = _VM.GetAssetData.NBBOOK_VALUE;
                        _VM.Data.NRESIDUAL_VALUE = _VM.GetAssetData.NRESIDUAL_VALUE;
                        _VM.Data.NLRESIDUAL_VALUE = _VM.GetAssetData.NLRESIDUAL_VALUE;
                        _VM.Data.NBRESIDUAL_VALUE = _VM.GetAssetData.NBRESIDUAL_VALUE;
                        _VM.Data.IUSEFUL_LIFE_YY = _VM.GetAssetData.IUSEFUL_LIFE_YY;
                        _VM.Data.IUSEFUL_LIFE_MM = _VM.GetAssetData.IUSEFUL_LIFE_MM;
                        _VM.Data.NYEAR_DEPR_PCT = _VM.GetAssetData.NYEAR_DEPR_PCT;
                        _VM.Data.NYEAR_DEPR = _VM.GetAssetData.NYEAR_DEPR;
                        _VM.Data.NLYEAR_DEPR = _VM.GetAssetData.NLYEAR_DEPR;
                        _VM.Data.NBYEAR_DEPR = _VM.GetAssetData.NBYEAR_DEPR;
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
                var lcMsg = Localizer["msg_submit"]; // Submit confirmation

                var leMsg = await R_MessageBox.Show("", lcMsg, R_eMessageBoxButtonType.YesNo);

                if (leMsg == R_eMessageBoxResult.No)
                {
                    return;
                }

                await _VM.SubmitTransAsync(new FAT01100SubmitTransParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CREC_ID = _VM.Data.CREC_ID
                });
                await R_MessageBox.Show("", Localizer["msg_submit_success"], R_eMessageBoxButtonType.OK);
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

                var leMsg = await R_MessageBox.Show("", Localizer["msg_redraf"], R_eMessageBoxButtonType.YesNo);
                if (leMsg == R_eMessageBoxResult.No)
                    return;
                await _VM.UpdateTransHdStatusAsync(new FAT01100UpdateTransHdStatusParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CREC_ID = _VM.Data.CREC_ID,
                    CNEW_STATUS = "00"
                });
                await R_MessageBox.Show("", Localizer["msg_redraf_success"], R_eMessageBoxButtonType.OK);
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
            R_Exception loException = new R_Exception();
            try
            {
                //validation when next
                if (_VM == null || _VM.Data == null)
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_dataNull"));
                }
                else
                {
                    
                    if (string.IsNullOrWhiteSpace(_VM.Data.CDEPT_CODE))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_department"));
                    }
                    if (_VM.Data.DREF_DATE == null)
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_ReferenceDate"));
                    }

                    //DateTime DsystemParamSoftPeriod = DateTime.ParseExact(_VM.SystemParamData.CSOFT_PERIOD, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    int IsystemParamSoftPeriod = int.TryParse(_VM.SystemParamData.CSOFT_PERIOD, out var result) ? result : 0;
                    string CrefDate = _VM.Data.DREF_DATE.Value.ToString("yyyyMM");
                    int IrefDate= int.TryParse(CrefDate, out var resultRef) ? resultRef : 0;
                    if (IrefDate < IsystemParamSoftPeriod)
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_ReferenceDateSoftPeriod"));
                    }
                    if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_CODE))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetCode"));
                    }
                    if (string.IsNullOrWhiteSpace(_VM.Data.CTRANS_DESC))
                    {
                        loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_transactionDescription"));
                    }

                }

                if (!loException.HasError)
                {
                    IsSuccess = true;
                    await _tabMain.SetActiveTabAsync("tab_AssetInfo");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task OnClickNextDepreciation()
        {
            R_Exception loException = new R_Exception();
            try
            {
                //Asset Code	Empty	Asset Code is required!
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetCode"));
                }
                //Asset Name	Empty 	Asset Name is required!
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_NAME))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetName"));
                }
                //Asset Department	Empty	Please select Asset Department!
                if (string.IsNullOrWhiteSpace(_VM.Data.CDEPT_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetDepartment"));
                }
                //Asset Journal Group	Empty	Please select Asset Journal Group!
                if (string.IsNullOrWhiteSpace(_VM.Data.CJRNGRP_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetJournalGroup"));
                }
                //Asset Category	Empty 	Please select Asset Category!
                if (string.IsNullOrWhiteSpace(_VM.Data.CCATEGORY_ID))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetCategory"));
                }
                //Asset Tax Category	Empty	Please select Asset Tax Category!
                if (string.IsNullOrWhiteSpace(_VM.Data.CTAX_CATEGORY_ID))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetTaxCategory"));
                }		
                //Quantity	<=0	Quantity must be greater than 0! 
                if (_VM.Data.IQTY <= 0)
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_Quantity"));
                }
                //Unit	Empty 	Unit is required!
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_UNIT))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_Unit"));
                }
                //Location	Empty	Location is required!
                if (string.IsNullOrWhiteSpace(_VM.Data.CLOCATION_ID))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_Location"));
                }
                //User / Owner	Empty	User / Owner is required!
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_OWNER))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_UserOwner"));
                }

                if (!loException.HasError)
                {
                    IsSuccess = true;
                    await _tabMain.SetActiveTabAsync("tab_DepreciationInfo");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnClickPreviousToEntry()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (!loException.HasError)
                {
                    await _tabMain.SetActiveTabAsync("tab_Entry");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnClickPreviousToAsset()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (!loException.HasError)
                {
                    await _tabMain.SetActiveTabAsync("tab_AssetInfo");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

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

        private async Task OnClickUploadImage(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loByteFile = await R_FrontUtility.ConvertStreamToByteAsync(eventArgs.File.OpenReadStream());
                string loFile = eventArgs.File.Name;

                _VM.Data.OASSET_IMAGE = loByteFile;
                _VM.Data.CFILE_NAME = Path.GetFileNameWithoutExtension(loFile);
                _VM.Data.CFILE_EXTENSION = Path.GetExtension(loFile);
            }
            catch (Exception ex)
            {
                if (IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
            loEx.ThrowExceptionIfErrors();
        }
        private Task OnClickResetImage()
        {
            if (_VM?.Data != null)
            {
                _VM.Data.OASSET_IMAGE = Array.Empty<byte>();
                _VM.Data.CSTORAGE_ID = "";
                _VM.Data.CFILE_NAME = string.Empty;
                _VM.Data.CFILE_EXTENSION = string.Empty;
            }
                

            return Task.CompletedTask;
        }

        
        private void OnDepreciationMethodChanged(string value)
        {

            if (_VM.Data != null)
            {
                _VM.Data.CDEPR_METHOD = value;
                CalculateYearlyDepreciationProcess(value);
            }
        }
        private void CalculateYearlyDepreciationProcess(string value)
        {
            //VAR_NO_DEPRECIATION = 00-ND
            //VAR_MANUAL = 10-MN
            //VAR_STRAIGHT_LINE = 20-SL
            //VAR_DECLINING = 30-DC
            //VAR_DOUBLE_DECLINING = 40-DD

            if (value == "00-ND" || value == "10-MN")

            {
                _VM.Data.NYEAR_DEPR_PCT = 0;
            }
            else
            if (value == "40-DD")
            {
                decimal lnDenominator = (_VM.Data.IREMAINING_LIFE_YY * 12) + _VM.Data.IREMAINING_LIFE_MM;
                if (lnDenominator == 0)
                {
                    _VM.Data.NYEAR_DEPR_PCT = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR_PCT = ((1m / lnDenominator) * 12) * 200;
                }
            }
            else
            {
                decimal lnDenominator = (_VM.Data.IREMAINING_LIFE_YY * 12) + _VM.Data.IREMAINING_LIFE_MM;
                if (lnDenominator == 0)
                {
                    _VM.Data.NYEAR_DEPR_PCT = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR_PCT = ((1m / lnDenominator) * 12) * 100;
                }
            }

            decimal decVal = 0.01m;
            if (value == "20-SL")
            {
                decimal lnDenominator = (_VM.Data.NBOOK_VALUE - _VM.Data.NRESIDUAL_VALUE);
                if (lnDenominator == 0)
                {
                    _VM.Data.NYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator * decVal;
                }
                decimal lnDenominator2 = (_VM.Data.NLBOOK_VALUE - _VM.Data.NLRESIDUAL_VALUE);
                if (lnDenominator2 == 0)
                {
                    _VM.Data.NLYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NLYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator2 * decVal;
                }
                decimal lnDenominator3 = (_VM.Data.NBBOOK_VALUE - _VM.Data.NBRESIDUAL_VALUE);
                if (lnDenominator3 == 0)
                {
                    _VM.Data.NBYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NBYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator3 * decVal;
                }
            }
            else
            {
                decimal lnDenominator4 = _VM.Data.NBOOK_VALUE;
                if (lnDenominator4 == 0)
                {
                    _VM.Data.NYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator4 * decVal;
                }
                decimal lnDenominator5 = _VM.Data.NLBOOK_VALUE;
                if (lnDenominator5 == 0)
                {
                    _VM.Data.NLYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NLYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator5 * decVal;
                }
                decimal lnDenominator6 = _VM.Data.NBBOOK_VALUE;
                if (lnDenominator6 == 0)
                {
                    _VM.Data.NBYEAR_DEPR = 0;
                }
                else
                {
                    _VM.Data.NBYEAR_DEPR = _VM.Data.NYEAR_DEPR_PCT * lnDenominator6 * decVal;
                }
            }
        }
        private void OnResidualChanged(decimal val)
        {
            _VM.Data.NRESIDUAL_VALUE = val;
            _VM.Data.NLRESIDUAL_VALUE = (val / _VM.Data.NLBASE_RATE) * _VM.Data.NLCURRENCY_RATE;
            _VM.Data.NBRESIDUAL_VALUE = (val / _VM.Data.NBBASE_RATE) * _VM.Data.NBCURRENCY_RATE;
            CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
        }

        private void OnUsefulLifeYearChanged(int val)
        {
            _VM.Data.IUSEFUL_LIFE_YY = val;
            _VM.Data.IREMAINING_LIFE_YY = val;
            CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
        }

        private void OnUsefulLifeMonthChanged(int val)
        {
            _VM.Data.IUSEFUL_LIFE_MM = val;
            _VM.Data.IREMAINING_LIFE_MM = val;
            CalculateYearlyDepreciationProcess(_VM.Data.CDEPR_METHOD);
        }

        #region Asset Info Conductor (Asset Info tab)
        private async Task Conductor_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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
        private Task Conductor_R_Display(R_DisplayEventArgs eventArgs) => Task.CompletedTask;

        private async void Conductor_R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loEntity = (FAT01100DTO)eventArgs.Data;
                loEntity.DREF_DATE = DateTime.Now;
                loEntity.CREF_DATE = DateTime.Now.ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Conductor_R_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loEntity = (FAT01100DTO)eventArgs.Data;
                loEntity.CCOMPANY_ID = ClientHelper.CompanyId;
                loEntity.CUSER_ID = ClientHelper.UserId;
                // Set start date
                if (loEntity.DREF_DATE.HasValue)
                {
                    loEntity.CREF_DATE = loEntity.DREF_DATE.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CREF_DATE = string.Empty;
                }

                // Set in-service date
                if (loEntity.DINSERVICE_DATE.HasValue)
                {
                    loEntity.CINSERVICE_DATE = loEntity.DINSERVICE_DATE.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CINSERVICE_DATE = string.Empty;
                }

                if (loEntity.DSTART_DATE_OLD.HasValue)
                {
                    loEntity.CSTART_DATE_OLD = loEntity.DSTART_DATE_OLD.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CSTART_DATE_OLD = string.Empty;
                }

                if (loEntity.DSTART_DATE.HasValue)
                {
                    loEntity.CSTART_DATE = loEntity.DSTART_DATE.Value.ToString("yyyyMMdd");
                }
                else
                {
                    loEntity.CSTART_DATE = string.Empty;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Conductor_R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // Get entity from event args
                var loEntity = (FAT01100DTO)eventArgs.Data;

                // Ensure entity is not null
                if (loEntity == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "nullEntity"));
                }
                else
                {

                    await _VM.SaveRecordAsync(
                        loEntity,
                        eventArgs.ConductorMode
                    );
                    eventArgs.Result = _VM.Entity;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            var leMsg = await R_MessageBox.Show("", Localizer["msg_delete"], R_eMessageBoxButtonType.YesNo);
            eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
        }
        private async Task AfterDelete()
        {
            var loEx = new R_Exception();
            try
            {
                await R_MessageBox.Show("", Localizer["msg_delete_success"], R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private async Task Conductor_R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (FAT01100DTO)eventArgs.Data;
                await _VM.DeleteRecordAsync(loParam);
                await _conductorRef.R_GetEntity(_VM.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async void Conductor_R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                if (_VM.Data == null)
                    return;

                if (string.IsNullOrWhiteSpace(_VM.Data.CDEPT_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_department"));
                }
                if (_VM.Data.DREF_DATE == null)
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_ReferenceDate"));
                }

                //DateTime DsystemParamSoftPeriod = DateTime.ParseExact(_VM.SystemParamData.CSOFT_PERIOD, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                int IsystemParamSoftPeriod = int.TryParse(_VM.SystemParamData.CSOFT_PERIOD, out var result) ? result : 0;
                string CrefDate = _VM.Data.DREF_DATE.Value.ToString("yyyyMM");
                int IrefDate = int.TryParse(CrefDate, out var resultRef) ? resultRef : 0;
                if (IrefDate < IsystemParamSoftPeriod)
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_ReferenceDateSoftPeriod"));
                }
                if (string.IsNullOrWhiteSpace(_VM.Data.CASSET_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_assetInfo_AssetCode"));
                }
                if (string.IsNullOrWhiteSpace(_VM.Data.CTRANS_DESC))
                {
                    loException.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "val_entry_transactionDescription"));
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            if (loException.HasError)
            {
                eventArgs.Cancel = true;
            }

            loException.ThrowExceptionIfErrors();
        }
        private Task Conductor_BeforeCancel(R_BeforeCancelEventArgs eventArgs) => Task.CompletedTask;
        private Task Conductor_SetOther(R_SetEventArgs eventArgs) => Task.CompletedTask;
        private Task Conductor_R_CheckAdd(R_CheckAddEventArgs eventArgs) => Task.CompletedTask;
        private Task Conductor_R_CheckEdit(R_CheckEditEventArgs eventArgs) => Task.CompletedTask;

        private void BeforeOpenExpenseAllocation(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _VM?.Data ?? new FAT01100DTO();
                eventArgs.TargetPageType = typeof(FAT01100ExpenseAllocation);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterOpenExpenseAllocation(R_AfterOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // Load expense allocation data when tab is opened
                // NET4: gvAllocExpense.R_RefreshGrid(loParam) is called when tab is activated
                if (_tabPageExpenseAllocation != null && _VM?.Data != null)
                {
                    // The component will be initialized via R_Init_From_Master with the parameter
                    // Additional refresh can be done here if needed
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void ExpenseAllocationTabEventCallBack(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                // Handle callbacks from ExpenseAllocation component if needed
                // For now, no specific callback handling required
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
