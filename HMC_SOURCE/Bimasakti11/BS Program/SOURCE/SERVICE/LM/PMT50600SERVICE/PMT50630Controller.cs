using PMT50600BACK;
using PMT50600BACK.OpenTelemetry;
using PMT50600COMMON;
using PMT50600COMMON.DTOs.PMT50621;
using PMT50600COMMON.DTOs.PMT50630;
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
    public class PMT50630Controller : ControllerBase, IPMT50630
    {
        private LoggerPMT50630 _logger;
        private readonly ActivitySource _activitySource;

        public PMT50630Controller(ILogger<PMT50630Controller> logger)
        {
            LoggerPMT50630.R_InitializeLogger(logger);
            _logger = LoggerPMT50630.R_GetInstanceLogger();
            _activitySource = PMT50630ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50630Controller));
        }

        [HttpPost]
        public PMT50630ResultDTO GetSummaryInfo(GetSummaryParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetSummaryInfo");
            _logger.LogInfo("Start || GetSummaryInfo(Controller)");
            R_Exception loException = new R_Exception();
            PMT50630ResultDTO loRtn = new PMT50630ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSummaryInfo(Controller)");
                PMT50630Cls loCls = new PMT50630Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run RefreshInvoiceItem(Cls) || GetSummaryInfo(Controller)");
                loRtn.Data = loCls.GetSummaryInfo(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSummaryInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT50630ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMT50630ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT50630ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMT50630ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT50630ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT50630ParameterDTO> loRtn = new R_ServiceSaveResultDTO<PMT50630ParameterDTO>();
            PMT50630Cls loCls = new PMT50630Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

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
