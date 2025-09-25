using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using APT00100MODEL.ViewModel;
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
using APT00100COMMON.DTOs.APT00121;
using R_BlazorFrontEnd.Enums;
using APT00100COMMON.DTOs.APT00120;
using R_BlazorFrontEnd.Interfaces;
using Microsoft.AspNetCore.Components;

namespace APT00100FRONT
{
    public partial class APT00120 : R_Page
    {
        [Inject] private R_ILocalizer<APT00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private APT00111ViewModel loInvoiceItemViewModel = new APT00111ViewModel();

        private APT00120ViewModel loViewModel = new APT00120ViewModel();

        private R_ConductorGrid _conductorInvoiceItemRef;

        private R_Grid<APT00111ListDTO> _gridInvoiceItemRef;

        private bool IsInvoiceItemListExist = true;

        private bool IsSupplierEnabled = false;

        private bool IsClosing = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            InvoiceItemTabParameterDTO loParam = null;
            try
            {
                loParam = (InvoiceItemTabParameterDTO)poParameter;
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    await loInvoiceItemViewModel.GetCompanyInfoAsync();
                    loInvoiceItemViewModel.lcRecIdParameter = loParam.CREC_ID;
                    await loInvoiceItemViewModel.GetHeaderInfoAsync();
                    await _gridInvoiceItemRef.R_RefreshGrid(null);
                    if (loInvoiceItemViewModel.loInvoiceItemList.Count > 0)
                    {
                        loInvoiceItemViewModel.loInvoiceItem = loInvoiceItemViewModel.loInvoiceItemList.FirstOrDefault();
                        await loInvoiceItemViewModel.GetDetailInfoAsync();
                    }
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
                if (!IsClosing)
                {
                    eventArgs.Cancel = true;
                }
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
                await _gridInvoiceItemRef.R_RefreshGrid(null);
            }
        }

        private async Task OnClose()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (loInvoiceItemViewModel.loInvoiceItemList.Count() > 0)
                {
                    await loViewModel.CloseFormProcessAsync(new OnCloseProcessParameterDTO()
                    {
                        CREC_ID = loInvoiceItemViewModel.loHeader.CREC_ID,
                        CPROPERTY_ID = loInvoiceItemViewModel.loHeader.CPROPERTY_ID
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

        private async Task Grid_InvoiceItem_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loInvoiceItemViewModel.GetInvoiceItemListStreamAsync();
                eventArgs.ListEntityResult = loInvoiceItemViewModel.loInvoiceItemList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_InvoiceItem_R_Display(R_DisplayEventArgs eventArgs)
        {
            loInvoiceItemViewModel.loInvoiceItem = (APT00111ListDTO)eventArgs.Data;
            await loInvoiceItemViewModel.GetDetailInfoAsync();
        }

        private void R_Before_OpenItemEntry_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                eventArgs.Parameter = new TabItemEntryParameterDTO()
                {
                    CREC_ID = loInvoiceItemViewModel.loInvoiceItem.CREC_ID,
                    Data = loInvoiceItemViewModel.loHeader
                };
                eventArgs.TargetPageType = typeof(APT00121);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
