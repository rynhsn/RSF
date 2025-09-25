using BlazorClientHelper;
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
using PMT50600MODEL.ViewModel;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50600;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using PMT50600COMMON.DTOs.PMT50630;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.MessageBox;
using GLF00100COMMON;
using GLF00100FRONT;
using PMT50600COMMON.DTOs.PMT50610Print;
using R_BlazorFrontEnd.Interfaces;
using System.Reflection.Emit;
using System.Diagnostics.Tracing;
using R_BlazorFrontEnd.Helpers;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML00800;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Enums;

namespace PMT50600FRONT
{
    public partial class PMT50610 : R_Page
    {
        private PMT50610ViewModel loViewModel = new PMT50610ViewModel();

        private R_Conductor _conductorRef;

        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<PMT50600FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private bool IsCustomerEnabled = false;

        private bool IsBaseCurrencyEnabled = false;

        private bool IsLocalCurrencyEnabled = false;

        private bool IsTaxCurrencyEnabled = false;

        private bool IsTaxable = false;

        private bool IsTransStatus00 = false;

        private bool IsTransStatus10 = false;

        private bool IsJournalButtonEnabled = false;

        private bool IsCRUDModeButtonHidden = false;

        private string PageWidth = "width: auto;";

        private const string NEW = "NEW";
        
        private const string VIEW = "VIEW";

        private const string VIEW_ONLY = "VIEW_ONLY";

        private bool IsTaxEnabled = false;

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
                var loData = (PMT50610DTO)eventArgs.Data;

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
                        Table_Name = "PMT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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
                        Table_Name = "PMT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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
                loViewModel.loTabParameter = R_FrontUtility.ConvertObjectToObject<InvoiceEntryPredifineParameterDTO>(poParameter);

                await loViewModel.GetPMSystemParamAsync();
                await loViewModel.GetPeriodYearRangeAsync();
                await loViewModel.GetCompanyInfoAsync();
                await loViewModel.GetGLSystemParamAsync();
                await loViewModel.GetTransCodeInfoAsync();
                await loViewModel.GetPropertyListStreamAsync();
                await loViewModel.GetCurrencyListStreamAsync();

