using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT01700MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using BlazorClientHelper;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.Front;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.Events;
using PMT01700FrontResources;
using R_CommonFrontBackAPI;
using R_LockingFront;
using PMT01700COMMON.DTO.Utilities.Print;

namespace PMT01700FRONT
{
    public partial class PMT01700LOC_LOCList : R_Page, R_ITabPage
    {
        #region Master Page

        readonly PMT01700LOO_OfferListViewModel _viewModel = new();
        R_ConductorGrid? _conductorLOCList;
        R_ConductorGrid? _conductorUnitList;
        R_Grid<PMT01700LOO_OfferList_OfferListDTO>? _gridLOCList;
        R_Grid<PMT01700LOO_OfferList_UnitListDTO>? _gridAgreementUnitInfo;

        [Inject] IJSRuntime? JS { get; set; }
        [Inject] IClientHelper? _clientHelper { get; set; }
        PMT01700EventCallBackDTO _oEventCallBack = new PMT01700EventCallBackDTO();
        private int _pageSizeLOCList = 10;
        private int _pageSizeAggUnitInfo = 5;
        #endregion
        #region Master Tab

        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabOffer;
        private R_TabPage? _tabUnit;
        private R_TabPage? _tabCharges;
        private R_TabPage? _tabDeposit;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.lControlButton = false;
                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(poParameter);
                   _viewModel.oParameter.CTRANS_CODE = "802053";
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    _viewModel.lControlButton = true;
                    await _gridLOCList.R_RefreshGrid(null);

