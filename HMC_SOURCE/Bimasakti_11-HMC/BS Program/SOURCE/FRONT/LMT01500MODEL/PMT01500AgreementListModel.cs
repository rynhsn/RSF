using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT01500Common.Context;
using PMT01500Common.DTO._1._AgreementList;
using PMT01500Common.DTO._1._AgreementList.Upload;
using PMT01500Common.Interface;
using PMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT01500Model
{
    public class PMT01500AgreementListModel : R_BusinessObjectServiceClientBase<PMT01500BlankDTO>, IPMT01500AgreementList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01500AgreementList";
        private const string DEFAULT_MODULE = "PM";

        public PMT01500AgreementListModel(
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

        public async Task<List<PMT01500AgreementListOriginalDTO>> GetAgreementListAsync(string pcCPROPERTY_ID)
        {
            var loEx = new R_Exception();
            List<PMT01500AgreementListOriginalDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01500GetAgreementListContextDTO.CPROPERTY_ID, pcCPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500AgreementListOriginalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500AgreementList.GetAgreementList),
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
        
        /*
        public async Task<PMT01500SelectedAgreementGetUnitDescriptionDTO> GetSelectedAgreementGetUnitDescriptionAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01500SelectedAgreementGetUnitDescriptionDTO();
            PMT01500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = new PMT01500GetHeaderParameterDTO()
                    {
                        CPROPERTY_ID = poParameter.CPROPERTY_ID,
                        CDEPT_CODE = poParameter.CDEPT_CODE,
                        CREF_NO = poParameter.CREF_NO,
                        CTRANS_CODE = poParameter.CTRANS_CODE,
                        CBUILDING_ID = poParameter.CBUILDING_ID
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01500SelectedAgreementGetUnitDescriptionDTO, PMT01500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01500AgreementList.GetSelectedAgreementGetUnitDescription),
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

        public async Task<List<PMT01500UnitListOriginalDTO>> GetUnitListAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT01500UnitListOriginalDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500UnitListOriginalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500AgreementList.GetUnitList),
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

        public async Task<List<PMT01500PropertyListDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PMT01500PropertyListDTO>? loResult = null;

            try
            {

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500AgreementList.GetPropertyList),
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

        public async Task<List<PMT01500VarGsmTransactionCodeDTO>> GetVarGsmTransactionCodeAsync()
        {
            var loEx = new R_Exception();
            List<PMT01500VarGsmTransactionCodeDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500AgreementList.GetVarGsmTransactionCode),
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

        public async Task<PMT01500ProcessResultDTO> ProcessChangeStatusAsync(PMT01500ChangeStatusParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01500ProcessResultDTO();
            PMT01500ChangeStatusParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poEntity.CPROPERTY_ID))
                {
                    loParam = poEntity;

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01500ProcessResultDTO, PMT01500ChangeStatusParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01500AgreementList.ProcessChangeStatus),
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

        public async Task<PMT01500ChangeStatusDTO> GetChangeStatusAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01500ChangeStatusDTO();
            PMT01500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = poParameter;

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01500ChangeStatusDTO, PMT01500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01500AgreementList.GetChangeStatus),
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

        public async Task<List<PMT01500ComboBoxDTO>> GetComboBoxDataCTransStatusAsync()
        {
            var loEx = new R_Exception();
            List<PMT01500ComboBoxDTO>? loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500ComboBoxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500AgreementList.GetComboBoxDataCTransStatus),
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

        public async Task<PMT01500UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            PMT01500UploadFileDTO loResult = new PMT01500UploadFileDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01500UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500AgreementList.DownloadTemplateFile),
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

        public IAsyncEnumerable<PMT01500AgreementListOriginalDTO> GetAgreementList()
        {
            throw new NotImplementedException();
        }

        public PMT01500ChangeStatusDTO GetChangeStatus(PMT01500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCTransStatus()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01500PropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01500VarGsmTransactionCodeDTO> GetVarGsmTransactionCode()
        {
            throw new NotImplementedException();
        }

        public PMT01500ProcessResultDTO ProcessChangeStatus(PMT01500ChangeStatusParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public PMT01500SelectedAgreementGetUnitDescriptionDTO GetSelectedAgreementGetUnitDescription(PMT01500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01500UnitListOriginalDTO> GetUnitList()
        {
            throw new NotImplementedException();
        }

        public PMT01500UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }
        
        #endregion

    }
}