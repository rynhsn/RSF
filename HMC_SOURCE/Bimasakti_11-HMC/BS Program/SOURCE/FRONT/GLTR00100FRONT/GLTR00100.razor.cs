using BlazorClientHelper;
using GLTR00100COMMON;
using GLTR00100MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;

namespace GLTR00100FRONT
{
    public partial class GLTR00100 : R_Page
    {
        private GLTR00100ViewModel _viewModel = new GLTR00100ViewModel();
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private R_Conductor JournalTransaction;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLTR00100DTO>(poParameter);
                await JournalTransaction.R_GetEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task JournalTransaction_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetJournalTransaction((GLTR00100DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.DataJournal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loTempData = (GLTR00100DTO)JournalTransaction.R_GetCurrentData();
                var loData = R_FrontUtility.ConvertObjectToObject<GLTR00100PrintParamDTO>(loTempData);

                //Set Data
                loData.CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName;
                loData.CCOMPANY_ID = clientHelper.CompanyId;
                loData.CUSER_ID = clientHelper.UserId;

                await _reportService.GetReport(
                "R_DefaultServiceUrlGL",
                "GL",
                "rpt/GLTR00100Print/AllJournalTransactionPost",
                "rpt/GLTR00100Print/AllStreamJournalTransactionsGet",
                loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
