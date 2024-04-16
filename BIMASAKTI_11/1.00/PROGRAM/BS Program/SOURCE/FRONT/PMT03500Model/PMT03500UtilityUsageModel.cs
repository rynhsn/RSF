using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT03500Common;
using PMT03500Common.DTOs;
using PMT03500Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT03500Model
{
    public class PMT03500UtilityUsageModel : R_BusinessObjectServiceClientBase<PMT03500BuildingDTO>, IPMT03500UtilityUsage
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMT03500UtilityUsage";
        private const string DEFAULT_MODULE = "lm";

        public PMT03500UtilityUsageModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
            plSendWithContext,
            plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMT03500BuildingDTO> LMT03500GetBuildingListStream()
        {
            throw new NotImplementedException();
        }

        public PMT03500SingleDTO<PMT03500BuildingDTO> LMT03500GetBuildingRecord(PMT03500SearchTextDTO poText)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT03500UtilityUsageDTO> LMT03500GetUtilityUsageListStream()
        {
            throw new NotImplementedException();
        }

        public PMT03500SingleDTO<PMT03500UtilityUsageDetailDTO> LMT03500GetUtilityUsageDetail(PMT03500UtilityUsageDetailParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500ListDTO<PMT03500FunctDTO> LMT03500GetUtilityTypeList()
        {
            throw new NotImplementedException();
        }

        public PMT03500ListDTO<PMT03500FloorDTO> LMT03500GetFloorList(PMT03500FloorParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500ListDTO<PMT03500YearDTO> LMT03500GetYearList()
        {
            throw new NotImplementedException();
        }

        public PMT03500ListDTO<PMT03500PeriodDTO> LMT03500GetPeriodList(PMT03500PeriodParam poParam)
        {
            throw new NotImplementedException();
        }

        public PMT03500ExcelDTO LMT03500DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }


        //Untuk fetch data streaming dari controller
        public async Task<List<T>> GetListStreamAsync<T>(string pcNameOf)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        
        //Untuk fetch data streaming dari controller dengan parameter
        public async Task<List<T>> GetListStreamAsync<T, T1>(string pcNameOf, T1 poParameter)
        {
            var loEx = new R_Exception();
            List<T> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        //Untuk fetch data object dari controller
        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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

        //Untuk fetch data object dari controller dengan parameter
        public async Task<T> GetAsync<T, T1>(string pcNameOf, T1 poParameter) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
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
        
        //Untuk execute query non object (T hanya syarat untuk memenuhi generic)
        public async Task ExecuteAsync<T, T1>(string pcNameOf, T1 poParameter) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}