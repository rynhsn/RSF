using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLM00500Model
{
    public class GLM00500DetailModel : R_BusinessObjectServiceClientBase<GLM00500BudgetDTDTO>, IGLM00500Detail
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLM00500Detail";
        private const string DEFAULT_MODULE = "gl";

        public GLM00500DetailModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        #region IGLM00500Detail Members not implemented

        public IAsyncEnumerable<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTListStream()
        {
            throw new NotImplementedException();
        }

        public GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetRoundingMethodList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GLM00500BudgetWeightingDTO> GLM00500GetBudgetWeightingListStream()
        {
            throw new NotImplementedException();
        }

        public GLM00500PeriodCountDTO GLM00500GetPeriodCount(GLM00500YearParamsDTO poParams)
        {
            throw new NotImplementedException();
        }

        public GLM00500GSMCompanyDTO GLM00500GetGSMCompany()
        {
            throw new NotImplementedException();
        }

        public GLM00500BudgetCalculateDTO GLM00500BudgetCalculate(GLM00500CalculateParamDTO poParams)
        {
            throw new NotImplementedException();
        }

        public GLM00500ReturnDTO GLM00500GenerateBudget(GLM00500GenerateAccountBudgetDTO poGenerateAccountBudgetDTO)
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<List<GLM00500BudgetDTGridDTO>> GLM00500GetBudgetDTListStreamModel()
        {
            var loEx = new R_Exception();
            List<GLM00500BudgetDTGridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00500BudgetDTGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Detail.GLM00500GetBudgetDTListStream),
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

        public async Task<GLM00500ListDTO<GLM00500FunctionDTO>> GLM00500GetRoundingMethodListModel()
        {
            var loEx = new R_Exception();
            GLM00500ListDTO<GLM00500FunctionDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500FunctionDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Detail.GLM00500GetRoundingMethodList),
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

        public async Task<List<GLM00500BudgetWeightingDTO>> GLM00500GetBudgetWeightingListStreamModel()
        {
            var loEx = new R_Exception();
            List<GLM00500BudgetWeightingDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00500BudgetWeightingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Detail.GLM00500GetBudgetWeightingListStream),
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

        public async Task<GLM00500PeriodCountDTO> GLM00500GetPeriodCountModel(GLM00500YearParamsDTO poParams)
        {
            var loEx = new R_Exception();
            GLM00500PeriodCountDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500PeriodCountDTO, GLM00500YearParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Detail.GLM00500GetPeriodCount),
                    poParams,
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

        public async Task<GLM00500GSMCompanyDTO> GLM00500GetGSMCompanyModel()
        {
            var loEx = new R_Exception();
            GLM00500GSMCompanyDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500GSMCompanyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Detail.GLM00500GetGSMCompany),
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

        public async Task<GLM00500BudgetCalculateDTO> GLM00500BudgetCalculateModel(GLM00500CalculateParamDTO poParams)
        {
            var loEx = new R_Exception();
            GLM00500BudgetCalculateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<GLM00500BudgetCalculateDTO, GLM00500CalculateParamDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGLM00500Detail.GLM00500BudgetCalculate),
                        poParams,
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

        public async Task<GLM00500ReturnDTO> GLM00500GenerateBudgetModel(GLM00500GenerateAccountBudgetDTO result)
        {
            var loEx = new R_Exception();
            var loResult = new GLM00500ReturnDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper
                    .R_APIRequestObject<GLM00500ReturnDTO, GLM00500GenerateAccountBudgetDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGLM00500Detail.GLM00500GenerateBudget),
                        result,
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