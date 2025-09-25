using GLB00700COMMON;
using GLB00700COMMON.DTO_s;
using GLB00700COMMON.DTO_s.Helper;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLB00700FrontResources;
using System.Globalization;

namespace GLB00700MODEL.View_Model_s
{
    public class GLB00700ViewModel : R_IProcessProgressStatus
    {
        //variables
        private GLB00700Model _model = new GLB00700Model();
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_MODULE = "GL";
        private const int BATCH_TOTAL_STEP = 3;
        public Action StateChangeAction { get; set; }
        public Action<R_APIException> DisplayErrorAction { get; set; }
        public Func<Task> ShowSuccessAction { get; set; }
        public string Message = "";
        public int Percentage = 0;
        public string CompanyId { get; set; } = "";
        public string UserId { get; set; } = "";
        public GLSysParamDTO GLSysParam { get; set; } = new GLSysParamDTO();//init var
        public TodayDTO TodayDTO { get; set; } = new TodayDTO(); //init var
        public RateRevaluationParamDTO Param { get; set; } = new RateRevaluationParamDTO(); //for param
        public LastRateRevaluationDTO LastRateRevaluation { get; set; } = new LastRateRevaluationDTO();
        public List<GeneralTypeDTO> RadioProcesFor { get; set; } = new List<GeneralTypeDTO>();//list radio
        public DateTime DDATE { get; set; } = DateTime.Now;

        //methods
        public async Task InitProcessAsync(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetGLSysParamRecord();
                await GetTodayRecord();
                if (TodayDTO.DTODAY != null)
                {
                    DDATE = TodayDTO.DTODAY;
                }
                RadioProcesFor = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "AR", CTYPE_NAME = poParamLocalizer["_radio_ar"] },
                    new GeneralTypeDTO { CTYPE_CODE = "AP", CTYPE_NAME = poParamLocalizer ["_radio_ap"] },
                    new GeneralTypeDTO { CTYPE_CODE = "CB", CTYPE_NAME = poParamLocalizer ["_radio_cb"] },
                };
                Param.CPROCESS_FOR = "AR";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetGLSysParamRecord()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetGLSysParamRecordAsync();
                GLSysParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetTodayRecord()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetTodayRecordAsync();
                TodayDTO = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRateRevaluatoionRecordAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetLastRateRevaluationRecordAsync(new RateRevaluationParamDTO()
                {
                    CDEPT_CODE = Param.CDEPT_CODE
                });
                LastRateRevaluation = loResult;
                LastRateRevaluation.DLAST_AR_DATE = ChangeStringToDateFormat(LastRateRevaluation.CLAST_AR_DATE);
                LastRateRevaluation.DLAST_AP_DATE = ChangeStringToDateFormat(LastRateRevaluation.CLAST_AP_DATE);
                LastRateRevaluation.DLAST_CB_DATE = ChangeStringToDateFormat(LastRateRevaluation.CLAST_CB_DATE);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //batch methods
        public async Task ProcessBatchAsync()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>
                {
                    new R_KeyValue()
                    { Key = GLB00700ContextConstant.CDEPT_CODE, Value = Param.CDEPT_CODE },
                    new R_KeyValue()
                    { Key = GLB00700ContextConstant.CDATE, Value = DDATE.ToString("yyyyMMdd") },
                    new R_KeyValue()
                    { Key = GLB00700ContextConstant.CPROCESS_FOR, Value = Param.CPROCESS_FOR }
                };

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME,
                    poProcessProgressStatus: this);

                loBatchPar = new R_BatchParameter { COMPANY_ID = CompanyId, USER_ID = UserId, UserParameters = loBatchParUserParameters, ClassName = "GLB00700BACK.GLB00701Cls", BigObject = new object() };

                await loCls.R_BatchProcess<object>(loBatchPar, BATCH_TOTAL_STEP);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_APIException loEx = new R_APIException();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    await ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    await ServiceGetError(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loEx.add(ex);
            }
            StateChangeAction();
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            DisplayErrorAction.Invoke(ex);
            StateChangeAction();
            await Task.CompletedTask;
        }
        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);
            StateChangeAction();
            await Task.CompletedTask;
        }
        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            try
            {
                //Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    //RESOURCE_NAME = "RSP_PM_PROCESS_UPDATE_UTILITY_CHARGESResources"
                };

                loCls = new R_ProcessAndUploadClient(
                     pcModuleName: DEFAULT_MODULE, //pm
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME //R_DefaultServiceUrlPM
                    );

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();

                var loEx = new R_Exception();
                loUnhandleEx.ForEach(x => loEx.Add(x));

                loException = R_FrontUtility.R_ConvertToAPIException(loEx);
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        //helper
        private DateTime? ChangeStringToDateFormat(string? pcDate)
        {
            if (!string.IsNullOrWhiteSpace(pcDate))
            {
                return DateTime.ParseExact(pcDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            return null;
        }

    }
}
