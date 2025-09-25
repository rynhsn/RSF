using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd;
using PMT50600COMMON.DTOs.PMT50621;
using R_BlazorFrontEnd.Enums;
using PMT50600COMMON.DTOs.PMT50620;
using R_BlazorFrontEnd.Interfaces;
using Microsoft.AspNetCore.Components;

namespace PMT50600FRONT
{
    public partial class PMT50620 : R_Page
    {
        [Inject] private R_ILocalizer<PMT50600FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT50611ViewModel loCreditNoteItemViewModel = new PMT50611ViewModel();

        private PMT50620ViewModel loViewModel = new PMT50620ViewModel();

        private R_ConductorGrid _conductorCreditNoteItemRef;

        private R_Grid<PMT50611ListDTO> _gridCreditNoteItemRef;

        private R_TabStrip tabStripRef;

        private bool IsCreditNoteItemListExist = true;

        private bool IsCustomerEnabled = false;

        private bool IsClosing = false;

        private bool IsNew = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            InvoiceItemTabParameterDTO loParam = null;
            try
            {
                loParam = (InvoiceItemTabParameterDTO)poParameter;
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    await loCreditNoteItemViewModel.GetCompanyInfoAsync();
                    loCreditNoteItemViewModel.lcRecIdParameter = loParam.CREC_ID;
                    await loCreditNoteItemViewModel.GetHeaderInfoAsync();
                    await _gridCreditNoteItemRef.R_RefreshGrid(null);
                    if (loCreditNoteItemViewModel.loCreditNoteItemList.Count > 0)
                    {
                        loCreditNoteItemViewModel.loCreditNoteItem = loCreditNoteItemViewModel.loCreditNoteItemList.FirstOrDefault();
                        await loCreditNoteItemViewModel.GetDetailInfoAsync();
                    }
                }

                if (loParam.LIS_NEW)
                {
                    IsNew = loParam.LIS_NEW;
                    await tabStripRef.SetActiveTabAsync("ItemEntry");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        protected override ValueTask Dispose(bool disposing)
        {
            return base.Dispose(disposing);
        }

        protected override async Task R_PageClosing(R_PageClosingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //if (!IsClosing)
                //{
                //    eventArgs.Cancel = true;
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "ItemList")
            {
                await _gridCreditNoteItemRef.R_RefreshGrid(null);
            }
        }

        private async Task OnClose()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (loCreditNoteItemViewModel.loCreditNoteItemList.Count() > 0 && loCreditNoteItemViewModel.loHeader.CTRANS_STATUS == "00")
                {
                    await loViewModel.CloseFormProcessAsync(new OnCloseProcessParameterDTO()
                    {
                        CREC_ID = loCreditNoteItemViewModel.loHeader.CREC_ID,
                        CPROPERTY_ID = loCreditNoteItemViewModel.loHeader.CPROPERTY_ID
                    });
                }
                IsClosing = true;
                await this.Close(true, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
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

        private void R_Before_OpenItemEntry_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                eventArgs.Parameter = new TabItemEntryParameterDTO()
                {
                    CREC_ID = loCreditNoteItemViewModel.loCreditNoteItem.CREC_ID,
                    LIS_NEW = IsNew,
                    Data = loCreditNoteItemViewModel.loHeader
                };
                eventArgs.TargetPageType = typeof(PMT50621);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
