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
    public class PublicLookupRecordModel : R_BusinessObjectServiceClientBase<GSL00500DTO>, IPublicRecordLookup
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupGSRecord";
        private const string DEFAULT_MODULE = "GS";

        public PublicLookupRecordModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<GSL00100DTO> GSL00100GetSalesTaxAsync(GSL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00100DTO>, GSL00100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00100GetSalesTax),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00110DTO> GSL00110GetTaxByDateAsync(GSL00110ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00110DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00110DTO>, GSL00110ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00110GetTaxByDate),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00200DTO> GSL00200GetWithholdingTaxAsync(GSL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00200DTO>, GSL00200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00200GetWithholdingTax),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00300DTO> GSL00300GetCurrencyAsync(GSL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00300DTO>, GSL00300ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00300GetCurrency),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00400DTO> GSL00400GetJournalGroupAsync(GSL00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00400DTO>, GSL00400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00400GetJournalGroup),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00500DTO> GSL00500GetGLAccountAsync(GSL00500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00500DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00500DTO>, GSL00500ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00500GetGLAccount),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00510DTO> GSL00510GetCOAAsync(GSL00510ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00510DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00510DTO>, GSL00510ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00510GetCOA),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00520DTO> GSL00520GetGOACOAAsync(GSL00520ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00520DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00520DTO>, GSL00520ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00520GetGOACOA),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00550DTO> GSL00550GetGOAAsync(GSL00550ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00550DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00550DTO>, GSL00550ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00550GetGOA),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00600DTO> GSL00600GetUnitTypeCategoryAsync(GSL00600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00600DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00600DTO>, GSL00600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00600GetUnitTypeCategory),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00700DTO> GSL00700GetDepartmentAsync(GSL00700ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00700DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00700DTO>, GSL00700ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00700GetDepartment),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00710DTO> GSL00710GetDepartmentPropertyAsync(GSL00710ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00710DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00710DTO>, GSL00710ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00710GetDepartmentProperty),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00800DTO> GSL00800GetCurrencyTypeAsync(GSL00800ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00800DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00800DTO>, GSL00800ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00800GetCurrencyType),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL00900DTO> GSL00900GetCenterAsync(GSL00900ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00900DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL00900DTO>, GSL00900ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL00900GetCenter),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01000DTO> GSL01000GetUserAsync(GSL01000ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01000DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01000DTO>, GSL01000ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01000GetUser),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01100DTO> GSL01100GetUserApprovalAsync(GSL01100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01100DTO>, GSL01100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01100GetUserApproval),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01200DTO> GSL01200GetBankAsync(GSL01200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01200DTO>, GSL01200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01200GetBank),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01300DTO> GSL01300GetBankAccountAsync(GSL01300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01300DTO>, GSL01300ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01300GetBankAccount),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01400DTO> GSL01400GetOtherChargesAsync(GSL01400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01400DTO>, GSL01400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01400GetOtherCharges),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01600DTO> GSL01600GetCashFlowGroupTypeAsync(GSL01600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01600DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01600DTO>, GSL01600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01600GetCashFlowGroupType),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01800DTO> GSL01800GetCategoryAsync(GSL01800DTOParameter poEntity)
        {
            var loEx = new R_Exception();
            GSL01800DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01800DTO>, GSL01800DTOParameter>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01800GetCategory),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL01900DTO> GSL01900GetLOBAsync(GSL01900ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01900DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01900DTO>, GSL01900ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01900GetLOB),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL02100DTO> GSL02100GetPaymentTermAsync(GSL02100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02100DTO>, GSL02100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02100GetPaymentTerm),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL02200DTO> GSL02200GetBuildingAsync(GSL02200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02200DTO>, GSL02200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02200GetBuilding),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL02300DTO> GSL02300GetBuildingUnitAsync(GSL02300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02300DTO>, GSL02300ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02300GetBuildingUnit),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL02400DTO> GSL02400GetFloorAsync(GSL02400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02400DTO>, GSL02400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02400GetFloor),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL02500DTO> GSL02500GetCBAsync(GSL02500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02500DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02500DTO>, GSL02500ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02500GetCB),
                    poEntity,
                    DEFAULT_MODULE,
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

        public async Task<GSL02600DTO> GSL02600GetCBAccountAsync(GSL02600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02600DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02600DTO>, GSL02600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02600GetCBAccount),
                    poEntity,
                    DEFAULT_MODULE,
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

        #region Not Implement
        public GSLGenericRecord<GSL00100DTO> GSL00100GetSalesTax(GSL00100ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00110DTO> GSL00110GetTaxByDate(GSL00110ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00200DTO> GSL00200GetWithholdingTax(GSL00200ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00300DTO> GSL00300GetCurrency(GSL00300ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00400DTO> GSL00400GetJournalGroup(GSL00400ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00500DTO> GSL00500GetGLAccount(GSL00500ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00510DTO> GSL00510GetCOA(GSL00510ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00520DTO> GSL00520GetGOACOA(GSL00520ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00550DTO> GSL00550GetGOA(GSL00550ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00600DTO> GSL00600GetUnitTypeCategory(GSL00600ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00700DTO> GSL00700GetDepartment(GSL00700ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00710DTO> GSL00710GetDepartmentProperty(GSL00710ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00800DTO> GSL00800GetCurrencyType(GSL00800ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL00900DTO> GSL00900GetCenter(GSL00900ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01000DTO> GSL01000GetUser(GSL01000ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01100DTO> GSL01100GetUserApproval(GSL01100ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01200DTO> GSL01200GetBank(GSL01200ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01300DTO> GSL01300GetBankAccount(GSL01300ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01400DTO> GSL01400GetOtherCharges(GSL01400ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01600DTO> GSL01600GetCashFlowGroupType(GSL01600ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01800DTO> GSL01800GetCategory(GSL01800DTOParameter poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL01900DTO> GSL01900GetLOB(GSL01900ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02100DTO> GSL02100GetPaymentTerm(GSL02100ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02200DTO> GSL02200GetBuilding(GSL02200ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02300DTO> GSL02300GetBuildingUnit(GSL02300ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02400DTO> GSL02400GetFloor(GSL02400ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02500DTO> GSL02500GetCB(GSL02500ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02600DTO> GSL02600GetCBAccount(GSL02600ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}