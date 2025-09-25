using BlazorClientHelper;
using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Utility;
using PMT02000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02000FRONT
{
    public partial class PopUpHandOver
    {
        private PMT02000ViewModel _viewModel = new();
        private R_ConductorGrid? _conGrid;

        private R_Grid<PMT02000LOIDetailListDTO>? _griLOIDetailRef;
        //   private R_Grid<PMT02000LOIHeader_DetailDTO>? _gridHeaderRef;
        private R_Conductor? _conConductor;
        private R_TextBox? FocusLabelRefNo;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParamHeader = (PMT02000LOIHeader)poParameter;

                //TESSS
                _viewModel._LOIHeader = loParamHeader;


                var loDataconvert = R_FrontUtility.ConvertObjectToObject<PMT02000DBParameter>(loParamHeader);
                var loConverttt = _viewModel.ConvertDTO(loParamHeader);
                //_viewModel._LOIHeader
                _viewModel.GetMonth();
                //  var loDataconvert2 = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(loParamHeader);

                if (loParamHeader.CSAVEMODE == "NEW")
                {
                    await _viewModel.GetLOIDetailList(loDataconvert);
                    await _conConductor!.R_GetEntity(loConverttt);
                    await _conConductor!.Add();
                }
                else if (loParamHeader.CSAVEMODE == "EDIT")
                {
                    await _conConductor!.Edit();
                    await _viewModel.GetLOIDetailList(loDataconvert);
                    // await _conConductor?.R_GetEntity(loConverttt)!;

                    // var loData = (PMT02000LOIHeader_DetailDTO)_conConductor.R_GetCurrentData();

                    //    _viewModel.Data.NHO_ACTUAL_SIZE = loData.NHO_ACTUAL_SIZE;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Utility List       
        private async Task R_ServiceLOIDetailListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTemp = (PMT02000LOIHeader)(eventArgs.Parameter);
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000DBParameter>(loTemp);

                await _viewModel.GetLOIDetailList(loParam);
                eventArgs.ListEntityResult = _viewModel._listDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        //APAKAH MASIH DIBUTUHKAN?????????
        private async Task ServiceSaveDetailList(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIDetailListDTO>(eventArgs.Data);
                //loParam.ListDetail = new List<PMT02000LOIDetailListDTO>(_viewModel._listDetail);
                //await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                //eventArgs.Result = _viewModel._EntityHeaderDetail;
                //   _viewModel.Validation()
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT02000LOIHeader_DetailDTO currentData = (PMT02000LOIHeader_DetailDTO)eventArgs.Data;

            try
            {
                if (_viewModel._LOIHeader.CSAVEMODE == "EDIT")
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(_viewModel._LOIHeader);
                    await _viewModel.GetEntity(loParam);
                }
                else if ((_viewModel._LOIHeader.CSAVEMODE == "NEW"))
                {
                    await _viewModel.GetEntity(currentData);

                }
                eventArgs.Result = _viewModel._EntityHeaderDetail;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ServiceDisplayHederDetail(R_DisplayEventArgs eventArgs)
        {
            //var loEx = new R_Exception();
            //PMT02000LOIHeader_DetailDTO currentData = (PMT02000LOIHeader_DetailDTO)eventArgs.Data;
            //try
            //{
            //    await _focusDate!.FocusAsync();
            //}
            //catch (Exception ex)
            //{
            //    loEx.Add(ex);
            //}

            //loEx.ThrowExceptionIfErrors();
        }

        private void R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT02000LOIDetailListDTO)eventArgs.Data;
                _viewModel.GetEntityDetail(loParam);
                eventArgs.Result = _viewModel._EntityDetail;
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(eventArgs.Data);

                //   CONVERT INT TO STRING YEAR
                foreach (var item in _viewModel._listDetail)
                {
                    item.CYEAR = item.IYEAR.ToString();
                }

                var loParamConvert = _viewModel.ConvertDataHeaderToBack(loParam);
                loParamConvert.VAR_TRANS_CODE = _viewModel.VAR_HO_TRANS_CODE;
                loParamConvert.VAR_LOI_TRANS_CODE = _viewModel.VAR_LOI_TRANS_CODE;

                loParamConvert.ListDetail = new List<PMT02000LOIDetailListDTO>(_viewModel._listDetail);

                //VALIDATION
                _viewModel.ValidationHeader(loParamConvert);
                await _viewModel.ServiceSave(loParamConvert, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel._EntityHeaderDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Validation
        private void ValidationHeader(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(eventArgs.Data);
                _viewModel.ValidationHeader(loParam);
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIDetailListDTO>(eventArgs.Data);
                _viewModel.ValidationDetail(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {

            eventArgs.Data = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(_viewModel._EntityHeaderDetail);
            var loTemp = (PMT02000LOIHeader_DetailDTO)eventArgs.Data;
            await FocusLabelRefNo!.FocusAsync();

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
