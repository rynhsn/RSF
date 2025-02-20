using PMM01500BACK;
using PMM01500COMMON;
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

namespace PMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM01520Controller : ControllerBase, IPMM01520
    {
        private LoggerPMM01520 _Logger;
        private readonly ActivitySource _activitySource;

        public PMM01520Controller(ILogger<LoggerPMM01520> logger)
        {
            //Initial and Get Logger
            LoggerPMM01520.R_InitializeLogger(logger);
            _Logger = LoggerPMM01520.R_GetInstanceLogger();
            _activitySource = PMM01520ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01520Controller));
        }

        private async IAsyncEnumerable<T> GetListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01520DTO> GetPinaltyDateListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetPinaltyDateListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01520DTO> loRtn = null;
            var loParameter = new PMM01520DTO();
            _Logger.LogInfo("Start GetPinaltyDateListStream");

            try
            {
                var loCls = new PMM01520Cls();

                _Logger.LogInfo("Set Param GetPinaltyDateListStream");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);

                _Logger.LogInfo("Call Back Method GetAllAdditionalId");
                var loResult = loCls.GetAllPinaltyDate(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetPinaltyDateListStream");
                loRtn = GetListStream<PMM01520DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPinaltyDateListStream");

            return loRtn;
        }

        [HttpPost]
        public PMM01500SingleResult<PMM01520DTO> GetPinaltyDate(PMM01520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPinaltyDate");
            var loEx = new R_Exception();
            PMM01500SingleResult<PMM01520DTO> loRtn = new PMM01500SingleResult<PMM01520DTO>();
            _Logger.LogInfo("Start GetPinaltyDate PMM01520");

            try
            {
                _Logger.LogInfo("Set Param Entity GetPinaltyDate PMM01520");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01520Cls();

                _Logger.LogInfo("Call Back Method GetPinaltyDateSP PMM01520Cls");
                var loTempData = loCls.GetPinaltyDateSP(poEntity);
                loRtn.Data = loTempData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPinaltyDate PMM01520");

            return loRtn;
        }

        [HttpPost]
        public PMM01500SingleResult<PMM01520DTO> GetPinalty(PMM01520DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPinalty");
            var loEx = new R_Exception();
            PMM01500SingleResult<PMM01520DTO> loRtn = new PMM01500SingleResult<PMM01520DTO>();
            _Logger.LogInfo("Start GetPinalty PMM01520");

            try
            {
                _Logger.LogInfo("Set Param Entity GetPinalty PMM01520");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01520Cls();

                _Logger.LogInfo("Call Back Method GetPinaltySP PMM01520Cls");
                var loTempData = loCls.GetPinaltySP(poEntity);
                loRtn.Data = loTempData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPinalty PMM01520");

            return loRtn;
        }

        [HttpPost]
        public PMM01500SingleResult<PMM01520DTO> SaveDeletePinalty(PMM01520SaveParameterDTO<PMM01520DTO> poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SavePinalty");
            var loEx = new R_Exception();
            PMM01500SingleResult<PMM01520DTO> loRtn = new PMM01500SingleResult<PMM01520DTO>();
            _Logger.LogInfo("Start SavePinalty PMM01520");

            try
            {
                _Logger.LogInfo("Set Param Entity SavePinalty PMM01520");
                poEntity.poData.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.poData.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM01520Cls();

                _Logger.LogInfo("Call Back Method SavePinaltySP PMM01520Cls");
                loRtn.Data = loCls.SaveDeletePinalty(poEntity.poData, poEntity.poCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SavePinalty PMM01520");

            return loRtn;
        }
    }
}
