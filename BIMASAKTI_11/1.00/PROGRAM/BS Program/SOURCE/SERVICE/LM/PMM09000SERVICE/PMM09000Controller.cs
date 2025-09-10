using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM09000Back;
using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Interface;
using PMM09000COMMON.Logs;
using PMM09000COMMON.UtiliyDTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM09000Controller : ControllerBase, IPMM09000CRUDBase
    {
        private LoggerPMM09000 _logger;
        private readonly ActivitySource _activitySource;

        public PMM09000Controller(ILogger<PMM09000Controller> logger)
        {
            //Initial and Get Logger
            LoggerPMM09000.R_InitializeLogger(logger);
            _logger = LoggerPMM09000.R_GetInstanceLogger();
            _activitySource = PMM09000Activity.R_InitializeAndGetActivitySource(nameof(PMM09000Controller));

        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM09000EntryHeaderDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceDelete);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = null;
            PMM09000Cls loCls;

            try
            {
                loCls = new PMM09000Cls();
                loRtn = new R_ServiceDeleteResultDTO();
                poParameter.Entity.Header.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.Header.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.Header.CLANG_ID = R_BackGlobalVar.CULTURE;
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
        public R_ServiceGetRecordResultDTO<PMM09000EntryHeaderDetailDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM09000EntryHeaderDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceGetRecord);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<PMM09000EntryHeaderDetailDTO>();

            try
            {
                var loCls = new PMM09000Cls();
                poParameter.Entity.Header.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.Header.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.Header.CLANG_ID = R_BackGlobalVar.CULTURE;
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
        public R_ServiceSaveResultDTO<PMM09000EntryHeaderDetailDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM09000EntryHeaderDetailDTO> poParameter)
        {
            string lcMethodName = nameof(R_ServiceSave);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<PMM09000EntryHeaderDetailDTO>? loRtn = null;
            PMM09000Cls loCls;

            try
            {
                loCls = new PMM09000Cls();
                loRtn = new R_ServiceSaveResultDTO<PMM09000EntryHeaderDetailDTO>();
                poParameter.Entity.Header.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.Header.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.Header.CLANG_ID = R_BackGlobalVar.CULTURE;
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
        [HttpPost]
        public UpdateStatusDTO UpdateAmortizationStatus(PMM09000DbParameterDTO poParameter)
        {
            string lcMethodName = nameof(UpdateAmortizationStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            var loRtn = new UpdateStatusDTO();

            try
            {
                var loCls = new PMM09000Cls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Method UpdateAmortizationStatus");
                loCls.UpdateAmortizationStatus(poParameter);

                if (!loEx.Haserror)
                {
                    loRtn.LSTATUS = true;
                }
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
    }
}
