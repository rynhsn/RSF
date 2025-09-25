using PMR02000COMMON;
using PMR02000COMMON.DTO_s;
using PMR02000COMMON.DTO_s.Print;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR02000MODEL
{
    public class PMR02000Model : R_BusinessObjectServiceClientBase<ReportParamDTO>, IPMR02000
    {

        public PMR02000Model(string pcHttpClientName = PMR02000ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = PMR02000ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMR02000ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public GeneralResultDTO<TodayDTO> GetTodayDate()
        {
            throw new NotImplementedException();
        }

        public GeneralResultDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CategoryTypeDTO> GetCategoryTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TransactionTypeDTO> GetTransTypeList()
        {
            throw new NotImplementedException();
        }

        // method
        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR02000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR02000.GetPropertyList),
                    PMR02000ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public async Task<TodayDTO> GetTodayDateAsync()
        {
            var loEx = new R_Exception();
            TodayDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR02000ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralResultDTO<TodayDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMR02000.GetTodayDate),
                    PMR02000ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<PeriodYearDTO> GetPeriodYearRecordAsync(PeriodYearDTO poParam)
        {
            var loEx = new R_Exception();
            PeriodYearDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR02000ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralResultDTO<PeriodYearDTO>, PeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR02000.GetPeriodYearRecord),
                    poParam,
                    PMR02000ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<CategoryTypeDTO>> GetCustomerTypeListAsync()
        {
            var loEx = new R_Exception();
            List<CategoryTypeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR02000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CategoryTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR02000.GetCategoryTypeList),
                    PMR02000ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }

        public async Task<List<TransactionTypeDTO>> GetTransTypeListAsync()
        {
            var loEx = new R_Exception();
            List<TransactionTypeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMR02000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TransactionTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR02000.GetTransTypeList),
                    PMR02000ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;

        }


    }
}