                if (string.IsNullOrWhiteSpace(loViewModel.loTabParameter.PARAM_CALLER_ACTION))
                {
                    IsCRUDModeButtonHidden = !loViewModel.loTabParameter.LOPEN_AS_PAGE;
                    if (loViewModel.loTabParameter.LOPEN_AS_PAGE == false)
                    {
                        PageWidth = "width: 1250px;";
                    }

                    if (!string.IsNullOrWhiteSpace(loViewModel.loTabParameter.CREC_ID) || !string.IsNullOrWhiteSpace(loViewModel.loTabParameter.PARAM_CALLER_REF_NO))
                    {
                        await _conductorRef.R_GetEntity(new PMT50610DTO
                        {
                            CREC_ID = loViewModel.loTabParameter.CREC_ID,
                            CDEPT_CODE = loViewModel.loTabParameter.PARAM_DEPT_CODE,
                            CPROPERTY_ID = loViewModel.loTabParameter.PARAM_PROPERTY_ID,
                            CREF_NO = loViewModel.loTabParameter.PARAM_CALLER_REF_NO,
                            CTRANS_CODE = loViewModel.loTabParameter.PARAM_CALLER_TRANS_CODE
                        });
                        loViewModel.loProperty.CPROPERTY_ID = loViewModel.loCreditNoteHeader.CPROPERTY_ID;
                        await loViewModel.GetPaymentTermListStreamAsync();
                    }
                }
                else
                {
                    if (loViewModel.loTabParameter.PARAM_CALLER_ACTION == VIEW_ONLY)
                    {
                        IsCRUDModeButtonHidden = true;
                        await _conductorRef.R_GetEntity(new PMT50610DTO
                        {
                            CREC_ID = loViewModel.loTabParameter.CREC_ID,
                            CDEPT_CODE = loViewModel.loTabParameter.PARAM_DEPT_CODE,
                            CPROPERTY_ID = loViewModel.loTabParameter.PARAM_PROPERTY_ID,
                            CREF_NO = loViewModel.loTabParameter.PARAM_CALLER_REF_NO,
                            CTRANS_CODE = loViewModel.loTabParameter.PARAM_CALLER_TRANS_CODE
                        });
                    }
                    else if (loViewModel.loTabParameter.PARAM_CALLER_ID == "PMT05500")
                    {
                        switch (loViewModel.loTabParameter.PARAM_CALLER_ACTION)
                        {
                            case NEW:
                                await _conductorRef.Add();
                                break;
                            case VIEW:
                                await _conductorRef.R_GetEntity(new PMT50610DTO
                                {
                                    CREC_ID = loViewModel.loTabParameter.CREC_ID,
                                    CDEPT_CODE = loViewModel.loTabParameter.PARAM_DEPT_CODE,
                                    CPROPERTY_ID = loViewModel.loTabParameter.PARAM_PROPERTY_ID,
                                    CREF_NO = loViewModel.loTabParameter.PARAM_CALLER_REF_NO,
                                    CTRANS_CODE = loViewModel.loTabParameter.PARAM_CALLER_TRANS_CODE
                                });
                                break;
                            default:
                                break;
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

        private void R_Before_OpenCreditNoteItem_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new InvoiceItemTabParameterDTO()
            {
                CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(PMT50611);
        }

        private async Task InvoiceHeader_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50610DTO loData = (PMT50610DTO)eventArgs.Data;
                if (!string.IsNullOrWhiteSpace(loData.CREC_ID) || !string.IsNullOrWhiteSpace(loData.CREF_NO))
                {
                    await loViewModel.GetInvoiceHeaderAsync(loData);
                    TransStatusChanged();
                    eventArgs.Result = loViewModel.loCreditNoteHeader;
                }
                else
                {
                    eventArgs.Result = eventArgs.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceHeader_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.DeleteInvoiceHeaderAsync((PMT50610DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceHHeader_AfterDelete()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _conductorRef.R_GetEntity(new PMT50610DTO { CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID });

                await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex); 
            }
            loException.ThrowExceptionIfErrors();
        }

        private void InvoiceHeader_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50610DTO loData = (PMT50610DTO)eventArgs.Data;
                loViewModel.InvoiceHeaderValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task InvoiceHeader_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.No)
            {
                eventArgs.Cancel = true;
                return;
            }
        }

        private async Task InvoiceHeader_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            if (loViewModel.loTransCode.LINCREMENT_FLAG == false)
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.OK);

