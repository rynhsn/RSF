using Lookup_TXBACK;
using Lookup_TXCOMMON.DTOs.TXL00100;
using Lookup_TXCOMMON.DTOs.TXL00200;
using Lookup_TXCOMMON.DTOs.Utilities;
using Lookup_TXCOMMON.Interface;
using Lookup_TXCOMMON.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace Lookup_TXSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupTXGetRecordController : ControllerBase, IGetRecordPublicLookupTX
    {
        private LoggerLookupTX _loggerLookup;
        private readonly ActivitySource _activitySource;

        public PublicLookupTXGetRecordController(ILogger<LoggerLookupTX> logger)
        {
            //Initial and Get Logger
            LoggerLookupTX.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupTX.R_GetInstanceLogger();
            _activitySource = LookupTXActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupTXGetRecordController));
        }

        [HttpPost]
        public TXLGenericRecord<TXL00100DTO> TXL00100BranchLookUp(TXL00100ParameterGetRecordDTO poParameter)
        {
            string lcMethodName = nameof(TXL00100BranchLookUp);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            TXLParameterCompanyAndUserDTO loDbParameterInternal;
            var loEx = new R_Exception();
            TXLGenericRecord<TXL00100DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupTXCls();
                _loggerLookup.LogInfo("Call method TXL00100BranchLookUp");
                loDbParameterInternal = new TXLParameterCompanyAndUserDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };
                var loTempList = loCls.TXL00100BranchLookUpDb(loDbParameterInternal);

                _loggerLookup.LogInfo("Filter Search by text");
                loReturn.Data = loTempList
                    .Find(x => x.CBRANCH_CODE!
                        .Equals(poParameter.CSEARCH_TEXT!
                        .Trim(),
                        StringComparison.OrdinalIgnoreCase))!;

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }

        [HttpPost]
        public TXLGenericRecord<TXL00200DTO> TXL00200TaxNoLookUp(TXL00200ParameterGetRecordDTO poParameter)
        {
            string lcMethodName = nameof(TXL00200TaxNoLookUp);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            TXLParameterCompanyAndUserDTO loDbParameterInternal;
            TXL00200ParameterGetRecordDTO loDbParameter;
            var loEx = new R_Exception();
            TXLGenericRecord<TXL00200DTO> loReturn = new();
            try
            {
                var loCls = new PublicLookupTXCls();
                _loggerLookup.LogInfo("Call method TXL00100BranchLookUp");
                loDbParameterInternal = new TXLParameterCompanyAndUserDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };
                loDbParameter = new TXL00200ParameterGetRecordDTO();
                loDbParameter = poParameter;
                var loTempList = loCls.TXL00200TaxNoLookUpDb(poParameterInternal: loDbParameterInternal, poParameter: loDbParameter);

                if (!string.IsNullOrWhiteSpace(poParameter.CREMOVE_DATA_BOOKING_TAX_NO_SEPARATOR))
                {
                    // Gunakan HashSet untuk mempercepat pencarian
                    var itemsToRemove = new HashSet<string>(
                        poParameter.CREMOVE_DATA_BOOKING_TAX_NO_SEPARATOR.Split(',').Select(x => x.Trim()));

                    // Hapus item menggunakan HashSet untuk efisiensi
                    loTempList.RemoveAll(item => itemsToRemove.Contains(item.CBOOKING_TAX_NO!));
                }

                //INI GA DIPAKE JADI GA USAH DI UPDATE
                _loggerLookup.LogInfo("Filter Search by text");
                loReturn.Data = loTempList
                    .Find(x => !string.IsNullOrEmpty(x.CBRANCH_CODE) &&
                               x.CBRANCH_CODE.Equals(poParameter.CSEARCH_TEXT!.Trim(), StringComparison.OrdinalIgnoreCase))!;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
    }
}
