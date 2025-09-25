using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMR00400Common.DTOs;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace PMR00400Front;

public partial class PMR00400 : R_Page
{
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_IReport _reportService { get; set; }
    private async Task OnClickPrint()
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = new PMR00400ParamDTO();
            loParam.CCOMPANY_ID = _clientHelper.CompanyId;
            loParam.CUSER_ID = _clientHelper.UserId;
            loParam.CLANG_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
            
            //user parameters
            loParam.CPROPERTY_ID = "ashmd";
            loParam.CTYPE = "U";
            loParam.CFROM_BUILDING_ID = "TW-A";
            loParam.CTO_BUILDING_ID = "TW-A";
            loParam.CFROM_UNIT_TYPE_ID = "";
            loParam.CTO_UNIT_TYPE_ID = "";
            

            await _reportService.GetReport(
                "R_DefaultServiceUrlPM",
                "PM",
                "rpt/PMR00400Print/ActivityReportPost",
                "rpt/PMR00400Print/ActivityReportGet",
                loParam
            );
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}