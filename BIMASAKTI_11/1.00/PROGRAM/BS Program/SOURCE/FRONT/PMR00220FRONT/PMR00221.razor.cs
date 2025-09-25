using Microsoft.AspNetCore.Components;
using PMR00220FrontResources;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR00220FRONT
{
    public partial class PMR00221 : R_Page
    {
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        private string _reportType, _reportName = "";

        private List<string> _listFileType = new List<string> { "XLSX", "XLS", "CSV" };

        protected override Task R_Init_From_Master(object poParameter)
        {
            return base.R_Init_From_Master(poParameter);
        }

        private async Task OnClickOk()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await this.Close(true, _reportName + "," + _reportType);
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
    }
}
