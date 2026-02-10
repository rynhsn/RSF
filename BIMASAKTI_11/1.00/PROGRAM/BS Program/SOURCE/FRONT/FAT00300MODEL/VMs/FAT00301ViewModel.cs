using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_ContextFrontEnd;
using R_CommonFrontBackAPI;
using FAT00300Common;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using FAT00300FrontResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Enums;


namespace FAT00300Model.VMs
{
    public class FAT00301ViewModel : R_ViewModel<FAT00300DTO>
    {
        public FAT00300Model modelFAT00300 = new FAT00300Model();
        public FAT00300DTO loTransEntry = new FAT00300DTO();
        public FAT00300DTO paramTranEntry = new FAT00300DTO();
        public FAT00300DTO selectedTransEntry = new FAT00300DTO();
        public FAT00300DTO savingTransEntry = new FAT00300DTO();
        public FAT00300GetInitialProcessResultDTO InitialProcess { get; set; } = new FAT00300GetInitialProcessResultDTO();

        public FAT00300GetValidateTransDateResultDTO ValidateTransDate { get; set; } = new FAT00300GetValidateTransDateResultDTO();
        public FAT00300GetValidateOutstandTransResultDTO ValidateOutStandTrans { get; set; } = new FAT00300GetValidateOutstandTransResultDTO();
        public FAT00300GetValidationDataResultDTO ValidateData { get; set; } = new FAT00300GetValidationDataResultDTO();
        public FAT00300GetAssetResultDTO loAssetData { get; set; } = new FAT00300GetAssetResultDTO();

        public DateTime? refDateValue = DateTime.Now;
        public string cRefDate { get; set; } = "";
        public string cAssetCode { get; set; } = "";
        public string cTransCode { get; set; } = "210020";
        public decimal nDeprAmount { get; set; } = 0;
        public decimal NLTRANS_AMOUNT { get; set; } = 0;
        public decimal NBTRANS_AMOUNT { get; set; } = 0;
        public bool LENABLE_ASSET { get; set; } = true;
        public string CDEPR_AMOUN_CURRENCY_CODE = "";

        // State Component
        public bool llEnablePrint = true;
        public bool llEnableJournal = true;
        public bool llEnableDelete = true;
        public bool llEnableEdit = true;
        public bool llEnableAdd = true;
        public bool llDeletedRecord = true;
        public bool llInRangeStatus = false;


        public Resources_Dummy_Class a = new Resources_Dummy_Class();
        public async Task GetRecordTransEntry()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = await modelFAT00300.R_ServiceGetRecordAsync(paramTranEntry);
                loTransEntry = loTemp;
                CDEPR_AMOUN_CURRENCY_CODE = loTransEntry.CCURRENCY_CODE;
                loTransEntry.DREF_DATE = R_FrontUtility.R_ConvertToDateTime(loTransEntry.CREF_DATE, "yyyyMMdd");

                TransStatusInRange(loTransEntry.CTRANS_STATUS);
               

