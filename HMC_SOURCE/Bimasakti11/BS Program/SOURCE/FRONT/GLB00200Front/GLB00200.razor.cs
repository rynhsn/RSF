using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using GLB00200Common;
using GLB00200FrontResources;
using GLB00200Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLB00200Front
{
    public partial class GLB00200 : R_Page
    {
        private GLB00200ViewModel _viewModelGLB00200 = new GLB00200ViewModel();
        private R_Conductor? _conductorPeriod;

        private R_Grid<GLB00200DTO>? _gridReversing;
        private R_ConductorGrid? _conductorReversingJournal;

        [Inject] IClientHelper? clientHelper { get; set; }

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        private void DisplayErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await ServiceGetInitialProcess();
                GetMonth();
                await _gridReversing!.R_RefreshGrid(null);
                _viewModelGLB00200.COMPANYID = clientHelper?.CompanyId;
                _viewModelGLB00200.USERID = clientHelper?.UserId;

                _viewModelGLB00200.StateChangeAction = StateChangeInvoke;
                _viewModelGLB00200.DisplayErrorAction = DisplayErrorInvoke;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task ServiceGetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelGLB00200.GetInitialprocess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public void GetMonth()
        {
            _viewModelGLB00200.GetMonthList = new List<GetMonthDTO>();

            for (int i = 1; i <= 12; i++)
            {
                string monthId = i.ToString("D2");
                GetMonthDTO month = new GetMonthDTO { Id = monthId };
                _viewModelGLB00200.GetMonthList.Add(month);
            }

        }
        private void OnChangedMonth(object poParam)
        {
            var loEx = new R_Exception();
            string PeriodMonthValue = (string)poParam;
            try
            {
                _viewModelGLB00200.PeriodMonth = PeriodMonthValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task GetList_ReversingJournal(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModelGLB00200.GetAllReversingJournalProcess();
                eventArgs.ListEntityResult = _viewModelGLB00200.ReversingJournalProcessList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetList_ReversingJournalWithCustomPeriod()
        {
            var loEx = new R_Exception();
            try
            {

                await _viewModelGLB00200.GetAllReversingJournalProcess();

                if (_viewModelGLB00200.ReversingJournalProcessList.Count() < 1)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_00201");
                    loEx.Add(loErr);
                    await _gridReversing.R_RefreshGrid(null);
                    goto EndBlock;
                }
                _viewModelGLB00200.CurrentReversingJournal =
                    _viewModelGLB00200.ReversingJournalProcessList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #region SearchButton

        private async Task GetList_ReversingJournalWithSearch()
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelGLB00200.ValidationFieldEmpty();
                
                await _viewModelGLB00200.GetAllReversingJournalProcess();

                if (_viewModelGLB00200.ReversingJournalProcessList.Count() < 1)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_00201");
                    loEx.Add(loErr);
                    await _gridReversing.R_RefreshGrid(null);
                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
        private void Service_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    _viewModelGLB00200.CurrentReversingJournal = (GLB00200DTO)eventArgs.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void ButtonDetail_GetList_ReversingJournal(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.TargetPageType = typeof(GLB00200Detail);
                eventArgs.Parameter = _viewModelGLB00200.CurrentReversingJournal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region ProcessButton
        private async Task OnClickProcess()
        {
            var loEx = new R_Exception();
            try
            {
                await _gridReversing!.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Save Batch
        private void BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        { var loEx = new R_Exception();

            //var loData = (List<GLB00200DTO>)events.Data;

            //if (loData.Count == 0)
            //{
            //    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_04");
            //    loEx.Add(loErr);
            //    events.Cancel = true;
            //}
            //Validasi Incement Flag
            if (_viewModelGLB00200.GetInitialProcess.LINCREMENT_FLAG == false)
            {
                var loErr = R_FrontUtility.R_GetError(typeof(Resources_GLB00200_Class), "Error_00205");
                loEx.Add(loErr);
                events.Cancel = true;
            }
        }
        private async Task ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModelGLB00200.loProcessReversingList = (List<GLB00200DTO>)eventArgs.Data;

                await _viewModelGLB00200.GetSelectedDataToReversingJournal();
                await _gridReversing!.R_RefreshGrid(null);


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //R_MessageBox.Show("", $"Processing Master Ref. No. CREF_NO {_viewModelGLB00200.ResultProcessList} SUCCESSFUL!", R_eMessageBoxButtonType.OK);

                //if (!string.IsNullOrEmpty(_viewModelGLB00200.ResultFailedProcessList))
                //{
                //    loEx.Add(new Exception($"Failed to process Master Ref. No. CREF_NO {_viewModelGLB00200.ResultFailedProcessList} Failed!"));
                //    goto EndBlock;
                //}

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region OnLostFocus
        private void SetToDefaultValue()
        {
            var loEx = new R_Exception();

            try
            {
                //var loGetData = _viewModelGLB00200.PeriodYear;
                if (_viewModelGLB00200.PeriodYear < _viewModelGLB00200.GetInitialProcess.IMIN_YEAR)
                {
                    _viewModelGLB00200.PeriodYear = _viewModelGLB00200.GetInitialProcess.IMIN_YEAR;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region implement library for checkbox select
        private void R_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
        }
        #endregion

    }
}
