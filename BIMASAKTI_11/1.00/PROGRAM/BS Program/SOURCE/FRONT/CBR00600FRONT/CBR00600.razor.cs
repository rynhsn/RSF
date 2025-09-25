using BlazorClientHelper;
using CBR00600COMMON;
using CBR00600MODEL;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using Lookup_CBFRONT;
using Lookup_CBModel.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System.Threading.Channels;
using System.Xml.Linq;

namespace CBR00600FRONT
{
    public partial class CBR00600 : R_Page
    {
        private CBR00600ViewModel _viewModel = new CBR00600ViewModel();
        
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        #region Private Field
        private string PageWidth = "width: auto;";
        private string CDEPT_NAME = "";
        private string CMESSAGE_NAME = "";
        private string CMESSAGE_DESC = "";
        private string CMESSAGE_ADDITIONAL = "";
        private bool EnableJournal = true;
        private CBR00600CallerParameterDTO loCallerParameter = new CBR00600CallerParameterDTO();
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVar();
                await _viewModel.GetPeriodBindingList();

                if (poParameter != null)
                {
                    var loParameter = R_FrontUtility.ConvertObjectToObject<CBR00600CallerParameterDTO>(poParameter);
                    loCallerParameter = loParameter;
                    if (!string.IsNullOrWhiteSpace(loParameter.CREF_NO))
                    {
                        TransactionTypeComboBox_ValueChanged(loParameter.CTRANS_CODE);
                        if (!string.IsNullOrWhiteSpace(loParameter.CPERIOD))
                        {
                            _viewModel.LbForPeriod = true;
                            _viewModel.LiPeriodYear = int.Parse(loParameter.CPERIOD.Substring(0, 4));
                            _viewModel.LcPeriodMonth = loParameter.CPERIOD.Substring(4, 2);
                        }
                        _viewModel.ReportParameter.CPROPERTY_ID = loParameter.CPROPERTY_ID;
                        _viewModel.ReportParameter.CDEPT_CODE = loParameter.CDEPT_CODE;
                        await OnLostFocusDepartment();
                        _viewModel.CFROM_REF_NO = loParameter.CREF_NO;
                        _viewModel.CTO_REF_NO = loParameter.CREF_NO;
                        PageWidth = "width: 1100px;";
                    }
                }

                await FilterByRadioButton_ValueChanged("REF_NO"); 
                await MessageTypeRadioButton_ValueChanged("04");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Value Change
        private void PropertyComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.ReportParameter.CPROPERTY_ID = poParam;
                _viewModel.ReportParameter.CDEPT_CODE = "";
                CDEPT_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TransactionTypeComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.ReportParameter.CTRANS_CODE = poParam;
                EnableJournal = poParam == "990010" || poParam == "190010" || poParam == "190020" || poParam == "990020" || poParam == "190030" || poParam == "990030" || poParam == "191000";
                _viewModel.ReportParameter.LRECEIPT = poParam == "991000";
                _viewModel.ReportParameter.LALLOCATION = poParam == "991000";
                _viewModel.ReportParameter.LJOURNAL = EnableJournal;
                
                _viewModel.CFROM_REF_NO = "";
                _viewModel.CTO_REF_NO = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task FilterByRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.ReportParameter.CFILTER_BY = poParam;
                if (string.IsNullOrWhiteSpace(loCallerParameter.CREF_NO))
                {
                    _viewModel.CFROM_REF_NO = "";
                    _viewModel.CTO_REF_NO = "";
                    _viewModel.DFROM_DATE = null;
                    _viewModel.DTO_DATE = null;
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task MessageTypeRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.ReportParameter.CMESSAGE_TYPE = poParam;
                _viewModel.ReportParameter.CMESSAGE_NO = "";
                CMESSAGE_NAME = "";
                CMESSAGE_DESC = "";
                CMESSAGE_ADDITIONAL = "";
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task YearNumericTexBox_ValueChanged(int poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.LiPeriodYear = poParam;
                await _viewModel.GetPeriodBindingList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Departement Lookup
        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.ReportParameter.CDEPT_CODE))
                {
                    CDEPT_NAME = "";
                    _viewModel.CFROM_REF_NO = "";
                    _viewModel.CTO_REF_NO = "";
                    return;
                }

                if (string.IsNullOrWhiteSpace(_viewModel.ReportParameter.CPROPERTY_ID))
                {
                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                    GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.ReportParameter.CDEPT_CODE
                    };

