using BlazorClientHelper;
using PMT01500Common.DTO._1._AgreementList;
using PMT01500Common.Utilities;
using PMT01500Common.Utilities.Front;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Xml.Linq;

namespace PMT01500Front
{
    public partial class PMT01500AgreementList
    {

        #region Master List

        private readonly PMT01500AgreementListViewModel _viewModelPMT01500AgreementList = new();
        private R_ConductorGrid? _conductorPMT01500AgreementList;
        private R_ConductorGrid? _conductorPMT01500UnitList;
        private R_Grid<PMT01500AgreementListOriginalDTO>? _gridRefPMT01500AgreementList;
        private R_Grid<PMT01500UnitListOriginalDTO>? _gridRefPMT01500UnitList;

        private bool _isDataExist;
        private bool _isChargesInfoandDepositExist;
        private string _cCheckerChargesInfo = "";
        private bool _isAgreementListUsed = true;

        private bool _pageAgreementListOnCRUDmode;

        [Inject] private IJSRuntime? JS { get; set; }
        [Inject] private IClientHelper? _clientHelper { get; set; }
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPMT01500AgreementList.GetPropertyList();
                await _viewModelPMT01500AgreementList.GetVarGsmTransactionCode();
                await _gridRefPMT01500AgreementList.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region External Function

        #region Change Status


        private void R_Before_Open_Popup_ChangeStatus(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //PMT01500FrontParameterForChargesInfo_ChangeStatusDTO loParam = R_FrontUtility.ConvertObjectToObject<PMT01500FrontParameterForChargesInfo_ChangeStatusDTO>(_viewModel.loParameterList);

            eventArgs.Parameter = _viewModelPMT01500AgreementList.loParameterList;
            eventArgs.TargetPageType = typeof(PMT01500AgreementList_ChangeStatus);
        }

        private async Task R_After_Open_Popup_ChangeStatus(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                var result = eventArgs.Result;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            await _gridRefPMT01500AgreementList.R_RefreshGrid(null);
        }

        #endregion

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelPMT01500AgreementList._cPropertyId = poParam;
                //_viewModelPMT01500AgreementList._oParameterForAgreement.CPROPERTY_ID = poParam;
                _viewModelPMT01500AgreementList.loParameterList.CPROPERTY_ID = poParam;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                await _gridRefPMT01500AgreementList.R_RefreshGrid(null);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                /*

                switch (_tabStripRef.ActiveTab.Id)
                {
                    case "Agreement":
                        await _tabAgreementRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "UnitInfo":
                        await _tabUnitInfoRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "ChargesInfo":
                        await _tabChargesInfoRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "InvoicePlan":
                        await _tabInvoicePlanRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "Deposit":
                        await _tabDepositRef.InvokeRefreshTabPageAsync(null);
                        break;

                    case "Document":
                        await _tabDocumentRef.InvokeRefreshTabPageAsync(null);
                        break;

                    default:
                        break;
                }
                */

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnClickNexttoAddPageAgreementButtonAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                _viewModelPMT01500AgreementList._cModeParameter = "ADD";
                await _tabStripRef?.SetActiveTabAsync("Agreement")!;