                cAssetCode = loTransEntry.CASSET_CODE;
                cRefDate = loTransEntry.CREF_DATE;
                refDateValue = R_FrontUtility.R_ConvertToDateTime(cRefDate, "yyyyMMdd");

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInitialProcessAsync(FAT00300GetInitialProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await modelFAT00300.GetInitialProcess(poParameter);
                InitialProcess = loResult.Data ?? new FAT00300GetInitialProcessResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Validation
        public async Task ValidateRefDate()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (cRefDate[..4].CompareTo(InitialProcess.CCURRENT_PERIOD) < 0)
                {

                    cRefDate = R_FrontUtility.R_ConvertToDateTimeString(refDateValue, "yyyyMMdd");
                }
                //if (InitialProcess.LCUST_PERIOD_FLAG == false)
                //{
                //    cRefDate = R_FrontUtility.R_ConvertToDateTimeString(refDateValue, "yyyyMMdd");
                //}
                else
                {
                    var loParam = new FAT00300GetValidateTransDateParameterDTO();
                    loParam.CTRANSACTION_DATE = cRefDate = R_FrontUtility.R_ConvertToDateTimeString(refDateValue, "yyyyMMdd");

                    await GetValidateTransDateAsync(loParam);
                    cRefDate = ValidateTransDate.CPRD;
                }
                //cRefDate is less then CPRD
                if (cRefDate.CompareTo(ValidateTransDate.CPRD) < 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS009"));
                }

                if (cRefDate.CompareTo(cRefDate) > 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS010"));
                }
                // [..4] untuk mengambil 4 karakter string date alias tahun
                if (cRefDate[..4] != InitialProcess.CCURRENT_PERIOD[..4])
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS011"));
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationTransEntry(object poObject)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTempData = (FAT00300DTO)poObject;

                if (string.IsNullOrEmpty(loTempData.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS034"));
                }

                if (string.IsNullOrEmpty(loTempData.CREF_DATE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS036"));
                }

                if (loTempData.CREF_DATE.CompareTo(paramTranEntry.CSOFT_PERIOD) < 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS037"));
                }

                if (string.IsNullOrEmpty(loTempData.CASSET_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS038"));
                }

                if (loTempData.NTRANS_AMOUNT == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS039"));
                }

                if (string.IsNullOrEmpty(loTempData.CTRANS_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS040"));
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetValidateOutstandTrans(FAT00300GetValidateOutstandTransParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = await modelFAT00300.GetValidateOutstandTrans(poParam);
                ValidateOutStandTrans = loTemp.Data ?? new FAT00300GetValidateOutstandTransResultDTO();

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetValidationData(FAT00300GetValidationDataParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = await modelFAT00300.GetValidationData(poParam);
                ValidateData = loTemp.Data ?? new FAT00300GetValidationDataResultDTO();

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetValidateTransDateAsync(FAT00300GetValidateTransDateParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await modelFAT00300.GetValidateTransDate(poParameter);
                ValidateTransDate = loResult.Data ?? new FAT00300GetValidateTransDateResultDTO();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationSaving()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(savingTransEntry.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS013"));
                }
                if (string.IsNullOrEmpty(savingTransEntry.CREF_DATE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS014"));
                }
                if (string.IsNullOrEmpty(savingTransEntry.CASSET_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS015"));
                }
                if (nDeprAmount <= 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS016"));
                }
                if (string.IsNullOrEmpty(savingTransEntry.CTRANS_DESC))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS033"));
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Get Info Asset Data
        public async Task GetAssetData(FAT00300GetAssetParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await modelFAT00300.GetAsset(poParameter);
                loAssetData = loResult.Data ?? new FAT00300GetAssetResultDTO();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Helper
        public void RefreshBaseCurrency()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //nBaseCurrency = Math.Round(nDeprAmount * loAssetData.NLAST_BCURRENCY_RATE_AMOUNT / loAssetData.NLAST_BBASE_RATE_AMOUNT, 2);
                Data.NLTRANS_AMOUNT = (Data.NTRANS_AMOUNT / Data.NLBASE_RATE) * Data.NLCURRENCY_RATE;
                Data.NBTRANS_AMOUNT = (Data.NTRANS_AMOUNT / Data.NBBASE_RATE) * Data.NBCURRENCY_RATE;

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Saving & Delete
        public async Task SaveTransactionEntry(eCRUDMode poCrudMode)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (poCrudMode == eCRUDMode.AddMode)
                {
                    savingTransEntry.CMODE = "NEW";
                }
                else
                {
                    savingTransEntry.CMODE = "EDIT";
                }
                var loTemp = await modelFAT00300.R_ServiceSaveAsync(savingTransEntry, poCrudMode);
                loTransEntry = loTemp;

                // Refresh RefDate, Local, and Base Rate
                loTransEntry.DREF_DATE = R_FrontUtility.R_ConvertToDateTime(loTransEntry.CREF_DATE, "yyyyMMdd");
                RefreshBaseCurrency();
                TransStatusInRange(loTransEntry.CTRANS_STATUS);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void SavingTransactionEntry(FAT00300DTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //poEntity.CREF_DATE = cRefDate;
                //poEntity.CASSET_CODE = cAssetCode;
                //poEntity.NTRANS_AMOUNT = nDeprAmount;
                //poEntity.CASSET_NAME = loAssetData.CASSET_NAME;
                //poEntity.CTRANS_CODE = cTransCode;
                //poEntity.NLBASE_RATE = loAssetData.NLBASE_RATE;
                //poEntity.NLCURRENCY_RATE = loAssetData.NLCURRENCY_RATE;
                poEntity.CCURRENCY_CODE = CDEPR_AMOUN_CURRENCY_CODE;
                poEntity.LINCREMENT_FLAG = paramTranEntry.LINCREMENT_FLAG;
                //ViewModelFAT00301.savingTransEntry.CLOCAL_CURRENCY_CODE = ViewModelFAT00301.InitialProcess.CLOCAL_CURRENCY_CODE;
                //ViewModelFAT00301.savingTransEntry.LINCREMENT_FLAG = ViewModelFAT00301.InitialProcess.LINCREMENT_FLAG;
                savingTransEntry = poEntity;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteTransactionEntry(FAT00300DTO poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //poParameter.CTRANS_CODE = cTransCode;
                await modelFAT00300.DeleteTransaction(poParameter);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Submit Redraft
        public async Task SubmitTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                var loParam = new FAT00300SubmitProcessParameterDTO();
                loParam.CREC_ID = loTransEntry.CREC_ID;

                await modelFAT00300.SubmitProcess(loParam);

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RedraftTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                var loParam = new FAT00300SubmitProcessParameterDTO();
                loParam.CREC_ID = loTransEntry.CREC_ID;
                loParam.CNEW_STATUS = "00";

                await modelFAT00300.SubmitProcess(loParam);

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Helper
        public void TransStatusInRange(string pcStatus)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(pcStatus) || pcStatus.Length != 2)
                {
                    llInRangeStatus = false;
                }
                else if (pcStatus == "99" || pcStatus == "98")
                {
                    llInRangeStatus = false;
                    //int value = (pcStatus[0] - '0') * 10 + (pcStatus[1] - '0');

                    //llInRangeStatus = value <= 80;
                }
                else
                {
                    llInRangeStatus = true;
                }


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
