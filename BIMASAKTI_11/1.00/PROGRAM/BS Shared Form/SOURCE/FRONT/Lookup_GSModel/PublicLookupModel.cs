using Lookup_GSCOMMON;
using Lookup_GSCOMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Lookup_GSModel
{
    public class PublicLookupModel : R_BusinessObjectServiceClientBase<GSL00500DTO>, IPublicLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupGS";
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
        public IAsyncEnumerable<GSL00100DTO> GSL00100GetSalesTaxList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00100DTO>> GSL00100GetSalesTaxListAsync()
        {
            var loEx = new R_Exception();
            List<GSL00100DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00100GetSalesTaxList),
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
        #endregion

        #region GSL00110
        public IAsyncEnumerable<GSL00110DTO> GSL00110GetTaxByDateList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00110DTO>> GSL00110GetTaxByDateListAsync(GSL00110ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<GSL00110DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAX_DATE, poParam.CTAX_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00110DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00110GetTaxByDateList),
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

        #endregion

        #region GSL00200
        public IAsyncEnumerable<GSL00200DTO> GSL00200GetWithholdingTaxList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00200DTO>> GSL00200GetWithholdingTaxListAsync(GSL00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL00200DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAX_TYPE_LIST, poParameter.CTAX_TYPE_LIST);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00200GetWithholdingTaxList),
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
        #endregion

        #region GSL00300
        public IAsyncEnumerable<GSL00300DTO> GSL00300GetCurrencyList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00300DTO>> GSL00300GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<GSL00300DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00300GetCurrencyList),
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
        #endregion

        #region GSL00400
        public IAsyncEnumerable<GSL00400DTO> GSL00400GetJournalGroupList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00400DTO>> GSL00400GetJournalGroupListAsync(GSL00400ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<GSL00400DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CJRNGRP_TYPE, poParam.CJRNGRP_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00400GetJournalGroupList),
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
        #endregion

        #region GSL00500
        public IAsyncEnumerable<GSL00500DTO> GSL00500GetGLAccountList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00500DTO>> GSL00500GetGLAccountListAsync(GSL00500ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<GSL00500DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROGRAM_CODE, poParam.CPROGRAM_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBSIS, poParam.CBSIS);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDBCR, poParam.CDBCR);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LCENTER_RESTR, poParam.LCENTER_RESTR);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LUSER_RESTR, poParam.LUSER_RESTR);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCENTER_CODE, poParam.CCENTER_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CGOA_CODE, poParam.CGOA_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00500GetGLAccountList),
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
        #endregion

        #region GSL00510
        public IAsyncEnumerable<GSL00510DTO> GSL00510GetCOAList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00510DTO>> GSL00510GetCOAListAsync(GSL00510ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<GSL00510DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CGLACCOUNT_TYPE, poParam.CGLACCOUNT_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LINACTIVE_COA, poParam.LINACTIVE_COA);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00510GetCOAList),
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
        #endregion

        #region GSL00520
        public IAsyncEnumerable<GSL00520DTO> GSL00520GetGOACOAList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00520DTO>> GSL00520GetGOACOAListAsync(GSL00520ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<GSL00520DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CGOA_CODE, poParam.CGOA_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00520DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00520GetGOACOAList),
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
        #endregion

        #region GSL00550
        public IAsyncEnumerable<GSL00550DTO> GSL00550GetGOAList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00550DTO>> GSL00550GetGOAListAsync()
        {
            var loEx = new R_Exception();
            List<GSL00550DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00550DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00550GetGOAList),
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
        #endregion

        #region GSL00600
        public IAsyncEnumerable<GSL00600DTO> GSL00600GetUnitTypeCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00600DTO>> GSL00600GetUnitTypeCategoryListAsync(GSL00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<GSL00600DTO> loResult = new List<GSL00600DTO>();

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00600GetUnitTypeCategoryList),
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
        #endregion

        #region GSL00700
        public IAsyncEnumerable<GSL00700DTO> GSL00700GetDepartmentList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00700DTO>> GSL00700GetDepartmentListAsync(GSL00700ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL00700DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROGRAM_CODE, string.IsNullOrWhiteSpace(poParameter.CPROGRAM_ID) ? "" : poParameter.CPROGRAM_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00700GetDepartmentList),
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

        #endregion

        #region GSL00710
        public IAsyncEnumerable<GSL00710DTO> GSL00710GetDepartmentPropertyList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00710DTO>> GSL00710GetDepartmentPropertyListAsync(GSL00710ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL00710DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00710DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00710GetDepartmentPropertyList),
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
        #endregion

        #region GSL00800
        public IAsyncEnumerable<GSL00800DTO> GSL00800GetCurrencyTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00800DTO>> GSL00800GetCurrencyTypeListAsync()
        {
            var loEx = new R_Exception();
            List<GSL00800DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00800DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00800GetCurrencyTypeList),
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
        #endregion

        #region GSL00900
        public IAsyncEnumerable<GSL00900DTO> GSL00900GetCenterList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL00900DTO>> GSL00900GetCenterListAsync()
        {
            var loEx = new R_Exception();
            List<GSL00900DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL00900DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL00900GetCenterList),
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
        #endregion

        #region GSL01000
        public IAsyncEnumerable<GSL01000DTO> GSL01000GetUserList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01000DTO>> GSL01000GetUserListAsync()
        {
            var loEx = new R_Exception();
            List<GSL01000DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01000GetUserList),
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
        #endregion

        #region GSL01100
        public IAsyncEnumerable<GSL01100DTO> GSL01100GetUserApprovalList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01100DTO>> GSL01100GetUserApprovalListAsync(GSL01100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL01100DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROGRAM_ID, poParameter.CPROGRAM_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPARAMETER_ID, poParameter.CPARAMETER_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01100GetUserApprovalList),
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
        #endregion

        #region GSL01200
        public IAsyncEnumerable<GSL01200DTO> GSL01200GetBankList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01200DTO>> GSL01200GetBankListAsync(GSL01200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL01200DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCB_TYPE, poParameter.CCB_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01200GetBankList),
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
        #endregion

        #region GSL01300
        public IAsyncEnumerable<GSL01300DTO> GSL01300GetBankAccountList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01300DTO>> GSL01300GetBankAccountListAsync(GSL01300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL01300DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCB_CODE, poParameter.CCB_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBANK_TYPE, poParameter.CBANK_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParameter.CDEPT_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01300GetBankAccountList),
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
        #endregion

        #region GSL01400
        public IAsyncEnumerable<GSL01400DTO> GSL01400GetOtherChargesList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01400DTO>> GSL01400GetOtherChargesListAsync(GSL01400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL01400DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCHARGES_TYPE_ID, poParameter.CCHARGES_TYPE_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01400GetOtherChargesList),
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
        #endregion

        #region GSL01500
        public IAsyncEnumerable<GSL01500ResultDetailDTO> GSL01500GetCashDetailList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01500ResultDetailDTO>> GSL01500GetCashDetailListAsync(string poCashFlowGrpCode)
        {
            var loEx = new R_Exception();
            List<GSL01500ResultDetailDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCASH_FLOW_GROUP_CODE, poCashFlowGrpCode);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01500ResultDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01500GetCashDetailList),
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
        public IAsyncEnumerable<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01500ResultGroupDTO>> GSL01500GetCashFlowGroupListAsync()
        {
            var loEx = new R_Exception();
            List<GSL01500ResultGroupDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01500ResultGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01500GetCashFlowGroupList),
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
        #endregion

        #region GSL01600
        public IAsyncEnumerable<GSL01600DTO> GSL01600GetCashFlowGroupTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01600DTO>> GSL01600GetCashFlowGroupTypeListAsync()
        {
            var loEx = new R_Exception();
            List<GSL01600DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01600GetCashFlowGroupTypeList),
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
        #endregion

        #region GSL01700
        public IAsyncEnumerable<GSL01700DTO> GSL01700GetCurrencyRateList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01700DTO>> GSL01700GetCurrencyRateListAsync(GSL01700DTOParameter poParameter)
        {
            var loEx = new R_Exception();
            List<GSL01700DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CRATETYPE_CODE, poParameter.CRATETYPE_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CRATE_DATE, poParameter.CRATE_DATE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01700GetCurrencyRateList),
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
        public IAsyncEnumerable<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01702DTO>> GSL01700GetLocalAndBaseCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<GSL01702DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01702DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01700GetLocalAndBaseCurrencyList),
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
        public IAsyncEnumerable<GSL01701DTO> GSL01700GetRateTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01701DTO>> GSL01700GetRateTypeListAsync()
        {
            var loEx = new R_Exception();
            List<GSL01701DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01701DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01700GetRateTypeList),
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
        #endregion

        #region GSL01800
        public IAsyncEnumerable<GSL01800DTO> GSL01800GetCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01800DTO>> GSL01800GetCategoryListAsync(GSL01800DTOParameter poParameter)
        {
            var loEx = new R_Exception();
            List<GSL01800DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCATEGORY_TYPE, poParameter.CCATEGORY_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01800DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01800GetCategoryList),
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
        #endregion

        #region GSL01900
        public IAsyncEnumerable<GSL01900DTO> GSL01900GetLOBList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL01900DTO>> GSL01900GetLOBListAsync()
        {
            var loEx = new R_Exception();
            List<GSL01900DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL01900DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL01900GetLOBList),
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
        #endregion

        #region GSL02000

        public IAsyncEnumerable<GSL02000CityDTO> GSL02000GetCityGeographyList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02000CityDTO>> GSL02000GetCityGeographyListListAsync(string poCountry)
        {
            var loEx = new R_Exception();
            List<GSL02000CityDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCOUNTRY_ID, poCountry);

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02000CityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02000GetCityGeographyList),
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
        public IAsyncEnumerable<GSL02000CountryDTO> GSL02000GetCountryGeographyList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02000CountryDTO>> GSL02000GetCountryGeographyListAsync()
        {
            var loEx = new R_Exception();
            List<GSL02000CountryDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02000CountryDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02000GetCountryGeographyList),
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
        #endregion

        #region GSL02100
        public IAsyncEnumerable<GSL02100DTO> GSL02100GetPaymentTermList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02100DTO>> GSL02100GetPaymentTermListAsync(GSL02100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL02100DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02100GetPaymentTermList),
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

        #endregion

        #region GSL02200
        public IAsyncEnumerable<GSL02200DTO> GSL02200GetBuildingList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02200DTO>> GSL02200GetBuildingListAsync(GSL02200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL02200DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LAGREEMENT, poParameter.LAGREEMENT);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02200GetBuildingList),
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
        #endregion

        #region GSL02300
        public IAsyncEnumerable<GSL02300DTO> GSL02300GetBuildingUnitList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02300DTO>> GSL02300GetBuildingUnitListAsync(GSL02300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL02300DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LAGREEMENT, poParameter.LAGREEMENT);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, string.IsNullOrWhiteSpace(poParameter.CBUILDING_ID) ? "" : poParameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CFLOOR_ID, string.IsNullOrWhiteSpace(poParameter.CFLOOR_ID) ? "" : poParameter.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROGRAM_ID, string.IsNullOrWhiteSpace(poParameter.CPROGRAM_ID) ? "" : poParameter.CPROGRAM_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, string.IsNullOrWhiteSpace(poParameter.CTRANS_CODE) ? "" : poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CREF_NO, string.IsNullOrWhiteSpace(poParameter.CREF_NO) ? "" : poParameter.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02300GetBuildingUnitList),
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
        #endregion

        #region GSL02400
        public IAsyncEnumerable<GSL02400DTO> GSL02400GetFloorList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02400DTO>> GSL02400GetFloorListAsync(GSL02400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL02400DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LAGREEMENT, poParameter.LAGREEMENT);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, string.IsNullOrWhiteSpace(poParameter.CBUILDING_ID) ? "" : poParameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROGRAM_ID, string.IsNullOrWhiteSpace(poParameter.CPROGRAM_ID) ? "" : poParameter.CPROGRAM_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, string.IsNullOrWhiteSpace(poParameter.CTRANS_CODE) ? "" : poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CREF_NO, string.IsNullOrWhiteSpace(poParameter.CREF_NO) ? "" : poParameter.CREF_NO);


                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02400GetFloorList),
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
        #endregion

        #region GSL02500
        public IAsyncEnumerable<GSL02500DTO> GSL02500GetCBList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02500DTO>> GSL02500GetCBListAsync(GSL02500ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL02500DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, string.IsNullOrWhiteSpace(poParameter.CDEPT_CODE) ? "" : poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCB_TYPE, string.IsNullOrWhiteSpace(poParameter.CCB_TYPE) ? "" : poParameter.CCB_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBANK_TYPE, string.IsNullOrWhiteSpace(poParameter.CBANK_TYPE) ? "" : poParameter.CBANK_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCURRENCY_CODE, string.IsNullOrWhiteSpace(poParameter.CCURRENCY_CODE) ? "" : poParameter.CCURRENCY_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02500GetCBList),
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
        #endregion

        #region GSL02600
        public IAsyncEnumerable<GSL02600DTO> GSL02600GetCBAccountList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02600DTO>> GSL02600GetCBAccountListAsync(GSL02600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL02600DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, string.IsNullOrWhiteSpace(poParameter.CDEPT_CODE) ? "" : poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCB_CODE, string.IsNullOrWhiteSpace(poParameter.CCB_CODE) ? "" : poParameter.CCB_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCB_TYPE, string.IsNullOrWhiteSpace(poParameter.CCB_TYPE) ? "" : poParameter.CCB_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBANK_TYPE, string.IsNullOrWhiteSpace(poParameter.CBANK_TYPE) ? "" : poParameter.CBANK_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCURRENCY_CODE, string.IsNullOrWhiteSpace(poParameter.CCURRENCY_CODE) ? "" : poParameter.CCURRENCY_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02600GetCBAccountList),
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
        #endregion

        #region GSL02700
        public IAsyncEnumerable<GSL02700DTO> GSL02700GetOtherUnitList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSL02700DTO>> GSL02700GetOtherUnitListAsync(GSL02700ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GSL02700DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, string.IsNullOrWhiteSpace(poParameter.CPROPERTY_ID) ? "" : poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, string.IsNullOrWhiteSpace(poParameter.CBUILDING_ID) ? "" : poParameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LEVENT, poParameter.LEVENT);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSL02700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookup.GSL02700GetOtherUnitList),
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
        #endregion
    }
}