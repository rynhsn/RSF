﻿using Lookup_GSCOMMON;
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
        public async Task<GSL01500ResultDetailDTO> GSL01500GetCashDetailAsync(GSL01500ParameterDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL01500ResultDetailDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL01500ResultDetailDTO>, GSL01500ParameterDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL01500GetCashDetail),
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
        public async Task<GSL02000CityDTO> GSL02000GetCityGeographyAsync(GSL02000ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02000CityDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02000CityDTO>, GSL02000ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02000GetCityGeography),
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
        public async Task<GSL02510DTO> GSL02510GetCashBankAsync(GSL02510ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02510DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02510DTO>, GSL02510ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02510GetCashBank),
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
        public async Task<GSL02700DTO> GSL02700GetOtherUnitAsync(GSL02700ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02700DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02700DTO>, GSL02700ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02700GetOtherUnit),
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
        public async Task<GSL02800DTO> GSL02800GetOtherUnitMasterAsync(GSL02800ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02800DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02800DTO>, GSL02800ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02800GetOtherUnitMaster),
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
        public async Task<GSL02900DTO> GSL02900GetSupplierAsync(GSL02900ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02900DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02900DTO>, GSL02900ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02900GetSupplier),
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
        public async Task<GSL02910DTO> GSL02910GetSupplierInfoAsync(GSL02910ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL02910DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL02910DTO>, GSL02910ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL02910GetSupplierInfo),
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
        public async Task<GSL03010DTO> GSL03010GetProductUnitAsync(GSL03010ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03010DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03010DTO>, GSL03010ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03010GetProductUnit),
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
        public async Task<GSL03020DTO> GSL03020GetProductUOMAsync(GSL03020ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03020DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03020DTO>, GSL03020ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03020GetProductUOM),
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
        public async Task<GSL03000DTO> GSL03000GetProductAsync(GSL03000ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03000DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03000DTO>, GSL03000ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03000GetProduct),
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
        public async Task<GSL03100DTO> GSL03100GetExpenditureAsync(GSL03100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03100DTO>, GSL03100ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03100GetExpenditure),
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
        public async Task<GSL03200DTO> GSL03200GetProductAllocationAsync(GSL03200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03200DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03200DTO>, GSL03200ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03200GetProductAllocation),
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
        public async Task<GSL03300DTO> GSL03300GetTaxChargesAsync(GSL03300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03300DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03300DTO>, GSL03300ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03300GetTaxCharges),
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
        public async Task<GSL03400DTO> GSL03400GetDigitalSignAsync(GSL03400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03400DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03400DTO>, GSL03400ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03400GetDigitalSign),
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
        public async Task<GSL03500DTO> GSL03500GetWarehouseAsync(GSL03500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03500DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03500DTO>, GSL03500ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03500GetWarehouse),
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
        public async Task<GSL03600DTO> GSL03600GetCompanyAsync(GSL03600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03600DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03600DTO>, GSL03600ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03600GetCompany),
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
        public async Task<GSL03700DTO> GSL03700GetMessageAsync(GSL03700ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL03700DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSLGenericRecord<GSL03700DTO>, GSL03700ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicRecordLookup.GSL03700GetMessage),
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
        public GSLGenericRecord<GSL01500ResultDetailDTO> GSL01500GetCashDetail(GSL01500ParameterDetailDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02700DTO> GSL02700GetOtherUnit(GSL02700ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02800DTO> GSL02800GetOtherUnitMaster(GSL02800ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02510DTO> GSL02510GetCashBank(GSL02510ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02900DTO> GSL02900GetSupplier(GSL02900ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02910DTO> GSL02910GetSupplierInfo(GSL02910ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03000DTO> GSL03000GetProduct(GSL03000ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03100DTO> GSL03100GetExpenditure(GSL03100ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03200DTO> GSL03200GetProductAllocation(GSL03200ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03300DTO> GSL03300GetTaxCharges(GSL03300ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL02000CityDTO> GSL02000GetCityGeography(GSL02000ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03400DTO> GSL03400GetDigitalSign(GSL03400ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03500DTO> GSL03500GetWarehouse(GSL03500ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03600DTO> GSL03600GetCompany(GSL03600ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03010DTO> GSL03010GetProductUnit(GSL03010ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03020DTO> GSL03020GetProductUOM(GSL03020ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public GSLGenericRecord<GSL03700DTO> GSL03700GetMessage(GSL03700ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}