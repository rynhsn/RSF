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
    public class LMM01520Controller : ControllerBase, ILMM01520
    {
        private LoggerLMM01520 _Logger;

        public LMM01520Controller(ILogger<LoggerLMM01520> logger)
        {
            //Initial and Get Logger
            LoggerLMM01520.R_InitializeLogger(logger);
            _Logger = LoggerLMM01520.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01522DTO> GetAdditionalIdLookup()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01522DTO> loRtn = null;
            var loParameter = new LMM01522DTO();
            _Logger.LogInfo("Start GetAdditionalIdLookup");

            try
            {
                var loCls = new LMM01520Cls();

                _Logger.LogInfo("Set Param GetAdditionalIdLookup");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetAllAdditionalId");
                var loResult = loCls.GetAllAdditionalId(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetAdditionalIdLookup");
                loRtn = GetTemplateAndBankAccountListStream<LMM01522DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAdditionalIdLookup");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetTemplateAndBankAccountListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01520DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01520DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01520DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01520DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01520DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01520");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01520");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01520Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01520Cls");
                var loTempData = loCls.R_GetRecord(poParameter.Entity);
                loRtn.data = loTempData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01520");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01520DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01520DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01520DTO> loRtn = new R_ServiceSaveResultDTO<LMM01520DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01520");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01520");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01520Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01520Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01520");

            return loRtn;
        }
    }
}
