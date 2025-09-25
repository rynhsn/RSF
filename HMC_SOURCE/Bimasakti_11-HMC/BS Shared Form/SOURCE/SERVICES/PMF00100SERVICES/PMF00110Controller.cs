using PMF00100BACK.OpenTelemetry;
using PMF00100COMMON.Logger;
using PMF00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMF00100COMMON.DTOs.PMF00110;
using R_CommonFrontBackAPI;
using R_BackEnd;
using R_Common;
using PMF00100BACK;
using PMF00100COMMON.DTOs.PMF00100;
using PMF00100COMMON.DTOs;

namespace PMF00100SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMF00110Controller : ControllerBase, IPMF00110
    {
        private LoggerPMF00110 _logger;
        private readonly ActivitySource _activitySource;
        public PMF00110Controller(ILogger<PMF00110Controller> logger)
        {
            LoggerPMF00110.R_InitializeLogger(logger);
            _logger = LoggerPMF00110.R_GetInstanceLogger();
            _activitySource = PMF00110ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMF00110Controller));
        }


        [HttpPost]
        public IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetTransactionTypeList");
            _logger.LogInfo("Start || GetTransactionTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetTransactionTypeDTO> loRtn = null;
            PMF00110Cls loCls = new PMF00110Cls();
            List<GetTransactionTypeDTO> loTempRtn = null;
            GetTransactionTypeParameterDTO loParameter = new GetTransactionTypeParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetTransactionTypeList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<GetTransactionTypeParameterDTO>(ContextConstant.PMF00110_GET_TRANSACTION_TYPE_LIST_STREAMING_CONTEXT);
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetTransactionTypeList(Cls) || GetTransactionTypeList(Controller)");
                loTempRtn = loCls.GetTransactionTypeList(loParameter);

                _logger.LogInfo("Run GetTransactionTypeStream(Controller) || GetTransactionTypeList(Controller)");
                loRtn = GetTransactionTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetInvoiceList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeStream(List<GetTransactionTypeDTO> poParameter)
        {
            foreach (GetTransactionTypeDTO item in poParameter)
            {
                yield return item;
            }
        }
 
        [HttpPost]
        public PMF00110ResultDTO GetAllocationDetail(GetAllocationDetailParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetAllocationDetail");
            _logger.LogInfo("Start || GetAllocationDetail(Controller)");
            R_Exception loException = new R_Exception();
            PMF00110ResultDTO loRtn = new PMF00110ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetAllocationDetail(Controller)");
                PMF00110Cls loCls = new PMF00110Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE; 

                _logger.LogInfo("Run GetAllocationDetail(Cls) || GetAllocationDetail(Controller)");
                loRtn.Data = loCls.GetAllocationDetail(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllocationDetail(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMF00110ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMF00110ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<PMF00110ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<PMF00110ParameterDTO>();

            try
            {
                PMF00110Cls loCls = new PMF00110Cls();

                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
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
        public R_ServiceSaveResultDTO<PMF00110ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMF00110ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            //R_ServiceSaveResultDTO<PMF00110DTO> loRtn = new R_ServiceSaveResultDTO<PMF00110DTO>();
            R_ServiceSaveResultDTO<PMF00110ParameterDTO> loRtn = new R_ServiceSaveResultDTO<PMF00110ParameterDTO>();
            PMF00110Cls loCls = new PMF00110Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
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

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMF00110ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            PMF00110Cls loCls = new PMF00110Cls();

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
        public SubmitAllocationResultDTO SubmitAllocationProcess(SubmitAllocationParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("SubmitAllocationProcess");
            _logger.LogInfo("Start || SubmitAllocationProcess(Controller)");
            R_Exception loException = new R_Exception();
            SubmitAllocationResultDTO loRtn = new SubmitAllocationResultDTO();
            PMF00110Cls loCls = new PMF00110Cls();

            try
            {
                _logger.LogInfo("Set Parameter || SubmitAllocationProcess(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run SubmitAllocationProcess(Cls) || SubmitAllocationProcess(Controller)");
                loCls.SubmitAllocationProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || SubmitAllocationProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public RedraftAllocationResultDTO RedraftAllocationProcess(RedraftAllocationParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RedraftAllocationProcess");
            _logger.LogInfo("Start || RedraftAllocationProcess(Controller)");
            R_Exception loException = new R_Exception();
            RedraftAllocationResultDTO loRtn = new RedraftAllocationResultDTO();
            PMF00110Cls loCls = new PMF00110Cls();

            try
            {
                _logger.LogInfo("Set Parameter || RedraftAllocationProcess(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RedraftAllocationProcess(Cls) || RedraftAllocationProcess(Controller)");
                loCls.RedraftAllocationProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RedraftAllocationProcess(Controller)");
            return loRtn;
        }
    }
}
