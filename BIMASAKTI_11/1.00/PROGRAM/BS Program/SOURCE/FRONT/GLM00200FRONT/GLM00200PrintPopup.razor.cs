using BlazorClientHelper;
using GLM00200COMMON;
using GLM00200FrontResources;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace GLM00200FRONT
{
    public partial class GLM00200PrintPopup : R_Page
    {
        private JournalDTO journal = new JournalDTO();
        private R_Button PrintBtn;
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

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
                    "R_DefaultServiceUrl",
                    "GS",
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
