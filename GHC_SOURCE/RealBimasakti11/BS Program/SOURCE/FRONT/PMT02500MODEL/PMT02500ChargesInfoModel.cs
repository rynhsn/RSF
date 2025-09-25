using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT02500Common.Context;
using PMT02500Common.DTO._4._Charges_Info;
using PMT02500Common.Interface;
using PMT02500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT02500Model
{
    public class PMT02500ChargesInfoModel : R_BusinessObjectServiceClientBase<PMT02500ChargesInfoDetailDTO>, IPMT02500ChargesInfo
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT02500ChargesInfo";
        private const string DEFAULT_MODULE = "PM";

        public PMT02500ChargesInfoModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<PMT02500ChargesInfoHeaderDTO> GetChargesInfoHeaderAsync(PMT02500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT02500ChargesInfoHeaderDTO();
            PMT02500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = new PMT02500GetHeaderParameterDTO()
                    {
                        CPROPERTY_ID = poParameter.CPROPERTY_ID,
                        CDEPT_CODE = poParameter.CDEPT_CODE,
                        CREF_NO = poParameter.CREF_NO,
                        CTRANS_CODE = poParameter.CTRANS_CODE
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02500ChargesInfoHeaderDTO, PMT02500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500ChargesInfo.GetChargesInfoHeader),
                        loParam,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<PMT02500ChargesInfoListDTO>> GetChargesInfoListAsync(PMT02500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT02500ChargesInfoListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CCHARGE_MODE, poParameter.CCHARGE_MODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CBUILDING_ID, poParameter.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CFLOOR_ID, poParameter.CFLOOR_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CUNIT_ID, poParameter.CUNIT_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ChargesInfoListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500ChargesInfo.GetChargesInfoList),
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

        public async Task<List<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>> GetChargesInfoCalUnitListAsync(PMT02500GetHeaderParameterChargesInfoCalUnitDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterChargesInfoCalUnitContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterChargesInfoCalUnitContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterChargesInfoCalUnitContextDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterChargesInfoCalUnitContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterChargesInfoCalUnitContextDTO.CSEQ_NO, poParameter.CSEQ_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500FrontChargesInfo_FeeCalculationDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500ChargesInfo.GetChargesInfoCalUnitList),
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

        public async Task<List<PMT02500ComboBoxDTO>> GetComboBoxDataCFEE_METHODAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500ChargesInfo.GetComboBoxDataCFEE_METHOD),
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

        public async Task<List<PMT02500ComboBoxDTO>> GetComboBoxDataCINVOICE_PERIODAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500ChargesInfo.GetComboBoxDataCINVOICE_PERIOD),
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

        public async Task<PMT02500ChargesInfoResultActiveDTO> ProcessChangeStatusChargesInfoActiveAsync(PMT02500ChargesInfoParameterActiveDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT02500ChargesInfoResultActiveDTO();
            PMT02500ChargesInfoParameterActiveDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CSEQ_NO))
                {
                    loParam = poParameter;

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02500ChargesInfoResultActiveDTO, PMT02500ChargesInfoParameterActiveDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500ChargesInfo.ProcessChangeStatusChargesInfoActive),
                        loParam,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
                else
                {
                    loEx.Add("DevErr", "SEQ_NO Not Supplied");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }


        #region Not Used!

        public PMT02500ChargesInfoHeaderDTO GetChargesInfoHeader(PMT02500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ChargesInfoListDTO> GetChargesInfoList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500FrontChargesInfo_FeeCalculationDetailDTO> GetChargesInfoCalUnitList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ComboBoxDTO> GetComboBoxDataCFEE_METHOD()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ComboBoxDTO> GetComboBoxDataCINVOICE_PERIOD()
        {
            throw new NotImplementedException();
        }

        public PMT02500ChargesInfoResultActiveDTO ProcessChangeStatusChargesInfoActive(PMT02500ChargesInfoParameterActiveDTO poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}