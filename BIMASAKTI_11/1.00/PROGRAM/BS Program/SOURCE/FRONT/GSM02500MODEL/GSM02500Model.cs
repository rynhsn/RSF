using GSM02500COMMON;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02500;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02500Model : R_BusinessObjectServiceClientBase<GSM02500DTO>, IGSM02500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02500";
        private const string DEFAULT_MODULE = "gs";

        public GSM02500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetUnitTypeDTO> GetUnitTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetUnitTypeListDTO> GetUnitTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetUnitTypeDTO> loResult = null;
            GetUnitTypeListDTO loRtn = new GetUnitTypeListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetUnitTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetUnitTypeList),
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

        public IAsyncEnumerable<GetUnitViewDTO> GetUnitViewList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetUnitViewResultDTO> GetUnitViewListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetUnitViewDTO> loResult = null;
            GetUnitViewResultDTO loRtn = new GetUnitViewResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetUnitViewDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetUnitViewList),
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


        public IAsyncEnumerable<GetUnitCategoryDTO> GetUnitCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetUnitCategoryListDTO> GetUnitCategoryListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetUnitCategoryDTO> loResult = null;
            GetUnitCategoryListDTO loRtn = new GetUnitCategoryListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetUnitCategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetUnitCategoryList),
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

        public GetCUOMFromPropertyResultDTO GetCUOMFromProperty(GetCUOMFromPropertyParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GetCUOMFromPropertyDTO> GetCUOMFromPropertyAsync(GetCUOMFromPropertyParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GetCUOMFromPropertyResultDTO loResult = null;
            GetCUOMFromPropertyDTO loRtn = new GetCUOMFromPropertyDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GetCUOMFromPropertyResultDTO, GetCUOMFromPropertyParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetCUOMFromProperty),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public SelectedBuildingResultDTO GetSelectedBuilding(SelectedBuildingParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<SelectedBuildingDTO> GetSelectedBuildingAsync(SelectedBuildingParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SelectedBuildingResultDTO loResult = null;
            SelectedBuildingDTO loRtn = new SelectedBuildingDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SelectedBuildingResultDTO, SelectedBuildingParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetSelectedBuilding),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public SelectedPropertyResultDTO GetSelectedProperty(SelectedPropertyParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<SelectedPropertyDTO> GetSelectedPropertyAsync(SelectedPropertyParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SelectedPropertyResultDTO loResult = null;
            SelectedPropertyDTO loRtn = new SelectedPropertyDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SelectedPropertyResultDTO, SelectedPropertyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetSelectedProperty),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public SelectedFloorResultDTO GetSelectedFloor(SelectedFloorParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<SelectedFloorDTO> GetSelectedFloorAsync(SelectedFloorParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SelectedFloorResultDTO loResult = null;
            SelectedFloorDTO loRtn = new SelectedFloorDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SelectedFloorResultDTO, SelectedFloorParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetSelectedFloor),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public SelectedUnitResultDTO GetSelectedUnit(SelectedUnitParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<SelectedUnitDTO> GetSelectedUnitAsync(SelectedUnitParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SelectedUnitResultDTO loResult = null;
            SelectedUnitDTO loRtn = new SelectedUnitDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SelectedUnitResultDTO, SelectedUnitParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetSelectedUnit),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public SelectedUnitTypeResultDTO GetSelectedUnitType(SelectedUnitTypeParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<SelectedUnitTypeDTO> GetSelectedUnitTypeAsync(SelectedUnitTypeParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SelectedUnitTypeResultDTO loResult = null;
            SelectedUnitTypeDTO loRtn = new SelectedUnitTypeDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SelectedUnitTypeResultDTO, SelectedUnitTypeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02500.GetSelectedUnitType),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loResult.Data;
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
}
