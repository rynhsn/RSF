using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT02500Common.Context;
using PMT02500Common.DTO._3._Unit_Info;
using PMT02500Common.Interface;
using PMT02500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT02500Model
{
    public class PMT02500UnitInfo_UtilitiesModel : R_BusinessObjectServiceClientBase<PMT02500UnitInfoUnit_UtilitiesDetailDTO>, IPMT02500UnitInfo_Utilities
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT02500UnitInfo_Utilities";
        private const string DEFAULT_MODULE = "PM";

        public PMT02500UnitInfo_UtilitiesModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<PMT02500UnitInfoUnit_UtilitiesListDTO>> GetUnitInfoListAsync(PMT02500GetUnitInfo_UtilitiesParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT02500UnitInfoUnit_UtilitiesListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT02500GetUnitInfo_UtilitiesParameterContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetUnitInfo_UtilitiesParameterContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetUnitInfo_UtilitiesParameterContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetUnitInfo_UtilitiesParameterContextDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT02500GetUnitInfo_UtilitiesParameterContextDTO.CUNIT_ID, poParameter.CUNIT_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetUnitInfo_UtilitiesParameterContextDTO.CFLOOR_ID, poParameter.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetUnitInfo_UtilitiesParameterContextDTO.CBUILDING_ID, poParameter.CBUILDING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500UnitInfoUnit_UtilitiesListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500UnitInfo_Utilities.GetUnitInfoList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<PMT02500ComboBoxDTO>> GetComboBoxDataCCHARGES_TYPEAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500UnitInfo_Utilities.GetComboBoxDataCCHARGES_TYPE),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<PMT02500ComboBoxStartInvoicePeriodYearDTO>> GetComboBoxDataCSTART_INV_PRDYearAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxStartInvoicePeriodYearDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxStartInvoicePeriodYearDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500UnitInfo_Utilities.GetComboBoxDataCSTART_INV_PRDYear),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<PMT02500ComboBoxStartInvoicePeriodMonthDTO>> GetComboBoxDataCSTART_INV_PRDMonthAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxStartInvoicePeriodMonthDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxStartInvoicePeriodMonthDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500UnitInfo_Utilities.GetComboBoxDataCSTART_INV_PRDMonth),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<PMT02500ComboBoxCMeterNoDTO>> GetComboBoxDataCMETER_NOAsync(PMT02500GetUnitInfo_UtilitiesCMeterNoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxCMeterNoDTO>? loResult = null;
            PMT02500GetUnitInfo_UtilitiesCMeterNoParameterDTO? loParam;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = new PMT02500GetUnitInfo_UtilitiesCMeterNoParameterDTO()
                    {
                        CPROPERTY_ID = poParameter.CPROPERTY_ID,
                        CBUILDING_ID = poParameter.CBUILDING_ID,
                        CFLOOR_ID = poParameter.CFLOOR_ID,
                        CUNIT_ID = poParameter.CUNIT_ID,
                        CUTILITY_TYPE = poParameter.CUTILITY_TYPE,
                    };

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxCMeterNoDTO, PMT02500GetUnitInfo_UtilitiesCMeterNoParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500UnitInfo_Utilities.GetComboBoxDataCMETER_NO),
                        loParam,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
                else
                {
                    loEx.Add("", "No Property ID Supplied!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        #region Not Used!
        public IAsyncEnumerable<PMT02500ComboBoxDTO> GetComboBoxDataCCHARGES_TYPE()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500UnitInfoUnit_UtilitiesListDTO> GetUnitInfoList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ComboBoxStartInvoicePeriodYearDTO> GetComboBoxDataCSTART_INV_PRDYear()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ComboBoxStartInvoicePeriodMonthDTO> GetComboBoxDataCSTART_INV_PRDMonth()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ComboBoxCMeterNoDTO> GetComboBoxDataCMETER_NO(PMT02500GetUnitInfo_UtilitiesCMeterNoParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}