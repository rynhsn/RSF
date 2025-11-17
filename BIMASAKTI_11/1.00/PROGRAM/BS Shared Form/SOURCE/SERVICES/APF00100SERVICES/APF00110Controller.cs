using APF00100BACK.OpenTelemetry;
using APF00100COMMON.Logger;
using APF00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APF00100COMMON.DTOs.APF00110;
using R_CommonFrontBackAPI;
using R_BackEnd;
using R_Common;
using APF00100BACK;
using APF00100COMMON.DTOs.APF00100;
using APF00100COMMON.DTOs;

namespace APF00100SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APF00110Controller : ControllerBase, APF00100BACK.IAPF00110
    {
        private LoggerAPF00110 _logger;
        private readonly ActivitySource _activitySource;
        public APF00110Controller(ILogger<APF00110Controller> logger)
        {
            LoggerAPF00110.R_InitializeLogger(logger);
            _logger = LoggerAPF00110.R_GetInstanceLogger();
            _activitySource = APF00110ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APF00110Controller));
        }


        [HttpPost]
        public IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeList()
        {
            return GetTransactionTypeStream();
        }
        private async IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetTransactionTypeList");
            _logger.LogInfo("Start || GetTransactionTypeList(Controller)");
            R_Exception loException = new R_Exception();
            List<GetTransactionTypeDTO> loRtn = null;
            APF00110Cls loCls = new APF00110Cls();
            GetTransactionTypeParameterDTO loParameter = new GetTransactionTypeParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetTransactionTypeList(Controller)");
                loParameter = R_Utility.R_GetStreamingContext<GetTransactionTypeParameterDTO>(ContextConstant.APF00110_GET_TRANSACTION_TYPE_LIST_STREAMING_CONTEXT);
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetTransactionTypeList(Cls) || GetTransactionTypeList(Controller)");
                loRtn = await loCls.GetTransactionTypeList(loParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetInvoiceList(Controller)");

            foreach (GetTransactionTypeDTO item in loRtn)
            {
                yield return item;
            }
        }
 
        [HttpPost]
        public async Task<APF00110ResultDTO> GetAllocationDetail(GetAllocationDetailParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetAllocationDetail");
            _logger.LogInfo("Start || GetAllocationDetail(Controller)");
            R_Exception loException = new R_Exception();
            APF00110ResultDTO loRtn = new APF00110ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetAllocationDetail(Controller)");
                APF00110Cls loCls = new APF00110Cls();
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE; 

                _logger.LogInfo("Run GetAllocationDetail(Cls) || GetAllocationDetail(Controller)");
                loRtn.Data = await loCls.GetAllocationDetail(poParam);
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
        public async Task<R_ServiceGetRecordResultDTO<APF00110ParameterDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APF00110ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<APF00110ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<APF00110ParameterDTO>();

            try
            {
                APF00110Cls loCls = new APF00110Cls();

                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
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
        public async Task<R_ServiceSaveResultDTO<APF00110ParameterDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<APF00110ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APF00110ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APF00110ParameterDTO>();
            APF00110Cls loCls = new APF00110Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<APF00110ParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            APF00110Cls loCls = new APF00110Cls();

            try
            {
                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                await loCls.R_DeleteAsync(poParameter.Entity);
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
        public async Task<SubmitAllocationResultDTO> SubmitAllocationProcess(SubmitAllocationParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("SubmitAllocationProcess");
            _logger.LogInfo("Start || SubmitAllocationProcess(Controller)");
            R_Exception loException = new R_Exception();
            SubmitAllocationResultDTO loRtn = new SubmitAllocationResultDTO();
            APF00110Cls loCls = new APF00110Cls();

            try
            {
                _logger.LogInfo("Set Parameter || SubmitAllocationProcess(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run SubmitAllocationProcess(Cls) || SubmitAllocationProcess(Controller)");
                await loCls.SubmitAllocationProcess(poParam);
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
        public async Task<RedraftAllocationResultDTO> RedraftAllocationProcess(RedraftAllocationParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("RedraftAllocationProcess");
            _logger.LogInfo("Start || RedraftAllocationProcess(Controller)");
            R_Exception loException = new R_Exception();
            RedraftAllocationResultDTO loRtn = new RedraftAllocationResultDTO();
            APF00110Cls loCls = new APF00110Cls();

            try
            {
                _logger.LogInfo("Set Parameter || RedraftAllocationProcess(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RedraftAllocationProcess(Cls) || RedraftAllocationProcess(Controller)");
                await loCls.RedraftAllocationProcess(poParam);
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
