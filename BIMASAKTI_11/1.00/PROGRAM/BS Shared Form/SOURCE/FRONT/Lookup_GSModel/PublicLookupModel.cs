using Lookup_GSCOMMON;
using Lookup_GSCOMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSModel
{
    public class PublicLookupModel : R_BusinessObjectServiceClientBase<GSL00500DTO>, IPublicLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/PublicLookup";
        public PublicLookupModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        public GSLGenericList<GSL00500DTO> GSL00500GetGLAccountList(GSL00500ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00500DTO>> GSL00500GetGLAccountListAsync(GSL00500ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00500DTO> loResult = new GSLGenericList<GSL00500DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00500DTO>, GSL00500ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00500GetGLAccountList),
                    poParameter,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult ;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GSLGenericList<GSL01500ResultDetailDTO> GSL01500GetCashDetailList(GSL01500ParameterDetailDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public GSLGenericList<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList(GSL01500ParameterGroupDTO poParameter)
        {
            throw new NotImplementedException();
        }
    }
}
