using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00900;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMFRONT
{
    public partial class LML00900
    {
        private LookupLML00900ViewModel _viewModelLML00900 = new LookupLML00900ViewModel();
        private R_Grid<LML00900FrontDTO>? GridRef;
        private int _pageSize = 15;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelLML00900._Parameter =  (LML00900ParameterDTO)poParameter;
                await _viewModelLML00900.GetInitialProcess();
                _viewModelLML00900.GetMonth();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
             //   var loParam = (LML00900ParameterDTO)eventArgs.Parameter;
                await _viewModelLML00900.GetTransactionList();
                eventArgs.ListEntityResult = _viewModelLML00900.TransactionList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task BtnRefresh()
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelLML00900.ValidationFieldEmpty();
                await GridRef!.R_RefreshGrid(null);

                //if (_viewModelGLB00200.ReversingJournalProcessList.Count() < 1)
                //{
                //    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_01");
                //    loEx.Add(loErr);
                //    await _gridReversing.R_RefreshGrid(null);
                //    goto EndBlock;
                //}
                //_viewModelGLB00200.CurrentReversingJournal =
                //    _viewModelGLB00200.ReversingJournalProcessList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void IsPeriodAll(object poParam)
        {
            var loEx = new R_Exception();
            string lcPeriodModeValue = (string)poParam;
            try
            {
                _viewModelLML00900.PeriodValue = lcPeriodModeValue;

                if (_viewModelLML00900.PeriodValue == "P")
                {
                    _viewModelLML00900._enableYearMonthField = true;
                }
                else
                {
                    _viewModelLML00900._enableYearMonthField = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void ChangePeriodMonth(object poParam)
        {
            var loEx = new R_Exception();
            string lcPeriodMonth= (string)poParam;
            try
            {
                _viewModelLML00900.PeriodMonth = lcPeriodMonth;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        public async Task Button_OnClickOkAsync()
        {
            var loData = GridRef!.GetCurrentData();
            await Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await Close(true, null);
        }

        #region Utility

        #endregion
    }
}
