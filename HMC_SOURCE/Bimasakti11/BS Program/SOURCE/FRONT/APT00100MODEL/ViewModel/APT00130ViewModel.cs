using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs.APT00111;
using APT00100COMMON.DTOs.APT00130;
using APT00100COMMON.DTOs.APT00130;
using APT00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APT00100MODEL.ViewModel
{
    public class APT00130ViewModel : R_ViewModel<APT00130DTO>
    {
        private APT00130Model loModel = new APT00130Model();

        private APT00111Model loInvoiceItemModel = new APT00111Model();

        private APT00100Model loInvoiceListModel = new APT00100Model();

        public APT00130DTO loSummary = new APT00130DTO();

        public APT00130ResultDTO loRtn = null;

        public APT00111HeaderDTO loHeader = new APT00111HeaderDTO();

        public GetCompanyInfoDTO loCompanyInfo = null;

        public SummaryParameterDTO loTabParam = null;

        public decimal lnAdditionOrDeductionResult;

        public async Task GetSummaryInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetSummaryParameterDTO loParam = null;
            try
            {
                loParam = new GetSummaryParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loRtn = await loModel.GetSummaryInfoAsync(loParam);
                loSummary = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCompanyInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCompanyInfoResultDTO loResult = null;
            try
            {
                loResult = await loInvoiceListModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAdditionOrDeductionValue(string pcType)
        {
            R_Exception loEx = new R_Exception();
            APT00111HeaderParameterDTO loParam = null;
            APT00111HeaderResultDTO loHeaderRtn = null;
            APT00111HeaderDTO loTempResult = null;
            try
            {
                loParam = new APT00111HeaderParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loHeaderRtn = await loInvoiceItemModel.GetHeaderInfoAsync(loParam);
                loTempResult = loHeaderRtn.Data;
                if (pcType == "A")
                {
                    lnAdditionOrDeductionResult = loTempResult.NADDITION;
                }
                else if (pcType == "D")
                {
                    lnAdditionOrDeductionResult = loTempResult.NDEDUCTION;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetHeaderInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            APT00111HeaderParameterDTO loParam = null;
            APT00111HeaderResultDTO loHeaderRtn = null;
            try
            {
                loParam = new APT00111HeaderParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loHeaderRtn = await loInvoiceItemModel.GetHeaderInfoAsync(loParam);
                loHeader = loHeaderRtn.Data;
                loHeader.CLOCAL_CURRENCY_CODE = loCompanyInfo.CLOCAL_CURRENCY_CODE;
                try
                {
                    loHeader.DREF_DATE = DateTime.ParseExact(loHeader.CREF_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void SummaryValidation(APT00130DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = (poParam.LSUMMARY_DISCOUNT == true) && (poParam.CSUMMARY_DISC_TYPE == "P") && ((poParam.NSUMMARY_DISC_PCT < 0) || (poParam.NSUMMARY_DISC_PCT > 100));
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V033"));
                }

                llCancel = (poParam.LSUMMARY_DISCOUNT == true) && (poParam.CSUMMARY_DISC_TYPE == "V") && (poParam.NSUMMARY_DISCOUNT < 0);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V034"));
                }

                llCancel = (poParam.CSUMMARY_DISC_TYPE == "V") && (poParam.NSUMMARY_DISCOUNT > (poParam.NTOTAL_AMOUNT - poParam.NDISCOUNT));
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V035"));
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveSummaryAsync(APT00130DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00130ParameterDTO loParam = null;
            APT00130ParameterDTO loResult = null;
            try
            {
                loParam = new APT00130ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = loHeader.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loSummary = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
