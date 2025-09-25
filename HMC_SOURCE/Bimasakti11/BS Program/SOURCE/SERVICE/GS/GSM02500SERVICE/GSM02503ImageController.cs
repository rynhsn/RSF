using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.Loggers;
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

namespace GSM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02503ImageController : ControllerBase, IGSM02503Image
    {
        private LoggerGSM02503Image _logger;
        private readonly ActivitySource _activitySource;
        public GSM02503ImageController(ILogger<GSM02503ImageController> logger)
        {
            LoggerGSM02503Image.R_InitializeLogger(logger);
            _logger = LoggerGSM02503Image.R_GetInstanceLogger();
            _activitySource = GSM02503ImageActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02503ImageController));
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02503ImageDTO> GetUnitTypeImageList()
        {
            using Activity activity = _activitySource.StartActivity("GetUnitTypeImageList");
            _logger.LogInfo("Start || GetUnitTypeImageList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM02503ImageDTO> loRtn = null;
            GetUnitTypeImageListParameterDTO loParam = new GetUnitTypeImageListParameterDTO();
            GSM02503ImageCls loCls = new GSM02503ImageCls();
            List<GSM02503ImageDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetUnitTypeImageList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02503_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_UNIT_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02503_UNIT_TYPE_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetUnitTypeImageList(Cls) || GetUnitTypeImageList(Controller)");
                loTempRtn = loCls.GetUnitTypeImageList(loParam);

                _logger.LogInfo("Run GetUnitTypeImageStream(Controller) || GetUnitTypeImageList(Controller)");
                loRtn = GetUnitTypeImageStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetUnitTypeImageList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM02503ImageDTO> GetUnitTypeImageStream(List<GSM02503ImageDTO> poParameter)
        {
            foreach (GSM02503ImageDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM02503ImageParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02503ImageParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            GSM02503ImageParameterDTO loParam = new GSM02503ImageParameterDTO();
            R_ServiceGetRecordResultDTO<GSM02503ImageParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02503ImageParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02503ImageCls loCls = new GSM02503ImageCls();

                loParam = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(loParam);
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
        public ShowUnitTypeImageResultDTO ShowUnitTypeImage(ShowUnitTypeImageParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("ShowUnitTypeImage");
            _logger.LogInfo("Start || ShowUnitTypeImage(Controller)");
            R_Exception loException = new R_Exception();
            ShowUnitTypeImageResultDTO loRtn = new ShowUnitTypeImageResultDTO();
            GSM02503ImageCls loCls = new GSM02503ImageCls();

            try
            {
                _logger.LogInfo("Set Parameter || ShowUnitTypeImage(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("RunShowUnitTypeImage(Cls) || ShowUnitTypeImage(Controller)");
                loRtn.Data = loCls.ShowUnitTypeImage(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ShowUnitTypeImage(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM02503ImageParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02503ImageParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02503ImageParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02503ImageParameterDTO>();
            GSM02503ImageCls loCls = new GSM02503ImageCls();
            GSM02503ImageParameterDTO loParam = new GSM02503ImageParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                //loParam.OIMAGE = Convert.FromBase64String(lcTemp);

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    loParam.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    loParam.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(loParam, poParameter.CRUDMode);
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

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02503ImageParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02503ImageParameterDTO loParam = new GSM02503ImageParameterDTO();
            GSM02503ImageCls loCls = new GSM02503ImageCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                loParam = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                //loParam.OIMAGE = Convert.FromBase64String(lcTemp);
                loParam.CACTION = "DELETE";
                
                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(loParam);
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
    }
}
