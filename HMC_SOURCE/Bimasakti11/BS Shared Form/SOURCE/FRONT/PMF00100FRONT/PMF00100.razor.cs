using PMF00100COMMON.DTOs.PMF00100;
using PMF00100COMMON.DTOs.PMF00110;
using PMF00100Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMF00100Model.Constant;
using R_BlazorFrontEnd.Helpers;

namespace PMF00100FRONT
{
    public partial class PMF00100 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMF00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMF00100ViewModel loAllocationViewModel = new PMF00100ViewModel();

        private R_ConductorGrid _conductorAllocationRef;

        private R_Grid<PMF00100ListDTO> _gridAllocationRef;

        private bool IsAllocationListExist = true;

        private bool _pagePropertyOnCRUDmode = false;

        private R_TabStrip _TabStripRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loAllocationViewModel.loAllocationParameter = (OpenAllocationParameterDTO)poParameter;
                if (string.IsNullOrWhiteSpace(loAllocationViewModel.loAllocationParameter.CREC_ID) && string.IsNullOrWhiteSpace(loAllocationViewModel.loAllocationParameter.CREF_NO))
                {
                    return;
                }
                if (loAllocationViewModel.loAllocationParameter.CTRANS_CODE != TransCodeConstant.VAR_RECEIVE_FROM_CUSTOMER)
                {
                    await loAllocationViewModel.GetHeaderAsync();
                }
                else
                {
                    if (loAllocationViewModel.loAllocationParameter.CPAYMENT_TYPE == "CA" || loAllocationViewModel.loAllocationParameter.CPAYMENT_TYPE == "WT")
                    {
                        await loAllocationViewModel.GetCAWTCustReceiptAsync();
                    }
                    else
                    {
                        await loAllocationViewModel.GetCQCustReceiptAsync();
                    }
                }
                await loAllocationViewModel.InitialProcess();
                await _gridAllocationRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pagePropertyOnCRUDmode = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            //_pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private async Task OnClickRefresh()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //loAllocationViewModel.RefreshAllocationListValidation();
                loAllocationViewModel.loAllocation = new PMF00100ListDTO();
                await _gridAllocationRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "AllocationList")
            {
                await _gridAllocationRef.R_RefreshGrid(null);
            }
        }

        private async Task Grid_Allocation_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loAllocationViewModel.loAllocation = new PMF00100ListDTO();
                await loAllocationViewModel.GetAllocationListStreamAsync();
                eventArgs.ListEntityResult = loAllocationViewModel.loAllocationList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Allocation_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loAllocationViewModel.loAllocation = (PMF00100ListDTO)eventArgs.Data;
            }
        }

        private void R_Before_OpenAllocationEntry_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMF00100ListDTO loData = R_FrontUtility.ConvertObjectToObject<PMF00100ListDTO>(_conductorAllocationRef.R_GetCurrentData());
                eventArgs.Parameter = new OpenAllocationEntryParameterDTO()
                {
                    CREC_ID = loAllocationViewModel.loAllocationParameter.CREC_ID,
                    CALLOCATION_ID = loData.CALLOC_ID,
                    CDEPT_CODE = loAllocationViewModel.loAllocationParameter.CDEPT_CODE,
                    CPROPERTY_ID = loAllocationViewModel.loAllocationParameter.CPROPERTY_ID,
                    CREF_NO = loAllocationViewModel.loAllocationParameter.CREF_NO,
                    CTRANS_CODE = loAllocationViewModel.loAllocationParameter.CTRANS_CODE,
                    CPAYMENT_TYPE = loAllocationViewModel.loAllocationParameter.CPAYMENT_TYPE,
                    LDISPLAY_ONLY = loAllocationViewModel.loAllocationParameter.LDISPLAY_ONLY
                };
                eventArgs.TargetPageType = typeof(PMF00110);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}