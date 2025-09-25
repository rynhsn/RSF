using BlazorClientHelper;
using GLM00200COMMON;
using GLM00200MODEL;
using GLM00200FrontResources;
using GLTR00100COMMON;
using GLTR00100FRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;
using System.Xml.Linq;

namespace GLM00200FRONT
{
    public partial class GLM00220 : R_Page //actual journal list
    {
        private GLM00203ViewModel _journalVM = new();
        private R_Grid<JournalDetailActualGridDTO> _gridJournalDet;
        private R_Conductor _conJournalNavigator;
        private R_ConductorGrid _conJournalDetail;
        [Inject] IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<JournalDTO>(poParameter);

                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    loParam.CJRN_ID = loParam.CREC_ID;
                    await _conJournalNavigator.R_GetEntity(loParam);
                }

                await _journalVM.GetInitData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #region Header Data
        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)eventArgs.Data;
                await _journalVM.GetlJournal(loParam);
                eventArgs.Result = _journalVM.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task JournalForm_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loHeaderData = (JournalDTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (eventArgs.Data != null)
                    {
                        if (DateTime.TryParseExact(loHeaderData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                        {
                            _journalVM.RefDate = ldRefDate;
                        }
                        else
                        {
                            _journalVM.RefDate = null;
                        }
                        if (DateTime.TryParseExact(loHeaderData.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                        {
                            _journalVM.DocDate = ldDocDate;
                        }
                        else
                        {
                            _journalVM.RefDate = null;
                        }

                        if (DateTime.TryParseExact(loHeaderData.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                        {
                            _journalVM.StartDate = ldStartDate;
                        }
                        else
                        {
                            _journalVM.StartDate = null;
                        }

                        if (DateTime.TryParseExact(loHeaderData.CNEXT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldNextDate))
                        {
                            _journalVM.NextDate = ldNextDate;
                        }
                        else
                        {
                            _journalVM.NextDate = null;
                        }

                        if (DateTime.TryParseExact(loHeaderData.CLAST_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldLastDate))
                        {
                            _journalVM.LastDate = ldLastDate;
                        }
                        else
                        {
                            _journalVM.LastDate = null;
                        }

                        if (!string.IsNullOrWhiteSpace(loHeaderData.CREF_NO) || !string.IsNullOrWhiteSpace(loHeaderData.CDEPT_CODE))
                        {
                            await _gridJournalDet.R_RefreshGrid(loHeaderData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Detail
        private async Task JournalDetGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<RecurringJournalListParamDTO>(eventArgs.Parameter);
                await _journalVM.ShowAllJournalDetail(loParam);
                eventArgs.ListEntityResult = _journalVM.JournaDetailActualGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void JournalDetGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        #endregion

        #region Print
        private void BtnJournalActual_PrintJournal(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (JournalDetailActualGridDTO)_conJournalDetail.R_GetCurrentData();
                var param = new GLTR00100DTO()
                {
                    CREC_ID = loData.CREC_ID
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GLTR00100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BtnJournalActual_PrintList(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)_conJournalNavigator.R_GetCurrentData();
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GLM00201);
                eventArgs.PageTitle = _localizer["_pageTitlePrintPopup"];

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
