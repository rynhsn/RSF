using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT02600BACK.OpenTelemetry;
using PMT02600COMMON.Loggers;
using PMT02600COMMON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Common;
using R_CommonFrontBackAPI;
using PMT02600COMMON.DTOs.PMT02610;
using PMT02600BACK;
using R_BackEnd;

namespace PMT02600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT02610Controller : ControllerBase, IPMT02610
    {
        private LoggerPMT02610 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT02610Controller(ILogger<LoggerPMT02610> logger)
        {
            //Initial and Get Logger
            LoggerPMT02610.R_InitializeLogger(logger);
            _Logger = LoggerPMT02610.R_GetInstanceLogger();
            _activitySource = PMT02610ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT02610Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT02610ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT02610ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT02610ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT02610ParameterDTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMT02610Parameter");

            try
            {
                var loCls = new PMT02610Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMT02610Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMT02610Parameter");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT02610ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT02610ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMT02610ParameterDTO> loRtn = new R_ServiceSaveResultDTO<PMT02610ParameterDTO>();
            _Logger.LogInfo("Start ServiceSave PMT02610Parameter");

            try
            {
                var loCls = new PMT02610Cls();

                _Logger.LogInfo("Call Back Method R_Save PMT02610Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMT02610Parameter");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT02610ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete PMT02610");

            try
            {
                var loCls = new PMT02610Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMT02610Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete PMT02610");

            return loRtn;
        }
    }
}
