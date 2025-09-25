using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM02000Model
{
    public class GSM02000TaxModel:R_BusinessObjectServiceClientBase<GSM02000TaxDTO>, IGSM02000Tax
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02000Tax";
        private const string DEFAULT_MODULE = "gs";
        public GSM02000TaxModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME, 
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
            string pcModuleName = DEFAULT_MODULE, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region Not Implemented
        public GSM02000ListDTO<GSM02000TaxSalesDTO> GSM02000GetAllSalesTaxList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM02000TaxSalesDTO> GSM02000GetAllSalesTaxListStream()
        {
            throw new NotImplementedException();
        }

        public GSM02000ListDTO<GSM02000TaxDTO> GSM02000GetAllTaxList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM02000TaxDTO> GSM02000GetAllTaxListStream()
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<List<GSM02000TaxSalesDTO>> GetAllSalesTaxListStreamModel()
        {
            var loEx = new R_Exception();
            List<GSM02000TaxSalesDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02000TaxSalesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000Tax.GSM02000GetAllSalesTaxListStream),
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
        
        public async Task<List<GSM02000TaxDTO>> GetAllTaxListStreamModel()
        {
            var loEx = new R_Exception();
            List<GSM02000TaxDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02000TaxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02000Tax.GSM02000GetAllTaxListStream),
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