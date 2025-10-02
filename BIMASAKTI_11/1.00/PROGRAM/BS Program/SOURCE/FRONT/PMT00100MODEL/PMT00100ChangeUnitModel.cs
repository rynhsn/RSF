using PMT00100COMMON.Booking;
using PMT00100COMMON.ChangeUnit;
using PMT00100COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT00100MODEL
{
    public class PMT00100ChangeUnitModel : R_BusinessObjectServiceClientBase<PMT00100ChangeUnitDTO>, IPM00100ChangeUnit
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT00100ChangeUnit";
        private const string DEFAULT_MODULE = "PM";
        public PMT00100ChangeUnitModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        public async Task<PMT00100ChangeUnitDTO> GetAgreementUnitInfoDetailAsync(PMT00100ChangeUnitDTO poParam)
        {
            var loEx = new R_Exception();
            var loResult = new PMT00100ChangeUnitDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT00100ChangeUnitDTO, PMT00100ChangeUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPM00100ChangeUnit.GetAgreementUnitInfoDetail),
                      poParam,
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

            return loResult;
        }

        //Implement Library
        public PMT00100ChangeUnitDTO GetAgreementUnitInfoDetail(PMT00100ChangeUnitDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
