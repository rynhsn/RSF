using APT00200BACK;
using APT00200BACK.OpenTelemetry;
using APT00200COMMON;
using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using APT00200COMMON.DTOs.APT00221;
using APT00200COMMON.Loggers;
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

namespace APT00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00221Controller : ControllerBase, IAPT00221
    {
        private LoggerAPT00221 _logger;
        private readonly ActivitySource _activitySource;
        public APT00221Controller(ILogger<APT00221Controller> logger)
        {
            LoggerAPT00221.R_InitializeLogger(logger);
            _logger = LoggerAPT00221.R_GetInstanceLogger();
            _activitySource = APT00221ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00221Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GetProductTypeDTO> GetProductTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetProductTypeList");
            _logger.LogInfo("Start || GetProductTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetProductTypeDTO> loRtn = null;
            APT00221Cls loCls = new APT00221Cls();
            List<GetProductTypeDTO> loTempRtn = null;
            GetProductTypeParameterDTO loParameter = new GetProductTypeParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetProductTypeList(Controller)");
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetProductTypeList(Cls) || GetProductTypeList(Controller)");
                loTempRtn = loCls.GetProductTypeList(loParameter);

                _logger.LogInfo("Run GetProductTypeStream(Controller) || GetProductTypeList(Controller)");
                loRtn = GetProductTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetProductTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetProductTypeDTO> GetProductTypeStream(List<GetProductTypeDTO> poParameter)
        {
            foreach (GetProductTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public APT00221ResultDTO RefreshPurchaseReturnItem(APT00221RefreshParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RefreshPurchaseReturnItem");
            _logger.LogInfo("Start || RefreshPurchaseReturnItem(Controller)");
            R_Exception loException = new R_Exception();
            APT00221ResultDTO loRtn = new APT00221ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || RefreshPurchaseReturnItem(Controller)");
                APT00221Cls loCls = new APT00221Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run RefreshPurchaseReturnItem(Cls) || RefreshPurchaseReturnItem(Controller)");
                loRtn.Data = loCls.RefreshPurchaseReturnItem(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RefreshPurchaseReturnItem(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00221ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            APT00221Cls loCls = new APT00221Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CACTION = "DELETE";
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

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
        public R_ServiceGetRecordResultDTO<APT00221ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00221ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<APT00221ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<APT00221ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                APT00221Cls loCls = new APT00221Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
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
        public R_ServiceSaveResultDTO<APT00221ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00221ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APT00221ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APT00221ParameterDTO>();
            APT00221Cls loCls = new APT00221Cls();

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
