using APT00200COMMON.DTOs.APT00200;
using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using APT00200COMMON.DTOs.APT00221;
using APT00200FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace APT00200MODEL.ViewModel
{
    public class APT00221ViewModel : R_ViewModel<APT00221DTO>
    {
        private APT00221Model loModel = new APT00221Model();

        public APT00221DTO loPurchaseReturnItem = new APT00221DTO();

        public List<GetProductTypeDTO> loProductTypeList = null;

        public GetProductTypeResultDTO loProductTypeRtn = null;

        public GetProductTypeDTO loProductType = null;

        public APT00221ResultDTO loRtn = null;

        public APT00211HeaderDTO loHeader = null;

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
            APT00221RefreshParameterDTO loParam = null;
            try
            {
                loParam = new APT00221RefreshParameterDTO()
                {
                    CREC_ID = loTabParam.CREC_ID
                };
                loRtn = await loModel.RefreshPurchaseReturnItemAsync(loParam);
                loPurchaseReturnItem = loRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void PurchaseReturnItemValidation(APT00221DTO poParam)
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

                llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID) && poParam.CPROD_TYPE == "P";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V022"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CPRODUCT_ID) && poParam.CPROD_TYPE == "E";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V023"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CALLOC_ID) && poParam.CPROD_TYPE == "P";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V024"));
                }

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

                llCancel = poParam.CDISC_TYPE == "V" && poParam.NDISC_PCT < 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V031"));
                }

                llCancel = poParam.CDISC_TYPE == "V" && poParam.NDISC_PCT > poParam.NAMOUNT;
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

        public async Task SavePurchaseReturnItemAsync(APT00221DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00221ParameterDTO loParam = null;
            APT00221ParameterDTO loResult = null;
            try
            {
                loParam = new APT00221ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = loHeader.CPROPERTY_ID,
                    Header = loHeader
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loPurchaseReturnItem = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeletePurchaseReturnItemAsync(APT00221DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00221ParameterDTO loParam = null;
            try
            {
                loParam = new APT00221ParameterDTO()
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
