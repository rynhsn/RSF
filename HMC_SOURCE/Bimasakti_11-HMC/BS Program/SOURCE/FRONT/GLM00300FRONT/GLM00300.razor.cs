using BlazorClientHelper;
using GLM00300Common.DTOs;
using GLM00300Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;

namespace GLM00300Front
{
    public partial class GLM00300 : R_Page
    {
        private GLM00300ViewModel _viewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<GLM00300DTO> _gridRef;

        private R_TextBox _cbwCodeTextbox;
        private R_TextBox _cbwNameTextbox;
        [Inject] private IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await GetCurrencyCodeListData();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var data = (GLM00300DTO)eventArgs.Data;
                data.CCREATE_DATE = (data.DCREATE_DATE.ToString());
                data.CUPDATE_DATE = (data.DUPDATE_DATE.ToString());
            }
        }

        private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetBudgetWeightingNameList();
                eventArgs.ListEntityResult = _viewModel.BudgetWeightingList;
                await _gridRef.AutoFitAllColumnsAsync();
;            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00300DTO>(eventArgs.Data);
                await _viewModel.GetBudgetWeightingById(loParam);
                eventArgs.Result = _viewModel.BudgetWeighting;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetCurrencyCodeListData()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetCurrencyCodeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loConvert = R_FrontUtility.ConvertObjectToObject<GLM00300DTO>(eventArgs.Data);
            loConvert.CBW_NAME_DISPLAY = loConvert.CBW_NAME + $" [{loConvert.CBW_CODE}]";
            eventArgs.GridData = loConvert;
        }

        private async Task ConductorServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00300DTO>(eventArgs.Data);
                await _viewModel.SaveBudgetWeighting(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.BudgetWeighting;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ConductorServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {

            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00300DTO)eventArgs.Data;
                await _viewModel.DeleteBudgetWeighting(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task R_AfterDelete()
        {
            await R_MessageBox.Show("", "Budget Weighting Deleted Successfully!", R_eMessageBoxButtonType.OK);
        }

        public void R_Saving(R_SavingEventArgs eventArgs)
        {
            var data = (GLM00300DTO)eventArgs.Data;
            //_viewModel.loTmp = data;
            //_viewModel.CREC_ID = data.CREC_ID;
            //_viewModel.CBW_NAME_DISPLAY = data.CBW_NAME_DISPLAY;
            data.CBW_CODE = data.CBW_CODE.ToUpper();
        }


        private void R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var data = (GLM00300DTO)eventArgs.Data;
            
            data.CUPDATE_DATE = _viewModel.GetTimeNow.ToString();
            data.CCREATE_DATE = _viewModel.GetTimeNow.ToString();
            data.CCREATE_BY = _clientHelper.UserId;
            data.CUPDATE_BY = _clientHelper.UserId;
            data.CREC_ID = "";
        }

        private void ValidaitonGLM00300(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                var loData = (GLM00300DTO)eventArgs.Data;

                var _iCounter = 0;

                if (string.IsNullOrEmpty(loData.CBW_CODE))
                {
                    loException.Add("001", "Budget Weight Code Cannot be empty");
                    if (_iCounter == 0)
                    {
                        _cbwCodeTextbox.FocusAsync();
                        _iCounter++;
                    }
                }

                if (string.IsNullOrEmpty(loData.CBW_NAME))
                {
                    loException.Add("002", "Budget Weight Name Cannot be empty");
                    if (_iCounter == 0)
                    {
                        _cbwNameTextbox.FocusAsync();
                        _iCounter++;
                    }
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

    }
}
