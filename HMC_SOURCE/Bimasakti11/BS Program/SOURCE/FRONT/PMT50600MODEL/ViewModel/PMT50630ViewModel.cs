using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600COMMON.DTOs.PMT50630;
using PMT50600COMMON.DTOs.PMT50630;
using PMT50600FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600MODEL.ViewModel
{
    public class PMT50630ViewModel : R_ViewModel<PMT50630DTO>
    {
        private PMT50630Model loModel = new PMT50630Model();

        private PMT50611Model loCreditNoteItemModel = new PMT50611Model();

        private PMT50600Model loCreditNoteListModel = new PMT50600Model();

        public PMT50630DTO loSummary = new PMT50630DTO();

        public PMT50630ResultDTO loRtn = null;

        public PMT50611HeaderDTO loHeader = new PMT50611HeaderDTO();

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
                loResult = await loCreditNoteListModel.GetCompanyInfoAsync();
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
            PMT50611HeaderParameterDTO loParam = null;
            PMT50611HeaderResultDTO loHeaderRtn = null;
            PMT50611HeaderDTO loTempResult = null;
            try
            {
                loParam = new PMT50611HeaderParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loHeaderRtn = await loCreditNoteItemModel.GetHeaderInfoAsync(loParam);
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
            PMT50611HeaderParameterDTO loParam = null;
            PMT50611HeaderResultDTO loHeaderRtn = null;
            try
            {
                loParam = new PMT50611HeaderParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loHeaderRtn = await loCreditNoteItemModel.GetHeaderInfoAsync(loParam);
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

        public void SummaryValidation(PMT50630DTO poParam)
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

                llCancel = (poParam.NSUMMARY_DISCOUNT > (poParam.NTOTAL_AMOUNT - poParam.NDISCOUNT));
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V035"));
                }

                llCancel = (poParam.NTOTAL_AMOUNT < 0);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V040"));
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveSummaryAsync(PMT50630DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            PMT50630ParameterDTO loParam = null;
            PMT50630ParameterDTO loResult = null;
            try
            {
                loParam = new PMT50630ParameterDTO()
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
