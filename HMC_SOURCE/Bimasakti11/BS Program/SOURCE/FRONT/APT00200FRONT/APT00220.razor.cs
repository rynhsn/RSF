using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using APT00200MODEL.ViewModel;
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
using APT00200COMMON.DTOs.APT00221;
using R_BlazorFrontEnd.Enums;
using APT00200COMMON.DTOs.APT00220;
using R_BlazorFrontEnd.Interfaces;
using Microsoft.AspNetCore.Components;

namespace APT00200FRONT
{
    public partial class APT00220 : R_Page
    {
        [Inject] private R_ILocalizer<APT00200FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private APT00211ViewModel loPurchaseReturnItemViewModel = new APT00211ViewModel();

        private APT00220ViewModel loViewModel = new APT00220ViewModel();

        private R_ConductorGrid _conductorPurchaseReturnItemRef;

        private R_Grid<APT00211ListDTO> _gridPurchaseReturnItemRef;

        private bool IsPurchaseReturnItemListExist = true;

        private bool IsSupplierEnabled = false;

        private bool IsClosing = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            PurchaseReturnItemTabParameterDTO loParam = null;
            try
            {
                loParam = (PurchaseReturnItemTabParameterDTO)poParameter;
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    await loPurchaseReturnItemViewModel.GetCompanyInfoAsync();
                    loPurchaseReturnItemViewModel.lcRecIdParameter = loParam.CREC_ID;
                    await loPurchaseReturnItemViewModel.GetHeaderInfoAsync();
                    await _gridPurchaseReturnItemRef.R_RefreshGrid(null);
                    if (loPurchaseReturnItemViewModel.loPurchaseReturnItemList.Count > 0)
                    {
                        loPurchaseReturnItemViewModel.loPurchaseReturnItem = loPurchaseReturnItemViewModel.loPurchaseReturnItemList.FirstOrDefault();
                        await loPurchaseReturnItemViewModel.GetDetailInfoAsync();
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
            if (!IsClosing)
            {
                eventArgs.Cancel = true;
            }
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "ItemList")
            {
                await _gridPurchaseReturnItemRef.R_RefreshGrid(null);
            }
        }

        private async Task OnClose()
        {
            if (loPurchaseReturnItemViewModel.loPurchaseReturnItemList.Count() > 0)
            {
                await loViewModel.CloseFormProcessAsync(new OnCloseProcessParameterDTO()
                {
                    CREC_ID = loPurchaseReturnItemViewModel.loHeader.CREC_ID,
                    CPROPERTY_ID = loPurchaseReturnItemViewModel.loHeader.CPROPERTY_ID
                });
            }
            IsClosing = true;   
            await this.Close(true, false);
        }

        private async Task Grid_PurchaseReturnItem_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loPurchaseReturnItemViewModel.GetPurchaseReturnItemListStreamAsync();
                eventArgs.ListEntityResult = loPurchaseReturnItemViewModel.loPurchaseReturnItemList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_PurchaseReturnItem_R_Display(R_DisplayEventArgs eventArgs)
        {
            loPurchaseReturnItemViewModel.loPurchaseReturnItem = (APT00211ListDTO)eventArgs.Data;
            await loPurchaseReturnItemViewModel.GetDetailInfoAsync();
        }

        private void R_Before_OpenItemEntry_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                eventArgs.Parameter = new TabItemEntryParameterDTO()
                {
                    CREC_ID = loPurchaseReturnItemViewModel.loPurchaseReturnItem.CREC_ID,
                    Data = loPurchaseReturnItemViewModel.loHeader
                };
                eventArgs.TargetPageType = typeof(APT00221);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
