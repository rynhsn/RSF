using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Utility;
using PMT02000FrontResources;
using PMT02000MODEL.ViewModel;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PMT02000FRONT
{
    public partial class PopUpHandOver
    {
        private PMT02000ViewModel _viewModel = new();
        private R_Conductor? _conConductorHeader;
        private R_ConductorGrid? _conGridUnit;
        private R_ConductorGrid? _conGridUtility;

        private R_Grid<PMT02000LOIHandOverUnitDTO>? _griLOIDetailRefUnit;
        private R_Grid<PMT02000LOIHandoverUtilityDTO>? _griLOIDetailUtilityRef;
        private R_TextBox? FocusLabelRefNo;
        private bool _lDataCREF_NO = false;
        private bool _lAdd_Mode = false;
        private int _pageSizeUnit = 10;
        private int _pageSize = 10;
        private bool _cekEdit = true;
        public string labelHO_LOIRefNo= "_LabelLOIRefNo";
        [Inject] private IClientHelper? _clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel._oParamHeaderPopUp = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader>(poParameter);

                var loParamFromAnotherProgram = R_FrontUtility.ConvertObjectToObject<PMT02000ParameterFrontChangePageDTO>(poParameter);
                _viewModel._oParamGetHeader = _viewModel.ConvertDTO(_viewModel._oParamHeaderPopUp);

                _viewModel.GetMonth();
                if (_viewModel._oParamHeaderPopUp.CALLER_ACTION == "VIEW")
                {
                    labelHO_LOIRefNo = "_LabelHORefNo";
                    _cekEdit = false;
                    _viewModel._oParamHeaderPopUp.CSAVEMODE = "EDIT";
                    _viewModel._oParamGetHeader.CSAVEMODE = "EDIT";
                    await _conConductorHeader!.R_GetEntity(null);
                }
                else
                {
                    _cekEdit = true;
                    await _viewModel.GetVAR_GSM_TRANSACTION_CODE();
                    if (_viewModel._oParamHeaderPopUp.CSAVEMODE == "NEW")
                    {
                        labelHO_LOIRefNo = "_LabelLOIRefNo";
                        await _conConductorHeader!.R_GetEntity(_viewModel._oParamGetHeader);
                        await _conConductorHeader!.Add();
                    }
                    else if (_viewModel._oParamHeaderPopUp.CSAVEMODE == "EDIT")
                    {
                        labelHO_LOIRefNo = "_LabelHORefNo";
                        await _conConductorHeader!.Edit();

                    }
                }
                await _griLOIDetailRefUnit!.R_RefreshGrid(_viewModel._oParamGetHeader);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Header
        private async Task R_ServiceGetRecordUtility(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT02000LOIHandoverUtilityDTO)eventArgs.Data;
                _viewModel._ChargeType = loParam.CCHARGES_TYPE!;

                bool isIBlockEnabled = _viewModel._ChargeType == "01" || _viewModel._ChargeType == "02";
                _viewModel.lEnableIBlock = isIBlockEnabled;
                bool isMeterEnabled = _viewModel._ChargeType == "03" || _viewModel._ChargeType == "04";
                _viewModel.lEnableMeter = isMeterEnabled;

                _viewModel.GetEntityUtility(loParam);
                await Task.Delay(1);
                eventArgs.Result = _viewModel._EntityUtility;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceSaveAllData(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ConvertDataHeaderToBack(_viewModel.Data);

                _viewModel.ValidationHeader(_viewModel.Data);
                PMT02000LOIHeader_DetailDTO loData = _viewModel.Data;
                await _viewModel.ServiceSave(loData, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel._EntityHeaderDetail;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                await Close(true, "SUCCESS");
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);

        }
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT02000LOIHeader_DetailDTO currentData = (PMT02000LOIHeader_DetailDTO)eventArgs.Data;

            try
            {
                if (_viewModel._oParamHeaderPopUp.CSAVEMODE == "EDIT")
                {
                    //  var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(_viewModel._oParamHeaderPopUp);
                    await _viewModel.GetEntityHeaderDetail(_viewModel._oParamGetHeader);
                    // await _griLOIDetailUtilityRef!.R_RefreshGrid(null);
                }
                else if ((_viewModel._oParamHeaderPopUp.CSAVEMODE == "NEW"))
                {
                    await _viewModel.GetEntityHeaderDetail(currentData);

                }
                eventArgs.Result = _viewModel._EntityHeaderDetail;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ValidationHeader(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_conGridUtility!.R_ConductorMode != R_eConductorMode.Normal)
                {

                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationModeUtility");
                    loEx.Add(loErr);
                }
                else if (_conGridUnit!.R_ConductorMode != R_eConductorMode.Normal)
                {

                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT02000_Class), "ValidationModeUnit");
                    loEx.Add(loErr);
                }
                else
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(eventArgs.Data);
                    _viewModel.ValidationHeader(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region Unit
        private void R_ServiceLOIUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.GetLOIUnitList(_viewModel._EntityHeaderDetail);
                eventArgs.ListEntityResult = _viewModel._listDetailUnit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void R_ServiceGetRecordUnit(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT02000LOIHandOverUnitDTO)eventArgs.Data;
                _viewModel.GetEntityUnit(loParam);
                eventArgs.Result = _viewModel._EntityUnit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_DisplayUnit(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT02000LOIHandOverUnitDTO loData = (PMT02000LOIHandOverUnitDTO)eventArgs.Data;
                await _griLOIDetailUtilityRef!.R_RefreshGrid(loData);
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void ValidationUnit(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHandOverUnitDTO>(eventArgs.Data);
                _viewModel.ValidationUnit(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Utility List       
        private void R_ServiceUtilityList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMT02000LOIHandOverUnitDTO loTemp = (PMT02000LOIHandOverUnitDTO)(eventArgs.Parameter);
                PMT02000DBParameter loParam = R_FrontUtility.ConvertObjectToObject<PMT02000DBParameter>(loTemp);

                _viewModel.GetLOIUtilityList(loParam);
                eventArgs.ListEntityResult = _viewModel._listDetailUtilityFiltered;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_DisplayUtility(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT02000LOIHandoverUtilityDTO loData = (PMT02000LOIHandoverUtilityDTO)eventArgs.Data;
                var loDataList = _viewModel._listDetailUtilityFiltered.ToList();

                AddOrUpdateUtility(newUtility: loData);
            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void ValidationUtility(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHandoverUtilityDTO>(eventArgs.Data);
                _viewModel.ValidationUtility(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void AddOrUpdateUtility(PMT02000LOIHandoverUtilityDTO newUtility)
        {
            var loEx = new R_Exception();

            List<PMT02000LOIHandoverUtilityDTO> loReturn = new();
            try
            {
                // Cari data yang memiliki CHarge Type sama dan Unit Id sama
                var existingUtility = _viewModel._listDetailUtilityUnfiltered.FirstOrDefault(data => data.CUNIT_ID == newUtility.CUNIT_ID && data.CCHARGES_TYPE == newUtility.CCHARGES_TYPE && data.CCHARGES_ID == newUtility.CCHARGES_ID);

                if (existingUtility != null)
                {
                    // Jika ditemukan, update data lainnya
                    existingUtility.IYEAR = newUtility.IYEAR;
                    existingUtility.CYEAR = newUtility.IYEAR.ToString();
                    existingUtility.CMONTH = newUtility.CMONTH;
                    existingUtility.NMETER_START = newUtility.NMETER_START;
                    existingUtility.NBLOCK1_START = newUtility.NBLOCK1_START;
                    existingUtility.NBLOCK2_START = newUtility.NBLOCK2_START;
                }
                else
                {
                    // Jika tidak ditemukan, tambahkan data baru
                    _viewModel._listDetailUtilityUnfiltered.Add(newUtility);
                }
                // loReturn =  Listutilities;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            // return  loReturn;
        }
        #endregion

        #region Validation
        #endregion
        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {

            eventArgs.Data = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(_viewModel._EntityHeaderDetail);
            var loTemp = (PMT02000LOIHeader_DetailDTO)eventArgs.Data;

            //negasi [_lDataCREF_NO = 1] == true else negasi
            _lDataCREF_NO = !_viewModel.oVarGSMTransactionCode.LINCREMENT_FLAG;
            var x = (_lAdd_Mode && _lDataCREF_NO);
            if (_lDataCREF_NO)
            {
                await FocusLabelRefNo!.FocusAsync();
            }
            await _griLOIDetailUtilityRef!.R_RefreshGrid(null);


        }
        private void R_SetEdit(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _lDataCREF_NO = false;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private void R_SetAdd(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _lAdd_Mode = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }
        private async Task OnCancel()
        {
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Cancel_Confirmation"], R_eMessageBoxButtonType.YesNo);
            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await this.Close(true, false);
            }
        }
     
    }
}
