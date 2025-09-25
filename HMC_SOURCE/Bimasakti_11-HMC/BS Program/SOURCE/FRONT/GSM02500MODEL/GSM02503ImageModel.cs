using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs;

namespace GSM02500MODEL
{
    public class GSM02503ImageModel : R_BusinessObjectServiceClientBase<GSM02503ImageParameterDTO>, IGSM02503Image
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02503Image";
        private const string DEFAULT_MODULE = "gs";

        public GSM02503ImageModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM02503ImageDTO> GetUnitTypeImageList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02503ImageListDTO> GetUnitTypeImageListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02503ImageDTO> loResult = null;
            GSM02503ImageListDTO loRtn = new GSM02503ImageListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02503ImageDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02503Image.GetUnitTypeImageList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public ShowUnitTypeImageResultDTO ShowUnitTypeImage(ShowUnitTypeImageParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<ShowUnitTypeImageResultDTO> ShowUnitTypeImageAsync(ShowUnitTypeImageParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            ShowUnitTypeImageResultDTO loRtn = new ShowUnitTypeImageResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<ShowUnitTypeImageResultDTO, ShowUnitTypeImageParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02503Image.ShowUnitTypeImage),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
