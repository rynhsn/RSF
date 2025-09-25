using LMM01500COMMON;
using LMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Xml.Linq;

namespace LMM01500FRONT
{
    public partial class LMM01520 : R_Page, R_ITabPage
    {
        private LMM01520ViewModel _InvPinalty_viewModel = new LMM01520ViewModel();
        private R_Conductor _InvPinalty_conductorRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<LMM01520DTO>(poParameter);
                _InvPinalty_viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                _InvPinalty_viewModel.InvGrpCode = loData.CINVGRP_CODE;
                _InvPinalty_viewModel.InvGrpName = loData.CINVGRP_NAME;

                await _InvPinalty_conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loData = R_FrontUtility.ConvertObjectToObject<LMM01520DTO>(poParam);
            if (string.IsNullOrWhiteSpace(loData.CINVGRP_CODE))
            {
                await _InvPinalty_conductorRef.R_SetCurrentData(null);
                _InvPinalty_viewModel.InvGrpCode = "";
                _InvPinalty_viewModel.InvGrpName = "";
            }
            else
            {
                _InvPinalty_viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                _InvPinalty_viewModel.InvGrpCode = loData.CINVGRP_CODE;
                _InvPinalty_viewModel.InvGrpName = loData.CINVGRP_NAME;

                await _InvPinalty_conductorRef.R_GetEntity(loData);
            }
        }

        private async Task Pinalty_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinalty_viewModel.GetInvoicePinalty((LMM01520DTO)eventArgs.Data);

                eventArgs.Result = _InvPinalty_viewModel.InvPinalty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private R_CheckBox EnbalePinalty_CheckBox;
        private async Task Pinalty_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await EnbalePinalty_CheckBox.FocusAsync();
            }
        }

        private bool MonthlyAmmountEnable = false;
        private bool MonthlyPercentageEnable = false;
        private bool DailyAmmountEnable = false;
        private bool DailyPercentageEnable = false;
        private bool OneTimeAmmountEnable = false;
        private bool OneTimePercentageEnable = false;
        private bool CalcBaseonMonthEnable = false;
        private bool CalcBaseonDaysEnable = false;

        private void Pinalty_RadioButtonOnChange(string poParam)
        {
            _InvPinalty_viewModel.Data.CPENALTY_TYPE = poParam;

            var loData = (LMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();
            MonthlyAmmountEnable = poParam.ToString() == "10";

            MonthlyPercentageEnable = poParam.ToString() == "11";
            CalcBaseonMonthEnable = poParam.ToString() == "11";

            DailyAmmountEnable = poParam.ToString() == "20";

            DailyPercentageEnable = poParam.ToString() == "21";
            CalcBaseonDaysEnable = poParam.ToString() == "21";

            OneTimeAmmountEnable = poParam.ToString() == "30";

            OneTimePercentageEnable = poParam.ToString() == "31";

            if (MonthlyAmmountEnable == false)
                loData.NPENALTY_TYPE_VALUE_MonthlyAmmount = 0;

            if (DailyAmmountEnable == false)
                loData.NPENALTY_TYPE_VALUE_DailyAmmount = 0;

            if (OneTimeAmmountEnable == false)
                loData.NPENALTY_TYPE_VALUE_OneTimeAmmount = 0;

            if (MonthlyPercentageEnable == false)
            {
                loData.NPENALTY_TYPE_VALUE_MonthlyPercentage = 0;
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth = "";
            }
            else
            {
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth = "01";
            }

            if (DailyPercentageEnable == false)
            {
                loData.NPENALTY_TYPE_VALUE_DailyPercentage = 0;
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays = "";
            }
            else
            {
                loData.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays = "01";
            }
        }

        private void Pinalty_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _InvPinalty_viewModel.ValidationPinalty(
                     (LMM01520DTO)eventArgs.Data);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        private async Task Pinalty_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinalty_viewModel.SaveInvoicePinalty(
                    (LMM01520DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _InvPinalty_viewModel.InvPinalty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool MinAmmountEnable;
        private void Pinalty_OnCheckedMinAmmount(bool poParam)
        {
            _InvPinalty_viewModel.MinPinaltyAmount = poParam;
            MinAmmountEnable = poParam;
        }

        private bool MaxAmmountEnable;
        private void Pinalty_OnCheckedMaxAmmount(bool poParam)
        {
            _InvPinalty_viewModel.MaxPinaltyAmount = poParam;
            MaxAmmountEnable = poParam;
        }

        private void Pinalty_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loData = (LMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();
            MonthlyAmmountEnable = loData.CPENALTY_TYPE == "10";

            MonthlyPercentageEnable = loData.CPENALTY_TYPE == "11";
            CalcBaseonMonthEnable = loData.CPENALTY_TYPE == "11";

            DailyAmmountEnable = loData.CPENALTY_TYPE == "20";

            DailyPercentageEnable = loData.CPENALTY_TYPE == "21";
            CalcBaseonDaysEnable = loData.CPENALTY_TYPE == "21";

            OneTimeAmmountEnable = loData.CPENALTY_TYPE == "30";

            OneTimePercentageEnable = loData.CPENALTY_TYPE == "31";
        }

        private void Pinalty_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            MonthlyAmmountEnable = false;

            MonthlyPercentageEnable = false;
            CalcBaseonMonthEnable = false;

            DailyAmmountEnable = false;

            DailyPercentageEnable = false;
            CalcBaseonDaysEnable = false;

            OneTimeAmmountEnable = false;

            OneTimePercentageEnable = false;
        }

        private void Pinalty_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            MonthlyAmmountEnable = false;

            MonthlyPercentageEnable = false;
            CalcBaseonMonthEnable = false;

            DailyAmmountEnable = false;

            DailyPercentageEnable = false;
            CalcBaseonDaysEnable = false;

            OneTimeAmmountEnable = false;

            OneTimePercentageEnable = false;
        }
        private void PinaltyId_OnLostFocus(object poParam)
        {
            //_InvPinalty_viewModel.Data.CPENALTY_ADD_ID = (string)poParam;
        }
        private void Pinalty_OtherCharges_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01400ParameterDTO { CPROPERTY_ID = _InvPinalty_viewModel.PropertyValueContext, CCHARGES_TYPE_ID = "A" };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01400);
        }

        private void Pinalty_OtherCharges_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL01400DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                var loData = (LMM01520DTO)_InvPinalty_conductorRef.R_GetCurrentData();
                loData.CPENALTY_ADD_ID = loTempResult.CCHARGES_ID;
                loData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
