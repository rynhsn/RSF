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
    public class PMM01530Controller : ControllerBase, IPMM01530
    {
        private LoggerPMM01530 _Logger;
        private readonly ActivitySource _activitySource;

        public PMM01530Controller(ILogger<LoggerPMM01530> logger)
        {
            //Initial and Get Logger
            LoggerPMM01530.R_InitializeLogger(logger);
            _Logger = LoggerPMM01530.R_GetInstanceLogger();
            _activitySource = PMM01530ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMM01530Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01530DTO> GetAllOtherChargerList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllOtherChargerList");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01530DTO> loRtn = null;
            List<PMM01530DTO> loTempRtn = null;
            var loParameter = new PMM01530DTO();
            _Logger.LogInfo("Start GetAllOtherChargerList");

            try
            {
                var loCls = new PMM01530Cls();
                loTempRtn = new List<PMM01530DTO>();

                _Logger.LogInfo("Set Param GetAllOtherChargerList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllOtherCharges");
                loTempRtn = loCls.GetAllOtherCharges(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetAllOtherChargerList");
                loRtn = GetAllOtherChargerListtStream<PMM01530DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllOtherChargerList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetAllOtherChargerListtStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<PMM01531DTO> GetOtherChargesLookup()
        {
            using Activity activity = _activitySource.StartActivity("GetOtherChargesLookup");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMM01531DTO> loRtn = null;
            _Logger.LogInfo("Start GetOtherChargesLookup");

            try
            {
                var loCls = new PMM01530Cls();
                var poParam = new PMM01531DTO();

                _Logger.LogInfo("Set Param GetOtherChargesLookup");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetAllOtherChargesLookup");
                var loResult = loCls.GetAllOtherChargesLookup(poParam);

                _Logger.LogInfo("Call Stream Method Data GetOtherChargesLookup");
                loRtn = GetAllOtherChargerListtStream<PMM01531DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetOtherChargesLookup");

            return loRtn;
        }

        [HttpPost]
        public PMM01500SingleResult<PMM01531DTO> GetOtherChargesRecordLookup(PMM01531DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetOtherChargesRecordLookup");
            var loEx = new R_Exception();
            PMM01500SingleResult<PMM01531DTO> loRtn = new();
            _Logger.LogInfo("Start GetOtherChargesRecordLookup");

            try
            {
                var loCls = new PMM01530Cls();

                _Logger.LogInfo("Set Param GetOtherChargesRecordLookup");
                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllOtherChargesLookup");
                var loResult = loCls.GetAllOtherChargesLookup(poEntity);

                _Logger.LogInfo("Get data by search text GetOtherChargesRecordLookup");
                loRtn.Data = loResult.Find(x => x.CCHARGES_ID == poEntity.CSEARCH_TEXT);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetOtherChargesRecordLookup");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM01530DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete PMM01530");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete PMM01530");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new PMM01530Cls();

                _Logger.LogInfo("Call Back Method R_Delete PMM01530Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete PMM01530");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM01530DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM01530DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<PMM01530DTO> loRtn = new R_ServiceGetRecordResultDTO<PMM01530DTO>();
            _Logger.LogInfo("Start ServiceGetRecord PMM01530");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord PMM01530");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new PMM01530Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord PMM01530Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord PMM01530");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM01530DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM01530DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<PMM01530DTO> loRtn = new R_ServiceSaveResultDTO<PMM01530DTO>();
            _Logger.LogInfo("Start ServiceSave PMM01530");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave PMM01530");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new PMM01530Cls();

                _Logger.LogInfo("Call Back Method R_Save PMM01530Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave PMM01530");

            return loRtn;
        }
    }
}
