using Lookup_TXBACK;
using Lookup_TXCOMMON.DTOs.TXL00100;
using Lookup_TXCOMMON.DTOs.TXL00200;
using Lookup_TXCOMMON.DTOs.Utilities;
using Lookup_TXCOMMON.Interface;
using Lookup_TXCOMMON.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_TXSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupTXController : ControllerBase, IPublicLookupTX
    {
        private LoggerLookupTX _loggerLookup;
        private readonly ActivitySource _activitySource;
        public PublicLookupTXController(ILogger<PublicLookupTXController> logger)
        {

            LoggerLookupTX.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupTX.R_GetInstanceLogger();
            _activitySource = LookupTXActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupTXController));
        }


        [HttpPost]
        public IAsyncEnumerable<TXL00100DTO> TXL00100BranchLookUp()
        {
            string lcMethodName = nameof(TXL00100BranchLookUp);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            TXLParameterCompanyAndUserDTO? loDbParameterInternal;
            IAsyncEnumerable<TXL00100DTO>? loRtn = null;
            List<TXL00100DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupTXCls();
                loDbParameterInternal = new TXLParameterCompanyAndUserDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID
                };
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));

                loReturnTemp = loCls.TXL00100BranchLookUpDb(loDbParameterInternal);
                loRtn = GetStream(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<TXL00200DTO> TXL00200TaxNoLookUp()
        {
            string lcMethodName = nameof(TXL00100BranchLookUp);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            TXLParameterCompanyAndUserDTO? loDbParameterInternal;
            TXL00200ParameterDTO? loDbParameter;
            IAsyncEnumerable<TXL00200DTO>? loRtn = null;
            List<TXL00200DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupTXCls();
                loDbParameterInternal = new TXLParameterCompanyAndUserDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID
                };
                loDbParameter = new TXL00200ParameterDTO();
                loDbParameter.CBRANCH_CODE = R_Utility.R_GetStreamingContext<string>(TXL00200ContextDTO.CBRANCH_CODE);
                loDbParameter.LREUSE_FLAG = R_Utility.R_GetStreamingContext<bool>(TXL00200ContextDTO.LREUSE_FLAG);
                loDbParameter.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(TXL00200ContextDTO.CTENANT_ID);
                loDbParameter.CTAX_ID = R_Utility.R_GetStreamingContext<string>(TXL00200ContextDTO.CTAX_ID);
                loDbParameter.LREPLACEMENT = R_Utility.R_GetStreamingContext<bool>(TXL00200ContextDTO.LREPLACEMENT);

                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));

                loReturnTemp = loCls.TXL00200TaxNoLookUpDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);
                loRtn = GetStream(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        #region Stream Data

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }


        #endregion


    }
}