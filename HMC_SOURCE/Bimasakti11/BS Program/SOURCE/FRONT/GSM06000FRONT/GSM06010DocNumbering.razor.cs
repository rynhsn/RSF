using BlazorClientHelper;
using GSM06000Common;
using GSM06000Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM06000Front
{
    public partial class GSM06010DocNumbering : R_Page
    {
        #region GSM06020

        private readonly GSM06020ViewModel _viewModelGSM06020 = new();
        private R_ConductorGrid? _conductorRefGSM06020;
        private R_Grid<GSM06020DTO>? _gridRefGSM06020;
        [Inject] private IClientHelper? clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelGSM06020.loParameterGSM06020 = R_FrontUtility.ConvertObjectToObject<GSM06020ParameterDTO>(poParameter);

                await _viewModelGSM06020.GetParameterInfo();
                _viewModelGSM06020.lcParameterCYearFormat = _viewModelGSM06020.loParameterGSM06020.CYEAR_FORMAT;
                await _gridRefGSM06020.R_RefreshGrid(null);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private async Task R_ServiceGetListRecordGSM06020(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelGSM06020.GetAllCashBankDocList();
                eventArgs.ListEntityResult = _viewModelGSM06020.loGridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task R_ServiceGetRecordAsyncGSM06020(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM06020DTO>(eventArgs.Data);
                await _viewModelGSM06020.GetEntity(loParam);
                eventArgs.Result = _viewModelGSM06020.loEntity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDeleteGSM06020(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06020DTO)eventArgs.Data;
                await _viewModelGSM06020.DeleteCashBankDoc(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Conductor_AfterDeleteGSM06020()
        {

            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task ServiceSaveGSM06020(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06020DTO)eventArgs.Data;
                await _viewModelGSM06020.SaveCashBankDoc(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelGSM06020.loEntity;
                await _gridRefGSM06020.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_AfterAddNumbering(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM06020DTO)eventArgs.Data;
                loParam.CUSER_LOGIN_ID = clientHelper.UserId;
                _viewModelGSM06020.GeneratePeriod(loParam);
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
