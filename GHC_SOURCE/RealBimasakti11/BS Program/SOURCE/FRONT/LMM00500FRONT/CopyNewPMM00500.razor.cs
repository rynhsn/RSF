using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using PMM00500Common.DTOs;
using PMM00500Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMM00500Front
{
    public partial class CopyNewPMM00500 : R_Page
    {
        private PMM00510ViewModel _viewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<PMM00500GridDTO> _gridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Charge = (PMM00510DTO)poParameter;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task OnOk()
        {
            R_Exception loException = new R_Exception();
            try
            {

                PMM00510DTO param = new PMM00510DTO()
                {
                    CPROPERTY_ID = _viewModel.Charge.CPROPERTY_ID,
                    CCURRENT_CHARGES_TYPE = _viewModel.Charge.CCHARGE_TYPE_ID,
                    CCURRENT_CHARGE_ID = _viewModel.Charge.CCHARGES_ID,
                    CNEW_CHARGES_ID = _viewModel.Charge.CNEW_CHARGES_ID,
                    CNEW_CHARGES_NAME = _viewModel.Charge.CNEW_CHARGES_NAME,

                };
                await _viewModel.CopyNewProcessAsync(param);
                await this.Close(true, null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        private async Task OnClose()
        {
            await this.Close(true, null);
        }
    }
}

