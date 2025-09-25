using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GLM00300Common;
using GLM00300Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLM00300Model
{
    public class GLM00300Model : R_BusinessObjectServiceClientBase<GLM00300DTO>, IGLM00300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLM00300";
        private const string DEFAULT_MODULE = "GL";

        public GLM00300Model() :
            base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        public async Task<GLM00300ListDTO> GetBudgetWeightingNameListAsync()
        {
            var loEx = new R_Exception();
            GLM00300ListDTO loRes = new GLM00300ListDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loResTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLM00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00300.GetBudgetWeightingNameList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRes.Data = loResTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRes;
        }
        public async Task<CurrencyCodeListDTO> GetCurrencyCodeListAsync()
        {
            var loEx = new R_Exception();
            CurrencyCodeListDTO loRes = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRes = await R_HTTPClientWrapper.R_APIRequestObject<CurrencyCodeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00300.GetCurrencyCodeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRes;
        }

        #region notImpelemnted
        public IAsyncEnumerable<GLM00300DTO> GetBudgetWeightingNameList()
        {
            throw new NotImplementedException();
        }
        public CurrencyCodeListDTO GetCurrencyCodeList()
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
