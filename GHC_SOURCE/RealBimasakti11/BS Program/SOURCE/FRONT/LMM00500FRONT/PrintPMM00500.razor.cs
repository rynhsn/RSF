using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorClientHelper;
using PMM00500Common.DTOs;
using PMM00500Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace PMM00500Front
{
    public partial class PrintPMM00500 : R_Page
    {
        private PMM00510ViewModel _viewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<PMM00500GridDTO> _gridRef;

        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetUnitChargesType();
                await getInit();
                _viewModel.Charge = (PMM00510DTO)poParameter;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task getInit()
        {
            var firstType = _viewModel.loUnitChargesTypeList.FirstOrDefault();
            _viewModel.Data.CCHARGES_TYPE_FROM = firstType.CCODE;
            _viewModel.Data.CCHARGES_TYPE_TO = firstType.CCODE;
            var shortBy = _viewModel.radioPrint.FirstOrDefault();
            _viewModel.Data.CSHORT_BY = shortBy.CCODE;
        }
        private async Task GetPrint()
        {
            var loEx = new R_Exception();
            var loParam = new PrintParamDTO();
            try
            {
                loParam = new PrintParamDTO()
                {
                    CPROPERTY_ID = _viewModel.Charge.CPROPERTY_ID,
                    CPROPERTY_NAME = _viewModel.Charge.CPROPERTY_NAME,
                    CCHARGES_TYPE_FROM = _viewModel.Data.CCHARGES_TYPE_FROM,
                    CCHARGES_TYPE_TO = _viewModel.Data.CCHARGES_TYPE_TO,
                    CSHORT_BY = _viewModel.Data.CSHORT_BY,
                    LPRINT_INACTIVE = _viewModel.Data.LPRINT_INACTIVE,
                    LPRINT_DETAIL_ACC = _viewModel.Data.LPRINT_DETAIL_ACC,
                    CUSER_LOGIN_ID = _clientHelper.UserId,
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPRINT_EXT_TYPE = _viewModel._printType
                };

                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMM00500Report/UnitChargesReportPost",
                    "rpt/PMM00500Report/UnitChargesReportGet",
                    loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
