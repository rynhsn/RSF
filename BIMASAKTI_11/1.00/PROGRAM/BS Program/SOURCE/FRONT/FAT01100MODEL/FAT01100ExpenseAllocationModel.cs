using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using FAT01100Common;
using FAT01100Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT01100Model
{
    public class FAT01100ExpenseAllocationModel : R_BusinessObjectServiceClientBase<FAT01100ExpenseAllocationDTO>, IFAT01100ExpenseAllocation
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT01100ExpenseAllocation";
        private const string DEFAULT_MODULE = "FA";

        public FAT01100ExpenseAllocationModel()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        // IMPORTANT RULE: DO NOT IMPLEMENT BUSINESS OBJECT FUNCTIONS from R_IServiceCRUDAsyncBase<FAT01100ExpenseAllocationDTO>, IT IS ALREADY IMPLEMENTED BY R_BUSINESSOBJECTSERVICECLIENTBASE<FAT01100ExpenseAllocationDTO>.

        #region RSP_FA_GET_ASSET_EXP_ALLOC_LIST
       

        public IAsyncEnumerable<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO> RSP_FA_GET_ASSET_EXP_ALLOC_LIST()
        {
            throw new NotImplementedException();
        }

        public async Task<FAT01100ResultDTO<List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>>> RSP_FA_GET_ASSET_EXP_ALLOC_LISTAsync()
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>> loRtn = new FAT01100ResultDTO<List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100ExpenseAllocation.RSP_FA_GET_ASSET_EXP_ALLOC_LIST),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #endregion

        #region RSP_FA_GET_TRANS_EXP_ALLOC_LIST
        

        public async Task<FAT01100ResultDTO<List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>>> RSP_FA_GET_TRANS_EXP_ALLOC_LISTAsync()
        {
            var loEx = new R_Exception();
            FAT01100ResultDTO<List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>> loRtn = new FAT01100ResultDTO<List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT01100ExpenseAllocation.RSP_FA_GET_TRANS_EXP_ALLOC_LIST),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public IAsyncEnumerable<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO> RSP_FA_GET_TRANS_EXP_ALLOC_LIST()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