                    //if (!string.IsNullOrEmpty(_viewModel.oParameter.ODataUnitList))
                    //{
                    //    await _tabStripRef?.SetActiveTabAsync("LOC")!;
                    //}
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task BlankFunction()
        {
            var loException = new R_Exception();

            try
            {
                var llTrue = await R_MessageBox.Show("", "This function still on Development Process!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }



        #region Offer list (Agreement List)
        private async Task R_ServiceLOCListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetOfferList();
                if (!_viewModel.loListOfferList.Any())
                {
                    _viewModel.loListUnitList.Clear();
                    _viewModel.cBuildingSelectedUnit = "";
                }
                _viewModel.lControlTabUnit = _viewModel.loListOfferList.Any();
                _viewModel.lControlTabDeposit = _viewModel.loListOfferList.Any();

                eventArgs.ListEntityResult = _viewModel.loListOfferList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_DisplayLOCListGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMT01700LOO_OfferList_OfferListDTO loData = (PMT01700LOO_OfferList_OfferListDTO)eventArgs.Data;

                switch (loData.CTRANS_STATUS)
                {
                    case "00":
                        _viewModel.lControlButtonRevise =
                        _viewModel.lControlButtonCancelLOO_LOC =
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = true;
                        break;
                    case "10":
                        _viewModel.lControlButtonRevise =
                         _viewModel.lControlButtonSubmit =
                         _viewModel.lControlButtonCancelLOO_LOC = false;
                        _viewModel.lControlButtonRedraft = true;
                        break;
                    case "30":
                        _viewModel.lControlButtonRedraft =
                        _viewModel.lControlButtonSubmit = false;
                        _viewModel.lControlButtonRevise =
                        _viewModel.lControlButtonCancelLOO_LOC = true;
                        break;
                    case "80":
                    case "98":
                        _viewModel.lControlButtonRedraft =
                        _viewModel.lControlButtonSubmit =
                        _viewModel.lControlButtonRevise =
                        _viewModel.lControlButtonCancelLOO_LOC = false;
                        break;
                }


                if (!string.IsNullOrEmpty(loData.CREF_NO))
                {
                    _viewModel.oParameter.CPROPERTY_ID = loData.CPROPERTY_ID;
                    _viewModel.oParameter.CTRANS_CODE = loData.CTRANS_CODE;
                    _viewModel.oParameter.CREF_NO = loData.CREF_NO;
                    _viewModel.oParameter.CDEPT_CODE = loData.CDEPT_CODE;
                    _viewModel.oParameter.CBUILDING_ID = loData.CBUILDING_ID;
                    _viewModel.oParameter.CTRANS_STATUS = loData.CTRANS_STATUS;
                    _viewModel.cBuildingSelectedUnit = _viewModel.oParameter.CBUILDING_NAME = loData.CBUILDING_NAME;

                    //this entity for submit
                    _viewModel.loEntityOffer = loData;
                    await _gridAgreementUnitInfo!.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        #endregion
        #region Unit Info List
        private async Task R_ServiceGetListUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetUnitList();
                eventArgs.ListEntityResult = _viewModel.loListUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region OTHER TAB
        #region Tab LOCList
        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "LOCList":
                    await _gridLOCList.R_RefreshGrid(null);
                    _viewModel.oParameter.ODataUnitList = null;
                    break;
                default:
                    break;
            }

        }
        private async Task OnActiveTabIndexChangingAsync(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            switch (eventArgs.TabStripTab.Id)
            {
                case "OfferList":
                    _oEventCallBack.LUSING_PROPERTY_ID = _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = true;
                    break;
                case "Offer":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = false;
                    break;
                case "Unit":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = false;
                    break;
                case "Deposit":
                    _oEventCallBack.LUSING_PROPERTY_ID = false;
                    _oEventCallBack.LCRUD_MODE = true;
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    //_viewModel.lControlData = false;
                    break;
                default:
                    break;
            }

        }

        #endregion

        #region LOC Tab
        private async Task R_TabEventCallbackAsync(object poValue)
        {
            var loEx = new R_Exception();

            try
            {

                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01700EventCallBackDTO>(poValue);
                //var loValue = R_FrontUtility.ConvertObjectToObject<PMT01500EventCallBackDTO>(poValue);
                if (string.IsNullOrEmpty(loValue.CCRUD_MODE))
                {
                    _viewModel.lControlTabOfferList = _viewModel.lControlTabOffer = loValue.LCRUD_MODE;
                    if (_viewModel.loListOfferList.Any())
                    {
                        _viewModel.lControlTabUnit = _viewModel.lControlTabDeposit = loValue.LCRUD_MODE;
                    }
                    //_viewModel.oParameter.ODataUnitList = null;
                    await InvokeTabEventCallbackAsync(loValue);
                }
                else
                {

                    switch (loValue.CCRUD_MODE)
                    {
                        case "A_ADD":
                            _viewModel.oParameter.CDEPT_CODE = loValue.ODATA_PARAMETER.CDEPT_CODE;
                            _viewModel.oParameter.CREF_NO = loValue.ODATA_PARAMETER.CREF_NO;
                            _viewModel.oParameter.CBUILDING_ID = loValue.ODATA_PARAMETER.CBUILDING_ID;

                            _viewModel.lControlTabDeposit = _viewModel.lControlTabOfferList = true;
                            break;

                        case "A_DELETE":
                            _viewModel.oParameter.CDEPT_CODE = "";
                            _viewModel.oParameter.CREF_NO = "";
                            _viewModel.oParameter.CBUILDING_ID = "";

                            _viewModel.lControlTabDeposit = _viewModel.lControlTabOfferList = false;
                            break;

                        case "A_CANCEL":
                            _viewModel.oParameter.ODataUnitList = null;

                            _viewModel.lControlTabOfferList = _viewModel.lControlTabOffer = loValue.LCRUD_MODE;
                            if (_viewModel.loListOfferList.Any())
                            {
                                _viewModel.lControlTabUnit = _viewModel.lControlTabDeposit = !string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO) ? loValue.LCRUD_MODE : false;
                            }
                            else
                            {
                                _viewModel.lControlTabUnit = _viewModel.lControlTabDeposit = false;
                            }
                            break;

                        default:
                            _viewModel.oParameter.ODataUnitList = null;
                            break;
                    }

                    _viewModel.oParameter.ODataUnitList = null;
                    await InvokeTabEventCallbackAsync(loValue);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }


        //ini buat Dapet fungsi dari Page lookup
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(poParam);
                _viewModel.oParameter.CPROPERTY_ID = loParam.CPROPERTY_ID;
                await _gridLOCList!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }
        #endregion
        #region Tab UNIT
        private void Before_Open_LOC_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01700LOC_LOC);
        }
        #endregion
        #region Button
        private async Task SubmitBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //SUBMIT CODE == "10"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                   var loReturn =  await _viewModel.ProcessUpdateAgreement(lcNewStatus: "10");
                    if (loReturn.IS_PROCESS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_SuccessMessageOfferSubmit"));
                        await _gridLOCList!.R_RefreshGrid(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_FailedUpdate"));
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        private async Task RedraftBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                  R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_ConfirmationRedraft"),
                  R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    var loReturn = await _viewModel.ProcessUpdateAgreement(lcNewStatus: "00");
                    if (loReturn.IS_PROCESS_SUCCESS)
                    {

                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_SuccessMessageOfferRedraft"));
                        await _gridLOCList!.R_RefreshGrid(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        private async Task CancelLOCBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //SUBMIT CODE == "10"
                //REDRAFT CODE == "00"
                //CANCEL LOO CODE == "98"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_ConfirmationCancelLOO"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    var loResult = await _viewModel.ProcessUpdateAgreement(lcNewStatus: "98");
                    if (loResult.IS_PROCESS_SUCCESS!)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class),
                            "_SuccessMessageOfferCancelLOO"));
                        await _gridLOCList!.R_RefreshGrid(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class),
                            "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        #endregion


        #region Tab Charges
        private void General_Before_Open_UnitCharges_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01700LOO_UnitCharges_UnitUtilities);
        }
        #endregion

        #region Tab Deposit

        private void General_Before_Open_Deposit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.oParameter;
            eventArgs.TargetPageType = typeof(PMT01700LOO_Deposit);
        }

        #endregion
        #region PopUp Print
        private async Task BeforeOpen_PopupPrint(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (PMT01700LOO_OfferList_OfferListDTO)_gridLOCList.GetCurrentData();

                ParameterPrintDTO loParam = new ParameterPrintDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CDEPT_CODE = loData.CDEPT_CODE ?? _viewModel.oParameter.CDEPT_CODE,
                    CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE,
                    CREF_NO = loData.CREF_NO ?? _viewModel.oParameter.CREF_NO,
                    CUSER_ID = _clientHelper.UserId,

                };

                if (loData == null)
                {
                    var loValidate = await R_MessageBox.Show("", _localizer["ValidationNoRecord"], R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }

                eventArgs.PageTitle = _localizer["_Print"];
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(PMT01700Print);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        #endregion
        #endregion
        #region LOcking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";

        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(_conductorLOCList!.R_GetCurrentData());
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
                if (param) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01700",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01700",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
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
