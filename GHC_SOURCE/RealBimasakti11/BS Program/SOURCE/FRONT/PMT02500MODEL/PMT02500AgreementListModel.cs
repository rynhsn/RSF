using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT02500Common.Context;
using PMT02500Common.DTO._1._AgreementList;
using PMT02500Common.DTO._1._AgreementList.Upload;
using PMT02500Common.Interface;
using PMT02500Common.Utilities;
using PMT02500Common.Utilities.Db;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT02500Model
{
    public class PMT02500AgreementListModel : R_BusinessObjectServiceClientBase<PMT02500BlankDTO>, IPMT02500AgreementList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT02500AgreementList";
        private const string DEFAULT_MODULE = "PM";

        public PMT02500AgreementListModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                pcModule,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<PMT02500AgreementListOriginalDTO>> GetAgreementListAsync(PMT02500UtilitiesParameterDbGetAgreementListDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT02500AgreementListOriginalDTO>? loResult = null;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CTRANS_CODE) && !string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    R_FrontContext.R_SetStreamingContext(PMT02500GetAgreementListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT02500GetAgreementListContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500AgreementListOriginalDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500AgreementList.GetAgreementList),
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
                else
                {
                    loResult = new List<PMT02500AgreementListOriginalDTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        /*
        public async Task<PMT02500SelectedAgreementGetUnitDescriptionDTO> GetSelectedAgreementGetUnitDescriptionAsync(PMT02500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT02500SelectedAgreementGetUnitDescriptionDTO();
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
                        CTRANS_CODE = poParameter.CTRANS_CODE,
                        CBUILDING_ID = poParameter.CBUILDING_ID
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02500SelectedAgreementGetUnitDescriptionDTO, PMT02500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500AgreementList.GetSelectedAgreementGetUnitDescription),
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
        */

        public async Task<List<PMT02500UnitListOriginalDTO>> GetUnitListAsync(PMT02500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT02500UnitListOriginalDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT02500GetHeaderParameterContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500UnitListOriginalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500AgreementList.GetUnitList),
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

        public async Task<List<PMT02500PropertyListDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500PropertyListDTO>? loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500AgreementList.GetPropertyList),
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

        public async Task<List<PMT02500VarGsmTransactionCodeDTO>> GetVarGsmTransactionCodeAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500VarGsmTransactionCodeDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500AgreementList.GetVarGsmTransactionCode),
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

        public async Task<PMT02500ProcessResultDTO> ProcessChangeStatusAsync(PMT02500ChangeStatusParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            var loResult = new PMT02500ProcessResultDTO();
            PMT02500ChangeStatusParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poEntity.CPROPERTY_ID))
                {
                    loParam = poEntity;

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02500ProcessResultDTO, PMT02500ChangeStatusParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500AgreementList.ProcessChangeStatus),
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

        public async Task<PMT02500ChangeStatusDTO> GetChangeStatusAsync(PMT02500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT02500ChangeStatusDTO();
            PMT02500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = poParameter;

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02500ChangeStatusDTO, PMT02500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT02500AgreementList.GetChangeStatus),
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

        public async Task<List<PMT02500ComboBoxDTO>> GetComboBoxDataCTransStatusAsync()
        {
            var loEx = new R_Exception();
            List<PMT02500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500AgreementList.GetComboBoxDataCTransStatus),
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

        public async Task<PMT02500TemplateFileUploadDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            PMT02500TemplateFileUploadDTO loResult = new PMT02500TemplateFileUploadDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02500TemplateFileUploadDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02500AgreementList.DownloadTemplateFile),
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

        #region NotUsed

        public IAsyncEnumerable<PMT02500AgreementListOriginalDTO> GetAgreementList()
        {
            throw new NotImplementedException();
        }

        public PMT02500ChangeStatusDTO GetChangeStatus(PMT02500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500ComboBoxDTO> GetComboBoxDataCTransStatus()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500PropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500VarGsmTransactionCodeDTO> GetVarGsmTransactionCode()
        {
            throw new NotImplementedException();
        }

        public PMT02500ProcessResultDTO ProcessChangeStatus(PMT02500ChangeStatusParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public PMT02500SelectedAgreementGetUnitDescriptionDTO GetSelectedAgreementGetUnitDescription(PMT02500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02500UnitListOriginalDTO> GetUnitList()
        {
            throw new NotImplementedException();
        }

        public PMT02500TemplateFileUploadDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}