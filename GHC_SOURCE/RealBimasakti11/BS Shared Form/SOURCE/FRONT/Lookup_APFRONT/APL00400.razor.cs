// using Lookup_APCOMMON.DTOs.APL00400;
// using Lookup_APModel.ViewModel.APL00400;
// using R_BlazorFrontEnd.Controls.DataControls;
// using R_BlazorFrontEnd.Controls.Events;
// using R_BlazorFrontEnd.Controls;
// using R_BlazorFrontEnd.Exceptions;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using R_BlazorFrontEnd.Controls.MessageBox;
//
// namespace Lookup_APFRONT
// {
//     public partial class APL00400 : R_Page
//     {
//         private LookupAPL00400ViewModel _viewModel = new LookupAPL00400ViewModel();
//         private R_Grid<APL00400DTO> GridRef;
//         private R_Conductor _conductorRef;
//
//         protected override async Task R_Init_From_Master(object poParameter)
//         {
//             var loEx = new R_Exception();
//
//             try
//             {
//
//
//                 _viewModel.ParameterLookup = (APL00400ParameterDTO)poParameter;
//
//               await  GridRef.R_RefreshGrid(null);
//
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             loEx.ThrowExceptionIfErrors();
//             await Task.CompletedTask;
//         }
//
//         public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
//         {
//             var loEx = new R_Exception();
//
//             try
//             {
//
//                 await _viewModel.GetProductAllocationList();
//
//
//                 eventArgs.ListEntityResult = _viewModel.ProductAllocationGrid;
//
//
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             R_DisplayException(loEx);
//         }
//
//         public async Task Button_OnClickOkAsync()
//         {
//             if (_viewModel.ProductAllocationGrid.Count == 0)
//             {
//                 await R_MessageBox.Show("Error", "Data not found!", R_eMessageBoxButtonType.OK);
//                 return;
//             }
//             else
//             {
//                 var loData = GridRef.GetCurrentData();
//                 await this.Close(true, loData);
//             }
//         }
//         public async Task Button_OnClickCloseAsync()
//         {
//             await this.Close(true, null);
//         }
//         public async Task Refresh_Button()
//         {
//             var loEx = new R_Exception();
//
//             try
//             {
//              await _viewModel.GetProductAllocationList();
//              await  GridRef.R_RefreshGrid(null);
//                 
//                 if (_viewModel.ProductAllocationGrid.Count == 0)
//                 {
//                     await R_MessageBox.Show("Error", "Data not found!", R_eMessageBoxButtonType.OK);
//                     return;
//                 }
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             loEx.ThrowExceptionIfErrors();
//         }
//     }
// }
