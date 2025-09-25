using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM10000BACK;
using PMM10000COMMON.Interface;
using PMM10000COMMON.Logs;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM10000CategoryController : ControllerBase, IPMM10000CRUDCategory
    {
        private LoggerPMM10000 _logger;
        private readonly ActivitySource _activitySource;

        public PMM10000CategoryController(ILogger<PMM10000CallTypeController> logger)
        {
            //Initial and Get Logger
            LoggerPMM10000.R_InitializeLogger(logger);
            _logger = LoggerPMM10000.R_GetInstanceLogger();
            _activitySource = PMM10000Activity.R_InitializeAndGetActivitySource(nameof(PMM10000CallTypeController));

        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM10000CategoryDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            PMM10000CategoryCls loCls;

            try
            {
                loCls = new PMM10000CategoryCls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Call method R_Delete");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM10000CategoryDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM10000CategoryDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMM10000CategoryDTO>();

            try
            {
                var loCls = new PMM10000CategoryCls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Call method R_GetRecord");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<PMM10000CategoryDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM10000CategoryDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMM10000CategoryDTO>? loRtn = null;
            PMM10000CategoryCls loCls;

            try
            {
                loCls = new PMM10000CategoryCls();
                loRtn = new R_ServiceSaveResultDTO<PMM10000CategoryDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                //poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Call method R_ServiceSave");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn!;
        }
    }
}
