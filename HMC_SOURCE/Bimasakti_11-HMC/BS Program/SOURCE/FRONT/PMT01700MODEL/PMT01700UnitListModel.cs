using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL
{
    public class PMT01700UnitListModel : R_BusinessObjectServiceClientBase<PMT01700BlankDTO>, IPMT01700UnitList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700UnitList";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700UnitListModel(
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
        public async Task<PMT01700GenericList<PMT01700PropertyListDTO>> PropertyListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700PropertyListDTO> loResult = new PMT01700GenericList<PMT01700PropertyListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700UnitList.GetPropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<PMT01700GenericList<PMT01700OtherUnitList_OtherUnitListDTO>> OtherUnitListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700OtherUnitList_OtherUnitListDTO> loResult = new PMT01700GenericList<PMT01700OtherUnitList_OtherUnitListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700OtherUnitList_OtherUnitListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700UnitList.GetUnitOtherList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }


        #region LibraryNotImplement
        public IAsyncEnumerable<PMT01700PropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700OtherUnitList_OtherUnitListDTO> GetUnitOtherList()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
