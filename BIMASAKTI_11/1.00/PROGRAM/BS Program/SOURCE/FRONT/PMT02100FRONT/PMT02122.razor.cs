using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON.DTOs.PMT02120Print;
using PMT02100MODEL.FrontDTOs.PMT02100;
using R_BlazorFrontEnd.Helpers;

namespace PMT02100FRONT
{
    public partial class PMT02122 : R_Page
    {
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        //private PMT02100ViewModel loCenterViewModel = new PMT02100ViewModel();

        private PMT02120PrintReportParameterDTO loPrintParameter = new PMT02120PrintReportParameterDTO();

        private PrintCheckboxDTO loCheckbox = new PrintCheckboxDTO();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loPrintParameter = (PMT02120PrintReportParameterDTO)poParameter;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task OnClose()
        {
            await this.Close(true, false);
        }

        private async Task OnClickPrintButton()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (!loCheckbox.LASSIGNMENT && !loCheckbox.LCHECKLIST)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V018"));
                }

                if (loEx.HasError)
                {
                    goto EndBlock;
                }

                loPrintParameter.LASSIGNMENT = loCheckbox.LASSIGNMENT;
                loPrintParameter.LCHECKLIST = loCheckbox.LCHECKLIST;
                loPrintParameter.CCOMPANY_ID = _clientHelper.CompanyId;
                loPrintParameter.CLANG_ID = _clientHelper.Culture.TwoLetterISOLanguageName;


                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMT02120Print/PrintReportPost",
                    "rpt/PMT02120Print/PrintReportGet",
                    loPrintParameter);

                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}