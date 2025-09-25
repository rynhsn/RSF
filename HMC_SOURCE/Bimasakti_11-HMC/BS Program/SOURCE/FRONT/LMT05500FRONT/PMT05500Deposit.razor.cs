using BlazorClientHelper;
using PMT05500COMMON.DTO;
using PMT05500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using PMT05500COMMON;
using R_BlazorFrontEnd.Controls.Popup;

namespace PMT05500Front
{
    public partial class PMT05500Deposit : R_Page
    {
        private LMT05500DepositViewModel _depositViewModel = new();
        private R_Grid<LMT05500DepositListDTO>? _gridDepositRef;
        private R_Grid<LMT05500DepositDetailListDTO>? _gridDepositDetailRef;

        private R_ConductorGrid? _conGridDeposit;
        private R_ConductorGrid? _conGridDepositDetail;

        private R_TabStrip? _tabStripDeposit;
        private R_TabPage? _tabPageSubDeposit;
        private bool _pageDepositOnCRUDmode;
        [Inject] private IClientHelper? _clientHelper { get; set; }
        [Inject] private R_PopupService? _popUpService { get; set; }
        private int _pageSizeDeposit = 10;
        private int _pageSizeDepositDt = 5;
        public bool _enableDepositInfo = true;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                if ((LMT05500DBParameter)poParameter != null)
                {
                    _enableDepositInfo = true;
                    _depositViewModel._currentDataAgreement = (LMT05500DBParameter)poParameter;

                    await R_ServiceHeaderRecord((LMT05500DBParameter)poParameter);
                    await _gridDepositRef!.R_RefreshGrid(null);
                }
                else
                {
                    _enableDepositInfo = false;
                    _depositViewModel._currentDataAgreement = new();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region HeaderDeposit
        private async Task R_ServiceHeaderRecord(LMT05500DBParameter poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                poParameter.CCOMPANY_ID = _clientHelper!.CompanyId;
                poParameter.CUSER_ID = _clientHelper.UserId;
                await _depositViewModel.GetHeaderDeposit(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region Deposit List
        private async Task R_ServiceDepositListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _depositViewModel.GetAllDepositList();
                eventArgs.ListEntityResult = _depositViewModel._depositList;

                if (_depositViewModel._depositList.Count > 0)
                {
                    await _gridDepositDetailRef!.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_DisplayDeposit(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {

                var loParam = (LMT05500DepositListDTO)eventArgs.Data;

                _depositViewModel._currentDeposit = loParam;
                _depositViewModel._lIsPayment = loParam.LPAYMENT;
                _depositViewModel._lRemainingAmountMoreThanZero = true;
                  _depositViewModel._lRemainingAmountMoreThanZero = loParam.NREMAINING_AMOUNT > 0 ? true : false;
               await _gridDepositDetailRef!.R_RefreshGrid(null);
            }
        }

        #endregion

        #region DepositDetail List
        private async Task R_ServiceDepositDetailListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _depositViewModel.GetAllDepositDetailList();
                eventArgs.ListEntityResult = _depositViewModel._depositDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_DisplayDepositDetail(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMT05500DepositDetailListDTO)eventArgs.Data;
                _depositViewModel._DataDepositDetail = loParam;
            }
        }
        #endregion

        #region Button Customize
        private async Task R_Before_BtnAdjustment(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //  string codeSpec = "GLT00100"; //GLT00100 (Journal Entries)
                var temp = _depositViewModel._currentDeposit;

                PMT05500ParamDepositDTO loParam = new PMT05500ParamDepositDTO()
                {
                    CCHEQUE_ID = "",
                    PARAM_CALLER_ID = "PMT05500",
                    PARAM_CALLER_TRANS_CODE = _depositViewModel._currentDeposit.CTRANS_CODE!, //"802030"
                    PARAM_CALLER_REF_NO = _depositViewModel._currentDeposit.CREF_NO!, //"CheckFIN-20240400002-PM"
                    PARAM_CALLER_ACTION = "NEW",
                    PARAM_GLACCOUNT_NO = _depositViewModel._currentDeposit.CGLACCOUNT_NO!,
                    PARAM_GLACCOUNT_NAME = _depositViewModel._currentDeposit.CGLACCOUNT_NAME!, //"'0A155"
                    PARAM_DEPT_CODE = _depositViewModel._currentDeposit.CDEPT_CODE!, //"FIN"
                    PARAM_DEPT_NAME = _depositViewModel._currentDeposit.CDEPT_NAME!, //"FIN"
                    PARAM_DOC_NO = _depositViewModel._currentDeposit.CDOC_NO!, //""
                    PARAM_DOC_DATE = _depositViewModel._currentDeposit.CDOC_DATE!, //""
                    PARAM_DESCRIPTION = "",
                    PARAM_CENTER_CODE = _depositViewModel._currentDeposit.CCENTER_CODE!, // ""
                    PARAM_CENTER_NAME = _depositViewModel._currentDeposit.CCENTER_NAME!, // ""
                    PARAM_DBCR = _depositViewModel._currentDeposit.CDBCR!,  //"D"
                    PARAM_BSIS = _depositViewModel._currentDeposit.CBSIS!, //"B"
                    PARAM_CURRENCY_CODE = _depositViewModel._currentDeposit.CCURRENCY_CODE!,
                    PARAM_LC_BASE_RATE = _depositViewModel._currentDeposit.NLBASE_RATE_AMOUNT,
                    PARAM_LC_RATE = _depositViewModel._currentDeposit.NLCURRENCY_RATE_AMOUNT,
                    PARAM_BC_BASE_RATE = _depositViewModel._currentDeposit.NBBASE_RATE_AMOUNT,
                    PARAM_BC_RATE = _depositViewModel._currentDeposit.NBCURRENCY_RATE_AMOUNT,

                    PARAM_AMOUNT = _depositViewModel._currentDeposit.NREMAINING_AMOUNT,
                    PARAM_NDEPOSIT_AMOUNT = _depositViewModel._currentDeposit.NDEPOSIT_AMOUNT,
                    PARAM_SEQ_NO = _depositViewModel._currentDeposit.CSEQ_NO!,
                    CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID!,
                    PARAM_REF_NO = "",
                    //belum tau
                    PARAM_CASH_FLOW_GROUP_CODE = _depositViewModel._currentDeposit.CCASH_FLOW_GROUP_CODE!,
                    PARAM_CASH_FLOW_CODE = _depositViewModel._currentDeposit.CCASH_FLOW_CODE!,
                    //PARAM_GLACCOUNT_NAME = _depositViewModel._currentDeposit.CGLACCOUNT_NO,
                    //PARAM_CENTER_NAME = _depositViewModel._currentDeposit.CGLACCOUNT_NO,
                    //PARAM_DEPT_NAME = _depositViewModel._currentDeposit.CGLACCOUNT_NO,
                };

                eventArgs.Parameter = loParam;
                eventArgs.PageNamespace = "GLT00100FRONT.GLT00110";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        private async Task R_After_BtnAdjustmentAsync(R_AfterOpenDetailEventArgs eventArgs)
        {
            await _gridDepositRef!.R_RefreshGrid(null);
        }
        private async Task R_Before_BtnRefund(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var temp = _depositViewModel._currentDeposit;
                PMT05500ParamDepositDTO loParam = new PMT05500ParamDepositDTO()
                {
                    CCHEQUE_ID = "",
                    PARAM_CALLER_ID = "PMT05500",
                    PARAM_CALLER_TRANS_CODE = _depositViewModel._currentDeposit.CTRANS_CODE!,
                    PARAM_CALLER_REF_NO = _depositViewModel._currentDeposit.CREF_NO!,
                    PARAM_CALLER_ACTION = "NEW",
                    PARAM_DEPT_CODE = _depositViewModel._currentDeposit.CDEPT_CODE!,
                    PARAM_REF_NO = "",
                    PARAM_DOC_NO = _depositViewModel._currentDeposit.CDOC_NO!,
                    PARAM_DOC_DATE = _depositViewModel._currentDeposit.CDOC_DATE!,
                    PARAM_DESCRIPTION = "",
                    PARAM_GLACCOUNT_NO = _depositViewModel._currentDeposit.CGLACCOUNT_NO!,
                    PARAM_GLACCOUNT_NAME = _depositViewModel._currentDeposit.CGLACCOUNT_NAME!,
                    PARAM_CENTER_CODE = _depositViewModel._currentDeposit.CCENTER_CODE!,
                    PARAM_CASH_FLOW_GROUP_CODE = _depositViewModel._currentDeposit.CCASH_FLOW_GROUP_CODE!,
                    PARAM_CASH_FLOW_CODE = _depositViewModel._currentDeposit.CCASH_FLOW_CODE!,
                    PARAM_AMOUNT = _depositViewModel._currentDeposit.NREMAINING_AMOUNT,
                    PARAM_NDEPOSIT_AMOUNT = _depositViewModel._currentDeposit.NDEPOSIT_AMOUNT,
                    PARAM_SEQ_NO = _depositViewModel._currentDeposit.CSEQ_NO!,
                };
                eventArgs.Parameter = loParam;
                var loValue = await _popUpService!.Show(typeof(PopUpRefund), "");

                ResultPopUpDTO loResultPopup = (ResultPopUpDTO)loValue.Result;

                if (loResultPopup != null)
                {
                    string lcRefundValue = loResultPopup.CODE_PROGRAM!;

                    switch (lcRefundValue)
                    {
                        case "00200": //Cash Payment Journal
                            eventArgs.PageNamespace = "CBT00200FRONT.CBT00220";
                            break;
                        case "01200": //Wire Payment Journal
                            eventArgs.PageNamespace = "CBT01200FRONT.CBT01220"; //cbt01220.razor
                            break;
                        case "02200": //Cheque Payment Journal
                            eventArgs.PageNamespace = "CBT02200FRONT.CBT02220";
                            break;
                    };
                }
                else
                {
                    await Close(true, true);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        private async Task R_After_BtnRefundAsync(R_AfterOpenDetailEventArgs eventArgs)
        {
            await _gridDepositRef!.R_RefreshGrid(null);
        }
        private async Task R_Before_BtnSalesCreditNote(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //  string codeSpec = "GLT00100"; //GLT00100 (Journal Entries)
                var temp = _depositViewModel._currentDeposit;

                PMT05500ParamDepositDTO loParam = new PMT05500ParamDepositDTO()
                {
                    CCHEQUE_ID = "",
                    PARAM_CALLER_ID = "PMT05500",
                    PARAM_CALLER_ACTION = "NEW",
                    PARAM_CALLER_TRANS_CODE = _depositViewModel._currentDeposit.CTRANS_CODE!,
                    PARAM_CALLER_REF_NO = _depositViewModel._currentDeposit.CREF_NO!,
                    PARAM_PROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID!,
                    PARAM_DEPT_CODE = _depositViewModel._currentDeposit.CDEPT_CODE!,
                    PARAM_DEPT_NAME = _depositViewModel._currentDeposit.CDEPT_NAME!,
                    PARAM_CUSTOMER_ID = _depositViewModel._currentDeposit.CTENANT_ID!,
                    PARAM_CUSTOMER_NAME = _depositViewModel._currentDeposit.CTENANT_NAME!,
                    PARAM_CUSTOMER_TYPE_NAME = _depositViewModel._currentDeposit.CCUSTOMER_TYPE_NAME!,
                    PARAM_DOC_NO = _depositViewModel._currentDeposit.CDOC_NO!, //USER ISI
                    PARAM_DOC_DATE = _depositViewModel._currentDeposit.CDOC_DATE!, //USER ISI
                    PARAM_CURRENCY = _depositViewModel._currentDeposit.CCURRENCY_CODE!,
                    PARAM_DESCRIPTION = "", //USER ISI
                    PARAM_LBASE_RATE = _depositViewModel._currentDeposit.NLBASE_RATE_AMOUNT,
                    PARAM_LCURRENCY_RATE = _depositViewModel._currentDeposit.NLCURRENCY_RATE_AMOUNT,
                    PARAM_BBASE_RATE = _depositViewModel._currentDeposit.NBBASE_RATE_AMOUNT,
                    PARAM_BCURRENCY_RATE = _depositViewModel._currentDeposit.NBCURRENCY_RATE_AMOUNT,
                    PARAM_TAXABLE = _depositViewModel._currentDeposit.LTAXABLE!,
                    PARAM_TAX_ID = _depositViewModel._currentDeposit.CTAX_ID!,
                    PARAM_TAX_NAME = _depositViewModel._currentDeposit.CTAX_NAME!,
                    PARAM_TAX_PCT = _depositViewModel._currentDeposit.NTAX_PCT,
                    PARAM_TAX_BRATE = _depositViewModel._currentDeposit.NTAX_BASE_RATE,
                    PARAM_TAX_CURR_RATE = _depositViewModel._currentDeposit.NTAX_CURRENCY_RATE,
                    CLINK_FLOW_ID = _depositViewModel._currentDeposit.CFLOW_ID,
                    PARAM_TERM = _depositViewModel._currentDeposit.CPAYMENT_TERM_CODE,

                    PARAM_SERVICE_ID = _depositViewModel._currentDeposit.CDEPOSIT_ID,
                    PARAM_SERVICE_NAME = _depositViewModel._currentDeposit.CDEPOSIT_NAME,
                    PARAM_ITEM_NOTES = _depositViewModel._currentDeposit.CDESCRIPTION,
                    PARAM_CHARGE_TYPE_ID = _depositViewModel._currentDeposit.CCHARGES_TYPE,
                    PARAM_AMOUNT = _depositViewModel._currentDeposit.NREMAINING_AMOUNT,
                    PARAM_NDEPOSIT_AMOUNT = _depositViewModel._currentDeposit.NDEPOSIT_AMOUNT,
                    PARAM_SEQ_NO = _depositViewModel._currentDeposit.CSEQ_NO!,
                };

                eventArgs.Parameter = loParam;
                eventArgs.PageNamespace = "PMT50600FRONT.PMT50610DepositCreditNote";

                //eventArgs.Parameter = loParam;
                //eventArgs.PageNamespace = "UtilityFront.TryLookUp";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        private async Task R_After_BtnSalesCreditNote(R_AfterOpenDetailEventArgs eventArgs)
        {
            await _gridDepositRef!.R_RefreshGrid(null);
        }
        private async Task Before_OpenView(R_BeforeOpenDetailEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loDataDeposit = _depositViewModel._currentDeposit;
                var loDataDepositDetail = _depositViewModel._DataDepositDetail;

                if (loDataDepositDetail != null)
                {
                    PMT05500ParamDepositDTO loParam = new PMT05500ParamDepositDTO()
                    {
                        CCHEQUE_ID = "",
                        PARAM_CALLER_ID = "PMT05500",
                        PARAM_CALLER_ACTION = "VIEW",
                        //CREDIT NOTE
                        PARAM_REC_ID = loDataDepositDetail.CTRANS_HD_REC_ID,

                        //FADEL
                        PARAM_REF_NO = loDataDepositDetail.CREF_NO!,
                        PARAM_DEPT_CODE = loDataDepositDetail.CDEPT_CODE!,
                        PARAM_DEPT_NAME = loDataDepositDetail.CDEPT_NAME!,

                        //Adjustment
                        PARAM_CALLER_TRANS_CODE = _depositViewModel._currentDeposit.CTRANS_CODE!, //"802030"
                        PARAM_CALLER_REF_NO = _depositViewModel._currentDeposit.CREF_NO!,
                        PARAM_GLACCOUNT_NO = _depositViewModel._currentDeposit.CGLACCOUNT_NO!,
                        PARAM_GLACCOUNT_NAME = _depositViewModel._currentDeposit.CGLACCOUNT_NAME!,
                        PARAM_DOC_NO = _depositViewModel._currentDeposit.CDOC_NO!, //""
                        PARAM_DOC_DATE = _depositViewModel._currentDeposit.CDOC_DATE!, //""
                        PARAM_DESCRIPTION = "",
                        PARAM_CENTER_CODE = _depositViewModel._currentDeposit.CCENTER_CODE!, // ""
                        PARAM_CENTER_NAME = _depositViewModel._currentDeposit.CCENTER_NAME!, // ""
                        PARAM_DBCR = _depositViewModel._currentDeposit.CDBCR!,  //"D"
                        PARAM_BSIS = _depositViewModel._currentDeposit.CBSIS!, //"B"
                        PARAM_CURRENCY_CODE = _depositViewModel._currentDeposit.CCURRENCY_CODE!,
                        PARAM_LC_BASE_RATE = _depositViewModel._currentDeposit.NLBASE_RATE_AMOUNT,
                        PARAM_LC_RATE = _depositViewModel._currentDeposit.NLCURRENCY_RATE_AMOUNT,
                        PARAM_BC_BASE_RATE = _depositViewModel._currentDeposit.NBBASE_RATE_AMOUNT,
                        PARAM_BC_RATE = _depositViewModel._currentDeposit.NBCURRENCY_RATE_AMOUNT,
                        PARAM_AMOUNT = _depositViewModel._currentDeposit.NREMAINING_AMOUNT,
                        PARAM_NDEPOSIT_AMOUNT = loDataDepositDetail.NDEPOSIT_AMOUNT,
                        PARAM_SEQ_NO = _depositViewModel._currentDeposit.CSEQ_NO!,

                        CPROPERTY_ID = _depositViewModel._currentDeposit.CPROPERTY_ID!,
                        PARAM_CASH_FLOW_GROUP_CODE = _depositViewModel._currentDeposit.CCASH_FLOW_GROUP_CODE!,
                        PARAM_CASH_FLOW_CODE = _depositViewModel._currentDeposit.CCASH_FLOW_CODE!
                    };

                    eventArgs.Parameter = loParam;

                    switch (loDataDepositDetail.CDEPOSIT_TYPE)
                    {
                        case "Refund":
                            if (loDataDepositDetail.CTRX_CODE == "190010")
                            {
                                eventArgs.PageNamespace = "CBT00200FRONT.CBT00220";
                            }
                            else if (loDataDepositDetail.CTRX_CODE == "190020")
                            {
                                eventArgs.PageNamespace = "CBT02200FRONT.CBT02220";
                            }
                            else if (loDataDepositDetail.CTRX_CODE == "190030")
                            {
                                eventArgs.PageNamespace = "CBT01200FRONT.CBT01220";
                            }
                            break;

                        case "CN":
                            eventArgs.PageNamespace = "PMT50600FRONT.PMT50610DepositCreditNote";
                            break;

                        case "Adjustment":
                            eventArgs.PageNamespace = "GLT00100FRONT.GLT00110";
                            break;

                    }
                }

            
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        private async Task After_OpenView(R_AfterOpenDetailEventArgs eventArgs)
        {
            await _gridDepositDetailRef!.R_RefreshGrid(null);
        }
        #endregion

        #region CHANGE SUBTAB
        //CHANGE TAB
        private void SetOther(R_SetEventArgs eventArgs)
        {
            _pageDepositOnCRUDmode = eventArgs.Enable;
        }
        private void Before_Open_DepositInfo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                LMT05500DepositListDTO loCurrentData = _depositViewModel._currentDeposit;

                if (loCurrentData != null)
                {
                    var loTemp = (LMT05500DepositListDTO)_gridDepositRef!.GetCurrentData();
                    loTemp.CCURRENCY_CODE = _depositViewModel._headerDeposit.CCURRENCY_CODE!;

                    eventArgs.Parameter = loTemp;
                }
                else if (loCurrentData == null)
                {
                    eventArgs.Parameter = null;
                }
                else
                {
                    LMT05500DepositListDTO loParam = new LMT05500DepositListDTO()
                    {
                        CPROPERTY_ID = _depositViewModel._currentDataAgreement.CPROPERTY_ID!,
                        CCHARGE_MODE = _depositViewModel._currentDataAgreement.CCHARGE_MODE,
                        CBUILDING_ID = _depositViewModel._currentDataAgreement.CBUILDING_ID,
                        CFLOOR_ID = _depositViewModel._currentDataAgreement.CFLOOR_ID,
                        CUNIT_ID = _depositViewModel._currentDataAgreement.CUNIT_ID,
                        CDEPT_CODE = _depositViewModel._currentDataAgreement.CDEPT_CODE!,
                        CTRANS_CODE = _depositViewModel._currentDataAgreement.CTRANS_CODE!,
                        CREF_NO = _depositViewModel._currentDataAgreement.CREF_NO!,
                        CCURRENCY_CODE = _depositViewModel._headerDeposit.CCURRENCY_CODE!
                    };
                    eventArgs.Parameter = loParam;
                }

                eventArgs.TargetPageType = typeof(PMT05500DepositInfo);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task OnActiveTabDepositIndexChangingAsync(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            //that used, when user on crud mode and changing the tab. then the command  will be cancelled

            //   eventArgs.Cancel = !_pageDepositOnCRUDmode;

            if (_pageDepositOnCRUDmode && eventArgs.TabStripTab.Id == "TabDepositList")
            {
                await _gridDepositRef!.R_RefreshGrid(null);
            }
            else if (!_pageDepositOnCRUDmode)
            {
                eventArgs.Cancel = true;
            }
        }
        //   public bool _disableTab = true;
        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                if (_depositViewModel._currentDataAgreement != null)
                {
                    _pageDepositOnCRUDmode = (bool)poValue;
                }
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
