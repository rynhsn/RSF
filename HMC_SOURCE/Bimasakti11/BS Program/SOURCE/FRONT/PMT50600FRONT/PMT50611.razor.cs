using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600MODEL.ViewModel;
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

namespace PMT50600FRONT
{
    public partial class PMT50611 : R_Page
    {
        [Inject] private R_ILocalizer<PMT50600FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT50611ViewModel loCreditNoteItemViewModel = new PMT50611ViewModel();

        private R_ConductorGrid _conductorCreditNoteItemRef;

        private R_Grid<PMT50611ListDTO> _gridCreditNoteItemRef;

        private bool IsCreditNoteItemListExist = true;

        private bool IsCustomerEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            InvoiceItemTabParameterDTO loParam = null;
            try
            {
                loParam = (InvoiceItemTabParameterDTO)poParameter;
                await loCreditNoteItemViewModel.GetCompanyInfoAsync();
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    loCreditNoteItemViewModel.lcRecIdParameter = loParam.CREC_ID;
                    await loCreditNoteItemViewModel.GetHeaderInfoAsync();
                    await _gridCreditNoteItemRef.R_RefreshGrid(null);
                    if (loCreditNoteItemViewModel.loCreditNoteItemList.Count > 0)
                    {
                        loCreditNoteItemViewModel.loCreditNoteItem = loCreditNoteItemViewModel.loCreditNoteItemList.FirstOrDefault();
                        await loCreditNoteItemViewModel.GetDetailInfoAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_CreditNoteItem_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loCreditNoteItemViewModel.GetInvoiceItemListStreamAsync();
                eventArgs.ListEntityResult = loCreditNoteItemViewModel.loCreditNoteItemList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_CreditNoteItem_R_Display(R_DisplayEventArgs eventArgs)
        {
            loCreditNoteItemViewModel.loCreditNoteItem = (PMT50611ListDTO)eventArgs.Data;
            await loCreditNoteItemViewModel.GetDetailInfoAsync();
        }
    }
}
