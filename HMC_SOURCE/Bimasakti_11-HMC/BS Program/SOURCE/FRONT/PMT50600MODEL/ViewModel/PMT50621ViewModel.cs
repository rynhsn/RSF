using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600COMMON.DTOs.PMT50621;
using PMT50600FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600MODEL.ViewModel
{
    public class PMT50621ViewModel : R_ViewModel<PMT50621DTO>
    {
        private PMT50621Model loModel = new PMT50621Model();

        public PMT50621DTO loCreditNoteItem = new PMT50621DTO();

        public List<GetProductTypeDTO> loProductTypeList = null;

        public GetProductTypeResultDTO loProductTypeRtn = null;

        public GetProductTypeDTO loProductType = null;

        public PMT50621ResultDTO loRtn = null;

        public PMT50611HeaderDTO loHeader = new PMT50611HeaderDTO();

        public GetCompanyInfoDTO loCompanyInfo = null;

        public TabItemEntryParameterDTO loTabParam = null;

        public async Task GetProductTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loProductTypeRtn = await loModel.GetProductTypeListStreamAsync();
                loProductTypeList = loProductTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            PMT50621RefreshParameterDTO loParam = null;
            try
            {
                loParam = new PMT50621RefreshParameterDTO()
                {
                    CREC_ID = loCreditNoteItem.CREC_ID
                };
                loRtn = await loModel.RefreshInvoiceItemAsync(loParam);
                loCreditNoteItem = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void InvoiceItemValidation(PMT50621DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CPROD_DEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V020"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPROD_TYPE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V021"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V022"));
                }

                //llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID) && poParam.CPROD_TYPE == "E";
                //if (llCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V023"));
                //}

                //llCancel = string.IsNullOrWhiteSpace(poParam.CALLOC_ID) && poParam.CPROD_TYPE == "P";
                //if (llCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V024"));
                //}

                llCancel = poParam.NBILL_UNIT_QTY <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V025"));
                }

                llCancel = poParam.IBILL_UNIT == 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V026"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CBILL_UNIT) && poParam.IBILL_UNIT == 4;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V027"));
                }

                llCancel = poParam.NSUPP_CONV_FACTOR <= 0 && poParam.IBILL_UNIT == 4;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V028"));
                }

                llCancel = poParam.NUNIT_PRICE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V029"));
                }

                llCancel = poParam.CDISC_TYPE == "P" && (poParam.NDISC_PCT < 0 || poParam.NDISC_PCT > 100);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V030"));
                }

                llCancel = poParam.CDISC_TYPE == "V" && poParam.NDISC_AMOUNT < 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V031"));
                }

                llCancel = poParam.NDISC_AMOUNT > poParam.NAMOUNT;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V032"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveInvoiceItemAsync(PMT50621DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            PMT50621ParameterDTO loParam = null;
            PMT50621ParameterDTO loResult = null;
            try
            {
                loParam = new PMT50621ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = loHeader.CPROPERTY_ID,
                    Header = loHeader
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loCreditNoteItem = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteInvoiceItemAsync(PMT50621DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            PMT50621ParameterDTO loParam = null;
            try
            {
                loParam = new PMT50621ParameterDTO()
                {
                    Data = poEntity,
                };
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
