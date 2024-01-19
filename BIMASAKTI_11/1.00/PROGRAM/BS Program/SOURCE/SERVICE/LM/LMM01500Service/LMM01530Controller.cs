using LMM01500BACK;
using LMM01500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01530Controller : ControllerBase, ILMM01530
    {
        private LoggerLMM01530 _Logger;

        public LMM01530Controller(ILogger<LoggerLMM01530> logger)
        {
            //Initial and Get Logger
            LoggerLMM01530.R_InitializeLogger(logger);
            _Logger = LoggerLMM01530.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01530DTO> GetAllOtherChargerList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01530DTO> loRtn = null;
            List<LMM01530DTO> loTempRtn = null;
            var loParameter = new LMM01530DTO();
            _Logger.LogInfo("Start GetAllOtherChargerList");

            try
            {
                var loCls = new LMM01530Cls();
                loTempRtn = new List<LMM01530DTO>();

                _Logger.LogInfo("Set Param GetAllOtherChargerList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CINVGRP_CODE);
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllOtherCharges");
                loTempRtn = loCls.GetAllOtherCharges(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetAllOtherChargerList");
                loRtn = GetAllOtherChargerListtStream<LMM01530DTO>(loTempRtn);
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
        public IAsyncEnumerable<LMM01531DTO> GetOtherChargesLookup()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01531DTO> loRtn = null;
            _Logger.LogInfo("Start GetOtherChargesLookup");

            try
            {
                var loCls = new LMM01530Cls();
                var poParam = new LMM01531DTO();

                _Logger.LogInfo("Set Param GetOtherChargesLookup");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetAllOtherChargesLookup");
                var loResult = loCls.GetAllOtherChargesLookup(poParam);

                _Logger.LogInfo("Call Stream Method Data GetOtherChargesLookup");
                loRtn = GetAllOtherChargerListtStream<LMM01531DTO>(loResult);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01530DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete LMM01530");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete LMM01530");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new LMM01530Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM01530Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM01530");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01530DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01530DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01530DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01530DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01530");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01530");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new LMM01530Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01530Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01530");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01530DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01530DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01530DTO> loRtn = new R_ServiceSaveResultDTO<LMM01530DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01530");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01530");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new LMM01530Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01530Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01530");

            return loRtn;
        }
    }
}
