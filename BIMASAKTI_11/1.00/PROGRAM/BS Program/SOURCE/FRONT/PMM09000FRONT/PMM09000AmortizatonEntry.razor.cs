using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMCOMMON.DTOs.LML01400;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00200;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMModel.ViewModel.LML01300;
using Lookup_PMModel.ViewModel.LML01400;
using Microsoft.AspNetCore.Components;
using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using PMM09000FrontResources;
using PMM09000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Transactions;
using System.Xml.Linq;

namespace PMM09000FRONT
{
    public partial class PMM09000AmortizatonEntry : R_Page
    {
        private PMM9000RecordViewModel _RecordDataViewModel = new();
        private PMM09000AmortizationEntryViewModel _AmortizationEntryViewModel = new();

        private R_Grid<PMM09000AmortizationChargesDTO>? _gridAmortizationSchedule; //PMM09000AmortizationChargesDTO //PMM09000AmortizationSheduleDetailDTO

        private R_Conductor? _conductorHeader;
        private R_ConductorGrid? _conGridAmortizationShedule;
        PMM09000EntryHeaderDTO _AmortizationDetail = new();
        int _pageSizeAmortizationShedule = 15;
        bool _lDataExist;
        bool _lIsUnit = true;
        public bool lControlCRUDMode;
        public bool lControlCRUDModeAmorSch;
        public bool lGenerateMode;
        public decimal NTotalAmount = 0;
        public decimal NTempTotalAmount = 0;
        public bool lControlButtonClose;
        public bool lControlButtonReopen;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _AmortizationEntryViewModel.oParameter = (PMM09000DbParameterDTO)poParameter;
                lControlCRUDModeAmorSch = false;

