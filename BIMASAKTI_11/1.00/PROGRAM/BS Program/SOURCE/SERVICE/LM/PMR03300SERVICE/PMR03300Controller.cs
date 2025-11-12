using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR03300BACK;
using PMR03300COMMON;
using PMR03300COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PMR03300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMR03300Controller : ControllerBase, IPMR03300
    {
        private readonly LoggerPMR03300 _logger;
        private readonly ActivitySource _activitySource;
       
        public PMR03300Controller(ILogger<PMR03300Controller> logger)
        {
            LoggerPMR03300.R_InitializeLogger(logger);
            _logger = LoggerPMR03300.R_GetInstanceLogger(); 
            _activitySource = PMR03300Activity.R_InitializeAndGetActivitySource(nameof(PMR03300Controller));
        }

        [HttpPost]
        public async Task<PMR03300SingleDTO<PMR03300GetCompanyInfoDTO>> GetCompanyInfo()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetCompanyInfo));
            _logger.LogInfo("Start - GetCompanyInfo");
            R_Exception loEx = new();
            PMR03300Cls loCls = new();
            PMR03300SingleDTO<PMR03300GetCompanyInfoDTO> loReturn = new();
            PMR03300ParamDbDTO loParam = new PMR03300ParamDbDTO();

            try
            {
                _logger.LogInfo("Set Parameter");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Process GetCompanyInfo");
                loReturn.Data = await loCls.GetCompanyInfo(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetCompanyInfo");
            return loReturn;
        }

        [HttpPost]
        public async Task<PMR03300ListDTO<PMR03300GetPeriodDtListDTO>> GetPeriodDtList()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetPeriodDtList));
            R_Exception loEx = new();
            PMR03300ListDTO<PMR03300GetPeriodDtListDTO> loReturn = new();
            PMR03300Cls loCls = new();
            PMR03300ParamDbDTO loParam = new PMR03300ParamDbDTO();

            try
            {
                _logger.LogInfo("Start - GetPeriodDtList");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CYEAR = R_BackGlobalVar.USER_ID;
                loParam.CYEAR = R_Utility.R_GetStreamingContext<string>(PMR03300ContextHeaderDTO.CYEAR);
                loReturn.Data = await loCls.GetPeriodDtList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - GetPeriodDtList");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        [HttpPost]
        public async Task<PMR03300SingleDTO<PMR03300GetPeriodeYearRangeDTO>> GetPeriodeYearRange()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetPeriodeYearRange));
            _logger.LogInfo("Start - GetPeriodeYearRange");
            R_Exception loEx = new();
            PMR03300Cls loCls = new();
            PMR03300SingleDTO<PMR03300GetPeriodeYearRangeDTO> loReturn = new();
            PMR03300ParamDbDTO loParam = new PMR03300ParamDbDTO();

            try
            {
                _logger.LogInfo("Set Parameter");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CMODE = R_Utility.R_GetStreamingContext<string>(PMR03300ContextHeaderDTO.CMODE);
                loParam.CYEAR = R_Utility.R_GetStreamingContext<string>(PMR03300ContextHeaderDTO.CYEAR);
                _logger.LogInfo("Process GetPeriodeYearRange");
                loReturn.Data = await loCls.GetPeriodeYearRange(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetPeriodeYearRange");
            return loReturn;
        }

        [HttpPost]
        public async Task<PMR03300SingleDTO<PMR03300GetSystemParamDTO>> GetSystemParam()
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
            _logger.LogInfo("Start - GetSystemParam");
            R_Exception loEx = new();
            PMR03300Cls loCls = new();
            PMR03300SingleDTO<PMR03300GetSystemParamDTO> loReturn = new();
            PMR03300ParamDbDTO loParam = new PMR03300ParamDbDTO();

            try
            {
                _logger.LogInfo("Set Parameter");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMR03300ContextHeaderDTO.CPROPERTY_ID);
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Process GetSystemParam");
                loReturn.Data = await loCls.GetSystemParam(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("End - GetPeriodeYearRange");
            return loReturn;
        }

        [HttpPost]
        public async Task<PMR03300ListDTO<PMR03300PropertyDTO>> PMR03300GetPropertyList()
        {
            using var loActivity = _activitySource.StartActivity(nameof(PMR03300GetPropertyList));
            R_Exception loEx = new();
            PMR03300ListDTO<PMR03300PropertyDTO> loReturn = new();
            PMR03300Cls loCls = new();
            PMR03300ParamDbDTO loParam = new PMR03300ParamDbDTO();

            try
            {
                _logger.LogInfo("Start - Get Property List");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loReturn.Data = await loCls.GetPropertyList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            _logger.LogInfo("End - Get Property List");
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
    }
}
