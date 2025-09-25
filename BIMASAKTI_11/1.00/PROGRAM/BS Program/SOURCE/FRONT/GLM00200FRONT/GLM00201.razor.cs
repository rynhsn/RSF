using BlazorClientHelper;
using GLM00200COMMON;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GLM00200FRONT
{
    public partial class GLM00201 : R_Page //Print Popup
    {
        private JournalDTO journal = new();
        private R_Button PrintBtn;
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                journal = (JournalDTO)poParameter;
                await PrintBtn.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {

                // set Param
                journal.CCOMPANY_ID = clientHelper.CompanyId;
                journal.CUSER_ID = clientHelper.UserId;

                await _reportService.GetReport(
                    "R_DefaultServiceUrlGL",
                    "GL",
                    "rpt/GLM00200Print/AllGLRecurringJournalPost",
                    "rpt/GLM00200Print/AllStreamGLRecurringJournalGet",
                    journal);

                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
