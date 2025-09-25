// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using BlazorClientHelper;
// using Lookup_APCOMMON.DTOs.APL00100;
// using Lookup_APCOMMON.DTOs.APL00300;
// using Lookup_APCOMMON.DTOs.APL00300;
// using Lookup_APModel.ViewModel.APL00300;
// using Lookup_GSCOMMON.DTOs;
// using Lookup_GSFRONT;
// using Microsoft.AspNetCore.Components;
// using R_BlazorFrontEnd.Controls;
// using R_BlazorFrontEnd.Controls.DataControls;
// using R_BlazorFrontEnd.Controls.Events;
// using R_BlazorFrontEnd.Controls.MessageBox;
// using R_BlazorFrontEnd.Exceptions;
//
// namespace Lookup_APFRONT
// {
//     public partial class APL00300 : R_Page
//     {
//         private LookupAPL00300ViewModel _viewModel = new LookupAPL00300ViewModel();
//         private R_Grid<APL00300DTO> GridRef;
//         private R_Conductor _conductorRef;
//         private R_ConductorGrid _conGrid;
//         [Inject] private IClientHelper ClientHelper { get; set; }
//         protected override async Task R_Init_From_Master(object poParameter)
//         {
//             var loEx = new R_Exception();
//
//             try
//             {
//
//
//                 _viewModel.ParameterLookup = (APL00300ParameterDTO)poParameter;
//                 await _viewModel.GetProductLookupList();
//                 // GridRef.R_RefreshGrid(null);
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
//                 await _viewModel.GetProductLookupList();
//
//
//                 eventArgs.ListEntityResult = _viewModel.ProductLookupGrid;
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
//         private Task R_BeforeOpenLookUp(R_BeforeOpenLookupEventArgs eventArgs)
//         {
//             var loEx = new R_Exception();
//
//             try
//             {
//                 eventArgs.Parameter = new GSL01800DTOParameter()
//                 {
//                     CCOMPANY_ID = ClientHelper.CompanyId,
//                     CUSER_ID = ClientHelper.UserId,
//                     CPROPERTY_ID = _viewModel.ParameterLookup.CPROPERTY_ID,
//                     CCATEGORY_TYPE = "40"
//                 };
//                 eventArgs.TargetPageType = typeof(GSL01800);
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             loEx.ThrowExceptionIfErrors();
//             return Task.CompletedTask;
//         }
//
//         private void R_AfterOpenLookUp(R_AfterOpenLookupEventArgs eventArgs)
//         {
//             var loEx = new R_Exception();
//
//             try
//             {
//                 var loData = (GSL01800DTO)eventArgs.Result;
//                 if (loData == null)
//                     return;
//
//                 _viewModel.ProductLookupEntity.CCATEGORY_ID = loData.CCATEGORY_ID;
//                 _viewModel.ProductLookupEntity.CCATEGORY_NAME = loData.CCATEGORY_NAME;
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             loEx.ThrowExceptionIfErrors();
//             //return Task.CompletedTask;
//         }
//
//         public async Task Refresh_Button()
//         {
//             var loEx = new R_Exception();
//
//             try
//             {
//                 // await _viewModel.GetProductLookup();
//                 await GridRef.R_RefreshGrid(null);
//                 if (_viewModel.ProductLookupGrid.Count == 0)
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
//
//
//         private async Task OnChanged(object poParam)
//         {
//             var loEx = new R_Exception();
//             try
//             {
//                 if (_viewModel.ProductLookupEntity.RadioButton == "A")
//                 {
//                     _viewModel.ProductLookupEntity.CCATEGORY_ID = "";
//                     _viewModel.ProductLookupEntity.CCATEGORY_NAME = "";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 loEx.Add(ex);
//             }
//
//             R_DisplayException(loEx);
//         }
//
//
//
//         public async Task Button_OnClickOkAsync()
//         {
//             if (_viewModel.ProductLookupEntity.RadioButton == "S" && _viewModel.ProductLookupEntity.CCATEGORY_NAME == "")
//             {
//                 await R_MessageBox.Show("Error", "Please select Category!", R_eMessageBoxButtonType.OK);
//                 return;
//             }
//
//             if (_viewModel.ProductLookupEntity.RadioButton == "S" && _viewModel.ProductLookupEntity.CCATEGORY_NAME == null)
//             {
//                 await R_MessageBox.Show("Error", "Please select Category!", R_eMessageBoxButtonType.OK);
//                 return;
//             }
//
//             if (_viewModel.ProductLookupEntity.RadioButton == null && _viewModel.ProductLookupEntity.CCATEGORY_NAME == null)
//             {
//                 await R_MessageBox.Show("Error", "Please select Category!", R_eMessageBoxButtonType.OK);
//                 return;
//             }
//             
//             if (_viewModel.ProductLookupGrid.Count == 0)
//             {
//                 await R_MessageBox.Show("Error", "Data not found!", R_eMessageBoxButtonType.OK);
//                 return;
//             }
//       
//
//             var loData = GridRef.GetCurrentData();
//             await this.Close(true, loData);
//         }
//         public async Task Button_OnClickCloseAsync()
//         {
//             await this.Close(true, null);
//         }
//     }
// }
