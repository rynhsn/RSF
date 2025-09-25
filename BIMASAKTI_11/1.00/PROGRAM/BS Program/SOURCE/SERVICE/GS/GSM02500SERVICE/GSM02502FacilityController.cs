using GSM02500BACK;
using GSM02500BACK.OpenTelemetry;
using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02502Facility;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.Loggers;
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

namespace GSM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM02502FacilityController : ControllerBase, GSM02500BACK.IGSM02502Facility
    {
        private LoggerGSM02502Facility _logger;
        private readonly ActivitySource _activitySource;
        public GSM02502FacilityController(ILogger<GSM02502FacilityController> logger)
        {
            LoggerGSM02502Facility.R_InitializeLogger(logger);
            _logger = LoggerGSM02502Facility.R_GetInstanceLogger();
            _activitySource = GSM02502FacilityActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GSM02502FacilityController));
        }


        [HttpPost]
        public IAsyncEnumerable<GSM02502FacilityTypeDTO> GetFacilityTypeList()
        {
            return GetFacilityTypeStream();
        }
        private async IAsyncEnumerable<GSM02502FacilityTypeDTO> GetFacilityTypeStream()
        {
            using Activity activity = _activitySource.StartActivity("GetFacilityTypeList");
            _logger.LogInfo("Start || GetFacilityTypeList(Controller)");
            R_Exception loException = new R_Exception();
            List<GSM02502FacilityTypeDTO> loRtn = null;
            GSM02502FacilityCls loCls = new GSM02502FacilityCls();

            try
            {
                _logger.LogInfo("GetFacilityTypeList(Cls)  || GetFacilityTypeList(Controller)");
                loRtn = await loCls.GetFacilityTypeList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetFacilityTypeList(Controller)");

            foreach (GSM02502FacilityTypeDTO item in loRtn)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM02502FacilityDTO> GetFacilityList()
        {
            return GetFacilityStream();
        }
        private async IAsyncEnumerable<GSM02502FacilityDTO> GetFacilityStream()
        {
            using Activity activity = _activitySource.StartActivity("GetFacilityList");
            _logger.LogInfo("Start || GetFacilityList(Controller)");
            R_Exception loException = new R_Exception();
            List<GSM02502FacilityDTO> loRtn = null;
            GetFacilityListDTO loParam = new GetFacilityListDTO();
            GSM02502FacilityCls loCls = new GSM02502FacilityCls();

            try
            {
                _logger.LogInfo("Set Parameter || GetFacilityList(Controller)");
                loParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_FACILITY_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM02502_FACILITY_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetFacilityList(Cls) || GetFacilityList(Controller)");
                loRtn = await loCls.GetFacilityList(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetFacilityList(Controller)");

            foreach (GSM02502FacilityDTO item in loRtn)
            {
                yield return item;
            }
        }
        
        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<GSM02502FacilityParameterDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02502FacilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM02502FacilityParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02502FacilityParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM02502FacilityCls loCls = new GSM02502FacilityCls();

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
        public async Task<R_ServiceSaveResultDTO<GSM02502FacilityParameterDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02502FacilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM02502FacilityParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM02502FacilityParameterDTO>();
            GSM02502FacilityCls loCls = new GSM02502FacilityCls();

            try
            {
                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
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

        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02502FacilityParameterDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM02502FacilityCls loCls = new GSM02502FacilityCls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CACTION = "DELETE";

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
    }
}
