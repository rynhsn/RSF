using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using APF00100COMMON.DTOs.APF00100;
using APF00100COMMON.DTOs;
using System.ComponentModel;

namespace APF00100Model.ViewModel
{
    public class APF00100ViewModel : R_ViewModel<APF00100ListDTO>
    {
        private APF00100Model loModel = new APF00100Model();

        public APF00100ListDTO loAllocation = new APF00100ListDTO();

        public ObservableCollection<APF00100ListDTO> loAllocationList = new ObservableCollection<APF00100ListDTO>();

        public APF00100HeaderResultDTO loHeaderRtn = null;

        public APF00100HeaderDTO loHeader = new APF00100HeaderDTO();

        public APF00100ListResultDTO loListRtn = null;

        //public GetAPSystemParamResultDTO loAPSystemParamRtn = null;

        //public GetPeriodYearRangeResultDTO loPeriodYearRangeRtn = null;

        //public GetCompanyInfoResultDTO loCompanyInfoRtn = null;

        //public GetGLSystemParamResultDTO loGLSystemParamRtn = null;

        //public GetTransCodeInfoResultDTO loTransCodeInfoRtn = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public GetGLSystemParamDTO loGLSystemParam = null;

        public GetCallerTrxInfoDTO loCallerTrxInfo = null;

        public GetPeriodDTO loSoftPeriod = null;

        public GetPeriodDTO loCurrentPeriod = null;

        public GetTransactionFlagDTO loGetTransactionFlag = null;


        public OpenAllocationParameterDTO loAllocationParameter = new OpenAllocationParameterDTO();

        public async Task GetAllocationListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            APF00100ListParameterDTO loParam = null;
            List<APF00100ListDTO> loTemp = null;
            int a = 0;
            try
            {
                loParam = new APF00100ListParameterDTO()
                {
                    CPROPERTY_ID = loAllocationParameter.CPROPERTY_ID,
                    CDEPT_CODE = loAllocationParameter.CDEPT_CODE,
                    CREF_NO = loAllocationParameter.CREF_NO,
                    CTRANS_CODE = loAllocationParameter.CTRANS_CODE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.APF00100_GET_ALLOCATION_LIST_STREAMING_CONTEXT, loParam);
                loListRtn = await loModel.GetAllocationListStreamAsync();
                loListRtn.Data.ForEach(x =>
                {
                    x.INO = a + 1;
                    x.DALLOC_DATE = DateTime.ParseExact(x.CALLOC_DATE, "yyyyMMdd", null);
                    a++;
                });
                loAllocationList = new ObservableCollection<APF00100ListDTO>(loListRtn.Data);
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
                loResult = await loModel.GetHeaderAsync(new APF00100HeaderParameterDTO()
                {
                    CREC_ID = loAllocationParameter.CREC_ID
                });
                loHeader = loResult.Data;
                loHeader.DDOC_DATE = DateTime.ParseExact(loHeader.CDOC_DATE, "yyyyMMdd", null);
                loHeader.DREF_DATE = DateTime.ParseExact(loHeader.CREF_DATE, "yyyyMMdd", null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task InitialProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
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
        public async Task GetCompanyInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCompanyInfoResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetCompanyInfoAsync();
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
                loResult = await loModel.GetGLSystemParamAsync();
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
                loResult = await loModel.GetCallerTrxInfoAsync(new GetCallerTrxInfoParameterDTO()
                {
                    CREC_ID = loAllocationParameter.CREC_ID
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
                loResult = await loModel.GetPeriodAsync(new GetPeriodParameterDTO()
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
                loResult = await loModel.GetPeriodAsync(new GetPeriodParameterDTO()
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
                loResult = await loModel.GetTransactionFlagAsync(new GetTransactionFlagParameterDTO()
                {
                    CTRANS_CODE = loAllocationParameter.CTRANS_CODE
                });
                loGetTransactionFlag = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public void RefreshAllocationListValidation()
        {
            bool llCancel = false;
            R_Exception loEx = new R_Exception();

            try
            {
                //llCancel = string.IsNullOrWhiteSpace(loAllocation.CDEPARTMENT_CODE);
                //if (llCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V001"));
                //}

                //llCancel = string.IsNullOrWhiteSpace(loAllocation.CSUPPLIER_ID) && loAllocation.CSUPPLIER_OPTIONS == "S";
                //if (llCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V002"));
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}