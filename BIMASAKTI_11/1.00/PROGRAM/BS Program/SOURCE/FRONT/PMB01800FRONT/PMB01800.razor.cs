using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMB01800MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using PMB01800COMMON.DTOs;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using PMB01800FrontResources;
using R_BlazorFrontEnd.Exceptions;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Helpers;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls.Events;
using System.Globalization;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace PMB01800FRONT
{
    public partial class PMB01800 : R_Page
    {
        private PMB01800ViewModel _viewModel = new PMB01800ViewModel();
        private PMB01801ViewModel _viewModel01 = new PMB01801ViewModel();
        private R_ConductorGrid _conductorGrid;
        private R_Conductor _conductorRef;
        private bool _enabledBtn = true;

        private R_Grid<PMB01800GetDepositListDTO> _gridDepositList = new();
        [Inject] IClientHelper ClientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        #region HandleError

        private void DisplayErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }

        #endregion

        private async Task ActionFuncDataSetExcel()
        {
            await Task.CompletedTask;
        }

        public async Task ShowSuccessUpdateInvoke()
        {
            _enabledBtn = true;
            await R_MessageBox.Show("", "Update Successfully", R_eMessageBoxButtonType.OK);
            await _gridDepositList.R_RefreshGrid(null);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            await _viewModel.Init();
            if (!string.IsNullOrWhiteSpace(_viewModel.Param.CPROPERTY_ID))
            {
                await _gridDepositList.R_RefreshGrid(null);

                _viewModel01.StateChangeAction = StateChangeInvoke;
                _viewModel01.DisplayErrorAction = DisplayErrorInvoke;
                _viewModel01.ShowSuccessAction = async () => { await ShowSuccessUpdateInvoke(); };
            }
        }


        private async Task _valueChangedProperty(string value)
        {
            var loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value == _viewModel.Param.CPROPERTY_ID) return;

                    _viewModel.Param.CPROPERTY_ID = value;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void _valueChangedTransType(string value)
        {
            var loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value == _viewModel.Param.CTRANS_TYPE) return;

                    _viewModel.Param.CTRANS_TYPE = value;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void _valueChangedTransTypeCode(string value)
        {
            R_Exception loEx = new R_Exception();
            var loOldValue = _viewModel.Param.CPAR_TRANS_CODE;
            try
            {
                _viewModel.Param.CPAR_TRANS_CODE = value;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _viewModel.Param.CPAR_TRANS_CODE = loOldValue;
            }
            R_DisplayException(loEx);
        }

        private void _valueChangedDeptType(string value)
        {
            R_Exception loEx = new R_Exception();
            var loOldValue = _viewModel.Param.CPAR_DEPT_CODE;
            try
            {
                if (value == "1")
                {
                    
                    _viewModel.Param.CPAR_DEPT_CODE = "";
                    _viewModel.Param.CPAR_DEPT_NAME = "";
                }
                _viewModel.selectedRadioDeptType = value;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _viewModel.Param.CPAR_TRANS_CODE = loOldValue;
                _viewModel.selectedRadioDeptType = loOldValue;
            }
            R_DisplayException(loEx);
        }

        private async Task OnLostFocus_LookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Param.CPAR_DEPT_CODE))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel.Param.CPAR_DEPT_CODE, // property that bindded to search textbox
                        CPROGRAM_ID=_viewModel.CPROGRAM_ID,
                    };
                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.Param.CPAR_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel.Param.CPAR_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.Param.CPAR_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.Param.CPAR_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private void BeforeOpen_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00710ParameterDTO() { CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID };
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void AfterOpen_lookupDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.Param.CPAR_DEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.Param.CPAR_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
            else
            {
                _viewModel.Param.CPAR_DEPT_CODE = "";
                _viewModel.Param.CPAR_DEPT_NAME = "";
            }
        }

        private async Task onClickBtnRefresh()
        {
            R_Exception loEx = new();
            try
            {
                await _gridDepositList.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task onClickBtnProcess()
        {
            _enabledBtn = false;
            await _gridDepositList.R_SaveBatch();
        }

        private async Task Deposit_List(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.PMB01800GetDepositList();
                eventArgs.ListEntityResult = _viewModel.DepositList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Deposit_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMB01800GetDepositListDTO>(eventArgs.Data);
                
                eventArgs.Result = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DisplayUtility(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMB01800GetDepositListDTO)eventArgs.Data;
                _viewModel.Entity = loData;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Deposit_Validation(R_ValidationEventArgs eventArgs)
        {

        }

        private void CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            
            eventArgs.Enabled = true;
        }

        private async Task SaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // await _viewModelUtility.SaveBatch((List<PMT03500UtilityUsageDTO>)eventArgs.Data, ClientHelper.CompanyId, ClientHelper.UserId);
                var loTempDataList = (List<PMB01800GetDepositListDTO>)eventArgs.Data;

                
                var loDataList =
                    R_FrontUtility.ConvertCollectionToCollection<PMB01800BatchDTO>(loTempDataList.Where(x => x.LSELECTED).ToList());

                //_viewModelUpload.IsUpload = false;
                _viewModel.Param.CCOMPANY_ID = ClientHelper.CompanyId;
                _viewModel.Param.CUSER_ID = ClientHelper.UserId;

                await _viewModel01.SaveBulkFile(Param: _viewModel.Param,
                    poDataList: loDataList.ToList());

                //if (_viewModelUpload.IsError)
                //{
                //    loEx.Add("Error", "Cut Off saved is not successfully!");
                //}

                _enabledBtn = true;

                // await _gridRefUtility.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