                    var loResult = await loLookupViewModel.GetDepartment(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        CDEPT_NAME = "";
                        _viewModel.CFROM_REF_NO = "";
                        _viewModel.CTO_REF_NO = "";
                    }
                    else
                    {
                        _viewModel.ReportParameter.CDEPT_CODE = loResult.CDEPT_CODE;
                        CDEPT_NAME = loResult.CDEPT_NAME;
                        _viewModel.CFROM_REF_NO = "";
                        _viewModel.CTO_REF_NO = "";
                    }
                }
                else
                {
                    LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                    GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                    {
                        CSEARCH_TEXT = _viewModel.ReportParameter.CDEPT_CODE,
                        CPROPERTY_ID = _viewModel.ReportParameter.CPROPERTY_ID,
                    };

                    var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.CFROM_REF_NO = "";
                        _viewModel.CTO_REF_NO = "";
                        CDEPT_NAME = "";
                    }
                    else
                    {
                        _viewModel.ReportParameter.CDEPT_CODE = loResult.CDEPT_CODE;
                        CDEPT_NAME = loResult.CDEPT_NAME;
                        _viewModel.CFROM_REF_NO = "";
                        _viewModel.CTO_REF_NO = "";
                    }
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
            if (string.IsNullOrWhiteSpace(_viewModel.ReportParameter.CPROPERTY_ID))
            {
                GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                {
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            else
            {
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.ReportParameter.CPROPERTY_ID
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00710);
            }
        }
        private void R_After_Open_LookupDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            if (eventArgs.Result == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.ReportParameter.CPROPERTY_ID))
            {
                GSL00700DTO loTempResult = (GSL00700DTO)eventArgs.Result;

                _viewModel.ReportParameter.CDEPT_CODE = loTempResult.CDEPT_CODE;
                CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            else
            {
                GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;

                _viewModel.ReportParameter.CDEPT_CODE = loTempResult.CDEPT_CODE;
                CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            _viewModel.CFROM_REF_NO = "";
            _viewModel.CTO_REF_NO = "";
        }
        #endregion

        #region From Ref No Lookup
        private async Task OnLostFocusFromRefNo()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.CFROM_REF_NO))
                {
                    CDEPT_NAME = "";
                    return;
                }

                if (_viewModel.ReportParameter.CTRANS_CODE == "991000")
                {
                    CBL00100ViewModel loLookupViewModel = new CBL00100ViewModel();
                    CBL00100ParameterDTO loParam = new CBL00100ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.ReportParameter.CPROPERTY_ID,
                        CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                        CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                        CSEARCH_TEXT = _viewModel.CFROM_REF_NO,
                    };

                    var loResult = await loLookupViewModel.GetReceiptFromCustomer(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    else
                    {
                        _viewModel.CFROM_REF_NO = loResult.CREF_NO;
                    }
                }
                else if (_viewModel.ReportParameter.CTRANS_CODE == "990010" || _viewModel.ReportParameter.CTRANS_CODE == "191000" || _viewModel.ReportParameter.CTRANS_CODE == "190010" || _viewModel.ReportParameter.CTRANS_CODE == "190020" || _viewModel.ReportParameter.CTRANS_CODE == "990020" || _viewModel.ReportParameter.CTRANS_CODE == "190030" || _viewModel.ReportParameter.CTRANS_CODE == "990030")
                {
                    CBL00200ViewModel loLookupViewModel = new CBL00200ViewModel();
                    CBL00200ParameterDTO loParam = new CBL00200ParameterDTO()
                    {
                        CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                        CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                        CTRANS_CODE = _viewModel.ReportParameter.CTRANS_CODE,
                        CSEARCH_TEXT = _viewModel.CFROM_REF_NO,
                    };

                    var loResult = await loLookupViewModel.GetCBJournal(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    else
                    {
                        _viewModel.CFROM_REF_NO = loResult.CREF_NO;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupFromRefNo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (_viewModel.ReportParameter.CTRANS_CODE == "991000")
            {
                CBL00100ParameterDTO loParam = new CBL00100ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.ReportParameter.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                    CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                    CPROPERTY_NAME = _viewModel.VAR_PROPERTY_LIST.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.ReportParameter.CPROPERTY_ID).CPROPERTY_NAME,
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(CBL00100);
            }
            else if (_viewModel.ReportParameter.CTRANS_CODE == "990010" || _viewModel.ReportParameter.CTRANS_CODE == "191000" || _viewModel.ReportParameter.CTRANS_CODE == "190010" || _viewModel.ReportParameter.CTRANS_CODE == "190020" || _viewModel.ReportParameter.CTRANS_CODE == "990020" || _viewModel.ReportParameter.CTRANS_CODE == "190030" || _viewModel.ReportParameter.CTRANS_CODE == "990030")
            {
                CBL00200ParameterDTO loParam = new CBL00200ParameterDTO()
                {
                    CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                    CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                    CTRANS_CODE = _viewModel.ReportParameter.CTRANS_CODE,
                    CTRANS_NAME = _viewModel.VAR_TRX_TYPE_LIST.FirstOrDefault(x => x.CTRANS_CODE == _viewModel.ReportParameter.CTRANS_CODE).CTRANSACTION_NAME,
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(CBL00200);
            }
        }
        private void R_After_Open_LookupFromRefNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            if (_viewModel.ReportParameter.CTRANS_CODE == "991000")
            {
                CBL00100DTO loTempResult = (CBL00100DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _viewModel.CFROM_REF_NO = loTempResult.CREF_NO;
            }
            else if (_viewModel.ReportParameter.CTRANS_CODE == "990010" || _viewModel.ReportParameter.CTRANS_CODE == "191000" || _viewModel.ReportParameter.CTRANS_CODE == "190010" || _viewModel.ReportParameter.CTRANS_CODE == "190020" || _viewModel.ReportParameter.CTRANS_CODE == "990020" || _viewModel.ReportParameter.CTRANS_CODE == "190030" || _viewModel.ReportParameter.CTRANS_CODE == "990030")
            {
                CBL00200DTO loTempResult = (CBL00200DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _viewModel.CFROM_REF_NO = loTempResult.CREF_NO;
            }
        }
        #endregion

        #region To Ref No Lookup
        private async Task OnLostFocusToRefNo()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.CTO_REF_NO))
                {
                    CDEPT_NAME = "";
                    return;
                }

                if (_viewModel.ReportParameter.CTRANS_CODE == "991000")
                {
                    CBL00100ViewModel loLookupViewModel = new CBL00100ViewModel();
                    CBL00100ParameterDTO loParam = new CBL00100ParameterDTO()
                    {
                        CPROPERTY_ID = _viewModel.ReportParameter.CPROPERTY_ID,
                        CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                        CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                        CSEARCH_TEXT = _viewModel.CTO_REF_NO,
                    };

                    var loResult = await loLookupViewModel.GetReceiptFromCustomer(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    else
                    {
                        _viewModel.CTO_REF_NO = loResult.CREF_NO;
                    }
                }
                else if (_viewModel.ReportParameter.CTRANS_CODE == "990010" || _viewModel.ReportParameter.CTRANS_CODE == "191000" || _viewModel.ReportParameter.CTRANS_CODE == "190010" || _viewModel.ReportParameter.CTRANS_CODE == "190020" || _viewModel.ReportParameter.CTRANS_CODE == "990020" || _viewModel.ReportParameter.CTRANS_CODE == "190030" || _viewModel.ReportParameter.CTRANS_CODE == "990030")
                {
                    CBL00200ViewModel loLookupViewModel = new CBL00200ViewModel();
                    CBL00200ParameterDTO loParam = new CBL00200ParameterDTO()
                    {
                        CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                        CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                        CTRANS_CODE = _viewModel.ReportParameter.CTRANS_CODE,
                        CSEARCH_TEXT = _viewModel.CTO_REF_NO,
                    };

                    var loResult = await loLookupViewModel.GetCBJournal(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    else
                    {
                        _viewModel.CTO_REF_NO = loResult.CREF_NO;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupToRefNo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (_viewModel.ReportParameter.CTRANS_CODE == "991000")
            {
                CBL00100ParameterDTO loParam = new CBL00100ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.ReportParameter.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                    CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                    CPROPERTY_NAME = _viewModel.VAR_PROPERTY_LIST.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.ReportParameter.CPROPERTY_ID).CPROPERTY_NAME,
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(CBL00100);
            }
            else if (_viewModel.ReportParameter.CTRANS_CODE == "990010" || _viewModel.ReportParameter.CTRANS_CODE == "191000" || _viewModel.ReportParameter.CTRANS_CODE == "190010" || _viewModel.ReportParameter.CTRANS_CODE == "190020" || _viewModel.ReportParameter.CTRANS_CODE == "990020" || _viewModel.ReportParameter.CTRANS_CODE == "190030" || _viewModel.ReportParameter.CTRANS_CODE == "990030")
            {
                CBL00200ParameterDTO loParam = new CBL00200ParameterDTO()
                {
                    CDEPT_CODE = _viewModel.ReportParameter.CDEPT_CODE,
                    CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "",
                    CTRANS_CODE = _viewModel.ReportParameter.CTRANS_CODE,
                    CTRANS_NAME = _viewModel.VAR_TRX_TYPE_LIST.FirstOrDefault(x => x.CTRANS_CODE == _viewModel.ReportParameter.CTRANS_CODE).CTRANSACTION_NAME,
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(CBL00200);
            }
        }
        private void R_After_Open_LookupToRefNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            if (_viewModel.ReportParameter.CTRANS_CODE == "991000")
            {
                CBL00100DTO loTempResult = (CBL00100DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _viewModel.CTO_REF_NO = loTempResult.CREF_NO;
            }
            else if (_viewModel.ReportParameter.CTRANS_CODE == "990010" || _viewModel.ReportParameter.CTRANS_CODE == "191000" || _viewModel.ReportParameter.CTRANS_CODE == "190010" || _viewModel.ReportParameter.CTRANS_CODE == "190020" || _viewModel.ReportParameter.CTRANS_CODE == "990020" || _viewModel.ReportParameter.CTRANS_CODE == "190030" || _viewModel.ReportParameter.CTRANS_CODE == "990030")
            {
                CBL00200DTO loTempResult = (CBL00200DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _viewModel.CTO_REF_NO = loTempResult.CREF_NO;
            }
        }
        #endregion

        #region Message Lookup
        private async Task OnLostFocusMessage()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.ReportParameter.CMESSAGE_NO))
                {
                    CMESSAGE_NAME = "";
                    CMESSAGE_DESC = "";
                    CMESSAGE_ADDITIONAL = "";
                    return;
                }

                LookupGSL03700ViewModel loLookupViewModel = new LookupGSL03700ViewModel();
                GSL03700ParameterDTO loParam = new GSL03700ParameterDTO()
                {
                    CMESSAGE_TYPE = _viewModel.ReportParameter.CMESSAGE_TYPE,
                    CSEARCH_TEXT = _viewModel.ReportParameter.CMESSAGE_NO
                };

                var loResult = await loLookupViewModel.GetMessage(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    CMESSAGE_NAME = "";
                    CMESSAGE_DESC = "";
                    CMESSAGE_ADDITIONAL = "";
                }
                else
                {
                    _viewModel.ReportParameter.CMESSAGE_NO = loResult.CMESSAGE_NO;
                    CMESSAGE_NAME = loResult.CMESSAGE_NAME;
                    CMESSAGE_DESC = loResult.TMESSAGE_DESCRIPTION;
                    CMESSAGE_ADDITIONAL = loResult.CADDITIONAL_DESCRIPTION;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupMessage(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.ReportParameter.CMESSAGE_TYPE))
            {
                return;
            }
            GSL03700ParameterDTO loParam = new GSL03700ParameterDTO()
            {
                CMESSAGE_TYPE = _viewModel.ReportParameter.CMESSAGE_TYPE,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL03700);
        }
        private void R_After_Open_LookupMessage(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL03700DTO loTempResult = (GSL03700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.ReportParameter.CMESSAGE_NO = loTempResult.CMESSAGE_NO;
            CMESSAGE_NAME = loTempResult.CMESSAGE_NAME;
            CMESSAGE_DESC = loTempResult.TMESSAGE_DESCRIPTION;
            CMESSAGE_ADDITIONAL = loTempResult.CADDITIONAL_DESCRIPTION;
        }
        #endregion

        #region Button
        private async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (CBR00600DTO)_viewModel.ReportParameter;
                
                // Set Data
                loData.CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName;
                loData.CUSER_ID = clientHelper.UserId;
                loData.CCOMPANY_ID = clientHelper.CompanyId;
                loData.LIS_PRINT = true;
                loData.CREPORT_CULTURE = clientHelper.ReportCulture;
                loData.CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "";

                bool lCancel = false;

                lCancel = string.IsNullOrEmpty(loData.CDEPT_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(CBR00600FrontResources.Resources_Dummy_Class),
                        "V02"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTRANS_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(CBR00600FrontResources.Resources_Dummy_Class),
                        "V03"));
                }

                if (loData.CFILTER_BY == "REF_NO")
                {
                    lCancel = string.IsNullOrEmpty(_viewModel.CFROM_REF_NO);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V04"));
                    }

                    lCancel = string.IsNullOrEmpty(_viewModel.CTO_REF_NO);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V05"));
                    }
                }
                else
                {
                    lCancel = _viewModel.DFROM_DATE == null;

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V06"));
                    }

                    lCancel = _viewModel.DTO_DATE == null;

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V07"));
                    }
                }

                lCancel = !_viewModel.ReportParameter.LRECEIPT && !_viewModel.ReportParameter.LALLOCATION && !_viewModel.ReportParameter.LJOURNAL;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(CBR00600FrontResources.Resources_Dummy_Class),
                        "V08"));
                }

                if (lCancel == false)
                {
                    if (loData.CFILTER_BY == "REF_NO")
                    {
                        loData.CFROM_VALUE = _viewModel.CFROM_REF_NO;
                        loData.CTO_VALUE = _viewModel.CTO_REF_NO;
                    }
                    else
                    {
                        loData.CFROM_VALUE = _viewModel.DFROM_DATE.Value.ToString("yyyyMMdd");
                        loData.CTO_VALUE = _viewModel.DTO_DATE.Value.ToString("yyyyMMdd");
                    }

                    if (loData.LRECEIPT)
                    {
                        await _reportService.GetReport(
                                       "R_DefaultServiceUrlCB",
                                       "CB",
                                       "rpt/CBR00600PrintReceipt/AllPrintReceiptCBTransactionPost",
                                       "rpt/CBR00600PrintReceipt/AllStreamPrintReceiptCBTransactionsGet",
                                       loData);
                    }

                    if (loData.LALLOCATION)
                    {
                        if (loData.CTRANS_CODE == "191000")
                        {
                            await _reportService.GetReport(
                                       "R_DefaultServiceUrlCB",
                                       "CB",
                                       "rpt/CBR00600PrintAllocationPaymentToSupplier/AllPrintAllocationPaymentToSupplierCBTransactionPost",
                                       "rpt/CBR00600PrintAllocationPaymentToSupplier/AllStreamPrintAllocationPaymentToSupplierCBTransactionsGet",
                                       loData);
                        }
                        else
                        {
                            await _reportService.GetReport(
                                       "R_DefaultServiceUrlCB",
                                       "CB",
                                       "rpt/CBR00600PrintAllocation/AllPrintAllocationCBTransactionPost",
                                       "rpt/CBR00600PrintAllocation/AllStreamPrintAllocationCBTransactionsGet",
                                       loData);
                        }
                    }

                    if (loData.LJOURNAL)
                    {
                        if (loData.CTRANS_CODE == "991000")
                        {
                            await _reportService.GetReport(
                                       "R_DefaultServiceUrlCB",
                                       "CB",
                                       "rpt/CBR00600PrintJournal/AllPrintJournalCBTransactionPost",
                                       "rpt/CBR00600PrintJournal/AllStreamPrintJournalCBTransactionsGet",
                                       loData);
                        }
                        else if (loData.CTRANS_CODE == "191000")
                        {
                            await _reportService.GetReport(
                                       "R_DefaultServiceUrlCB",
                                       "CB",
                                       "rpt/CBR00600PrintJournalPaymentToSupplier/AllPrintJournalPaymentToSupplierCBTransactionPost",
                                       "rpt/CBR00600PrintJournalPaymentToSupplier/AllStreamPrintJournalPaymentToSupplierCBTransactionsGet",
                                       loData);
                        }
                        else
                        {
                            await _reportService.GetReport(
                                       "R_DefaultServiceUrlCB",
                                       "CB",
                                       "rpt/CBR00600PrintJournalWithoutCustomer/AllPrintJournalWithoutCustomerCBTransactionPost",
                                       "rpt/CBR00600PrintJournalWithoutCustomer/AllStreamPrintJournalWithoutCustomerCBTransactionsGet",
                                       loData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Popup Save As
        private void SaveAs_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (CBR00600DTO)_viewModel.ReportParameter;

                bool lCancel = false;

                lCancel = string.IsNullOrEmpty(loData.CDEPT_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(CBR00600FrontResources.Resources_Dummy_Class),
                        "V02"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTRANS_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(CBR00600FrontResources.Resources_Dummy_Class),
                        "V03"));
                }

                if (loData.CFILTER_BY == "REF_NO")
                {
                    lCancel = string.IsNullOrEmpty(_viewModel.CFROM_REF_NO);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V04"));
                    }

                    lCancel = string.IsNullOrEmpty(_viewModel.CTO_REF_NO);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V05"));
                    }
                }
                else
                {
                    lCancel = _viewModel.DFROM_DATE == null;

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V06"));
                    }

                    lCancel = _viewModel.DTO_DATE == null;

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(CBR00600FrontResources.Resources_Dummy_Class),
                            "V07"));
                    }
                }

                lCancel = !_viewModel.ReportParameter.LRECEIPT && !_viewModel.ReportParameter.LALLOCATION && !_viewModel.ReportParameter.LJOURNAL;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(CBR00600FrontResources.Resources_Dummy_Class),
                        "V08"));
                }

                if (lCancel == false)
                {
                    eventArgs.TargetPageType = typeof(CBR00601);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task SaveAs_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Success == true)
                {
                    if (eventArgs.Result != null)
                    {
                        var loTypeFile = (CBR00600DTO)eventArgs.Result;
                        var loData = (CBR00600DTO)_viewModel.ReportParameter;

                        // Set Data
                        loData.CREPORT_FILETYPE = loTypeFile.CREPORT_FILETYPE;
                        loData.CREPORT_FILENAME = loTypeFile.CREPORT_FILENAME;
                        loData.CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName;
                        loData.CUSER_ID = clientHelper.UserId;
                        loData.CCOMPANY_ID = clientHelper.CompanyId;
                        loData.LIS_PRINT = false;
                        loData.CREPORT_CULTURE = clientHelper.ReportCulture;
                        loData.CPERIOD = _viewModel.LbForPeriod ? _viewModel.LiPeriodYear + _viewModel.LcPeriodMonth : "";

                        if (loData.CFILTER_BY == "REF_NO")
                        {
                            loData.CFROM_VALUE = _viewModel.CFROM_REF_NO;
                            loData.CTO_VALUE = _viewModel.CTO_REF_NO;
                        }
                        else
                        {
                            loData.CFROM_VALUE = _viewModel.DFROM_DATE.Value.ToString("yyyyMMdd");
                            loData.CTO_VALUE = _viewModel.DTO_DATE.Value.ToString("yyyyMMdd");
                        }

                        if (loData.LRECEIPT)
                        {
                            await _reportService.GetReport(
                                           "R_DefaultServiceUrlCB",
                                           "CB",
                                           "rpt/CBR00600PrintReceipt/AllPrintReceiptCBTransactionPost",
                                           "rpt/CBR00600PrintReceipt/AllStreamPrintReceiptCBTransactionsGet",
                                           loData);
                        }

                        if (loData.LALLOCATION)
                        {
                            await _reportService.GetReport(
                                           "R_DefaultServiceUrlCB",
                                           "CB",
                                           "rpt/CBR00600PrintAllocation/AllPrintAllocationCBTransactionPost",
                                           "rpt/CBR00600PrintAllocation/AllStreamPrintAllocationCBTransactionsGet",
                                           loData);
                        }

                        if (loData.LJOURNAL)
                        {
                            if (loData.CTRANS_CODE == "991000")
                            {
                                await _reportService.GetReport(
                                           "R_DefaultServiceUrlCB",
                                           "CB",
                                           "rpt/CBR00600PrintJournal/AllPrintJournalCBTransactionPost",
                                           "rpt/CBR00600PrintJournal/AllStreamPrintJournalCBTransactionsGet",
                                           loData);
                            }
                            else
                            {
                                await _reportService.GetReport(
                                           "R_DefaultServiceUrlCB",
                                           "CB",
                                           "rpt/CBR00600PrintJournalWithoutCustomer/AllPrintJournalWithoutCustomerCBTransactionPost",
                                           "rpt/CBR00600PrintJournalWithoutCustomer/AllStreamPrintJournalCBTransactionsGet",
                                           loData);
                            }
                        }
                    }
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