                if (_AmortizationEntryViewModel.oParameter.CGENERATE_MODE != null)
                {
                    lGenerateMode = _AmortizationEntryViewModel.oParameter.CGENERATE_MODE == "M";
                }
                if (_AmortizationEntryViewModel.oParameter.CREF_NO != null)
                {
                    await _AmortizationEntryViewModel.ChargesTypeList();
                    _AmortizationEntryViewModel.GetMonth();
                    await _conductorHeader!.R_GetEntity(null);
                }
                else
                {
                    _lDataExist = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Filter
        private async Task RadioButton_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            string lsUnitOption = poParam;
            try
            {
                _AmortizationEntryViewModel._UnitOptionValueEntry = lsUnitOption;
                _AmortizationEntryViewModel.Data.Header.CUNIT_OPTION = lsUnitOption;
                switch (lsUnitOption)
                {
                    case "U":
                        _lIsUnit = true;
                        break;
                    case "O":
                        //_ListDataViewModel._BuildingList.Clear();
                        _lIsUnit = false;
                        //  await _gridAmortization.R_RefreshGrid(null);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion


        #region CRUDBASE HEADER DETAIL
        private async Task GetRecordHeaderDetail(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            PMM09000EntryHeaderDetailDTO loParam = new PMM09000EntryHeaderDetailDTO();

            loParam.Header.CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID;
            loParam.Header.CUNIT_OPTION = _AmortizationEntryViewModel.oParameter.CUNIT_OPTION;
            loParam.Header.CBUILDING_ID = _AmortizationEntryViewModel.oParameter.CBUILDING_ID;
            loParam.Header.CTRANS_TYPE = _AmortizationEntryViewModel.oParameter.CTRANS_TYPE;
            loParam.Header.CREF_NO = _AmortizationEntryViewModel.oParameter.CREF_NO.Trim();

            try
            {
                await _AmortizationEntryViewModel.GetEntityHeaderDetail(loParam);

                if (_AmortizationEntryViewModel._EntityHeaderDetail != null)
                {
                    _lDataExist = true;
                    await _gridAmortizationSchedule!.R_RefreshGrid(null);
                    _AmortizationEntryViewModel._TempAmortizationChargesList = _AmortizationEntryViewModel._AmortizationChargesList;
                    NTempTotalAmount = NTotalAmount;
                    eventArgs.Result = _AmortizationEntryViewModel._EntityHeaderDetail;
                    lControlButtonClose = _AmortizationEntryViewModel._EntityHeaderDetail.Header.CSTATUS == "10" ? true : false;
                    lControlButtonReopen =  _AmortizationEntryViewModel._EntityHeaderDetail.Header.CSTATUS == "80" ? true : false;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void AfterAddAsync(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (PMM09000EntryHeaderDetailDTO)eventArgs.Data;

            try
            {
                loData.Header.CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID;

                lControlCRUDMode = true;
                lGenerateMode = true;
                _AmortizationEntryViewModel._AmortizationChargesList = new ObservableCollection<PMM09000AmortizationChargesDTO>();
                NTotalAmount = 0;
                // await _gridAmortizationSchedule!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private async Task ServiceSaveAllData(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMM09000EntryHeaderDetailDTO loData = _AmortizationEntryViewModel.Data;
                loData.Header.CUNIT_OPTION = _AmortizationEntryViewModel._UnitOptionValueEntry;
                await _AmortizationEntryViewModel.ServiceSave(loData, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _AmortizationEntryViewModel._EntityHeaderDetail;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _AmortizationEntryViewModel._AmortizationChargesList = new ObservableCollection<PMM09000AmortizationChargesDTO>();
                NTotalAmount = 0;

                var res = await R_MessageBox.Show("", @_localizer["ValidationBeforeCancel"],
                    R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
                else
                {

                    _AmortizationEntryViewModel._AmortizationChargesList = _AmortizationEntryViewModel._TempAmortizationChargesList;
                    NTotalAmount = NTempTotalAmount;

                    await Close(false, false);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private async Task DeleteAllData(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM09000EntryHeaderDetailDTO)eventArgs.Data;
                loData.Header.CUNIT_OPTION = _AmortizationEntryViewModel._UnitOptionValueEntry;
                await _AmortizationEntryViewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Validation
        private void ValidationHeader(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conGridAmortizationShedule!.R_ConductorMode != R_eConductorMode.Normal)
                {

                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationModeAmortizationSchedule");
                    loEx.Add(loErr);

                    //_AmortizationEntryViewModel.ValidationHeader(_AmortizationEntryViewModel.Data);
                }
                else
                {
                    if (!_gridAmortizationSchedule.HasData)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationEmptyCharges");
                        loEx.Add(loErr);
                    }
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMM09000EntryHeaderDetailDTO>(eventArgs.Data);
                    _AmortizationEntryViewModel.ValidationHeader(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void ValidationDetail(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //if (_conGridAmortizationShedule!.R_ConductorMode != R_eConductorMode.Normal)
                //{

                //    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMM09000_Class), "ValidationModeAmortizationSchedule");
                //    loEx.Add(loErr);
                //}
                //else
                //{
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM09000AmortizationChargesDTO>(eventArgs.Data);
                _AmortizationEntryViewModel.ValidationDetail(loParam);
                // }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        private async Task R_SetOtherHeaderDetailAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                lControlCRUDMode = (!eventArgs.Enable);
                await Task.Delay(10);
                // lControlCRUDModeAmorSch = (!eventArgs.Enable);
                //   _oEventCallBack.LCRUD_MODE = _viewModel.lControlCRUDMode = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        #region AmortizationScheduleList
        private void R_ServiceAmortizationScheduleListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _AmortizationEntryViewModel.GetAmortizationChargesList();
                eventArgs.ListEntityResult = _AmortizationEntryViewModel._AmortizationChargesList;

                decimal tempTotal = 0;
                foreach (var item in _AmortizationEntryViewModel._AmortizationChargesList)
                {
                    tempTotal += item.NAMOUNT;
                }
                NTotalAmount = tempTotal;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void R_ServiceGetRecordAmorSch(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMM09000AmortizationChargesDTO)eventArgs.Data;
                _AmortizationEntryViewModel.GetAmorCharges(loParam);
                eventArgs.Result = _AmortizationEntryViewModel._oAmorCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void AfterAddAmorSch(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (PMM09000AmortizationChargesDTO)eventArgs.Data;

            try
            {
                int loDataCount = _AmortizationEntryViewModel._AmortizationChargesList.Count;
                loData.CSEQ_NO = (loDataCount + 1).ToString("D3");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private void AfterSaveAmorSch()
        {
            var loEx = new R_Exception();

            try
            {
                if (_gridAmortizationSchedule!.DataSource.Count < 1)
                {
                    NTotalAmount = 0;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_ServiceDisplayAmorSch(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                // var loData = (PMT01700LOO_UnitCharges_Charges_ChargesItemDTO)eventArgs.Data;
                // var PMM09000AmortizationSheduleDetailDTO = (PMM09000AmortizationSheduleDetailDTO)_conGridAmortizationShedule!.R_GetCurrentData();

                var loHeaderData = _AmortizationEntryViewModel._AmortizationChargesList;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_gridAmortizationSchedule!.DataSource.Count > 0)
                    {
                        NTotalAmount = ((_gridAmortizationSchedule.DataSource.Sum(x => x.NAMOUNT)));
                    }
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            //EndBlocks:
            loException.ThrowExceptionIfErrors();
        }
        #endregion
        private void CuttOfPeriod_OnChange(bool poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _AmortizationEntryViewModel.Data.Header.LCUT_OFF_PRD = poParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region Button Custom
        private async Task OnClose()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                  R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_ConfirmationClose"),
                  R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    var loDataTemp = _AmortizationEntryViewModel._EntityHeaderDetail.Header;
                    PMM09000DbParameterDTO poParam = new PMM09000DbParameterDTO
                    {
                        CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                        CUNIT_OPTION = _AmortizationEntryViewModel._UnitOptionValueEntry,
                        CBUILDING_ID = _AmortizationEntryViewModel._UnitOptionValueEntry == "O" ? "" : loDataTemp.CBUILDING_ID,
                        CTRANS_TYPE = loDataTemp.CTRANS_TYPE,
                        CREF_NO = loDataTemp.CREF_NO.Trim(),
                        CACTION = "Close"
                    };
                    var loReturn = await _AmortizationEntryViewModel.ProcessUpdateAmortization(poParam);

                    if (loReturn.LSTATUS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_SuccessMessageOfferClose"));
                        await _conductorHeader.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_FailedUpdate"));
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
        private async Task OnReOpen()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                  R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_ConfirmationReOpen"),
                  R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    var loDataTemp = _AmortizationEntryViewModel._EntityHeaderDetail.Header;
                    PMM09000DbParameterDTO poParam = new PMM09000DbParameterDTO
                    {
                        CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                        CUNIT_OPTION = _AmortizationEntryViewModel._UnitOptionValueEntry,
                        CBUILDING_ID = _AmortizationEntryViewModel._UnitOptionValueEntry == "O" ? "" : loDataTemp.CBUILDING_ID,
                        CTRANS_TYPE = loDataTemp.CTRANS_TYPE,
                        CREF_NO = loDataTemp.CREF_NO.Trim(),
                        CACTION = "Open"
                    };
                    var loReturn = await _AmortizationEntryViewModel.ProcessUpdateAmortization(poParam);
                    if (loReturn.LSTATUS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_SuccessMessageOfferReOpen"));
                        await _conductorHeader.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMM09000_Class), "_FailedUpdate"));
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

        #region Check add edit delete
        private void R_CheckAddAmorSch(R_CheckAddEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                //eventArgs.Allow = lGenerateMode;
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        private void R_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                eventArgs.Allow = _AmortizationEntryViewModel.oParameter.CGENERATE_MODE == "M";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        private void R_CheckDelete(R_CheckDeleteEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                eventArgs.Allow = _AmortizationEntryViewModel.oParameter.CGENERATE_MODE == "M";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }

        #endregion
        #region  ALL_Lookup

        #region Lookup Button Building
        private R_Lookup? R_LookupBuildingLookup;

        private void BeforeOpenLookUpBuildingLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL02200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_AmortizationEntryViewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL02200ParameterDTO
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private void AfterOpenLookUpBuildingLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL02200DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (GSL02200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _AmortizationEntryViewModel.Data.Header.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _AmortizationEntryViewModel.Data.Header.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
                _AmortizationEntryViewModel.Data.Header.CTENANT_ID = "";
                _AmortizationEntryViewModel.Data.Header.CTENANT_NAME = "";
                _AmortizationEntryViewModel.Data.Header.CDEPT_CODE = "";
                _AmortizationEntryViewModel.Data.Header.CDEPT_NAME = "";
                _AmortizationEntryViewModel.Data.Header.CTRANS_DEPT_CODE = "";
                _AmortizationEntryViewModel.Data.Header.CTRANS_DEPT_NAME = "";
                _AmortizationEntryViewModel.Data.Header.CREF_NO = "";
                _AmortizationEntryViewModel.Data.Header.DSTART_DATE = null;
                _AmortizationEntryViewModel.Data.Header.DEND_DATE = null;
                _AmortizationEntryViewModel.Data.Header.CCHARGES_ID = "";
                _AmortizationEntryViewModel.Data.Header.CCHARGES_NAME = ""; ;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusBuilding()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM09000EntryHeaderDetailDTO loGetData = (PMM09000EntryHeaderDetailDTO)_AmortizationEntryViewModel.Data;

                if (string.IsNullOrWhiteSpace(_AmortizationEntryViewModel.Data.Header.CBUILDING_ID))
                {
                    loGetData.Header.CBUILDING_ID = "";
                    loGetData.Header.CBUILDING_NAME = "";
                    return;
                }

                LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel();
                GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.Header.CBUILDING_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetBuilding(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.Header.CBUILDING_ID = "";
                    loGetData.Header.CBUILDING_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.Header.CBUILDING_ID = loResult.CBUILDING_ID;
                    loGetData.Header.CBUILDING_NAME = loResult.CBUILDING_NAME;
                    loGetData.Header.CTENANT_ID = "";
                    loGetData.Header.CTENANT_NAME = "";
                    loGetData.Header.CDEPT_CODE = "";
                    loGetData.Header.CDEPT_NAME = "";
                    loGetData.Header.CTRANS_DEPT_CODE = "";
                    loGetData.Header.CTRANS_DEPT_NAME = "";
                    loGetData.Header.CREF_NO = "";
                    loGetData.Header.DSTART_DATE = null;
                    loGetData.Header.DEND_DATE = null;
                    loGetData.Header.CCHARGES_ID = "";
                    loGetData.Header.CCHARGES_NAME = ""; ;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region Lookup Tenant

        private R_Lookup? R_LookupTenantLookup;

        private void BeforeOpenLookUpTenantLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_AmortizationEntryViewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private void AfterOpenLookUpTenantLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                _AmortizationEntryViewModel.Data.Header.CTENANT_ID = loTempResult.CTENANT_ID;
                _AmortizationEntryViewModel.Data.Header.CTENANT_NAME = loTempResult.CTENANT_NAME;
                _AmortizationEntryViewModel.Data.Header.CDEPT_CODE = "";
                _AmortizationEntryViewModel.Data.Header.CDEPT_NAME = "";
                _AmortizationEntryViewModel.Data.Header.CTRANS_DEPT_CODE = "";
                _AmortizationEntryViewModel.Data.Header.CTRANS_DEPT_NAME = "";
                _AmortizationEntryViewModel.Data.Header.CREF_NO = "";
                _AmortizationEntryViewModel.Data.Header.DSTART_DATE = null;
                _AmortizationEntryViewModel.Data.Header.DEND_DATE = null;
                _AmortizationEntryViewModel.Data.Header.CCHARGES_ID = "";
                _AmortizationEntryViewModel.Data.Header.CCHARGES_NAME = "";


            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnLostFocusTenant()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM09000EntryHeaderDetailDTO loGetData = (PMM09000EntryHeaderDetailDTO)_AmortizationEntryViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.Header.CTENANT_ID))
                {
                    loGetData.Header.CTENANT_ID = "";
                    loGetData.Header.CTENANT_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                    CSEARCH_TEXT = loGetData.Header.CTENANT_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                    loGetData.Header.CTENANT_ID = "";
                    loGetData.Header.CTENANT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.Header.CTENANT_ID = loResult.CTENANT_ID;
                    loGetData.Header.CTENANT_NAME = loResult.CTENANT_NAME;
                    loGetData.Header.CDEPT_CODE = "";
                    loGetData.Header.CDEPT_NAME = "";
                    loGetData.Header.CTRANS_DEPT_CODE = "";
                    loGetData.Header.CTRANS_DEPT_NAME = "";
                    loGetData.Header.CREF_NO = "";
                    loGetData.Header.DSTART_DATE = null;
                    loGetData.Header.DEND_DATE = null;
                    loGetData.Header.CCHARGES_ID = "";
                    loGetData.Header.CCHARGES_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button Department Lookup

        private R_Lookup? R_LookupDepartmentLookup;

        private void BeforeOpenLookUpDepartmentLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00710ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_AmortizationEntryViewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL00710ParameterDTO
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void AfterOpenLookUpDepartmentLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00710DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _AmortizationEntryViewModel.Data.Header.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _AmortizationEntryViewModel.Data.Header.CDEPT_NAME = loTempResult.CDEPT_NAME;
                _AmortizationEntryViewModel.Data.Header.CREF_NO = "";
                _AmortizationEntryViewModel.Data.Header.DSTART_DATE = null;
                _AmortizationEntryViewModel.Data.Header.DEND_DATE = null;
                _AmortizationEntryViewModel.Data.Header.CCHARGES_ID = "";
                _AmortizationEntryViewModel.Data.Header.CCHARGES_NAME = "";
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM09000EntryHeaderDetailDTO loGetData = (PMM09000EntryHeaderDetailDTO)_AmortizationEntryViewModel.Data;

                if (string.IsNullOrWhiteSpace(_AmortizationEntryViewModel.Data.Header.CDEPT_CODE))
                {
                    loGetData.Header.CDEPT_CODE = "";
                    loGetData.Header.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.Header.CDEPT_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.Header.CDEPT_CODE = "";
                    loGetData.Header.CDEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.Header.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.Header.CDEPT_NAME = loResult.CDEPT_NAME;
                    loGetData.Header.CREF_NO = "";
                    loGetData.Header.DSTART_DATE = null;
                    loGetData.Header.DEND_DATE = null;
                    loGetData.Header.CCHARGES_ID = "";
                    loGetData.Header.CCHARGES_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button DepartmentTransaction Lookup

        private R_Lookup? R_LookupDepartmentTransactionLookup;

        private void BeforeOpenLookUpDepartmentTransactionLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00710ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_AmortizationEntryViewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL00710ParameterDTO
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void AfterOpenLookUpDepartmentTransactionLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00710DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _AmortizationEntryViewModel.Data.Header.CTRANS_DEPT_CODE = loTempResult.CDEPT_CODE;
                _AmortizationEntryViewModel.Data.Header.CTRANS_DEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusDepartmentTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM09000EntryHeaderDetailDTO loGetData = (PMM09000EntryHeaderDetailDTO)_AmortizationEntryViewModel.Data;

                if (string.IsNullOrWhiteSpace(_AmortizationEntryViewModel.Data.Header.CTRANS_DEPT_CODE))
                {
                    loGetData.Header.CTRANS_DEPT_CODE = "";
                    loGetData.Header.CTRANS_DEPT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.Header.CTRANS_DEPT_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.Header.CTRANS_DEPT_CODE = "";
                    loGetData.Header.CTRANS_DEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.Header.CTRANS_DEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.Header.CTRANS_DEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button LOI Lookup

        private R_Lookup? R_LookupLOIAgreementLookup;

        private void BeforeOpenLookUpLOIAgreementLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                LML01300ParameterDTO? param = null;
                if (!string.IsNullOrEmpty(_AmortizationEntryViewModel.oParameter.CPROPERTY_ID))
                {
                    param = new LML01300ParameterDTO
                    {
                        CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                        CDEPT_CODE = _AmortizationEntryViewModel.Data.Header.CDEPT_CODE!,
                        CTENANT_ID = _AmortizationEntryViewModel.Data.Header.CTENANT_ID!,
                    };
                }
                if (string.IsNullOrEmpty(param.CDEPT_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(
                              typeof(Resources_PMM09000_Class),
                              "ValidationLOIDept"));
                };

                if (string.IsNullOrEmpty(param.CTENANT_ID))
                {
                    loException.Add(R_FrontUtility.R_GetError(
                              typeof(Resources_PMM09000_Class),
                              "ValidationLOITenant"));
                };
                if (!string.IsNullOrEmpty(param.CTENANT_ID) && !string.IsNullOrEmpty(param.CDEPT_CODE))
                {
                    eventArgs.Parameter = param;
                    eventArgs.TargetPageType = typeof(LML01300);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }

        private void AfterOpenLookUpLOIAgreementLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML01300DTO? loTempResult = null;
            try
            {
                loTempResult = (LML01300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                _AmortizationEntryViewModel.Data.Header.CREF_NO = loTempResult.CREF_NO;
                _AmortizationEntryViewModel.Data.Header.CTRANS_CODE = loTempResult.CTRANS_CODE;
                _AmortizationEntryViewModel.Data.Header.DSTART_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loTempResult.CSTART_DATE!);
                _AmortizationEntryViewModel.Data.Header.DEND_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loTempResult.CEND_DATE!);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusLOIAgreement()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM09000EntryHeaderDetailDTO loGetData = (PMM09000EntryHeaderDetailDTO)_AmortizationEntryViewModel.Data;

                if (string.IsNullOrWhiteSpace(_AmortizationEntryViewModel.Data.Header.CREF_NO))
                {
                    loGetData.Header.CREF_NO = "";
                    _AmortizationEntryViewModel.Data.Header.DSTART_DATE = null;
                    _AmortizationEntryViewModel.Data.Header.DEND_DATE = null;
                    return;
                }

                LookupLML01300ViewModel loLookupViewModel = new LookupLML01300ViewModel();
                LML01300ParameterDTO loParam = new LML01300ParameterDTO()
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                    CDEPT_CODE = _AmortizationEntryViewModel.Data.Header.CDEPT_CODE!,
                    CTENANT_ID = _AmortizationEntryViewModel.Data.Header.CTENANT_ID!,
                    CSEARCH_TEXT = loGetData.Header.CREF_NO ?? "",
                };

                var loResult = await loLookupViewModel.GetLOIAgreement(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                    loGetData.Header.CREF_NO = "";
                    _AmortizationEntryViewModel.Data.Header.DSTART_DATE = null;
                    _AmortizationEntryViewModel.Data.Header.DEND_DATE = null;
                }
                else
                {
                    loGetData.Header.CREF_NO = loResult.CREF_NO;
                    _AmortizationEntryViewModel.Data.Header.CTRANS_CODE = loResult.CTRANS_CODE;
                    _AmortizationEntryViewModel.Data.Header.DSTART_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loResult.CSTART_DATE!);
                    _AmortizationEntryViewModel.Data.Header.DEND_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loResult.CEND_DATE!);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button Charges ACCRUAL Lookup

        private R_Lookup? R_LookupChargesLookup;

        private void BeforeOpenLookUpChargesLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_AmortizationEntryViewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00200ParameterDTO
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "01,02,04,05,06,07,08",
                    LACCRUAL = true
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private void AfterOpenLookUpChargesLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00200DTO? loTempResult = null;
            try
            {
                loTempResult = (LML00200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                _AmortizationEntryViewModel.Data.Header.CCHARGES_ID = loTempResult.CCHARGES_ID;
                _AmortizationEntryViewModel.Data.Header.CCHARGES_NAME = loTempResult.CCHARGES_NAME; ;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusCharges()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM09000EntryHeaderDetailDTO loGetData = (PMM09000EntryHeaderDetailDTO)_AmortizationEntryViewModel.Data;

                if (string.IsNullOrWhiteSpace(_AmortizationEntryViewModel.Data.Header.CREF_NO))
                {
                    loGetData.Header.CREF_NO = "";
                    return;
                }

                LookupLML00200ViewModel loLookupViewModel = new LookupLML00200ViewModel();
                LML00200ParameterDTO loParam = new LML00200ParameterDTO()
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "01,02,04,05,06,07,08",
                    LACCRUAL = true,
                    CSEARCH_TEXT = loGetData.Header.CCHARGES_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetUnitCharges(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                            "_ErrLookup01"));
                    loGetData.Header.CCHARGES_ID = "";
                    loGetData.Header.CCHARGES_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.Header.CCHARGES_ID = loResult.CCHARGES_ID;
                    loGetData.Header.CCHARGES_NAME = loResult.CCHARGES_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Agreement Unit Charges
        private void BeforeOpenLookUpCharge(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var param = new LML01400ParameterDTO();
                var loData = _AmortizationEntryViewModel.Data.Header;

                if (!string.IsNullOrEmpty(_AmortizationEntryViewModel.oParameter.CPROPERTY_ID))
                {
                    param = new LML01400ParameterDTO
                    {
                        CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                        CDEPT_CODE = loData.CDEPT_CODE ?? "",
                        CTRANS_CODE = loData.CTRANS_CODE ?? "",
                        CREF_NO = loData.CREF_NO ?? "",
                        CSEQ_NO = "",
                        LNON_ACCRUAL = true
                    };
                };
                if (string.IsNullOrEmpty(param.CDEPT_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(
                              typeof(Resources_PMM09000_Class),
                              "ValidationDeptHeader"));
                };

                if (string.IsNullOrEmpty(param.CREF_NO))
                {
                    loException.Add(R_FrontUtility.R_GetError(
                              typeof(Resources_PMM09000_Class),
                              "ValidationRefNoHeader"));
                };
                if (string.IsNullOrEmpty(param.CTRANS_CODE))
                {
                    loException.Add(R_FrontUtility.R_GetError(
                              typeof(Resources_PMM09000_Class),
                              "ValidationTransactionCodeHeader"));
                };
                if (!string.IsNullOrEmpty(param.CTRANS_CODE) && !string.IsNullOrEmpty(param.CDEPT_CODE) && !string.IsNullOrEmpty(param.CREF_NO))
                {
                    eventArgs.Parameter = param;
                    eventArgs.TargetPageType = typeof(LML01400);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);

        }

        private void AfterOpenLookCharge(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            //mengambil result dari popup dan set ke data row
            if (eventArgs.Result == null)
            {
                return;
            }
            if (eventArgs.ColumnName == "Charge")
            {
                LML01400DTO loTempResult2 = R_FrontUtility.ConvertObjectToObject<LML01400DTO>(eventArgs.Result);
                ((PMM09000AmortizationChargesDTO)eventArgs.ColumnData).CCHARGES_ID = loTempResult2.CCHARGES_ID;
                ((PMM09000AmortizationChargesDTO)eventArgs.ColumnData).CCHARGES_NAME = loTempResult2.CCHARGES_NAME;

                ((PMM09000AmortizationChargesDTO)eventArgs.ColumnData).CCHARGES_TYPE = loTempResult2.CCHARGES_TYPE;
                ((PMM09000AmortizationChargesDTO)eventArgs.ColumnData).CCHARGES_TYPE_NAME = loTempResult2.CCHARGES_TYPE_DESCR;
                ((PMM09000AmortizationChargesDTO)eventArgs.ColumnData).DSTART_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loTempResult2.CSTART_DATE!); //loTempResult2.DSTART_DATE;
                ((PMM09000AmortizationChargesDTO)eventArgs.ColumnData).DEND_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loTempResult2.CEND_DATE!);
                ((PMM09000AmortizationChargesDTO)eventArgs.ColumnData).NAMOUNT = loTempResult2.NINVOICE_AMT;
            }
        }

        private async Task R_CellLostFocusedCharge(R_CellLostFocusedEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMM09000AmortizationChargesDTO loGetData = (PMM09000AmortizationChargesDTO)eventArgs.CurrentRow;
                var loData = _AmortizationEntryViewModel.Data.Header;

                if (eventArgs.ColumnName == "Charge")
                {
                    if (!string.IsNullOrWhiteSpace((string)eventArgs.Value))
                    {
                        LookupLML01400ViewModel loLookupViewModel = new LookupLML01400ViewModel();
                        var param = new LML01400ParameterDTO
                        {
                            CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                            CDEPT_CODE = loData.CDEPT_CODE ?? "",
                            CTRANS_CODE = loData.CTRANS_CODE ?? "",
                            CREF_NO = loData.CREF_NO ?? "",
                            CSEQ_NO = "",
                            CSEARCH_TEXT = (string)eventArgs.Value
                        };
                        var loResult = await loLookupViewModel.GetAgreementUnitCharges(param);

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                    "_ErrLookup01"));
                            loGetData.CCHARGES_ID = "";
                            loGetData.CCHARGES_NAME = "";
                            loGetData.CCHARGES_TYPE_NAME = "";
                            loGetData.DSTART_DATE = null;
                            loGetData.DEND_DATE = null;
                            loGetData.NAMOUNT = 0;
                        }
                        else
                        {
                            loGetData.CCHARGES_TYPE_NAME = loResult.CCHARGES_TYPE_DESCR;
                            loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                            loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
                            loGetData.DSTART_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loResult.CSTART_DATE!); //loResult.DSTART_DATE;
                            loGetData.DEND_DATE = _AmortizationEntryViewModel.ConvertStringToDateTimeFormat(loResult.CEND_DATE!);
                            loGetData.NAMOUNT = loResult.NINVOICE_AMT;
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
        // private async 
        #endregion
        #endregion

        #region UserLocking
        [Inject] IClientHelper clientHelper { get; set; }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loDataTemp = (PMM09000EntryHeaderDetailDTO)eventArgs.Data;
                var loData = loDataTemp.Header;
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {

                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM09000",
                        Table_Name = "PMM_AMORTIZATION_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)

                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM09000",
                        Table_Name = "PMM_AMORTIZATION_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loDataTemp = _AmortizationEntryViewModel._EntityHeaderDetail.Header;
                PMM09000DbParameterDTO poParam = new PMM09000DbParameterDTO
                {
                    CPROPERTY_ID = _AmortizationEntryViewModel.oParameter.CPROPERTY_ID,
                    CUNIT_OPTION = _AmortizationEntryViewModel._UnitOptionValueEntry,
                    CBUILDING_ID = _AmortizationEntryViewModel._UnitOptionValueEntry == "O" ? "" : loDataTemp.CBUILDING_ID,
                    CTRANS_TYPE = loDataTemp.CTRANS_TYPE,
                    CREF_NO = loDataTemp.CREF_NO.Trim(),
                    CACTION = "Open"
                };

                var loData = _AmortizationEntryViewModel._EntityHeaderDetail.Header;
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
                if (param) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM09000",
                        Table_Name = "PMM_AMORTIZATION_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, _AmortizationEntryViewModel.oParameter.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM09000",
                        Table_Name = "PMM_AMORTIZATION_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, _AmortizationEntryViewModel.oParameter.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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
