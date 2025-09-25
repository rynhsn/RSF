using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT50600MODEL.ViewModel;
using PMT50600COMMON.DTOs.PMT50600;
using System.Net.Security;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Helpers;
using Lookup_PMCOMMON.DTOs;
using PMT50600FrontResources;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;

namespace PMT50600FRONT
{
    public partial class PMT50600 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT50600FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT50600ViewModel loCreditNoteViewModel = new PMT50600ViewModel();

        private List<CustomerOptionRadioButton> loCustomerOptionRadioButton = new List<CustomerOptionRadioButton>()
        {
            new CustomerOptionRadioButton()
                {
                    CTENANT_OPTION_CODE = "A",
                    CTENANT_OPTION_NAME = "All Customer"
                },
            new CustomerOptionRadioButton()
                {
                    CTENANT_OPTION_CODE = "S",
                    CTENANT_OPTION_NAME = "Selected Customer"
                }
        };

        private List<PeriodMonth> loPeriodMonth = new List<PeriodMonth>()
        {
            new PeriodMonth() { CCODE = "01" },
            new PeriodMonth() { CCODE = "02" },
            new PeriodMonth() { CCODE = "03" },
            new PeriodMonth() { CCODE = "04" },
            new PeriodMonth() { CCODE = "05" },
            new PeriodMonth() { CCODE = "06" },
            new PeriodMonth() { CCODE = "07" },
            new PeriodMonth() { CCODE = "08" },
            new PeriodMonth() { CCODE = "09" },
            new PeriodMonth() { CCODE = "10" },
            new PeriodMonth() { CCODE = "11" },
            new PeriodMonth() { CCODE = "12" }
        };
        private R_ConductorGrid _conductorCreditNoteRef;

        private R_Grid<PMT50600DetailDTO> _gridInvoiceRef;

        private bool IsInvoiceListExist = true;

        private bool IsCustomerEnabled = false;

        private bool IsTabEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //loCreditNoteViewModel.loCreditNote.CTENANT_OPTIONS = "A";
                //loCreditNoteViewModel.loCreditNote.IPERIOD_FROM_YEAR = DateTime.Now.Year;
                //loCreditNoteViewModel.loCreditNote.IPERIOD_TO_YEAR = DateTime.Now.Year;
                //loCreditNoteViewModel.loCreditNote.CPERIOD_FROM_MONTH = DateTime.Now.ToString("MM");
                //loCreditNoteViewModel.loCreditNote.CPERIOD_TO_MONTH = DateTime.Now.ToString("MM");
                
