using PMT02000COMMON.LOI_List;
using PMT02000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Helpers;
using PMT02000COMMON.Utility;
using R_BlazorFrontEnd.Controls.MessageBox;
using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using System.Xml.Linq;

namespace PMT02000FRONT
{
    public partial class PMT02000HO : R_Page, R_ITabPage
    {
        private PMT02000ViewModel _viewModel = new();
        private R_Grid<PMT02000LOIDTO>? _gridLOIref;
        private R_ConductorGrid? _conGridLOI;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.PropertyValueID = (string)poParameter;
                await _gridLOIref!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region HOList
        private async Task R_ServiceHOListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAllList("HO");
                eventArgs.ListEntityResult = _viewModel.LOIList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loTemp = (PMT02000LOIDTO)eventArgs.Data;

                    _viewModel._CurrentLOI = loTemp;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion CHANGE TAB
        //ini buat Dapet fungsi dari Page 1
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<string>(poParam);
                _viewModel.PropertyValueID = loParam;
                await _gridLOIref!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }


            R_DisplayException(loException);

        }

        #region Button
        private void Btn_Edit(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.TargetPageType = typeof(PopUpHandOver);
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader>(_viewModel._CurrentLOI);
                loParam.CSAVEMODE = "EDIT";
                eventArgs.Parameter = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_DeleteAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Delete_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loParamDelete = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(_viewModel._CurrentLOI);
                    await _viewModel.ServiceDelete(loParamDelete);
                    await R_MessageBox.Show("", _localizer["Success_Delete"], R_eMessageBoxButtonType.OK);
                }
                else
                {
                    return;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_SubmitAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Submit_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SubmitRedraft("Submit");
                    await R_MessageBox.Show("", _localizer["Success_Submit"], R_eMessageBoxButtonType.OK);
                }
                else
                {
                    return;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_RedraftAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Redraft_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SubmitRedraft("Redraft");
                    await R_MessageBox.Show("", _localizer["Success_Redraft"], R_eMessageBoxButtonType.OK);
                }
                else
                {
                    return;

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
