using PMT50600BACK;
using PMT50600BACK.OpenTelemetry;
using PMT50600COMMON;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs.PMT50611;
using PMT50600COMMON.DTOs.PMT50621;
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
    public class PMT50621Controller : ControllerBase, IPMT50621
    {
        private LoggerPMT50621 _logger;
        private readonly ActivitySource _activitySource;

        public PMT50621Controller(ILogger<PMT50621Controller> logger)
        {
            LoggerPMT50621.R_InitializeLogger(logger);
            _logger = LoggerPMT50621.R_GetInstanceLogger();
            _activitySource = PMT50621ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT50621Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<GetProductTypeDTO> GetProductTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetProductTypeList");
            _logger.LogInfo("Start || GetProductTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetProductTypeDTO> loRtn = null;
            PMT50621Cls loCls = new PMT50621Cls();
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
        public PMT50621ResultDTO RefreshInvoiceItem(PMT50621RefreshParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RefreshInvoiceItem");
            _logger.LogInfo("Start || RefreshInvoiceItem(Controller)");
            R_Exception loException = new R_Exception();
            PMT50621ResultDTO loRtn = new PMT50621ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || RefreshInvoiceItem(Controller)");
                PMT50621Cls loCls = new PMT50621Cls();
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT50621ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            PMT50621Cls loCls = new PMT50621Cls();

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
        public R_ServiceGetRecordResultDTO<PMT50621ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT50621ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<PMT50621ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT50621ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                PMT50621Cls loCls = new PMT50621Cls();

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
        public R_ServiceSaveResultDTO<PMT50621ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT50621ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMT50621ParameterDTO> loRtn = new R_ServiceSaveResultDTO<PMT50621ParameterDTO>();
            PMT50621Cls loCls = new PMT50621Cls();

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
