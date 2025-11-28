using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace FAT00800Model
{
    /// <summary>
    /// Model class for FAT00800 List operations - Transaction List functionality
    /// Handles communication with FAT00800ListController
    /// </summary>
    public class FAT00800ListModel : R_BusinessObjectServiceClientBase<FAT00800TransListResultDTO>, IFAT00800List
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00800List";
        private const string DEFAULT_MODULE = "FA";

        public FAT00800ListModel()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region Streaming Methods

        public IAsyncEnumerable<FAT00800TransListResultDTO> FAT00800TransList()
        {
            throw new NotImplementedException();
        }

        public async Task<FAT00800ResultDTO<List<FAT00800TransListResultDTO>>> FAT00800TransListAsync()
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<List<FAT00800TransListResultDTO>> loRtn = new FAT00800ResultDTO<List<FAT00800TransListResultDTO>>();
            List<FAT00800TransListResultDTO> loResult = new List<FAT00800TransListResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00800TransListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800List.FAT00800TransList),
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
                
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #endregion
    }
}
