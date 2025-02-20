using BMM00500BACK;
using BMM00500BACK.OpenTelemetry;
using BMM00500COMMON;
using BMM00500COMMON.DTO;
using BMM00500COMMON.Interface;
using BMM00500COMMON.Loggers;
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

namespace BMM00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BMM00500Controller : ControllerBase, IBMM00500
    {
        private readonly LoggerBMM00500 _logger;
        private readonly ActivitySource _activitySource;
        public BMM00500Controller(ILogger<BMM00500Controller> logger)
        {
            LoggerBMM00500.R_InitializeLogger(logger);
            _logger = LoggerBMM00500.R_GetInstanceLogger();
            _activitySource = BMM00500ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(BMM00500Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<BMM00500DTO> GetMobileProgramList()
        {
            string? lcMethod = nameof(GetMobileProgramList);
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();

            BMM00500Cls loCls;
            BMM00500ParameterDTO loParameter;
            IAsyncEnumerable<BMM00500DTO>? loRtn = null;

            try
            {
                _logger.LogInfo(string.Format("Initialization loCls in Method {0}", lcMethod));
                loCls = new BMM00500Cls();

                _logger.LogInfo(string.Format("Set Parameter for {1} || {0}", lcMethod, nameof(loCls.GetMobileProgramDb)));
                loParameter = new BMM00500ParameterDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROGRAM_ID = "",
                };

                _logger.LogInfo(string.Format("Run {1} || {0}", lcMethod, nameof(loCls.GetMobileProgramDb)));
                var loTemp = loCls.GetMobileProgramDb(loParameter);

                loRtn = R_NetCoreUtility.R_GetAsyncStream(loTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<BMM00500CRUDParameterDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));
            
            R_Exception loEx = new R_Exception();

            BMM00500Cls loCls;
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                _logger.LogInfo(string.Format("Initialization loCls in Method {0}", lcMethod));
                loCls = new BMM00500Cls();

                _logger.LogInfo(string.Format("Set Parameter for {1} || {0}", lcMethod, nameof(loCls.R_Delete)));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CACTION = "DELETE";

                _logger.LogInfo(string.Format("Run {1} || {0}", lcMethod, nameof(loCls.R_Delete)));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<BMM00500CRUDParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<BMM00500CRUDParameterDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();

            BMM00500Cls loCls;
            R_ServiceGetRecordResultDTO<BMM00500CRUDParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<BMM00500CRUDParameterDTO>();

            try
            {
                _logger.LogInfo(string.Format("Initialization loCls in Method {0}", lcMethod));
                loCls = new BMM00500Cls();

                _logger.LogInfo(string.Format("Set Parameter for {1} || {0}", lcMethod, nameof(loCls.R_GetRecord)));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo(string.Format("Run {1} || {0}", lcMethod, nameof(loCls.R_GetRecord)));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<BMM00500CRUDParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<BMM00500CRUDParameterDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
            _logger.LogInfo(string.Format("START Method {0}", lcMethod));

            R_Exception loEx = new R_Exception();

            BMM00500Cls loCls;
            R_ServiceSaveResultDTO<BMM00500CRUDParameterDTO> loRtn = new R_ServiceSaveResultDTO<BMM00500CRUDParameterDTO>();

            try
            {
                _logger.LogInfo(string.Format("Initialization loCls in Method {0}", lcMethod));
                loCls = new BMM00500Cls();

                _logger.LogInfo(string.Format("Set Parameter for {1} || {0}", lcMethod, nameof(loCls.R_Save)));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo(string.Format("Run {1} || {0}", lcMethod, nameof(loCls.R_Save)));
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError("{@ErrorObject}", loEx.Message);
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }
    }
}
