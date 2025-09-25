using GLR00300Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;

namespace GLR00300Model
{
    public class GLR00300Model : R_BusinessObjectServiceClientBase<GLR00300DTO>, IGLR00300
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLR00300";
        private const string DEFAULT_MODULE = "GL";
        public GLR00300Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public GLR00300InitialProcess InitialProcess()
        {
            throw new NotImplementedException();
        }

        public GenericList<GLR00300GetPeriod> GetPeriod(GLR00300DBParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public GenericList<GLR00300DTO> GetTrialBalanceType()
        {
            throw new NotImplementedException();
        }
        public GenericList<GLR00300DTO> GetPrintMethodType()
        {
            throw new NotImplementedException();
        }
        public GenericList<GLR00300BudgetNoDTO> GetBudgetNo(GLR00300DBParameterDTO loParamDB)
        {
            throw new NotImplementedException();
        }
        public async Task<GLR00300InitialProcess> GetInitialProcessAsyncModel()
        {
            var loEx = new R_Exception();
            GLR00300InitialProcess loResult = new GLR00300InitialProcess();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLR00300InitialProcess>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00300.InitialProcess),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<GenericList<GLR00300DTO>> GetTrialBalanceTypeAsyncModel()
        {
            var loEx = new R_Exception();
            GenericList<GLR00300DTO> loResult = new GenericList<GLR00300DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GenericList<GLR00300DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00300.GetTrialBalanceType),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<GenericList<GLR00300DTO>> GetPrintMethodTypeAsyncModel()
        {
            var loEx = new R_Exception();
            GenericList<GLR00300DTO> loResult = new GenericList<GLR00300DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GenericList<GLR00300DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00300.GetPrintMethodType),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<GenericList<GLR00300BudgetNoDTO>> GetBudgetNoAsyncModel(GLR00300DBParameterDTO loParamDB)
        {
            var loEx = new R_Exception();
            GenericList<GLR00300BudgetNoDTO> loResult = new GenericList<GLR00300BudgetNoDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GenericList<GLR00300BudgetNoDTO>, GLR00300DBParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00300.GetBudgetNo),
                    loParamDB,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<GenericList<GLR00300GetPeriod>> GetPeriodAsyncoModel(GLR00300DBParameterDTO loParam)
        {
            R_Exception loEx = new R_Exception();
            GenericList<GLR00300GetPeriod> loResult = new GenericList<GLR00300GetPeriod>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GenericList<GLR00300GetPeriod>, GLR00300DBParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLR00300.GetPeriod),
                    loParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
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
