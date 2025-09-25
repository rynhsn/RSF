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
    public class GLM00500HeaderModel : R_BusinessObjectServiceClientBase<GLM00500BudgetHDDTO>,
        IGLM00500Header
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLM00500Header";
        private const string DEFAULT_MODULE = "gl";

        public GLM00500HeaderModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region IGLM00500Header Members not implemented

        public IAsyncEnumerable<GLM00500BudgetHDDTO> GLM00500GetBudgetHDListStream()
        {
            throw new NotImplementedException();
        }

        public GLM00500GSMPeriodDTO GLM00500GetPeriods()
        {
            throw new NotImplementedException();
        }

        public GLM00500GLSystemParamDTO GLM00500GetSystemParams()
        {
            throw new NotImplementedException();
        }

        public GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetCurrencyTypeList()
        {
            throw new NotImplementedException();
        }

        public GLM00500ReturnDTO GLM00500FinalizeBudget(GLM00500CrecParamsDTO poParams)
        {
            throw new NotImplementedException();
        }

        public GLM00500AccountBudgetExcelDTO GLM00500DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<List<GLM00500BudgetHDDTO>> GLM00500GetBudgetHDListStreamModel()
        {
            var loEx = new R_Exception();
            var loResult = new List<GLM00500BudgetHDDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00500BudgetHDDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500GetBudgetHDListStream),
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

        //get period list
        public async Task<GLM00500GSMPeriodDTO> GLM00500GetPeriodsModel()
        {
            var loEx = new R_Exception();
            var loResult = default(GLM00500GSMPeriodDTO);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500GSMPeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500GetPeriods),
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

        //get system param
        public async Task<GLM00500GLSystemParamDTO> GLM00500GetSystemParamModel()
        {
            var loEx = new R_Exception();
            var loResult = new GLM00500GLSystemParamDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500GLSystemParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500GetSystemParams),
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

        //get currency type list
        public async Task<GLM00500ListDTO<GLM00500FunctionDTO>> GLM00500GetCurrencyTypeListModel()
        {
            var loEx = new R_Exception();
            var loResult = new GLM00500ListDTO<GLM00500FunctionDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500FunctionDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500GetCurrencyTypeList),
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

        //finalize budget
        public async Task<GLM00500ReturnDTO> GLM00500FinalizeBudgetModel(GLM00500CrecParamsDTO poParams)
        {
            var loEx = new R_Exception();
            var loReturn = default(GLM00500ReturnDTO);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loReturn = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ReturnDTO, GLM00500CrecParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500FinalizeBudget),
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
            return loReturn;
        }

        public async Task<GLM00500AccountBudgetExcelDTO> GLM00500DownloadTemplateFileModel()
        {
            var loEx = new R_Exception();
            var loResult = new GLM00500AccountBudgetExcelDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500AccountBudgetExcelDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500DownloadTemplateFile),
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