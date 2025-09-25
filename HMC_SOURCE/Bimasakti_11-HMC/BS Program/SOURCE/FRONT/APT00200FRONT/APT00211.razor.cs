using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using APT00200MODEL.ViewModel;
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

namespace APT00200FRONT
{
    public partial class APT00211 : R_Page
    {
        [Inject] private R_ILocalizer<APT00200FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private APT00211ViewModel loPurchaseReturnItemViewModel = new APT00211ViewModel();

        private R_ConductorGrid _conductorPurchaseReturnItemRef;

        private R_Grid<APT00211ListDTO> _gridPurchaseReturnItemRef;

        private bool IsPurchaseReturnItemListExist = true;

        private bool IsSupplierEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            PurchaseReturnItemTabParameterDTO loParam = null;
            try
            {
                loParam = (PurchaseReturnItemTabParameterDTO)poParameter;
                await loPurchaseReturnItemViewModel.GetCompanyInfoAsync();
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
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
    }
}
