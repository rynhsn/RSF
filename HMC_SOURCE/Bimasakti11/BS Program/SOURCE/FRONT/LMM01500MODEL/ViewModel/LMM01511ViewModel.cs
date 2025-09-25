using LMM01500COMMON;
using LMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01511ViewModel : R_ViewModel<LMM01511DTO>
    {
        private LMM01510Model _LMM01510Model = new LMM01510Model();

        public LMM01511DTO TemplateBankAccount = new LMM01511DTO(); 
        public async Task GetTemplateBankAccount(LMM01511DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _LMM01510Model.R_ServiceGetRecordAsync(poParam);

                TemplateBankAccount = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationTemplateBankAccount(LMM01511DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

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
        public async Task SaveTemplateBankAccount(LMM01511DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01510Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                TemplateBankAccount = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTemplateBankAccount(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01510Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