                if (loValidate == R_eMessageBoxResult.OK)
                {
                    eventArgs.Cancel = true;
                    return;
                }
            }
        }

        private async Task InvoiceHeader_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            PMT50610DTO loData = (PMT50610DTO)eventArgs.Data;

            IsTaxEnabled = true;
            loData.CLOCAL_CURRENCY_CODE = loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE;
            loData.CBASE_CURRENCY_CODE = loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE;
            loData.DREF_DATE = DateTime.Today;
            loData.DDOC_DATE = DateTime.Today;
            IsTransStatus00 = false;
            IsTransStatus10 = false;
            IsJournalButtonEnabled = false;

            if (loViewModel.loTabParameter.PARAM_CALLER_ID == "PMT05500" && loViewModel.loTabParameter.PARAM_CALLER_ACTION == "NEW")
            {
                await PropertyComboBox_ValueChanged(loViewModel.loTabParameter.PARAM_PROPERTY_ID);
                loData.CPROPERTY_ID = loViewModel.loTabParameter.PARAM_PROPERTY_ID;

                loData.CDEPT_CODE = loViewModel.loTabParameter.PARAM_DEPT_CODE;
                loData.CDEPT_NAME = loViewModel.loTabParameter.PARAM_DEPT_NAME;
                loData.CTENANT_ID = loViewModel.loTabParameter.PARAM_CUSTOMER_ID;
                loData.CTENANT_NAME = loViewModel.loTabParameter.PARAM_CUSTOMER_NAME;
                loData.CCUSTOMER_TYPE_NAME = loViewModel.loTabParameter.PARAM_CUSTOMER_TYPE_NAME; 
                loData.CDOC_NO = loViewModel.loTabParameter.PARAM_DOC_NO;
                loData.DDOC_DATE = DateTime.Now;

                await CurrencyComboBox_ValueChanged(loViewModel.loTabParameter.PARAM_CURRENCY);
                loData.CCURRENCY_CODE = loViewModel.loTabParameter.PARAM_CURRENCY;

                loData.CTRANS_DESC = loViewModel.loTabParameter.PARAM_DESCRIPTION;
                loData.NLBASE_RATE = loViewModel.loTabParameter.PARAM_LBASE_RATE;
                loData.NLCURRENCY_RATE = loViewModel.loTabParameter.PARAM_LCURRENCY_RATE;
                loData.NBBASE_RATE = loViewModel.loTabParameter.PARAM_BBASE_RATE;
                loData.NBCURRENCY_RATE = loViewModel.loTabParameter.PARAM_BCURRENCY_RATE;

                await TaxableCheckbox_ValueChanged(loViewModel.loTabParameter.PARAM_TAXABLE);
                loData.LTAXABLE = loViewModel.loTabParameter.PARAM_TAXABLE;

                loData.CTAX_ID = loViewModel.loTabParameter.PARAM_TAX_ID;
                loData.CTAX_NAME = loViewModel.loTabParameter.PARAM_TAX_NAME;
                loData.NTAX_PCT = loViewModel.loTabParameter.PARAM_TAX_PCT;
                loData.NTAX_BASE_RATE = loViewModel.loTabParameter.PARAM_TAX_BRATE;
                loData.NTAX_CURRENCY_RATE = loViewModel.loTabParameter.PARAM_TAX_CURR_RATE;
                //loData.CDEPT_CODE = loViewModel.loTabParameter.PARAM_SERVICE_ID; Charges Id
                //loData.CDEPT_CODE = loViewModel.loTabParameter.PARAM_SERVICE_NAME; Charges Name
                //loData.CDEPT_CODE = loViewModel.loTabParameter.PARAM_ITEM_NOTES; Notes
            }
            else
            {
                if (loViewModel.loPropertyList.Count > 0)
                {
                    await PropertyComboBox_ValueChanged(loViewModel.loPropertyList.FirstOrDefault().CPROPERTY_ID);
                    loData.CPROPERTY_ID = loViewModel.loPropertyList.FirstOrDefault().CPROPERTY_ID;
                }
            }
        }

        private void InvoiceHeader_SetEdit(R_SetEventArgs eventArgs)
        {
            if (eventArgs.Enable)
            {
                IsTaxable = loViewModel.Data.LTAXABLE;
                IsTransStatus00 = false;
                IsTransStatus10 = false;
                IsJournalButtonEnabled = false;
                if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
                {
                    IsLocalCurrencyEnabled = true;
                    if (loViewModel.Data.LTAXABLE == true)
                    {
                        IsTaxCurrencyEnabled = true;
                    }
                    else
                    {
                        IsTaxCurrencyEnabled = false;
                    }
                }
                else
                {
                    IsTaxCurrencyEnabled = false;
                    IsLocalCurrencyEnabled = false;
                }

                if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CBASE_CURRENCY_CODE)
                {
                    IsBaseCurrencyEnabled = true;
                }
                else
                {
                    IsBaseCurrencyEnabled = false;
                }
            }
        }

        //private void InvoiceHeader_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        //{
        //    IsTaxable = loViewModel.Data.LTAXABLE;
        //    IsTransStatus00 = false;
        //    IsTransStatus10 = false;
        //    IsJournalButtonEnabled = false;
        //    if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
        //    {
        //        IsLocalCurrencyEnabled = true;
        //        if (loViewModel.Data.LTAXABLE == true)
        //        {
        //            IsTaxCurrencyEnabled = true;
        //        }
        //        else
        //        {
        //            IsTaxCurrencyEnabled = false;
        //        }
        //    }
        //    else
        //    {
        //        IsTaxCurrencyEnabled = false;
        //        IsLocalCurrencyEnabled = false;
        //    }

        //    if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CBASE_CURRENCY_CODE)
        //    {
        //        IsBaseCurrencyEnabled = true;
        //    }
        //    else
        //    {
        //        IsBaseCurrencyEnabled = false;
        //    }
        //}

        private async Task InvoiceHeader_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M006"], R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.No)
            {
                eventArgs.Cancel = true;
                return;
            }

            IsCustomerEnabled = false;
            IsBaseCurrencyEnabled = false;
            IsLocalCurrencyEnabled = false;
            IsTaxCurrencyEnabled = false;
            IsTaxable = false;
            TransStatusChanged();
        }

        private async Task InvoiceHeader_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResultDetail = null;
            R_PopupResult loResultSummary = null;

            try
            {
                TransStatusChanged();
                IsCustomerEnabled = false;
                IsBaseCurrencyEnabled = false;
                IsLocalCurrencyEnabled = false;
                IsTaxCurrencyEnabled = false;
                IsTaxable = false;
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loResultDetail = await PopupService.Show(typeof(PMT50600FRONT.PMT50620), new InvoiceItemTabParameterDTO()
                    {
                        CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID,
                        LIS_NEW = true
                    });

                    await loViewModel.CloseFormProcessAsync(new PMT50600COMMON.DTOs.PMT50620.OnCloseProcessParameterDTO()
                    {
                        CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID,
                        CPROPERTY_ID = loViewModel.loCreditNoteHeader.CPROPERTY_ID
                    });

                    loResultSummary = await PopupService.Show(typeof(PMT50600FRONT.PMT50630), new SummaryParameterDTO()
                    {
                        CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID
                    });
                    await _conductorRef.R_GetEntity(new PMT50610DTO { CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID });
                }

                //if (loResultDetail.Success == false || (bool)loResultDetail.Result == false)
                //{
                //    if (loResultSummary.Success == false || (bool)loResultSummary.Result == false)
                //    {

                //    }
                //    if ((bool)loResultSummary.Result)
                //    {
                //        await R_MessageBox.Show("", "AP System Parameter Created Successfully!", R_eMessageBoxButtonType.OK);
                //    }
                //}
                //if ((bool)loResultDetail.Result)
                //{
                //        await R_MessageBox.Show("", "AP System Parameter Created Successfully!", R_eMessageBoxButtonType.OK);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceHeader_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveInvoiceHeaderAsync((PMT50610DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                loViewModel.loCreditNoteHeader.CLOCAL_CURRENCY_CODE = loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE;
                loViewModel.loCreditNoteHeader.CBASE_CURRENCY_CODE = loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE;

                eventArgs.Result = loViewModel.loCreditNoteHeader;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region OnLostFocus

        private async Task OnLostFocusTax()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50610DTO loGetData = (PMT50610DTO)loViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CTAX_ID))
                {
                    loGetData.CTAX_NAME = "";
                    return;
                }

                loViewModel.Data.CREF_DATE = loViewModel.Data.DREF_DATE.Value.ToString("yyyyMMdd");

                LookupGSL00110ViewModel loLookupViewModel = new LookupGSL00110ViewModel();
                GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
                {
                    CTAX_DATE = loViewModel.Data.CREF_DATE,
                    CSEARCH_TEXT = loViewModel.Data.CTAX_ID
                };

                var loResult = await loLookupViewModel.GetTaxByDate(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    loGetData.NTAX_PCT = 0;
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTAX_ID = loResult.CTAX_ID;
                    loGetData.CTAX_NAME = loResult.CTAX_NAME;
                    loGetData.NTAX_PCT = loResult.NTAX_PERCENTAGE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50610DTO loGetData = (PMT50610DTO)loViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CDEPT_CODE))
                {
                    loGetData.CDEPT_NAME = "";
                    loGetData.CDOC_NO = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                    CSEARCH_TEXT = loViewModel.Data.CDEPT_CODE
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
                loGetData.CDOC_NO = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnLostFocusCustomer()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50610DTO loGetData = (PMT50610DTO)loViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CTENANT_ID))
                {
                    loGetData.CTENANT_NAME = "";
                    loGetData.CCUSTOMER_TYPE = "";
                    loGetData.CCUSTOMER_TYPE_NAME = "";
                    loGetData.CDOC_NO = "";

                    await CurrencyComboBox_ValueChanged("");
                    await TermComboBox_ValueChanged("");
                    await TaxableCheckbox_ValueChanged(false);

                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    loGetData.NTAX_PCT = 0;
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CCOMPANY_ID = "",
                    CCUSTOMER_TYPE = "",
                    CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                    CUSER_ID = "",
                    CSEARCH_TEXT = loViewModel.Data.CTENANT_ID
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    loGetData.CCUSTOMER_TYPE = "";
                    loGetData.CCUSTOMER_TYPE_NAME = "";
                    loGetData.CDOC_NO = "";

                    await CurrencyComboBox_ValueChanged("");
                    await TermComboBox_ValueChanged("");
                    await TaxableCheckbox_ValueChanged(false);

                    loGetData.CTAX_ID = "";
                    loGetData.CTAX_NAME = "";
                    loGetData.NTAX_PCT = 0;
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTENANT_ID = loResult.CTENANT_ID;
                    loGetData.CTENANT_NAME = loResult.CTENANT_NAME;
                    loGetData.CCUSTOMER_TYPE = loResult.CCUSTOMER_TYPE;
                    loGetData.CCUSTOMER_TYPE_NAME = loResult.CCUSTOMER_TYPE_NAME;
                    loGetData.CDOC_NO = "";

                    await CurrencyComboBox_ValueChanged(loResult.CCURRENCY_CODE);
                    await TermComboBox_ValueChanged(loResult.CPAYMENT_TERM_CODE);
                    await TaxableCheckbox_ValueChanged(loResult.LTAXABLE);

                    if (loGetData.LTAXABLE)
                    {
                        loGetData.CTAX_ID = loResult.CTAX_ID;
                        loGetData.CTAX_NAME = loResult.CTAX_NAME;
                        loGetData.NTAX_PCT = loResult.NTAX_PCT;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnLostFocusDocNo()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT50610DTO loGetData = (PMT50610DTO)loViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CDOC_NO))
                {
                    return;
                }
                if (loGetData.CCUSTOMER_TYPE == "01")
                {
                    return;
                }

                LookupLML00800ViewModel loLookupViewModel = new LookupLML00800ViewModel();
                LML00800ParameterDTO loParam = new LML00800ParameterDTO()
                {
                    CCOMPANY_ID = "",
                    CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                    CDEPT_CODE = loViewModel.Data.CDEPT_CODE,
                    CAGGR_STTS = "",
                    CLANG_ID = "",
                    CSEARCH_TEXT = loViewModel.Data.CDOC_NO
                };

                var loResult = await loLookupViewModel.GetAgreement(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDOC_NO = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CDOC_NO = loResult.CREF_NO;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup
        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.Data.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                CUSER_LOGIN_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void R_After_Open_LookupDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            //var loGetData = (PMT50600DTO)_conductorGeneralInfoRef.R_GetCurrentData();
            loViewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loViewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            loViewModel.Data.CDOC_NO = "";
        }

        private void R_Before_Open_LookupCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO loParam = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = "",
                CCUSTOMER_TYPE = "",
                CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                CUSER_ID = ""
            };
            //APL00100ParameterDTO loParam = new APL00100ParameterDTO()
            //{
            //    CCOMPANY_ID = "",
            //    CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
            //    CLANGUAGE_ID = ""
            //};
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private async Task R_After_Open_LookupCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {
            LML00600DTO loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CTENANT_ID = loTempResult.CTENANT_ID;
            loViewModel.Data.CTENANT_NAME = loTempResult.CTENANT_NAME;
            loViewModel.Data.CCUSTOMER_TYPE = loTempResult.CCUSTOMER_TYPE;
            loViewModel.Data.CCUSTOMER_TYPE_NAME = loTempResult.CCUSTOMER_TYPE_NAME;
            loViewModel.Data.CDOC_NO = "";

            await CurrencyComboBox_ValueChanged(loTempResult.CCURRENCY_CODE);
            await TermComboBox_ValueChanged(loTempResult.CPAYMENT_TERM_CODE);
            await TaxableCheckbox_ValueChanged(loTempResult.LTAXABLE);

            if (loViewModel.Data.LTAXABLE)
            {
                loViewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
                loViewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
                loViewModel.Data.NTAX_PCT = loTempResult.NTAX_PCT;
            }
        }

        private void R_Before_Open_LookupDocumentNo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00800ParameterDTO loParam = new LML00800ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                CDEPT_CODE = loViewModel.Data.CDEPT_CODE,
                CAGGR_STTS = "",
                CLANG_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00800);
        }

        private void R_After_Open_LookupDocumentNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            LML00800DTO loTempResult = (LML00800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CDOC_NO = loTempResult.CREF_NO;
        }

        private void R_Before_Open_LookupTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            loViewModel.Data.CREF_DATE = loViewModel.Data.DREF_DATE.Value.ToString("yyyyMMdd");
            GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
            {
                CCOMPANY_ID = "",
                CTAX_DATE = loViewModel.Data.CREF_DATE,
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00110);
        }

        private void R_After_Open_LookupTax(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00110DTO loTempResult = (GSL00110DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
            loViewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
            loViewModel.Data.NTAX_PCT = loTempResult.NTAX_PERCENTAGE;
        }
        #endregion


        #region OnChange

        private async Task TaxableCheckbox_ValueChanged(bool poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //loViewModel.Data.LTAXABLE = poParam;
                //IsTaxable = poParam;
                //if (_conductorRef.R_ConductorMode == R_eConductorMode.Edit && loViewModel.Data.IDETAIL_COUNT > 0)
                //{

                //}
                //else
                //{
                //    if (poParam == false)
                //    {
                //        IsTaxCurrencyEnabled = false;
                //        loViewModel.Data.CTAX_ID = "";
                //        loViewModel.Data.CTAX_NAME = "";
                //        loViewModel.Data.NTAX_PCT = 0;
                //    }
                //    else
                //    {
                //        if (string.IsNullOrWhiteSpace(loViewModel.Data.CCURRENCY_CODE) == false)
                //        {
                //            if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
                //            {
                //                IsTaxCurrencyEnabled = true;
                //            }
                //            else
                //            {
                //                IsTaxCurrencyEnabled = false;
                //            }
                //        }
                //        else
                //        {
                //            IsTaxCurrencyEnabled = false;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(poParam))
                {
                    poParam = "";
                }
                loViewModel.loProperty.CPROPERTY_ID = poParam;
                loViewModel.Data.CPROPERTY_ID = poParam;
                loViewModel.Data.CDEPT_CODE = "";
                loViewModel.Data.CDEPT_NAME = "";
                loViewModel.Data.CTENANT_ID = "";
                loViewModel.Data.CTENANT_NAME = "";
                loViewModel.Data.CCUSTOMER_TYPE_NAME = "";
                loViewModel.Data.CDOC_NO = "";
                await loViewModel.GetPaymentTermListStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DocDateDatePicker_ValueChanged(DateTime? poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPaymentTermListDTO loTemp = null;
            TimeSpan loTimeDiff;
            try
            {
                if (poParam > loViewModel.Data.DDUE_DATE || poParam == null)
                {
                    return;
                }
                loViewModel.Data.DDOC_DATE = poParam;
                loTimeDiff = loViewModel.Data.DDUE_DATE.Value - loViewModel.Data.DDOC_DATE.Value;
                loTemp = loViewModel.loPaymentTermList.Where(x => x.IPAY_TERM_DAYS == loTimeDiff.Days).FirstOrDefault();
                if (loTemp != null)
                {
                    loViewModel.Data.CPAY_TERM_CODE = loTemp.CPAY_TERM_CODE;
                }
                else
                {
                    loViewModel.Data.CPAY_TERM_CODE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DueDateDatePicker_ValueChanged(DateTime? poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPaymentTermListDTO loTemp = null;
            TimeSpan loTimeDiff;
            try
            {
                if (poParam < loViewModel.Data.DDOC_DATE || poParam == null)
                {
                    return;
                }
                loViewModel.Data.DDUE_DATE = poParam;
                loTimeDiff = loViewModel.Data.DDUE_DATE.Value - loViewModel.Data.DDOC_DATE.Value;
                loTemp = loViewModel.loPaymentTermList.Where(x => x.IPAY_TERM_DAYS == loTimeDiff.Days).FirstOrDefault();
                if (loTemp != null)
                {
                    loViewModel.Data.CPAY_TERM_CODE = loTemp.CPAY_TERM_CODE;
                }
                else
                {
                    loViewModel.Data.CPAY_TERM_CODE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task TermComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPaymentTermListDTO loTemp = null;

            try
            {
                if (string.IsNullOrEmpty(poParam))
                {
                    poParam = "";
                }
                loViewModel.Data.CPAY_TERM_CODE = poParam;
                try
                {
                    loTemp = loViewModel.loPaymentTermList.Where(x => x.CPAY_TERM_CODE == loViewModel.Data.CPAY_TERM_CODE).FirstOrDefault();
                }
                catch (Exception)
                {

                }

                if (loTemp == null)
                {
                    loViewModel.Data.DDUE_DATE = null;
                }
                else
                {
                    loViewModel.Data.DDUE_DATE = loViewModel.Data.DDOC_DATE.Value.AddDays(loTemp.IPAY_TERM_DAYS);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task CurrencyComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(poParam))
                {
                    poParam = "";
                }
                loViewModel.loCurrencyOrTaxRateParameter.CCURRENCY_CODE = poParam;
                loViewModel.loCurrencyOrTaxRateParameter.CRATETYPE_CODE = loViewModel.loAPSystemParam.CCUR_RATETYPE_CODE;
                loViewModel.loCurrencyOrTaxRateParameter.CREF_DATE = loViewModel.Data.DREF_DATE.Value.ToString("yyyyMMdd");
                loViewModel.Data.CCURRENCY_CODE = poParam;

                if (loViewModel.loTabParameter.PARAM_CALLER_ID == "PMT05500")
                {
                }
                else
                {
                    await loViewModel.GetCurrencyRateAsync();
                    if (loViewModel.loCurrencyRate != null)
                    {
                        loViewModel.Data.NLBASE_RATE = loViewModel.loCurrencyRate.NLBASE_RATE_AMOUNT;
                        loViewModel.Data.NLCURRENCY_RATE = loViewModel.loCurrencyRate.NLCURRENCY_RATE_AMOUNT;
                        loViewModel.Data.NBBASE_RATE = loViewModel.loCurrencyRate.NBBASE_RATE_AMOUNT;
                        loViewModel.Data.NBCURRENCY_RATE = loViewModel.loCurrencyRate.NBCURRENCY_RATE_AMOUNT;
                    }
                    else
                    {
                        loViewModel.Data.NLBASE_RATE = 1;
                        loViewModel.Data.NLCURRENCY_RATE = 1;
                        loViewModel.Data.NBBASE_RATE = 1;
                        loViewModel.Data.NBCURRENCY_RATE = 1;
                    }

                    loViewModel.loCurrencyOrTaxRateParameter.CRATETYPE_CODE = loViewModel.loAPSystemParam.CTAX_RATETYPE_CODE;
                    await loViewModel.GetTaxRateAsync();
                    if (loViewModel.loTaxRate != null)
                    {
                        loViewModel.Data.NTAX_BASE_RATE = loViewModel.loTaxRate.NLBASE_RATE_AMOUNT;
                        loViewModel.Data.NTAX_CURRENCY_RATE = loViewModel.loTaxRate.NLCURRENCY_RATE_AMOUNT;
                    }
                    else
                    {
                        loViewModel.Data.NTAX_BASE_RATE = 1;
                        loViewModel.Data.NTAX_CURRENCY_RATE = 1;
                    }
                }

                if (string.IsNullOrEmpty(loViewModel.Data.CCURRENCY_CODE))
                {
                    IsTaxCurrencyEnabled = false;
                    IsLocalCurrencyEnabled = false;
                    IsBaseCurrencyEnabled = false;
                }
                else
                {
                    if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
                    {
                        IsLocalCurrencyEnabled = true;
                        if (loViewModel.Data.LTAXABLE == true)
                        {
                            IsTaxCurrencyEnabled = true;
                        }
                        else
                        {
                            IsTaxCurrencyEnabled = false;
                        }
                    }
                    else
                    {
                        IsTaxCurrencyEnabled = false;
                        IsLocalCurrencyEnabled = false;
                    }

                    if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CBASE_CURRENCY_CODE)
                    {
                        IsBaseCurrencyEnabled = true;
                    }
                    else
                    {
                        IsBaseCurrencyEnabled = false;
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

        #region Button
        private async Task OnClickTax()
        {
            R_Exception loEx = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickClose()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickPrint()
        {
            R_Exception loEx = new R_Exception();
            PMT50610PrintReportParameterDTO loParam = null;
            try
            {
                if (string.IsNullOrWhiteSpace(loViewModel.loCreditNoteHeader.CREC_ID))
                {
                    return;
                }
                loParam = new PMT50610PrintReportParameterDTO()
                {
                    CLOGIN_COMPANY_ID = clientHelper.CompanyId,
                    CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName,
                    CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID
                };

                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMT50610Print/PrintReportPost",
                    "rpt/PMT50610Print/PrintReportGet",
                    loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void TransStatusChanged()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (loViewModel.loCreditNoteHeader.CTRANS_STATUS == "00")
                {
                    IsTransStatus00 = true;
                }
                else
                {
                    IsTransStatus00 = false;
                }

                if (loViewModel.loCreditNoteHeader.CTRANS_STATUS == "10")
                {
                    IsTransStatus10 = true;
                }
                else
                {
                    IsTransStatus10 = false;
                }

                int lnCompareResult = String.Compare(loViewModel.loCreditNoteHeader.CTRANS_STATUS, "00");
                if (lnCompareResult > 0 && loViewModel.loCreditNoteHeader.CGL_REF_NO != "")
                {
                    IsJournalButtonEnabled = true;
                }
                else
                {
                    IsJournalButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickSubmit()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M004"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.No)
                {
                    return;
                }

                await loViewModel.SubmitJournalProcessAsync();
                if (!loEx.HasError)
                {
                    await _conductorRef.R_GetEntity(new PMT50610DTO { CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID });
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRedraft()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M005"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.No)
                {
                    return;
                }

                await loViewModel.RedraftJournalProcessAsync();
                if (!loEx.HasError)
                {
                    await _conductorRef.R_GetEntity(new PMT50610DTO { CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID });
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Before_Open_Journal_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GLF00100ParameterDTO()
            {
                CDEPT_CODE = loViewModel.loCreditNoteHeader.CDEPT_CODE,
                CTRANS_CODE = "920040",
                CREF_NO = loViewModel.loCreditNoteHeader.CGL_REF_NO
            };  
            eventArgs.TargetPageType = typeof(GLF00100);
        }

        private void After_Open_Journal_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
        }

        private void Before_Open_Detail_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new InvoiceItemTabParameterDTO()
            {
                CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(PMT50620);
        }

        private async Task After_Open_Detail_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (loViewModel.Data.CTRANS_STATUS == "00")
            {
                R_PopupResult loResult = null;
                loResult = await PopupService.Show(typeof(PMT50600FRONT.PMT50630), new SummaryParameterDTO()
                {
                    CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID
                });

                await _conductorRef.R_GetEntity(new PMT50610DTO { CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID });

                if (eventArgs.Success == false)
                {
                    return;
                }
            }
        }

        private void Before_Open_Summary_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new SummaryParameterDTO()
            {
                CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(PMT50630);
        }

        private async Task After_Open_Summary_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            await _conductorRef.R_GetEntity(new PMT50610DTO { CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID });
            if (eventArgs.Success == false)
            {
                return;
            }
        }

        private void Before_Open_Allocate_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //eventArgs.Parameter = new OpenAllocationParameterDTO()
            //{
            //    CPROPERTY_ID = loViewModel.loCreditNoteHeader.CPROPERTY_ID,
            //    CREC_ID = loViewModel.loCreditNoteHeader.CREC_ID,
            //    CDEPT_CODE = loViewModel.loCreditNoteHeader.CDEPT_CODE,
            //    CTRANS_CODE = "920040",
            //    CREF_NO = loViewModel.loCreditNoteHeader.CREF_NO,
            //    LDISPLAY_ONLY = false
            //};
           // eventArgs.TargetPageType = typeof(PMF00100);
        }

        private void After_Open_Allocate_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
        }

        #endregion
    }
}
