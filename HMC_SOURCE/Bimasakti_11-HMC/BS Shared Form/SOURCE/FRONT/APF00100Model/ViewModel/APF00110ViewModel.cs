using APF00100COMMON.DTOs;
using APF00100COMMON.DTOs.APF00100;
using APF00100COMMON.DTOs.APF00110;
using APF00100FrontResources;
using APF00100Model.Constant;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace APF00100Model.ViewModel
{
    public class APF00110ViewModel : R_ViewModel<APF00110DTO>
    {
        private APF00100Model loAllocationListModel = new APF00100Model();

        private APF00110Model loModel = new APF00110Model();

        public APF00110DTO loAllocationDetail = new APF00110DTO();

        public OpenAllocationEntryParameterDTO loAllocationEntryParameter = null;

        public List<GetTransactionTypeDTO> loTransactionTypeList = new List<GetTransactionTypeDTO>();

        public APF00100HeaderDTO loHeader = new APF00100HeaderDTO();

        public GetCompanyInfoDTO loCompanyInfo = new GetCompanyInfoDTO();

        public GetGLSystemParamDTO loGLSystemParam = null;

        public GetCallerTrxInfoDTO loCallerTrxInfo = null;

        public GetPeriodDTO loSoftPeriod = null;

        public GetPeriodDTO loCurrentPeriod = null;

        public GetTransactionFlagDTO loGetTransactionFlag = null;

        public async Task InitialProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetHeaderAsync();
                await GetCompanyInfoAsync();
                await GetGLSystemParamAsync();
                await GetCallerTrxInfoAsync();
                await GetSoftPeriodAsync();
                await GetCurrentPeriodAsync();
                await GetTransactionFlagAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SubmitAllocationProcessAsync()
        {
            R_Exception loException = new R_Exception();
            SubmitAllocationParameterDTO loParam = null;
            SubmitAllocationResultDTO loRtn = null;
            try
            {
                loParam = new SubmitAllocationParameterDTO()
                {
                    CPROPERTY_ID = loAllocationEntryParameter.CPROPERTY_ID,
                    CALLOCATION_REC_ID = loAllocationDetail.CREC_ID
                };
                loRtn = await loModel.SubmitAllocationProcessAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task RedraftAllocationProcessAsync()
        {
            R_Exception loException = new R_Exception();
            RedraftAllocationParameterDTO loParam = null;
            RedraftAllocationResultDTO loRtn = null;
            try
            {
                loParam = new RedraftAllocationParameterDTO()
                {
                    CREC_ID = loAllocationDetail.CREC_ID
                };
                loRtn = await loModel.RedraftAllocationProcessAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetTransactionTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            GetTransactionTypeParameterDTO loParam = null;
            GetTransactionTypeResultDTO loRtn = null;
            List<GetTransactionTypeDTO> loTemp = null;
            try
            {
                loParam = new GetTransactionTypeParameterDTO()
                {
                    CTRANS_CODE = loAllocationEntryParameter.CTRANS_CODE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.APF00110_GET_TRANSACTION_TYPE_LIST_STREAMING_CONTEXT, loParam);
                loRtn = await loModel.GetTransactionTypeListStreamAsync();
                loTransactionTypeList = new List<GetTransactionTypeDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetHeaderAsync()
        {
            R_Exception loEx = new R_Exception();
            APF00100HeaderResultDTO loResult = null;
            try
            {
                loResult = await loAllocationListModel.GetHeaderAsync(new APF00100HeaderParameterDTO()
                {
                    CREC_ID = loAllocationEntryParameter.CREC_ID
                });
                loHeader = loResult.Data;
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
                loResult = await loAllocationListModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGLSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetGLSystemParamResultDTO loResult = null;
            try
            {
                loResult = await loAllocationListModel.GetGLSystemParamAsync();
                loGLSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCallerTrxInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCallerTrxInfoResultDTO loResult = null;
            try
            {
                loResult = await loAllocationListModel.GetCallerTrxInfoAsync(new GetCallerTrxInfoParameterDTO()
                {
                    CREC_ID = loAllocationEntryParameter.CREC_ID
                });
                loCallerTrxInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetSoftPeriodAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPeriodResultDTO loResult = null;
            try
            {
                loResult = await loAllocationListModel.GetPeriodAsync(new GetPeriodParameterDTO()
                {
                    CPERIOD_YY = loGLSystemParam.CSOFT_PERIOD_YY,
                    CPERIOD_MM = loGLSystemParam.CSOFT_PERIOD_MM
                });
                loSoftPeriod = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCurrentPeriodAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPeriodResultDTO loResult = null;
            try
            {
                loResult = await loAllocationListModel.GetPeriodAsync(new GetPeriodParameterDTO()
                {
                    CPERIOD_YY = loGLSystemParam.CCURRENT_PERIOD_YY,
                    CPERIOD_MM = loGLSystemParam.CCURRENT_PERIOD_MM
                });
                loCurrentPeriod = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransactionFlagAsync()
        {
            R_Exception loEx = new R_Exception();
            GetTransactionFlagResultDTO loResult = null;
            try
            {
                loResult = await loAllocationListModel.GetTransactionFlagAsync(new GetTransactionFlagParameterDTO()
                {
                    CTRANS_CODE = loAllocationEntryParameter.CTRANS_CODE
                });
                loGetTransactionFlag = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetAllocationAsync(APF00110DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APF00110ParameterDTO loParameter = new APF00110ParameterDTO();

            try
            {
                if (loAllocationEntryParameter.LDISPLAY_ONLY == true)
                {
                    loParameter.CDEPT_CODE = loAllocationEntryParameter.CDEPT_CODE;
                    loParameter.CTRANS_CODE = loAllocationEntryParameter.CTRANS_CODE;
                    loParameter.CVAR_TRANS_CODE = TransCodeConstant.VAR_TRANS_CODE;
                    loParameter.CREF_NO = loAllocationEntryParameter.CREF_NO;
                }
                else
                {
                    loParameter.CALLOCATION_ID = loAllocationEntryParameter.CALLOCATION_ID;
                }
                APF00110ParameterDTO loResult = await loModel.R_ServiceGetRecordAsync(loParameter);

                loAllocationDetail = loResult.Data;
                loAllocationDetail.DREF_DATE = DateTime.ParseExact(loAllocationDetail.CREF_DATE, "yyyyMMdd", null);
                loAllocationDetail.DTARGET_REF_DATE = DateTime.ParseExact(loAllocationDetail.CTARGET_REF_DATE, "yyyyMMdd", null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationAllocation(APF00110DTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CTARGET_TRANS_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V001"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CTARGET_REF_NO);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V002"));
                }

                llCancel = DateTime.ParseExact(poParam.CREF_DATE, "yyyyMMdd", null) < DateTime.ParseExact(loSoftPeriod.CSTART_DATE, "yyyyMMdd", null);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V003"));
                }

                llCancel = poParam.NTARGET_AMOUNT > poParam.NTARGET_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V004"));
                }

                llCancel = poParam.NTARGET_TAX_AMOUNT > poParam.NTARGET_TAX_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V005"));
                }

                llCancel = (poParam.NTARGET_TAX_AMOUNT > 0) && (poParam.NCALLER_TAX_AMOUNT > 0) && (poParam.NTARGET_TAX_AMOUNT != poParam.NCALLER_TAX_AMOUNT);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V006"));
                }

                llCancel = poParam.NCALLER_AMOUNT > poParam.NCALLER_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V007"));
                }

                llCancel = poParam.NCALLER_TAX_AMOUNT > poParam.NCALLER_TAX_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V008"));
                }

                llCancel = poParam.NTARGET_ALLOCATION == 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V009"));
                }

                llCancel = (poParam.NTARGET_ALLOCATION > 0) && (poParam.LSINGLE_CURRENCY) && (poParam.NTARGET_ALLOCATION != poParam.NCALLER_AMOUNT);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V0010"));
                }
                llCancel = poParam.NCALLER_ALLOCATION == 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V0011"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveAllocationAsync(APF00110DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            APF00110ParameterDTO loParameter = null;

            try
            {
                if (loGetTransactionFlag.LVALUE)
                {
                    loParameter = new APF00110ParameterDTO()
                    {
                        CFR_REC_ID = poEntity.CTARGET_REC_ID,
                        CFR_DEPT_CODE = poEntity.CTARGET_DEPT_CODE,
                        CFR_TRANS_CODE = poEntity.CTARGET_TRANS_CODE,
                        CFR_REF_NO = poEntity.CTARGET_REF_NO,
                        CFR_CURRENCY_CODE = poEntity.CTARGET_CURRENCY_CODE,
                        NFR_AP_AMOUNT = poEntity.NTARGET_AMOUNT,
                        NFR_TAX_AMOUNT = poEntity.NTARGET_TAX_AMOUNT,
                        NFR_DISC_AMOUNT = poEntity.NTARGET_DISC_AMOUNT,
                        NFR_LBASE_RATE = poEntity.NLTARGET_BASE_RATE,
                        NFR_LCURRENCY_RATE = poEntity.NLTARGET_CURRENCY_RATE,
                        NFR_BBASE_RATE = poEntity.NBTARGET_BASE_RATE,
                        NFR_BCURRENCY_RATE = poEntity.NBTARGET_CURRENCY_RATE,
                        NFR_TAX_BASE_RATE = poEntity.NTARGET_TAX_BASE_RATE,
                        NFR_TAX_CURRENCY_RATE = poEntity.NTARGET_TAX_CURRENCY_RATE,
                        CTO_REC_ID = loAllocationEntryParameter.CREC_ID,
                        CTO_DEPT_CODE = loHeader.CDEPT_CODE,
                        CTO_TRANS_CODE = loAllocationEntryParameter.CTRANS_CODE,
                        CTO_REF_NO = loHeader.CREF_NO,
                        CTO_CURRENCY_CODE = poEntity.CCALLER_CURRENCY_CODE,
                        NTO_AP_AMOUNT = poEntity.NCALLER_AMOUNT,
                        NTO_TAX_AMOUNT = poEntity.NCALLER_TAX_AMOUNT,
                        NTO_DISC_AMOUNT = poEntity.NCALLER_DISC_AMOUNT,
                        NTO_LBASE_RATE = poEntity.NLCALLER_BASE_RATE,
                        NTO_LCURRENCY_RATE = poEntity.NLCALLER_CURRENCY_RATE,
                        NTO_BBASE_RATE = poEntity.NBCALLER_BASE_RATE,
                        NTO_BCURRENCY_RATE = poEntity.NBCALLER_CURRENCY_RATE,
                        NTO_TAX_BASE_RATE = poEntity.NCALLER_TAX_BASE_RATE,
                        NTO_TAX_CURRENCY_RATE = poEntity.NCALLER_TAX_CURRENCY_RATE,
                    };
                }
                else
                {
                    loParameter = new APF00110ParameterDTO()
                    {
                        CFR_REC_ID = loAllocationEntryParameter.CREC_ID,
                        CFR_DEPT_CODE = loHeader.CDEPT_CODE,
                        CFR_TRANS_CODE = loAllocationEntryParameter.CTRANS_CODE,
                        CFR_REF_NO = loHeader.CREF_NO,
                        CFR_CURRENCY_CODE = poEntity.CCALLER_CURRENCY_CODE,
                        NFR_AP_AMOUNT = poEntity.NCALLER_AMOUNT,
                        NFR_TAX_AMOUNT = poEntity.NCALLER_TAX_AMOUNT,
                        NFR_DISC_AMOUNT = poEntity.NCALLER_DISC_AMOUNT,
                        NFR_LBASE_RATE = poEntity.NLCALLER_BASE_RATE,
                        NFR_LCURRENCY_RATE = poEntity.NLCALLER_CURRENCY_RATE,
                        NFR_BBASE_RATE = poEntity.NBCALLER_BASE_RATE,
                        NFR_BCURRENCY_RATE = poEntity.NBCALLER_CURRENCY_RATE,
                        NFR_TAX_BASE_RATE = poEntity.NCALLER_TAX_BASE_RATE,
                        NFR_TAX_CURRENCY_RATE = poEntity.NCALLER_TAX_CURRENCY_RATE,
                        CTO_REC_ID = poEntity.CTARGET_REC_ID,
                        CTO_DEPT_CODE = poEntity.CTARGET_DEPT_CODE,
                        CTO_TRANS_CODE = poEntity.CTARGET_TRANS_CODE,
                        CTO_REF_NO = poEntity.CTARGET_REF_NO,
                        CTO_CURRENCY_CODE = poEntity.CTARGET_CURRENCY_CODE,
                        NTO_AP_AMOUNT = poEntity.NTARGET_AMOUNT,
                        NTO_TAX_AMOUNT = poEntity.NTARGET_TAX_AMOUNT,
                        NTO_DISC_AMOUNT = poEntity.NTARGET_DISC_AMOUNT,
                        NTO_LBASE_RATE = poEntity.NLTARGET_BASE_RATE,
                        NTO_LCURRENCY_RATE = poEntity.NLTARGET_CURRENCY_RATE,
                        NTO_BBASE_RATE = poEntity.NBTARGET_BASE_RATE,
                        NTO_BCURRENCY_RATE = poEntity.NBTARGET_CURRENCY_RATE,
                        NTO_TAX_BASE_RATE = poEntity.NTARGET_TAX_BASE_RATE,
                        NTO_TAX_CURRENCY_RATE = poEntity.NTARGET_TAX_CURRENCY_RATE,
                    };

                }

                loParameter.NLFOREX_GAINLOSS = poEntity.NLFOREX_GAINLOSS;
                loParameter.NBFOREX_GAINLOSS = poEntity.NBFOREX_GAINLOSS;

                loParameter.CPROPERTY_ID = loAllocationEntryParameter.CPROPERTY_ID;
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    loParameter.CACTION = "NEW";
                    loParameter.CALLOCATION_ID = "";
                    loParameter.CREF_NO = "";
                }
                else if (peCRUDMode == eCRUDMode.EditMode)
                {
                    loParameter.CACTION = "EDIT";
                    loParameter.CALLOCATION_ID = poEntity.CREC_ID;
                    loParameter.CREF_NO = poEntity.CREF_NO;
                }

                if (loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_PURCHASE_RETURN || loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_PURCHASE_DEBIT_NOTE)
                {
                    loParameter.CDEPT_CODE = poEntity.CTARGET_DEPT_CODE;
                }
                else
                {
                    loParameter.CDEPT_CODE = loAllocationEntryParameter.CDEPT_CODE;
                }

                loParameter.CTRANS_CODE = TransCodeConstant.VAR_TRANS_CODE;
                loParameter.CALLOC_DATE = poEntity.DREF_DATE.Value.ToString("yyyyMMdd");
                loParameter.CSUPPLIER_ID = loHeader.CSUPPLIER_ID;
                loParameter.CSUPPLIER_SEQ_NO = loHeader.CSUPPLIER_SEQ_NO;
                
                APF00110ParameterDTO loResult = await loModel.R_ServiceSaveAsync(loParameter, peCRUDMode);

                loAllocationDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteAllocationAsync(APF00110DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            APF00110ParameterDTO loParam = new APF00110ParameterDTO();
            try
            {
                loParam.Data = poEntity;
                loParam.CALLOCATION_ID = loAllocationDetail.CREC_ID;

                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
