using Lookup_APBACK;
using Lookup_APCOMMON;
using Lookup_APCOMMON.DTOs;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00400;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APCOMMON.Loggers;

namespace Lookup_APSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicAPLookupRecordController : ControllerBase, IPublicAPLookupRecord
    {
        private LoggerAPPublicLookup _loggerAp;
        public readonly ActivitySource _activitySource;

        public PublicAPLookupRecordController(ILogger<LoggerAPPublicLookup> logger)
        {
            //Initial and Get Logger
            LoggerAPPublicLookup.R_InitializeLogger(logger);
            _loggerAp = LoggerAPPublicLookup.R_GetInstanceLogger();
            _activitySource = Lookup_APBACKActivity.R_InitializeAndGetActivitySource(nameof(PublicAPLookupRecordController));
        }
        [HttpPost]
        public APLGenericRecord<APL00100DTO> APL00100GetRecord(APL00100ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("APL00100GetRecord");
            var loEx = new R_Exception();
            APLGenericRecord<APL00100DTO> loRtn = new();
            _loggerAp.LogInfo("Start APL00100GetRecord");

            try
            {
                var loCls = new PublicAPLookUpCls();

             
                
                _loggerAp.LogInfo("Set Param APL00100SupplierLookUp");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loResult = loCls.SupplierLookup(poEntity);

                _loggerAp.LogInfo("Filter Search by text APL00100GetRecord");
                loRtn.Data = loResult.Find(x => x.CSUPPLIER_ID.Equals(poEntity.CSEARCH_CODE.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerAp.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00110GetRecord");
            return loRtn;
        }
        [HttpPost]
        public APLGenericRecord<APL00110DTO> APL00110GetRecord(APL00110ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("APL00110GetRecord");
            var loEx = new R_Exception();
            APLGenericRecord<APL00110DTO> loRtn = new();
            _loggerAp.LogInfo("Start APL00110GetRecord");

            try
            {
                var loCls = new PublicAPLookUpCls();

             

                _loggerAp.LogInfo("Set Param APL00110SupplierInfoLookUp");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loResult = loCls.SupplierInfoLookup(poEntity);

                _loggerAp.LogInfo("Filter Search by text APL00110GetRecord");
                loRtn.Data = loResult.Find(x => x.CSEQ_NO.Equals(poEntity.CSEARCH_CODE.Trim(), StringComparison.OrdinalIgnoreCase));
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerAp.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00110GetRecord");
            return loRtn;
        }
        [HttpPost]
        public APLGenericRecord<APL00200DTO> APL00200GetRecord(APL00200ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("APL00200GetRecord");
            var loEx = new R_Exception();
            APLGenericRecord<APL00200DTO> loRtn = new();
            _loggerAp.LogInfo("Start APL00200GetRecord");

            try
            {
                var loCls = new PublicAPLookUpCls();
                

                _loggerAp.LogInfo("Set Param APL00200ExpenditureLookUp");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loResult = loCls.ExpenditureLookup(poEntity);

                _loggerAp.LogInfo("Filter Search by text APL00200GetRecord");
                loRtn.Data = loResult.Find(x => x.CEXPENDITURE_ID.Equals(poEntity.CSEARCH_CODE.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerAp.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00200GetRecord");
            return loRtn;
        }
        [HttpPost]
        public APLGenericRecord<APL00300DTO> APL00300GetRecord(APL00300ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("APL00300GetRecord");
            var loEx = new R_Exception();
            APLGenericRecord<APL00300DTO> loRtn = new();
            _loggerAp.LogInfo("Start APL00300GetRecord");

            try
            {
                var loCls = new PublicAPLookUpCls();

               

                _loggerAp.LogInfo("Set Param APL00300ProductLookUp");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loResult = loCls.ProductLookup(poEntity);

                _loggerAp.LogInfo("Filter Search by text APL00300GetRecord");
                loRtn.Data =loResult.Find(x => x.CPRODUCT_ID.Equals(poEntity.CSEARCH_CODE.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerAp.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00300GetRecord");
            return loRtn;
        }
        [HttpPost]
        public APLGenericRecord<APL00400DTO> APL00400GetRecord(APL00400ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("APL00400DTOGetRecord");
            var loEx = new R_Exception();
            APLGenericRecord<APL00400DTO> loRtn = new();
            _loggerAp.LogInfo("Start APL00400DTOGetRecord");

            try
            {
                var loCls = new PublicAPLookUpCls();

            

                _loggerAp.LogInfo("Set Param APL00400ProductAllocationLookUp");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loResult = loCls.ProductAllocationLookup(poEntity);

                _loggerAp.LogInfo("Filter Search by text APL00400DTOGetRecord");
                loRtn.Data = loResult.Find(x => x.CALLOC_ID.Equals(poEntity.CSEARCH_CODE.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerAp.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00400DTOGetRecord");
            return loRtn;
        }
        [HttpPost]
        public APLGenericRecord<APL00500DTO> APL00500GetRecord(APL00500ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("APL00500DTOGetRecord");
            var loEx = new R_Exception();
            APLGenericRecord<APL00500DTO> loRtn = new();
            _loggerAp.LogInfo("Start APL00500DTOGetRecord");

            try
            {
                var loCls = new PublicAPLookUpCls();

         

                _loggerAp.LogInfo("Set Param APL00400ProductAllocationLookUp");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poEntity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                var loResult = loCls.TransactionLookup(poEntity);

                _loggerAp.LogInfo("Filter Search by text APL00500DTOGetRecord");
                loRtn.Data = loResult.Find(x => x.CSUPPLIER_ID.Equals(poEntity.CSEARCH_CODE.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerAp.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            _loggerAp.LogInfo("End APL00400DTOGetRecord");
            return loRtn;
        }
    }
}