using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT01700COMMON.DTO.Utilities.Print;
using PMT01700FrontResources;
using PMT01700MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700FRONT
{
    public partial class PMT01700Print : R_Page
    {
        readonly PMT01700LOO_OfferListViewModel _viewModel = new();
        [Inject] private R_ILocalizer<Resources_PMT01700_Class>? _localizer { get; set; }
        [Inject] private R_IReport? _reportService { get; set; }
        [Inject] private IClientHelper? ClientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {

                _viewModel.ParameterPrint = R_FrontUtility.ConvertObjectToObject<ParameterPrintDTO>(poParameter);

                ParamaterGetReportTemplateListDTO poParam = new ParamaterGetReportTemplateListDTO
                {
                    CCOMPANY_ID = _viewModel.ParameterPrint.CCOMPANY_ID,
                    CPROPERTY_ID = _viewModel.ParameterPrint.CPROPERTY_ID,
                    CPROGRAM_ID = "PMT01700",
                    CTEMPLATE_ID = ""
                };
                await _viewModel.GetReportTemplateList(poParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnClickOk()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await PrintProcess();
                if (!loEx.HasError)
                {
                    await Close(true, "SUCCESS");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickCancel()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Close(false, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PrintProcess()
        {
            var loEx = new R_Exception();
            ParameterPrintDTO loParameterPrint = new ParameterPrintDTO();

            try
            {

                loParameterPrint = _viewModel.ParameterPrint;
                loParameterPrint.CTITLE = "Penawaran Harga Sewa Lokasi Event";
                loParameterPrint.CPROGRAM_ID = "PMT01700";
                loParameterPrint.CTEMPLATE_ID = _viewModel.SelectedReportTemplate;
                loParameterPrint.CFILE_NAME = _viewModel.ListReportTemplate
                    .FirstOrDefault(x => x.CTEMPLATE_ID == _viewModel.SelectedReportTemplate)
                    .CFILE_NAME;

                await _reportService!.GetReport(
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    pcModuleName: "PM",
                    pcPostRequestUrl: "rpt/PMT01700Report/ReportPMT01700Post",
                    pcGetRequestUrl: "rpt/PMT01700Report/ReportPMT01700Get",
                    poParameter: loParameterPrint);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
