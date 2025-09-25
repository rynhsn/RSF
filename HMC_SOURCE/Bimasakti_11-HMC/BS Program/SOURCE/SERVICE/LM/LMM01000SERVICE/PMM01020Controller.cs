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
    public class PMM01020Controller : ControllerBase, IPMM01020
    {
        private LoggerPMM01020 _Logger;
        private readonly ActivitySource _activitySource;
        public PMM01020Controller(ILogger<LoggerPMM01020> logger)
        {
            //Initial and Get Logger
            LoggerPMM01020.R_InitializeLogger(logger);
            _Logger = LoggerPMM01020.R_GetInstanceLogger();
            _activitySource = PMM01020ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01020Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01021DTO> GetRateWGList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateWGList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01021DTO> loRtn = null;
            List<PMM01021DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateWGList");

            try
            {
                var loCls = new PMM01020Cls();
                loTempRtn = new List<PMM01021DTO>();
                var poParam = new PMM01021DTO();

                _Logger.LogInfo("Set Param GetRateWGList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CCHARGES_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_DATE);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateWGList");
                loTempRtn = loCls.GetAllRateWGList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateWGList");
                loRtn = GetRateWGListStream<PMM01021DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateWGList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetRateWGListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01020DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM01020DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMM01020DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01020DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMM01020");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord PMM01020");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01020Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMM01020Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMM01020");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01020DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01020DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMM01020DTO> loRtn = new R_ServiceSaveResultDTO<PMM01020DTO>();
            _Logger.LogInfo("Start ServiceSave PMM01020");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave PMM01020");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01020Cls();

                _Logger.LogInfo("Call Back Method R_Save PMM01020Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMM01020");

            return loRtn;
        }
        
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01020DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01020DTO> GetRateWGDateList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateWGDateList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01020DTO> loRtn = null;
            List<PMM01020DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateWGDateList");

            try
            {
                var loCls = new PMM01020Cls();
                loTempRtn = new List<PMM01020DTO>();
                var poParam = new PMM01020DTO();

                _Logger.LogInfo("Set Param GetRateWGDateList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateWGDateList");
                loTempRtn = loCls.GetAllRateWGDateList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateWGDateList");
                loRtn = GetRateWGListStream<PMM01020DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateWGDateList");

            return loRtn;
        }
    }
}