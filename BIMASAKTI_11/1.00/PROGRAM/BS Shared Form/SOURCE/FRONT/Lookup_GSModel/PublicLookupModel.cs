using Lookup_GSCOMMON;
using Lookup_GSCOMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSModel
{
    public class PublicLookupModel : R_BusinessObjectServiceClientBase<GSL00500DTO>, IPublicLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/PublicLookup";
        private const string DEFAULT_MODULE = "GS";

        public PublicLookupModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GSL00100
        public GSLGenericList<GSL00100DTO> GSL00100GetSalesTaxList(GSL00100ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<GSLGenericList<GSL00100DTO>> GSL00100GetSalesTaxListAsync(GSL00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00100DTO> loResult = new GSLGenericList<GSL00100DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00100DTO>, GSL00100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00100GetSalesTaxList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00200
        public GSLGenericList<GSL00200DTO> GSL00200GetWithholdingTaxList(GSL00200ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00200DTO>> GSL00200GetWithholdingTaxListAsync(GSL00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00200DTO> loResult = new GSLGenericList<GSL00200DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00200DTO>, GSL00200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00200GetWithholdingTaxList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00300
        public GSLGenericList<GSL00300DTO> GSL00300GetCurrencyList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00300DTO>> GSL00300GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00300DTO> loResult = new GSLGenericList<GSL00300DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00300DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00300GetCurrencyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00400
        public GSLGenericList<GSL00400DTO> GSL00400GetJournalGroupList(GSL00400ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00400DTO>> GSL00400GetJournalGroupListAsync(GSL00400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00400DTO> loResult = new GSLGenericList<GSL00400DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00400DTO>, GSL00400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00400GetJournalGroupList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00500
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
                    DEFAULT_MODULE,
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


        #endregion

        #region GSL00510
        public GSLGenericList<GSL00510DTO> GSL00510GetCOAList(GSL00510ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00510DTO>> GSL00510GetCOAListAsync(GSL00510ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00510DTO> loResult = new GSLGenericList<GSL00510DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00510DTO>, GSL00510ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00510GetCOAList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00520
        public GSLGenericList<GSL00520DTO> GSL00520GetGOACOAList(GSL00520ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00520DTO>> GSL00520GetGOACOAListAsync(GSL00520ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00520DTO> loResult = new GSLGenericList<GSL00520DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00520DTO>, GSL00520ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00520GetGOACOAList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00550
        public GSLGenericList<GSL00550DTO> GSL00550GetGOAList(GSL00550ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<GSLGenericList<GSL00550DTO>> GSL00550GetGOAListAsync(GSL00550ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00550DTO> loResult = new GSLGenericList<GSL00550DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00550DTO>, GSL00550ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00550GetGOAList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
        #region GSL00600
        public GSLGenericList<GSL00600DTO> GSL00600GetUnitTypeCategoryList(GSL00600ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00600DTO>> GSL00600GetUnitTypeCategoryListAsync(GSL00600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00600DTO> loResult = new GSLGenericList<GSL00600DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00600DTO>, GSL00600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00600GetUnitTypeCategoryList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00700
        public GSLGenericList<GSL00700DTO> GSL00700GetDepartmentList(GSL00700ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00700DTO>> GSL00700GetDepartmentListAsync(GSL00700ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00700DTO> loResult = new GSLGenericList<GSL00700DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00700DTO>, GSL00700ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00700GetDepartmentList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00800
        public GSLGenericList<GSL00800DTO> GSL00800GetCurrencyTypeList(GSL00800ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00800DTO>> GSL00800GetCurrencyTypeListAsync(GSL00800ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00800DTO> loResult = new GSLGenericList<GSL00800DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00800DTO>, GSL00800ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00800GetCurrencyTypeList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL00900
        public GSLGenericList<GSL00900DTO> GSL00900GetCenterList(GSL00900ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL00900DTO>> GSL00900GetCenterListAsync(GSL00900ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL00900DTO> loResult = new GSLGenericList<GSL00900DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL00900DTO>, GSL00900ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00900GetCenterList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL01000
        public GSLGenericList<GSL01000DTO> GSL01000GetUserList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01000DTO>> GSL01000GetUserListAsync()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01000DTO> loResult = new GSLGenericList<GSL01000DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01000DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01000GetUserList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL01100
        public GSLGenericList<GSL01100DTO> GSL01100GetUserApprovalList(GSL01100ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01100DTO>> GSL01100GetUserApprovalListAsync(GSL01100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01100DTO> loResult = new GSLGenericList<GSL01100DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01100DTO>, GSL01100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01100GetUserApprovalList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL01200
        public GSLGenericList<GSL01200DTO> GSL01200GetBankList(GSL01200ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01200DTO>> GSL01200GetBankListAsync(GSL01200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01200DTO> loResult = new GSLGenericList<GSL01200DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01200DTO>, GSL01200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01200GetBankList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL01300
        public GSLGenericList<GSL01300DTO> GSL01300GetBankAccountList(GSL01300ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01300DTO>> GSL01300GetBankAccountListAsync(GSL01300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01300DTO> loResult = new GSLGenericList<GSL01300DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01300DTO>, GSL01300ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01300GetBankAccountList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL01400
        public GSLGenericList<GSL01400DTO> GSL01400GetOtherChargesList(GSL01400ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01400DTO>> GSL01400GetOtherChargesListAsync(GSL01400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01400DTO> loResult = new GSLGenericList<GSL01400DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01400DTO>, GSL01400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01400GetOtherChargesList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL01500
        public GSLGenericList<GSL01500ResultDetailDTO> GSL01500GetCashDetailList(GSL01500ParameterDetailDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01500ResultDetailDTO>> GSL01500GetCashDetailListAsync(GSL01500ParameterDetailDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01500ResultDetailDTO> loResult = new GSLGenericList<GSL01500ResultDetailDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01500ResultDetailDTO>, GSL01500ParameterDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01500GetCashDetailList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GSLGenericList<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList(GSL01500ParameterGroupDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01500ResultGroupDTO>> GSL01500GetCashFlowGroupListAsync(GSL01500ParameterGroupDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01500ResultGroupDTO> loResult = new GSLGenericList<GSL01500ResultGroupDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01500ResultGroupDTO>, GSL01500ParameterGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01500GetCashFlowGroupList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GSL01600
        public GSLGenericList<GSL01600DTO> GSL01600GetCashFlowGroupTypeList(GSL01600ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01600DTO>> GSL01600GetCashFlowGroupTypeListAsync(GSL01600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01600DTO> loResult = new GSLGenericList<GSL01600DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01600DTO>, GSL01600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01600GetCashFlowGroupTypeList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        #endregion

        #region GSL01700
        public GSLGenericList<GSL01700DTO> GSL01700GetCurrencyRateList(GSL01700DTOParameter poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task<GSLGenericList<GSL01700DTO>> GSL01700GetCurrencyRateListAsync(GSL01700DTOParameter poParameter)
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01700DTO> loResult = new GSLGenericList<GSL01700DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01700DTO>, GSL01700DTOParameter>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01700GetCurrencyRateList),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
            
        }

        public GSLGenericList<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSLGenericList<GSL01702DTO>> GSL01700GetLocalAndBaseCurrencyListAsync()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01702DTO> loResult = new GSLGenericList<GSL01702DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01702DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01700GetLocalAndBaseCurrencyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GSLGenericList<GSL01701DTO> GSL01700GetRateTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSLGenericList<GSL01701DTO>> GSL01700GetRateTypeListAsync()
        {
            var loEx = new R_Exception();
            GSLGenericList<GSL01701DTO> loResult = new GSLGenericList<GSL01701DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericList<GSL01701DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01700GetRateTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
