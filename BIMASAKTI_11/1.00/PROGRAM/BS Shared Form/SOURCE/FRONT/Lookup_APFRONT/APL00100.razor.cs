using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APModel.ViewModel.APL00100;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APFRONT
{
    public partial class APL00100 :R_Page
    {
        private LookupAPL00100ViewModel _viewModel = new LookupAPL00100ViewModel();
        private R_Grid<APL00100DTO> GridRef;
        private R_Conductor _conductorRef;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {


                _viewModel.ParameterLookup = (APL00100ParameterDTO)poParameter;

                GridRef.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        //private async Task Supplier_ServiceGetListRecord(object poParameter)
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        var loParam = (APL00100ParameterDTO)poParameter;
        //        await _viewModel.GetSupplierList(loParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    R_DisplayException(loEx);
        //}
        public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
        
                await _viewModel.GetSupplierList();


                eventArgs.ListEntityResult = _viewModel.SupplierGrid;


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task ShowAll_Button()
        {
            var loEx = new R_Exception();
 
            try
            {
                
                _viewModel.SearchText = "";
                await GridRef.R_RefreshGrid(null);
                if (_viewModel.SupplierGrid.Count == 0)
                {
                    await R_MessageBox.Show("Error", "Data not found!", R_eMessageBoxButtonType.OK);
                    return;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Search_Button()
        {
            var loEx = new R_Exception();
            var Condition1 = _viewModel.SearchText == "" && string.IsNullOrWhiteSpace(_viewModel.SearchText);
            var Condition2 =  _viewModel.SearchText.Length < 3;
            try
            {
                ////    if (_viewModel.SearchText == "" && string.IsNullOrWhiteSpace(_viewModel.SearchText))
                ////    {
                ////       await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);

                ////    }
                ////    else
                ////    {


                //    }

                if (Condition1)
                {
                    await R_MessageBox.Show("Error", "You Must Fill Empty Field", R_eMessageBoxButtonType.OK);
                    return;
                }
                if (Condition2)
                {
                    await R_MessageBox.Show("Error", "Minimum search keyword is 3 characters!", R_eMessageBoxButtonType.OK);
                    return;
                }
                else
                {
                    await GridRef.R_RefreshGrid(null);
                }
                if (_viewModel.SupplierGrid.Count == 0)
                {
                    await R_MessageBox.Show("Error", "Data not found!", R_eMessageBoxButtonType.OK);
                    return;
                }
                //await GridRef.R_RefreshGrid(null);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                
            }
           
            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickOkAsync()
        {
            var loData = GridRef.GetCurrentData();
            await this.Close(true, loData);
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