                /*
                if (!loException.HasError)
                {
                    //IsSuccess = true;
                    R_BeforeOpenTabPageEventArgs test = new R_BeforeOpenTabPageEventArgs() 
                    { 
                        Parameter = new PMT01500FrontParameterForAgreementDTO()
                        {
                            CMODE = "ADD"
                        }
                    };
                    General_Before_Open_Agreement_TabPage(test);
                }
                */
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnClickNexttoEditPageAgreementButton()
        {
            R_Exception loException = new R_Exception();
            try
            {
                _viewModelPMT01500AgreementList._cModeParameter = "EDIT";
                await _tabStripRef?.SetActiveTabAsync("Agreement")!;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task BlankFunction()
        {
            var loException = new R_Exception();
            //APM00500ProductDetailDTO? poEntityAPM00500Detail = (APM00500ProductDetailDTO)_conductorAPM00500ProductDetail.R_GetCurrentData();

            try
            {
                var llTrue = await R_MessageBox.Show("", "You Clicked the Button!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master Upload
        private async Task DownloadTemplate()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModelPMT01500AgreementList.DownloadTemplate();

                    var saveFileName = $"Lease Manager.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Before_Open_Upload_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(PMT01500AgreementList_Upload);
            PMT01500ParameterUploadChangePageDTO loParameter = new PMT01500ParameterUploadChangePageDTO
            {
                CPROPERTY_ID = _viewModelPMT01500AgreementList._cPropertyId,
                CPROPERTY_NAME = _viewModelPMT01500AgreementList.loPropertyList
                    .Where(property => property.CPROPERTY_ID == _viewModelPMT01500AgreementList._cPropertyId)
                    .Select(property => property.CPROPERTY_NAME)
                    .First()
            };
            eventArgs.Parameter = loParameter;
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result)
            {
                await _gridRefPMT01500AgreementList.R_RefreshGrid(null);
            }
        }

        #endregion


        #region Core Page

        #region List AgreementList

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPMT01500AgreementList.GetAgreementList();
                eventArgs.ListEntityResult = _viewModelPMT01500AgreementList.loListPMT01500Agreement;
                if (_viewModelPMT01500AgreementList.loListPMT01500Agreement.Any())
                {
                    _isDataExist = true;
                    var loData = _viewModelPMT01500AgreementList.loListPMT01500Agreement.First();
                    _viewModelPMT01500AgreementList.loParameterList.CREF_NO = loData.CREF_NO;
                    _viewModelPMT01500AgreementList.loParameterList.CDEPT_CODE = loData.CDEPT_CODE;
                    _viewModelPMT01500AgreementList.loParameterList.CTRANS_CODE = loData.CTRANS_CODE;
                    _viewModelPMT01500AgreementList.loSelectedAgreementGetUnitDescription.CUNIT_DESCRIPTION = loData.CUNIT_DESCRIPTION!;
                }
                else
                {
                    _isDataExist = false;
                    _viewModelPMT01500AgreementList.loSelectedAgreementGetUnitDescription.CUNIT_DESCRIPTION = "";
                    _viewModelPMT01500AgreementList.loParameterList = new PMT01500GetHeaderParameterDTO();

                }
                //await _viewModelPMT01500AgreementList.GetSelectedAgreementGetUnitDescriptionAsync();
                await _gridRefPMT01500UnitList.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_DisplayGetRecord(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01500AgreementListOriginalDTO loData = (PMT01500AgreementListOriginalDTO)eventArgs.Data;

            try
            {
                _viewModelPMT01500AgreementList._oTempData = loData;
                _viewModelPMT01500AgreementList.loParameterList.CDEPT_CODE = loData.CDEPT_CODE;
                _viewModelPMT01500AgreementList.loParameterList.CREF_NO = loData.CREF_NO;
                _viewModelPMT01500AgreementList.loParameterList.CTRANS_CODE = loData.CTRANS_CODE;
                _viewModelPMT01500AgreementList.loParameterList.CBUILDING_ID = loData.CBUILDING_ID;
                _viewModelPMT01500AgreementList.loParameterList.CCHARGE_MODE = loData.CCHARGE_MODE!;
                _viewModelPMT01500AgreementList.loParameterList.CCURRENCY_CODE = loData.CCURRENCY_CODE;
                _viewModelPMT01500AgreementList.loParameterList.CREF_DATE = loData.CREF_DATE;

                _viewModelPMT01500AgreementList.loParameterList.CPROPERTY_ID = _viewModelPMT01500AgreementList._cPropertyId;

                //Untuk Tab - 1
                _viewModelPMT01500AgreementList.loSelectedAgreementGetUnitDescription.CUNIT_DESCRIPTION = loData.CUNIT_DESCRIPTION!;
                //await _viewModelPMT01500AgreementList.GetSelectedAgreementGetUnitDescriptionAsync();
                await _gridRefPMT01500UnitList.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region List UnitList

        private async Task R_ServiceGetListRecordUnitList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (_viewModelPMT01500AgreementList.loListPMT01500Agreement.Any())
                    await _viewModelPMT01500AgreementList.GetUnitList();
                else
                    _viewModelPMT01500AgreementList.loListPMT01500UnitList.Clear();

                var loData = _viewModelPMT01500AgreementList.loListPMT01500UnitList;
                _isChargesInfoandDepositExist = loData.Any();
                _cCheckerChargesInfo = !_isChargesInfoandDepositExist ? "NOT" : "";
                _viewModelPMT01500AgreementList.loParameterList.CUNIT_ID = _isChargesInfoandDepositExist ? loData.First().CUNIT_ID! : "";
                _viewModelPMT01500AgreementList.loParameterList.CUNIT_NAME = _isChargesInfoandDepositExist ? loData.First().CUNIT_NAME! : "";
                _viewModelPMT01500AgreementList.loParameterList.CFLOOR_ID = _isChargesInfoandDepositExist ? loData.First().CFLOOR_ID! : "";

                _viewModelPMT01500AgreementList._oTotalUnitAgreementList = new PMT01500FrontTotalUnitandAreaAgreementListDTO()
                {
                    ITOTAL_UNIT = loData.Count(),
                    NTOTAL_GROSS = loData.Sum(x => x.NGROSS_AREA_SIZE),
                    NTOTAL_NET = loData.Sum(x => x.NNET_AREA_SIZE),
                    NTOTAL_COMMON = loData.Sum(x => x.NCOMMON_AREA_SIZE),
                };

                eventArgs.ListEntityResult = loData;
                /*
                if (!_viewModelLMM02500.loGridListLMM02500.Any())
                {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    _loTabParameter.CTENANT_GROUP_ID = null;
                    _loTabParameter.CTENANT_GROUP_NAME = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                    _isDataExist = false;
                }
                else if (_viewModelLMM02500.loGridListLMM02500.Any())
                {
                    _loTabParameter.CTENANT_GROUP_ID =
                        _viewModelLMM02500.loGridListLMM02500.FirstOrDefault()!.CTENANT_GROUP_ID;
                    _isDataExist = true;
                }
                */
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_DisplayGetRecordUnitList(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01500UnitListOriginalDTO loData = (PMT01500UnitListOriginalDTO)eventArgs.Data;
            try
            {
                _viewModelPMT01500AgreementList.loParameterList.CFLOOR_ID = loData.CFLOOR_ID!;
                _viewModelPMT01500AgreementList.loParameterList.CUNIT_ID = loData.CUNIT_ID!;
                _viewModelPMT01500AgreementList.loParameterList.CUNIT_NAME = loData.CUNIT_NAME!;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #endregion

        #region Master Tab
        private R_TabStrip? _tabStripRef;
        private R_TabPage? _tabAgreementRef;
        private R_TabPage? _tabUnitInfoRef;
        private R_TabPage? _tabChargesInfoRef;
        private R_TabPage? _tabInvoicePlanRef;
        private R_TabPage? _tabDepositRef;
        private R_TabPage? _tabDocumentRef;

        //private LMM02500TabParameterDTO _loTabParameter = new();

        #region Tab List
        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "AgreementList":
                    await _gridRefPMT01500AgreementList.R_RefreshGrid(null);
                    break;
                default:
                    break;
            }

        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            _viewModelPMT01500AgreementList._lComboBoxProperty = true;
            eventArgs.Cancel = _pageAgreementListOnCRUDmode;

            if (eventArgs.TabStripTab.Id != "AgreementList")
            {
                _viewModelPMT01500AgreementList._lComboBoxProperty = false;
            }
        }

        #endregion

        #region Utulities
        private async Task R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                var loValue = R_FrontUtility.ConvertObjectToObject<PMT01500EventCallBackDTO>(poValue);
                if (!string.IsNullOrEmpty(loValue.CFlagUnitInfoHasData))
                {
                    switch (loValue.CFlagUnitInfoHasData)
                    {
                        case "UI_ADD":
                            _isChargesInfoandDepositExist = loValue.LACTIVEUnitInfoHasData;
                            _cCheckerChargesInfo = "";
                            _viewModelPMT01500AgreementList.loParameterList.CFLOOR_ID = loValue.CFLOOR_ID;
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_ID = loValue.CUNIT_ID;
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_NAME = loValue.CUNIT_NAME;
                            break;

                        case "UI_DELETE":
                            _isChargesInfoandDepositExist = loValue.LACTIVEUnitInfoHasData;
                            _cCheckerChargesInfo = "NOT";
                            _viewModelPMT01500AgreementList.loParameterList.CFLOOR_ID = null;
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_ID = null;
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_NAME = null;
                            break;

                        case "UI_DISPLAY":
                            _viewModelPMT01500AgreementList.loParameterList.CFLOOR_ID = loValue.CFLOOR_ID;
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_ID = loValue.CUNIT_ID;
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_NAME = loValue.CUNIT_NAME;
                            break;

                        case "A_ADD":
                            _viewModelPMT01500AgreementList.loParameterList.CDEPT_CODE = loValue.CDEPT_CODE;
                            _viewModelPMT01500AgreementList.loParameterList.CREF_NO = loValue.CREF_NO;
                            _viewModelPMT01500AgreementList.loParameterList.CTRANS_CODE = loValue.CTRANS_CODE;

                            _viewModelPMT01500AgreementList.loParameterList.CBUILDING_ID = loValue.CBUILDING_ID;
                            _viewModelPMT01500AgreementList.loParameterList.CCHARGE_MODE = loValue.CCHARGE_MODE;
                            _viewModelPMT01500AgreementList.loParameterList.CCURRENCY_CODE = loValue.CCURRENCY_CODE;

                            //Updated 3 Mei 2024 : Refresh, Clear Data For Unit Info
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_ID = "";
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_NAME = "";
                            _viewModelPMT01500AgreementList.loParameterList.CREF_DATE = "";
                            _isDataExist = true;
                            _isChargesInfoandDepositExist = false;
                            break;

                        case "A_DELETE":
                            _viewModelPMT01500AgreementList.loParameterList.CDEPT_CODE = null;
                            _viewModelPMT01500AgreementList.loParameterList.CREF_NO = null;
                            _viewModelPMT01500AgreementList.loParameterList.CTRANS_CODE = null;

                            _viewModelPMT01500AgreementList.loParameterList.CBUILDING_ID = "";
                            _viewModelPMT01500AgreementList.loParameterList.CCHARGE_MODE = "";
                            _viewModelPMT01500AgreementList.loParameterList.CCURRENCY_CODE = "";
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_ID = "";
                            _viewModelPMT01500AgreementList.loParameterList.CUNIT_NAME = "";
                            _viewModelPMT01500AgreementList.loParameterList.CREF_DATE = "";
                            _isDataExist = false;
                            _isChargesInfoandDepositExist = false;
                            break;
                        default:
                            break;
                    }

                    //INI MASALAH PASTI
                    await Task.Delay(100);

                }
                else
                {
                    _pageAgreementListOnCRUDmode = !loValue.LContractorOnCRUDmode;
                    //_isChargesInfoExist = _viewModelPMT01500AgreementList.loListPMT01500UnitList.Any();
                    _isAgreementListUsed = loValue.LContractorOnCRUDmode;
                    _isDataExist = !string.IsNullOrEmpty(_viewModelPMT01500AgreementList.loParameterList.CREF_NO) ? loValue.LContractorOnCRUDmode : false;
                    _isChargesInfoandDepositExist = !string.IsNullOrEmpty(_viewModelPMT01500AgreementList.loParameterList.CUNIT_ID) ? loValue.LContractorOnCRUDmode : false;

                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #endregion

        #region Master Tab

        #region Tab Agreement

        private void General_Before_Open_Agreement_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            _viewModelPMT01500AgreementList.loParameterList.DataAgreement.LINCREMENT_FLAGFORCREF_NO = _viewModelPMT01500AgreementList.loGsmTransactionCode.LINCREMENT_FLAG;
            _viewModelPMT01500AgreementList.loParameterList.DataAgreement.CMODE = _viewModelPMT01500AgreementList._cModeParameter;

            _viewModelPMT01500AgreementList._cModeParameter = "";
            eventArgs.Parameter = _viewModelPMT01500AgreementList.loParameterList;
            eventArgs.TargetPageType = typeof(PMT01500Agreement);
        }

        private async Task General_After_Open_Agreement_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {
            await _gridRefPMT01500AgreementList.R_RefreshGrid(null);

            /*
            if (eventArgs.Result == null)
            {
                return;
            }
            if ((bool)eventArgs.Result)
            {
                await _gridRefPMT01500AgreementList.R_RefreshGrid(null);
            }
            */
        }

        #endregion

        #region Tab UnitInfo
        private void General_Before_Open_UnitInfo_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModelPMT01500AgreementList.loParameterList;
            eventArgs.TargetPageType = typeof(PMT01500UnitInfo);
        }

        #endregion

        #region Tab ChargesInfo
        private void General_Before_Open_ChargesInfo_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {

            eventArgs.Parameter = _viewModelPMT01500AgreementList.loParameterList;
            eventArgs.TargetPageType = typeof(PMT01500ChargesInfo);
        }

        private async Task General_After_Open_ChargesInfo_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {
            await _gridRefPMT01500UnitList.R_RefreshGrid(null);
            /*
            if (eventArgs.Result == null)
            {
                return;
            }
            if ((bool)eventArgs.Result)
            {
                await _gridRefPMT01500AgreementList.R_RefreshGrid(null);
            }
            */
        }

        #endregion

        #region Tab InvoicePlan
        private void General_Before_Open_InvoicePlan_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {

            eventArgs.Parameter = _viewModelPMT01500AgreementList.loParameterList;
            eventArgs.TargetPageType = typeof(PMT01500InvoicePlan);
        }

        #endregion

        #region Tab Deposit
        private void General_Before_Open_Deposit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {

            eventArgs.Parameter = _viewModelPMT01500AgreementList.loParameterList;
            eventArgs.TargetPageType = typeof(PMT01500Deposit);
        }

        #endregion

        #region Tab Document
        private void General_Before_Open_Document_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {

            eventArgs.Parameter = _viewModelPMT01500AgreementList.loParameterList;
            eventArgs.TargetPageType = typeof(PMT01500Document);
        }

        #endregion

        #endregion

        #endregion

    }
}
