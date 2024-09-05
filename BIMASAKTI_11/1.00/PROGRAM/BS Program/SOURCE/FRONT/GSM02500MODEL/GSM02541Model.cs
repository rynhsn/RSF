using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02541Model : R_BusinessObjectServiceClientBase<GSM02541ParameterDTO>, IGSM02541
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02541";
        private const string DEFAULT_MODULE = "gs";

        public GSM02541Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateOtherUnitDTO DownloadTemplateOtherUnit()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateOtherUnitDTO> DownloadTemplateOtherUnitAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateOtherUnitDTO loResult = new TemplateOtherUnitDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateOtherUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02541.DownloadTemplateOtherUnit),
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

        public IAsyncEnumerable<BuildingDTO> GetBuildingList()
        {
            throw new NotImplementedException();
        }

        public async Task<BuildingResultDTO> GetBuildingListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<BuildingDTO> loResult = null;
                BuildingResultDTO loRtn = new BuildingResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<BuildingDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02541.GetBuildingList),
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
        }

        public IAsyncEnumerable<FloorDTO> GetFloorList()
        {
            throw new NotImplementedException();
        }

        public async Task<FloorResultDTO> GetFloorListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<FloorDTO> loResult = null;
                FloorResultDTO loRtn = new FloorResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FloorDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02541.GetFloorList),
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
        }

        public IAsyncEnumerable<GSM02541DTO> GetOtherUnitList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02541ResultDTO> GetOtherUnitListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<GSM02541DTO> loResult = null;
                GSM02541ResultDTO loRtn = new GSM02541ResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02541DTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02541.GetOtherUnitList),
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
        }

        public IAsyncEnumerable<OtherUnitTypeDTO> GetOtherUnitTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<OtherUnitTypeResultDTO> GetOtherUnitTypeListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<OtherUnitTypeDTO> loResult = null;
                OtherUnitTypeResultDTO loRtn = new OtherUnitTypeResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<OtherUnitTypeDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02541.GetOtherUnitTypeList),
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
        }

        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02541.RSP_GS_ACTIVE_INACTIVE_OTHER_UNITMethod),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
