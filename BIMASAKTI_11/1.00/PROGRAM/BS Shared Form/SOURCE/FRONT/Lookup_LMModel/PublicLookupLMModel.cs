using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lookup_PMCOMMON;
using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_APIClient;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMCOMMON.DTOs.LML01400;
using Lookup_PMCOMMON.DTOs.LML01500;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01700;
using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.LML01900;
using Lookup_PMCOMMON.DTOs.UtilityDTO;

namespace Lookup_PMModel
{
    public class PublicLookupLMModel : R_BusinessObjectServiceClientBase<LML00200DTO>, IPublicLookupLM
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PublicLookupLM";
        private const string DEFAULT_MODULE = "PM";

        public PublicLookupLMModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region implements INTERFACE
        public IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00300DTO> LML00300SupervisorList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00500DTO> LML00500SalesmanList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML00600DTO> LML00600TenantList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML00700DTO> LML00700DiscountList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00800DTO> LML00800AgreementList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML00900DTO> LML00900TransactionList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML01200DTO> LML01200InvoiceGroupList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML01000DTO> LML01000BillingRuleList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML01100DTO> LML01100TNCList()
        {
            throw new NotImplementedException();
        }
        public LML00900InitialProcessDTO InitialProcess()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML01300DTO> LML01300LOIAgreementList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML01400DTO> LML01400AgreementUnitChargesList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML01500DTO> LML01500SLACategoryList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML01600DTO> LML01600SLACallTypeList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML01700DTO> LML01700CancelReceiptFromCustomerList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML01700DTO> LML01700PrerequisiteCustReceiptList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LML01800DTO> LML01800UnitTenantList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LML01900DTO> LML01900StaffList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> PropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<BuildingDTO> BuildingList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<FloorDTO> FloorList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<UnitDTO> UnitList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<DepartmentDTO> DepartmentList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<SupervisorDTO> SupervisorList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<StaffTypeDTO> StaffTypeList()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region LML00200 

        public async Task<LMLGenericList<LML00200DTO>> LML00200GetUnitChargesListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00200DTO> loResult = new LMLGenericList<LML00200DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00200UnitChargesList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00300 

        public async Task<LMLGenericList<LML00300DTO>> LML00300SupervisorListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00300DTO> loResult = new LMLGenericList<LML00300DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00300SupervisorList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00400
        public async Task<LMLGenericList<LML00400DTO>> LML00400GetUtilityChargesListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00400DTO> loResult = new LMLGenericList<LML00400DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00400UtilityChargesList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00500
        public async Task<LMLGenericList<LML00500DTO>> LML00500GetSalesmanListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00500DTO> loResult = new LMLGenericList<LML00500DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00500SalesmanList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML00600
        public async Task<LMLGenericList<LML00600DTO>> LML00600GetTenantListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00600DTO> loResult = new LMLGenericList<LML00600DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00600TenantList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion      
        #region LML00700
        public async Task<LMLGenericList<LML00700DTO>> LML00700GetDiscountListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00700DTO> loResult = new LMLGenericList<LML00700DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00700DiscountList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML00800
        public async Task<LMLGenericList<LML00800DTO>> LML00800GetAgreementListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00800DTO> loResult = new LMLGenericList<LML00800DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00800DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00800AgreementList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region LML00900
        public async Task<LMLGenericList<LML00900DTO>> LML00900GetTransactionListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00900DTO> loResult = new LMLGenericList<LML00900DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML00900DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML00900TransactionList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01000
        public async Task<LMLGenericList<LML01000DTO>> LML01000GetBillingRuleListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01000DTO> loResult = new LMLGenericList<LML01000DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01000BillingRuleList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01100
        public async Task<LMLGenericList<LML01100DTO>> LML01100GetTermNConditionListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01100DTO> loResult = new LMLGenericList<LML01100DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01100TNCList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01200
        public async Task<LMLGenericList<LML01200DTO>> PML01200InvoiceGroupListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01200DTO> loResult = new LMLGenericList<LML01200DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01200InvoiceGroupList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01300
        public async Task<LMLGenericList<LML01300DTO>> LML01300LOIAgreementListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01300DTO> loResult = new LMLGenericList<LML01300DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01300LOIAgreementList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01400
        public async Task<LMLGenericList<LML01400DTO>> LML01400AgreementUnitChargesListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01400DTO> loResult = new LMLGenericList<LML01400DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01400AgreementUnitChargesList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01500
        public async Task<LMLGenericList<LML01500DTO>> LML01500SLACategoryListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01500DTO> loResult = new LMLGenericList<LML01500DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01500SLACategoryList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01600
        public async Task<LMLGenericList<LML01600DTO>> LML01600SLACallTypeListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01600DTO> loResult = new LMLGenericList<LML01600DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01600SLACallTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01700
        public async Task<LMLGenericList<LML01700DTO>> LML01700CancelReceiptFromCustomerAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01700DTO> loResult = new LMLGenericList<LML01700DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01700CancelReceiptFromCustomerList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMLGenericList<LML01700DTO>> LML01700PrerequisiteCustReceiptAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01700DTO> loResult = new LMLGenericList<LML01700DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01700PrerequisiteCustReceiptList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01800
        public async Task<LMLGenericList<LML01800DTO>> LML01800UnitTenantListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01800DTO> loResult = new LMLGenericList<LML01800DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01800DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01800UnitTenantList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion
        #region LML01900
        public async Task<LMLGenericList<LML01900DTO>> LML01900StaffListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<LML01900DTO> loResult = new LMLGenericList<LML01900DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LML01900DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.LML01900StaffList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        #endregion

        #region Utility
        public async Task<LML00900InitialProcessDTO> GetInitialProcessAsyncModel()
        {
            var loEx = new R_Exception();
            LML00900InitialProcessDTO loResult = new LML00900InitialProcessDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LML00900InitialProcessDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.InitialProcess),
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
        public async Task<LMLGenericList<PropertyDTO>> PropertyListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<PropertyDTO> loResult = new LMLGenericList<PropertyDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.PropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMLGenericList<BuildingDTO>> BuildingListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<BuildingDTO> loResult = new LMLGenericList<BuildingDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<BuildingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.BuildingList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMLGenericList<FloorDTO>> FloorListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<FloorDTO> loResult = new LMLGenericList<FloorDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FloorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.FloorList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMLGenericList<UnitDTO>> UnitListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<UnitDTO> loResult = new LMLGenericList<UnitDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.UnitList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMLGenericList<DepartmentDTO>> DepartmentListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<DepartmentDTO> loResult = new LMLGenericList<DepartmentDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<DepartmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.DepartmentList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMLGenericList<SupervisorDTO>> SupervisorListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<SupervisorDTO> loResult = new LMLGenericList<SupervisorDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<SupervisorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.SupervisorList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMLGenericList<StaffTypeDTO>> StaffTypeListAsync()
        {
            var loEx = new R_Exception();
            LMLGenericList<StaffTypeDTO> loResult = new LMLGenericList<StaffTypeDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<StaffTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPublicLookupLM.StaffTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
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
