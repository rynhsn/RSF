using FAF00100COMMON.DTOs;
using FAF00100FrontResources;
using FAF00100Model.VMs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAF00100FRONT
{
    public partial class FAF00100 : R_Page
    {
        private R_TabStrip _tabStripRef;
        private R_Conductor _conAssetInformation;
        private R_Grid<FAF00100GetAssetAllocResultDTO> gridAllocationExpense;
        public FAF00100ViewModel ViewModelFAF00100 = new FAF00100ViewModel();
        private FAF00100GetAssetResultDTO glParamGetAssetInformation = new FAF00100GetAssetResultDTO();
        private FAF00100GetAssetAllocParameterDTO glParamGetListExpenseAlloc = new FAF00100GetAssetAllocParameterDTO();
        [Inject] R_ILocalizer<Resources_Dummy_Class> Localizer { get; set; } = default!;

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                if (poParameter != null)
                {
                    // Assign CASSET_CODE 
                    glParamGetAssetInformation.CASSET_CODE = (string)poParameter;
                    glParamGetListExpenseAlloc.CASSET_CODE = glParamGetAssetInformation.CASSET_CODE;

                    ViewModelFAF00100.paramGetAssetExpense = glParamGetAssetInformation;
                    ViewModelFAF00100.paramListAllocExpense = glParamGetListExpenseAlloc;
                    await _conAssetInformation.R_GetEntity(new FAF00100GetAssetResultDTO());
                }

            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        public async Task GetAssetAllocation(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await ViewModelFAF00100.GetAssetAllocationInfo();
                eventArgs.Result = ViewModelFAF00100.loAssetInformation;

                await gridAllocationExpense.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetListExpenseAllocation(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await ViewModelFAF00100.GetListAssetAlloc();
                eventArgs.ListEntityResult = ViewModelFAF00100.loListAssetAlloc;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
