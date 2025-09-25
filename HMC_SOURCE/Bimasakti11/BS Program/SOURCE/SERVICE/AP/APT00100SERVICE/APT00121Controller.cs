using APT00100BACK;
using APT00100BACK.OpenTelemetry;
using APT00100COMMON;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using APT00100COMMON.DTOs.APT00121;
using APT00100COMMON.Loggers;
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

namespace APT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00121Controller : ControllerBase, IAPT00121
    {
        private LoggerAPT00121 _logger;
        private readonly ActivitySource _activitySource;

        public APT00121Controller(ILogger<APT00121Controller> logger)
        {
            LoggerAPT00121.R_InitializeLogger(logger);
            _logger = LoggerAPT00121.R_GetInstanceLogger();
            _activitySource = APT00121ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00121Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GetProductTypeDTO> GetProductTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetProductTypeList");
            _logger.LogInfo("Start || GetProductTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetProductTypeDTO> loRtn = null;
            APT00121Cls loCls = new APT00121Cls();
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
        public APT00121ResultDTO RefreshInvoiceItem(APT00121RefreshParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RefreshInvoiceItem");
            _logger.LogInfo("Start || RefreshInvoiceItem(Controller)");
            R_Exception loException = new R_Exception();
            APT00121ResultDTO loRtn = new APT00121ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || RefreshInvoiceItem(Controller)");
                APT00121Cls loCls = new APT00121Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run RefreshInvoiceItem(Cls) || RefreshInvoiceItem(Controller)");
                loRtn.Data = loCls.RefreshInvoiceItem(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RefreshInvoiceItem(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00121ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            APT00121Cls loCls = new APT00121Cls();

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
        public R_ServiceGetRecordResultDTO<APT00121ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00121ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<APT00121ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<APT00121ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                APT00121Cls loCls = new APT00121Cls();

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
        public R_ServiceSaveResultDTO<APT00121ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00121ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APT00121ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APT00121ParameterDTO>();
            APT00121Cls loCls = new APT00121Cls();

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
