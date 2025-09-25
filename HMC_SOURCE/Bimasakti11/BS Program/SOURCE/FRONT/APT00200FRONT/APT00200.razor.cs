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
using APT00200MODEL.ViewModel;
using APT00200COMMON.DTOs.APT00200;
using System.Net.Security;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_APFRONT;
using Lookup_APCOMMON.DTOs;
using Lookup_APCOMMON.DTOs.APL00100;
using R_BlazorFrontEnd.Interfaces;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Helpers;

namespace APT00200FRONT
{
    public partial class APT00200 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<APT00200FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private APT00200ViewModel loPurchaseReturnViewModel = new APT00200ViewModel();

        private List<SupplierOptionRadioButton> loSupplierOptionRadioButton = new List<SupplierOptionRadioButton>()
        {
            new SupplierOptionRadioButton()
                {
                    CSUPPLIER_OPTION_CODE = "A",
                    CSUPPLIER_OPTION_NAME = "All Suppliers"
                },
            new SupplierOptionRadioButton()
                {
                    CSUPPLIER_OPTION_CODE = "S",
                    CSUPPLIER_OPTION_NAME = "Selected Supplier"
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
        private R_ConductorGrid _conductorPurchaseReturnRef;

        private R_Grid<APT00200DetailDTO> _gridPurchaseReturnRef;

        private bool IsPurchaseReturnListExist = true;

        private bool IsSupplierEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_OPTIONS = "A";
                //loPurchaseReturnViewModel.loPurchaseReturn.IPERIOD_FROM_YEAR = DateTime.Now.Year;
                //loPurchaseReturnViewModel.loPurchaseReturn.IPERIOD_TO_YEAR = DateTime.Now.Year;
                //loPurchaseReturnViewModel.loPurchaseReturn.CPERIOD_FROM_MONTH = DateTime.Now.ToString("MM");
                //loPurchaseReturnViewModel.loPurchaseReturn.CPERIOD_TO_MONTH = DateTime.Now.ToString("MM");
                
                IsPurchaseReturnListExist = false;
                await loPurchaseReturnViewModel.InitialProcess();
                await loPurchaseReturnViewModel.GetPropertyListStreamAsync();
                if (loPurchaseReturnViewModel.loPropertyList.Count() > 0)
                {
                    loPurchaseReturnViewModel.loProperty = loPurchaseReturnViewModel.loPropertyList.FirstOrDefault();
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
                loPurchaseReturnViewModel.RefreshPurchaseReturnListValidation();
                await _gridPurchaseReturnRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task SupplierOptionRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_OPTIONS = poParam;
                if (poParam == "A")
                {
                    loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_ID = "";
                    loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_NAME = "";
                    IsSupplierEnabled = false;
                }
                else if (poParam == "S")
                {
                    IsSupplierEnabled = true;
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
                if (poParam <= loPurchaseReturnViewModel.loPurchaseReturn.IPERIOD_TO_YEAR)
                {
                    loPurchaseReturnViewModel.loPurchaseReturn.IPERIOD_FROM_YEAR = poParam;
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
                if (poParam >= loPurchaseReturnViewModel.loPurchaseReturn.IPERIOD_FROM_YEAR)
                {
                    loPurchaseReturnViewModel.loPurchaseReturn.IPERIOD_TO_YEAR = poParam;
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
                if (int.Parse(poParam) <= int.Parse(loPurchaseReturnViewModel.loPurchaseReturn.CPERIOD_TO_MONTH))
                {
                    loPurchaseReturnViewModel.loPurchaseReturn.CPERIOD_FROM_MONTH = poParam;
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
                if (int.Parse(poParam) >= int.Parse(loPurchaseReturnViewModel.loPurchaseReturn.CPERIOD_FROM_MONTH))
                {
                    loPurchaseReturnViewModel.loPurchaseReturn.CPERIOD_TO_MONTH = poParam;
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
                loPurchaseReturnViewModel.loPurchaseReturn.CPROPERTY_ID = poParam;
                loPurchaseReturnViewModel.loProperty.CPROPERTY_ID = poParam;
                loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_ID = "";
                loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_NAME = "";
                loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_CODE = "";
                loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_PurchaseReturn_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loPurchaseReturnViewModel.loSelectedPurchaseReturn = null;
                await loPurchaseReturnViewModel.GetPurchaseReturnListStreamAsync();
                if (loPurchaseReturnViewModel.loPurchaseReturnList.Count() == 0)
                {
                    IsPurchaseReturnListExist = false;
                    loPurchaseReturnViewModel.loSelectedPurchaseReturn = new APT00200DetailDTO();
                    await R_MessageBox.Show("", "No data found!", R_eMessageBoxButtonType.OK);
                }
                else if (loPurchaseReturnViewModel.loPurchaseReturnList.Count() > 0)
                {
                    IsPurchaseReturnListExist = true;
                    loPurchaseReturnViewModel.loSelectedPurchaseReturn = loPurchaseReturnViewModel.loPurchaseReturnList.FirstOrDefault();
                }
                eventArgs.ListEntityResult = loPurchaseReturnViewModel.loPurchaseReturnList;
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
            PurchaseReturnEntryPredifineParameterDTO loParam = null;
            try
            {
                loParam = new PurchaseReturnEntryPredifineParameterDTO()
                {
                    CREC_ID = loPurchaseReturnViewModel.loSelectedPurchaseReturn.CREC_ID,
                    LOPEN_AS_PAGE = true
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(APT00210);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task R_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_ID) || string.IsNullOrWhiteSpace(loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_CODE))
            {
                return;
            }
            await _gridPurchaseReturnRef.R_RefreshGrid(null);
        }

        private void Grid_PurchaseReturn_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loPurchaseReturnViewModel.loSelectedPurchaseReturn = (APT00200DetailDTO)eventArgs.Data;
            }
        }

        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_CODE))
                {
                    loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = loPurchaseReturnViewModel.loProperty.CPROPERTY_ID,
                    CSEARCH_TEXT = loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_CODE
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_CODE = "";
                    loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_CODE = loResult.CDEPT_CODE;
                    loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_NAME = loResult.CDEPT_NAME;
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
            if (string.IsNullOrWhiteSpace(loPurchaseReturnViewModel.loProperty.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loPurchaseReturnViewModel.loProperty.CPROPERTY_ID,
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
            //var loGetData = (APT00200DTO)_conductorGeneralInfoRef.R_GetCurrentData();
            loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_CODE = loTempResult.CDEPT_CODE;
            loPurchaseReturnViewModel.loPurchaseReturn.CDEPARTMENT_NAME = loTempResult.CDEPT_NAME;
        }

        private void R_Before_Open_LookupSupplier(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00100ParameterDTO loParam = new APL00100ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loPurchaseReturnViewModel.loProperty.CPROPERTY_ID,
                CLANGUAGE_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APL00100);
        }

        private void R_After_Open_LookupSupplier(R_AfterOpenLookupEventArgs eventArgs)
        {
            APL00100DTO loTempResult = (APL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            loPurchaseReturnViewModel.loPurchaseReturn.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }

    }
    public class SupplierOptionRadioButton
    {
        public string CSUPPLIER_OPTION_CODE { get; set; }
        public string CSUPPLIER_OPTION_NAME { get; set; }
    }
    public class PeriodMonth
    {
        public string CCODE { get; set; }
    }
}
