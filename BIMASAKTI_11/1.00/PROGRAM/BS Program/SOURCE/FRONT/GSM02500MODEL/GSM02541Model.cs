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

        public TemplateUnitPromotionDTO DownloadTemplateUnitPromotion()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateUnitPromotionDTO> DownloadTemplateUnitPromotionAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitPromotionDTO loResult = new TemplateUnitPromotionDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateUnitPromotionDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02541.DownloadTemplateUnitPromotion),
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

        public IAsyncEnumerable<GSM02541DTO> GetUnitPromotionList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02541ResultDTO> GetUnitPromotionListStreamAsync()
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
                        nameof(IGSM02541.GetUnitPromotionList),
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

        public IAsyncEnumerable<UnitPromotionTypeDTO> GetUnitPromotionTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<UnitPromotionTypeResultDTO> GetUnitPromotionTypeListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<UnitPromotionTypeDTO> loResult = null;
                UnitPromotionTypeResultDTO loRtn = new UnitPromotionTypeResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UnitPromotionTypeDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02541.GetUnitPromotionTypeList),
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

        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02541.RSP_GS_ACTIVE_INACTIVE_UNIT_PROMOTIONMethod),
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
