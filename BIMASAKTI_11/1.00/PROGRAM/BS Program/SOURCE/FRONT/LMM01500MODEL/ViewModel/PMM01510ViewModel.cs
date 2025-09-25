using PMM01500COMMON;
using PMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01510ViewModel : R_ViewModel<PMM01511DTO>
    {
        private PMM01500Model _PMM01500Model = new PMM01500Model();
        private PMM01510Model _PMM01510Model = new PMM01510Model();
        public ObservableCollection<PMM01511DTO> TemplateBankAccountGrid { get; set; } = new ObservableCollection<PMM01511DTO>();
        public LinkedList<PMM01500DTOInvTemplate> InvoiceTemplateList { get; set; } = new LinkedList<PMM01500DTOInvTemplate>();


        public string PropertyValueContext = "";
        public string InvGrpCode { get; set; } = "";
        public string InvGrpName { get; set; } = "";
        public bool StatusChange;

        public async Task GetListTemplateBankAccount()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01510Model.PMM01510TemplateAndBankAccountListAsync(PropertyValueContext, InvGrpCode);
                var loConvert = R_FrontUtility.ConvertCollectionToCollection<PMM01511DTO>(loResult);

                TemplateBankAccountGrid = new ObservableCollection<PMM01511DTO>(loConvert);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public PMM01511DTO TemplateBankAccount = new PMM01511DTO();
        public async Task GetTemplateBankAccount(PMM01511DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(poParam.CINVGRP_CODE))
                {
                    var loResult = await _PMM01510Model.R_ServiceGetRecordAsync(poParam);

                    TemplateBankAccount = loResult;
                }
                else
                {
                    TemplateBankAccount = poParam;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationTemplateBankAccount(PMM01511DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CBUILDING_ID);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1522"));
                }

                // lCancel = string.IsNullOrEmpty(poParam.FileName);
                lCancel = string.IsNullOrEmpty(poParam.CINVOICE_TEMPLATE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1506"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CDEPT_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1507"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CBANK_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1508"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CBANK_ACCOUNT);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1509"));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveTemplateBankAccount(PMM01511DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01510Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                TemplateBankAccount = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTemplateBankAccount(PMM01511DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMM01510Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetInvoiceTemplate(string pcPropertyId)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01500Model.GetInvoiceTemplateAsync(pcPropertyId);
                InvoiceTemplateList = new LinkedList<PMM01500DTOInvTemplate>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
