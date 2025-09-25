using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using APT00100MODEL.ViewModel;
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

namespace APT00100FRONT
{
    public partial class APT00111 : R_Page
    {
        [Inject] private R_ILocalizer<APT00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private APT00111ViewModel loInvoiceItemViewModel = new APT00111ViewModel();

        private R_ConductorGrid _conductorInvoiceItemRef;

        private R_Grid<APT00111ListDTO> _gridInvoiceItemRef;

        private bool IsInvoiceItemListExist = true;

        private bool IsSupplierEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            InvoiceItemTabParameterDTO loParam = null;
            try
            {
                loParam = (InvoiceItemTabParameterDTO)poParameter;
                await loInvoiceItemViewModel.GetCompanyInfoAsync();
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
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
    }
}
