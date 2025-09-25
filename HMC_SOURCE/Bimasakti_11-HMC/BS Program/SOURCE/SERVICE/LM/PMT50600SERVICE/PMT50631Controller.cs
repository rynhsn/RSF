using PMT50600BACK;
using PMT50600BACK.OpenTelemetry;
using PMT50600COMMON;
using PMT50600COMMON.DTOs;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50631;
using PMT50600COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT50631Controller : ControllerBase, IPMT50631
    {
        private LoggerPMT50631 _logger;
        private readonly ActivitySource _activitySource;

        public PMT50631Controller(ILogger<PMT50631Controller> logger)
        {
            LoggerPMT50631.R_InitializeLogger(logger);
            _logger = LoggerPMT50631.R_GetInstanceLogger();
            _activitySource = PMT50631ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50631Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMT50631DTO> GetAdditionalList()
        {
            using Activity activity = _activitySource.StartActivity("GetAdditionalList");
            _logger.LogInfo("Start || GetAdditionalList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<PMT50631DTO> loRtn = null;
            PMT50631Cls loCls = new PMT50631Cls();
            List<PMT50631DTO> loTempRtn = null;
            GetAdditionalListParameterDTO loParameter = new GetAdditionalListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetAdditionalList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<GetAdditionalListParameterDTO>(ContextConstant.PMT50631_GET_ADDITIONAL_LIST_STREAMING_CONTEXT);
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetAdditionalList(Cls) || GetAdditionalList(Controller)");
                loTempRtn = loCls.GetAdditionalList(loParameter);

                _logger.LogInfo("Run GetAdditionalStream(Controller) || GetAdditionalList(Controller)");
                loRtn = GetAdditionalStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAdditionalList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<PMT50631DTO> GetAdditionalStream(List<PMT50631DTO> poParameter)
        {
            foreach (PMT50631DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT50631ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            PMT50631Cls loCls = new PMT50631Cls();

            try
            {
                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceDelete(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT50631ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT50631ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT50631ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT50631ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                PMT50631Cls loCls = new PMT50631Cls();
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT50631ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT50631ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT50631ParameterDTO> loRtn = new R_ServiceSaveResultDTO<PMT50631ParameterDTO>();
            PMT50631Cls loCls = new PMT50631Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "NEW";
                    poParameter.Entity.Data.CREC_ID = "";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceSave(Controller)");

            return loRtn;
        }

    }
}
