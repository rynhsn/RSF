using System;
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
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region IGLM00500Header Members not implemented

        public GLM00500ListDTO<GLM00500BudgetHDDTO> GLM00500GetBudgetHDList()
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

        public void GLM00500FinalizeBudget()
        {
            throw new NotImplementedException();
        }

        public GLM00500AccountBudgetExcelDTO GLM00500DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<GLM00500ListDTO<GLM00500BudgetHDDTO>> GLM00500GetBudgetHDListModel()
        {
            var loEx = new R_Exception();
            GLM00500ListDTO<GLM00500BudgetHDDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500BudgetHDDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500GetBudgetHDList),
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
            GLM00500GSMPeriodDTO loResult = null;

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
            GLM00500GLSystemParamDTO loResult = null;

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
            GLM00500ListDTO<GLM00500FunctionDTO> loResult = null;

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
        public async Task GLM00500FinalizeBudgetModel()
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500FunctionDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00500Header.GLM00500FinalizeBudget),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GLM00500AccountBudgetExcelDTO> GLM00500DownloadTemplateFileModel()
        {
            var loEx = new R_Exception();
            GLM00500AccountBudgetExcelDTO loResult = new();

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