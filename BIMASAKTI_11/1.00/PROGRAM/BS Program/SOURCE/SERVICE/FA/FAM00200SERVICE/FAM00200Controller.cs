using FAM00200Back;
using FAM00200Back.OpenTelemetry;
using FAM00200Common;
using FAM00200Common.DTOs;
using FAM00200Common.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;

namespace FAM00200Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAM00200Controller : ControllerBase, IFAM00200
    {
        private LoggerFAM00200 _Logger;
        private readonly ActivitySource _activitySource;
        public FAM00200Controller(ILogger<LoggerFAM00200> logger)
        {
            //Initial and Get Logger
            LoggerFAM00200.R_InitializeLogger(logger);
            _Logger = LoggerFAM00200.R_GetInstanceLogger();
            _activitySource = FAM00200ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(FAM00200Controller));
        }


        [HttpPost]
        public async Task<FAM00200SingleResult<FAM00200DTO>> GetTaxType(FAM00200DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetTaxType");
            var loEx = new R_Exception();
            FAM00200SingleResult<FAM00200DTO> loRtn = new FAM00200SingleResult<FAM00200DTO>();
            _Logger.LogInfo("Start GetTaxType");

            try
            {
                var loCls = new FAM00200Cls();

                _Logger.LogInfo("Call Back Method GetTaxType");
                loRtn.Data = await loCls.GetTaxType(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTaxType");

            return loRtn;
        }

        [HttpPost]
        public async Task<FAM00200SingleResult<FAM00200DTO>> SaveTaxType(FAM00200SaveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveTaxType");
            var loEx = new R_Exception();
            FAM00200SingleResult<FAM00200DTO> loRtn = new FAM00200SingleResult<FAM00200DTO>();
            _Logger.LogInfo("Start SaveTaxType");

            try
            {
                var loCls = new FAM00200Cls();

                _Logger.LogInfo("Call Back Method SaveTaxType");
                loRtn.Data = await loCls.SaveTaxType(poEntity.Entity, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveTaxType");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<FAM00200DTO> GetListTaxType()
        {
            return GetTaxTypeStreamData();
        }
        private async IAsyncEnumerable<FAM00200DTO> GetTaxTypeStreamData()
        {
            using Activity activity = _activitySource.StartActivity("GetListTaxType");
            var loEx = new R_Exception();
            List<FAM00200DTO> loRtn = null;
            _Logger.LogInfo("Start GetListTaxType");

            try
            {
                var loCls = new FAM00200Cls();

                _Logger.LogInfo("Set Param GetListTaxType");

                _Logger.LogInfo("Call Back Method GetListTaxType");
                loRtn = await loCls.GetListTaxType();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetListTaxType");

            foreach (var item in loRtn)
            {
                yield return item;
            }
        }
    }
}