                IsInvoiceListExist = false;
                await loCreditNoteViewModel.GetPropertyListStreamAsync();
                if (loCreditNoteViewModel.loPropertyList.Count() > 0)
                {
                    IsTabEnabled = true;
                    loCreditNoteViewModel.loProperty = loCreditNoteViewModel.loPropertyList.FirstOrDefault();
                    await loCreditNoteViewModel.InitialProcess();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRefresh()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(loCreditNoteViewModel.loProperty.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V041"));
                }
                if (!loEx.HasError)
                {
                    loCreditNoteViewModel.RefreshInvoiceListValidation();
                    await _gridInvoiceRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task CustomerOptionRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loCreditNoteViewModel.loCreditNote.CTENANT_OPTIONS = poParam;
                if (poParam == "A")
                {
                    loCreditNoteViewModel.loCreditNote.CTENANT_ID = "";
                    loCreditNoteViewModel.loCreditNote.CTENANT_NAME = "";
                    IsCustomerEnabled = false;
                }
                else if (poParam == "S")
                {
                    IsCustomerEnabled = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PeriodYearFromDropdownList_ValueChanged(int poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (poParam <= loCreditNoteViewModel.loCreditNote.IPERIOD_TO_YEAR)
                {
                    loCreditNoteViewModel.loCreditNote.IPERIOD_FROM_YEAR = poParam;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PeriodYearToDropdownList_ValueChanged(int poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (poParam >= loCreditNoteViewModel.loCreditNote.IPERIOD_FROM_YEAR)
                {
                    loCreditNoteViewModel.loCreditNote.IPERIOD_TO_YEAR = poParam;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        private async Task PeriodMonthFromDropdownList_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (int.Parse(poParam) <= int.Parse(loCreditNoteViewModel.loCreditNote.CPERIOD_TO_MONTH))
                {
                    loCreditNoteViewModel.loCreditNote.CPERIOD_FROM_MONTH = poParam;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PeriodMonthToDropdownList_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (int.Parse(poParam) >= int.Parse(loCreditNoteViewModel.loCreditNote.CPERIOD_FROM_MONTH))
                {
                    loCreditNoteViewModel.loCreditNote.CPERIOD_TO_MONTH = poParam;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loCreditNoteViewModel.loCreditNote.CPROPERTY_ID = poParam;
                loCreditNoteViewModel.loProperty.CPROPERTY_ID = poParam;
                loCreditNoteViewModel.loCreditNote.CTENANT_ID = "";
                loCreditNoteViewModel.loCreditNote.CTENANT_NAME = "";
                loCreditNoteViewModel.loCreditNote.CDEPARTMENT_CODE = "";
                loCreditNoteViewModel.loCreditNote.CDEPARTMENT_NAME = "";
                await loCreditNoteViewModel.GetPMSystemParamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_CreditNote_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loCreditNoteViewModel.loSelectedInvoice = null;
                await loCreditNoteViewModel.GetInvoiceListStreamAsync();
                if (loCreditNoteViewModel.loCreditNoteList.Count() == 0)
                {
                    IsInvoiceListExist = false;
                    loCreditNoteViewModel.loSelectedInvoice = new PMT50600DetailDTO();
                    await R_MessageBox.Show("", "No data found!", R_eMessageBoxButtonType.OK);
                }
                else if (loCreditNoteViewModel.loCreditNoteList.Count() > 0)
                {
                    IsInvoiceListExist = true;
                    loCreditNoteViewModel.loSelectedInvoice = loCreditNoteViewModel.loCreditNoteList.FirstOrDefault();
                }
                eventArgs.ListEntityResult = loCreditNoteViewModel.loCreditNoteList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void PreDock_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            InvoiceEntryPredifineParameterDTO loParam = null;
            try
            {
                loParam = new InvoiceEntryPredifineParameterDTO()
                {
                    CREC_ID = loCreditNoteViewModel.loSelectedInvoice.CREC_ID,
                    PARAM_PROPERTY_ID = loCreditNoteViewModel.loProperty.CPROPERTY_ID,
                    LOPEN_AS_PAGE = true
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(PMT50610);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task R_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loCreditNoteViewModel.loCreditNote.CTENANT_ID) || string.IsNullOrWhiteSpace(loCreditNoteViewModel.loCreditNote.CDEPARTMENT_CODE))
            {
                return;
            }
            await _gridInvoiceRef.R_RefreshGrid(null);
        }

        private void Grid_CreditNote_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loCreditNoteViewModel.loSelectedInvoice = (PMT50600DetailDTO)eventArgs.Data;
            }
        }

        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(loCreditNoteViewModel.loCreditNote.CDEPARTMENT_CODE))
                {
                    loCreditNoteViewModel.loCreditNote.CDEPARTMENT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = loCreditNoteViewModel.loProperty.CPROPERTY_ID,
                    CSEARCH_TEXT = loCreditNoteViewModel.loCreditNote.CDEPARTMENT_CODE
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loCreditNoteViewModel.loCreditNote.CDEPARTMENT_CODE = "";
                    loCreditNoteViewModel.loCreditNote.CDEPARTMENT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loCreditNoteViewModel.loCreditNote.CDEPARTMENT_CODE = loResult.CDEPT_CODE;
                    loCreditNoteViewModel.loCreditNote.CDEPARTMENT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loCreditNoteViewModel.loProperty.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loCreditNoteViewModel.loProperty.CPROPERTY_ID,
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
            loCreditNoteViewModel.loCreditNote.CDEPARTMENT_CODE = loTempResult.CDEPT_CODE;
            loCreditNoteViewModel.loCreditNote.CDEPARTMENT_NAME = loTempResult.CDEPT_NAME;
        }

        private async Task OnLostFocusCustomer()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //PMT50210DTO loGetData = (PMT50210DTO)loViewModel.Data;

                if (string.IsNullOrWhiteSpace(loCreditNoteViewModel.loCreditNote.CTENANT_ID))
                {
                    loCreditNoteViewModel.loCreditNote.CTENANT_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CCOMPANY_ID = "",
                    CCUSTOMER_TYPE = "",
                    CPROPERTY_ID = loCreditNoteViewModel.loProperty.CPROPERTY_ID,
                    CUSER_ID = "",
                    CSEARCH_TEXT = loCreditNoteViewModel.loCreditNote.CTENANT_ID
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loCreditNoteViewModel.loCreditNote.CTENANT_ID = "";
                    loCreditNoteViewModel.loCreditNote.CTENANT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loCreditNoteViewModel.loCreditNote.CTENANT_ID = loResult.CTENANT_ID;
                    loCreditNoteViewModel.loCreditNote.CTENANT_NAME = loResult.CTENANT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void R_Before_Open_LookupCustomer(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO loParam = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loCreditNoteViewModel.loProperty.CPROPERTY_ID,
                CCUSTOMER_TYPE = "",
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private void R_After_Open_LookupCustomer(R_AfterOpenLookupEventArgs eventArgs)
        {
            LML00600DTO loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loCreditNoteViewModel.loCreditNote.CTENANT_ID = loTempResult.CTENANT_ID;
            loCreditNoteViewModel.loCreditNote.CTENANT_NAME = loTempResult.CTENANT_NAME;
        }

    }
    public class CustomerOptionRadioButton
    {
        public string CTENANT_OPTION_CODE { get; set; }
        public string CTENANT_OPTION_NAME { get; set; }
    }
    public class PeriodMonth
    {
        public string CCODE { get; set; }
    }
}
