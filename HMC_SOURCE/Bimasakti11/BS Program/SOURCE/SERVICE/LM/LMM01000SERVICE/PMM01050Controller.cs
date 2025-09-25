using PMM01000BACK;
using PMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace PMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM01050Controller : ControllerBase, IPMM01050
    {
        private LoggerPMM01050 _Logger;
        private readonly ActivitySource _activitySource;
        public PMM01050Controller(ILogger<LoggerPMM01050> logger)
        {
            //Initial and Get Logger
            LoggerPMM01050.R_InitializeLogger(logger);
            _Logger = LoggerPMM01050.R_GetInstanceLogger();
            _activitySource = PMM01050ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01050Controller));
        }
        private async IAsyncEnumerable<T> GetRateOTListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public IAsyncEnumerable<PMM01051DTO> GetRateOTList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateOTList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01051DTO> loRtn = null;
            List<PMM01051DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateOTList");

            try
            {
                var loCls = new PMM01050Cls();
                loTempRtn = new List<PMM01051DTO>();
                var poParam = new PMM01051DTO();

                _Logger.LogInfo("Set Param GetRateOTList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CDAY_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDAY_TYPE);
                poParam.CCHARGES_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_DATE);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateOTList");
                loTempRtn = loCls.GetAllRateOTList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateOTList");
                loRtn = GetRateOTListStream<PMM01051DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateOTList");

            return loRtn;
        }

        

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01050DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM01050DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMM01050DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01050DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMM01050");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord PMM01050");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01050Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMM01050Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMM01050");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01050DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01050DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMM01050DTO> loRtn = new R_ServiceSaveResultDTO<PMM01050DTO>();
            _Logger.LogInfo("Start ServiceSave PMM01050");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave PMM01050");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01050Cls();

                _Logger.LogInfo("Call Back Method R_Save PMM01050Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMM01050");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01050DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01050DTO> GetRateOTDateList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateOTDateList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01050DTO> loRtn = null;
            List<PMM01050DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateOTDateList");

            try
            {
                var loCls = new PMM01050Cls();
                loTempRtn = new List<PMM01050DTO>();
                var poParam = new PMM01050DTO();

                _Logger.LogInfo("Set Param GetRateOTDateList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateOTDateList");
                loTempRtn = loCls.GetAllRateOTDateList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateOTDateList");
                loRtn = GetRateOTListStream<PMM01050DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateOTDateList");

            return loRtn;
        }
    }
}