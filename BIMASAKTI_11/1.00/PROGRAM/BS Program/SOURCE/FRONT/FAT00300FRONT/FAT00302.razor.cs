using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Interfaces;
using FAT00300FrontResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAT00300Model.VMs;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using R_BlazorFrontEnd.Controls.DataControls;

namespace FAT00300Front
{
    public partial class FAT00302 : R_Page
    {
        private R_TabStrip _tabStripRef;
        private R_Conductor _conAssetInformation;
        private R_Grid<FAT00300GetAllocationExpenseListResultDTO> gridAllocationExpense;
        private FAT00302ViewModel ViewModelFAT00302 = new FAT00302ViewModel();
        private FAT00300GetAssetInformationTABParameterDTO glParamGetAssetInformation = new FAT00300GetAssetInformationTABParameterDTO();
        private FAT00300GetAllocationExpenseListParameterDTO glParamGetListExpenseAlloc = new FAT00300GetAllocationExpenseListParameterDTO();
        [Inject] R_ILocalizer<Resources_Dummy_Class> Localizer { get; set; } = default!;

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                if (poParameter != null)
                {
                    // Assign CASSET_CODE 
                    glParamGetAssetInformation = (FAT00300GetAssetInformationTABParameterDTO)poParameter;
                    glParamGetListExpenseAlloc.CASSET_CODE = glParamGetAssetInformation.CASSET_CODE;

                    ViewModelFAT00302.paramGetAssetExpense = glParamGetAssetInformation;
                    ViewModelFAT00302.paramListAllocExpense = glParamGetListExpenseAlloc;
                    await _conAssetInformation.R_GetEntity(new FAT00300GetAssetInformationTABResultDTO());
                }

            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        public async Task GetExpenseAllocation(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await ViewModelFAT00302.GetExpenseAllocationRecordAsync();
                eventArgs.Result = ViewModelFAT00302.loAllocExpense;

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
                await ViewModelFAT00302.GetListAllocExpenseAsync();
                eventArgs.ListEntityResult = ViewModelFAT00302.listAllocExpense;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
