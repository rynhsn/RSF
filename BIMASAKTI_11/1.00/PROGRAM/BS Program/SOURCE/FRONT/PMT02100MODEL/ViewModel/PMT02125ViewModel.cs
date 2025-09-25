using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.DTOs.PMT02120;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON;
using PMT02100COMMON.DTOs.PMT02130;
using R_APICommonDTO;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System.Linq;

namespace PMT02100MODEL.ViewModel
{
    public class PMT02125ViewModel : R_ViewModel<PMT02120HandoverUtilityDTO>, R_IProcessProgressStatus
    {
        public Action<R_APIException> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        private PMT02120Model loModel = new PMT02120Model();

        private PMT02130Model loTabHistoryModel = new PMT02130Model();

        public PMT02100HandoverDTO loHeader = new PMT02100HandoverDTO();

        public ObservableCollection<PMT02120HandoverUtilityDTO> loHandoverUtilityList = new ObservableCollection<PMT02120HandoverUtilityDTO>();

        public ObservableCollection<PMT02130HandoverUnitDTO> loHandoverUnitList = new ObservableCollection<PMT02130HandoverUnitDTO>();

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public async Task GetHandoverUtilityListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02120HandoverUtilityParameterDTO loParam = null;
            PMT02120HandoverUtilityResultDTO loRtn = null;
            try
            {
                loParam = new PMT02120HandoverUtilityParameterDTO()
                {
                    CPROPERTY_ID = loHeader.CPROPERTY_ID,
                    CDEPT_CODE = loHeader.CDEPT_CODE,
                    CREF_NO = loHeader.CREF_NO,
                    CTRANS_CODE = loHeader.CTRANS_CODE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02120_GET_HANDOVER_UTILITY_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loModel.GetHandoverUtilityListStreamAsync();

                loRtn.Data.ForEach(x =>
                {
                    if (!string.IsNullOrWhiteSpace(x.CSTART_INV_PRD))
                    {
                        x.ISTART_INV_PRD_YEAR = int.Parse(x.CSTART_INV_PRD.Substring(0, 4));
                        x.ISTART_INV_PRD_MONTH = int.Parse(x.CSTART_INV_PRD.Substring(x.CSTART_INV_PRD.Length - 2));
                    }
                    x.CCHARGES_DISPLAY = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")";
                });

                loHandoverUtilityList = new ObservableCollection<PMT02120HandoverUtilityDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetHandoverUnitListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            PMT02130HandoverUnitParameterDTO loParam = null;
            PMT02130HandoverUnitResultDTO loRtn = null;
            try
            {
                loParam = new PMT02130HandoverUnitParameterDTO()
                {
                    CPROPERTY_ID = loHeader.CPROPERTY_ID,
                    CDEPT_CODE = loHeader.CDEPT_CODE,
                    CREF_NO = loHeader.CREF_NO,
                    CTRANS_CODE = loHeader.CTRANS_CODE
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT02130_GET_HANDOVER_UNIT_LIST_STREAM_CONTEXT, loParam);
                loRtn = await loTabHistoryModel.GetHandoverUnitListStreamAsync();
                loHandoverUnitList = new ObservableCollection<PMT02130HandoverUnitDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task HandoverProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            PMT02125BigObjectDTO Bigobject = new PMT02125BigObjectDTO();
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02125_HANDOVER_PROCESS_PROPERTY_ID, Value = loHeader.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02125_HANDOVER_PROCESS_CDEPT_CODE, Value = loHeader.CDEPT_CODE });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02125_HANDOVER_PROCESS_CTRANS_CODE, Value = loHeader.CTRANS_CODE });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02125_HANDOVER_PROCESS_CREF_NO, Value = loHeader.CREF_NO });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.PMT02125_HANDOVER_PROCESS_CHO_ACTUAL_DATE, Value = loHeader.DHO_ACTUAL_DATE.Value.ToString("yyyyMMdd") });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //Check Data
                if (loHandoverUnitList.Count == 0 && loHandoverUtilityList.Count == 0)
                    return;


                Bigobject.UnitList = loHandoverUnitList.Select((x, i) => new PMT02120HandoverProcessUnitDTO()
                {
                    NO = i + 1,
                    CUNIT_ID = x.CUNIT_ID,
                    CFLOOR_ID = x.CFLOOR_ID,
                    CBUILDING_ID = x.CBUILDING_ID,
                    NACTUAL_AREA_SIZE = x.NACTUAL_AREA_SIZE
                }).ToList();

                Bigobject.UtilityList = loHandoverUtilityList.Select((x, i) => new PMT02120HandoverProcessUtilityDTO()
                {
                    NO = i + 1,
                    CUNIT_ID = x.CUNIT_ID,
                    CFLOOR_ID = x.CFLOOR_ID,
                    CBUILDING_ID = loHeader.CBUILDING_ID,
                    CCHARGES_TYPE = x.CCHARGES_TYPE,
                    CCHARGES_ID = x.CCHARGES_ID,
                    CSEQ_NO = x.CSEQ_NO,
                    CSTART_INV_PRD = x.CSTART_INV_PRD,
                    NMETER_START = x.NMETER_START,
                    NBLOCK1_START = x.NBLOCK1_START,
                    NBLOCK2_START = x.NBLOCK2_START
                }).ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "PMT02100BACK.PMT02125Cls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                //PROCESS_TYPE = UPLOAD_PROCESS;
                lcGuid = await loCls.R_BatchProcess<PMT02125BigObjectDTO>(loBatchPar, 4);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn> loResult = null;

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    loResult = await ServiceGetError(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loException.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            ShowErrorAction(ex);
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            await Task.CompletedTask;
        }

        private async Task<List<R_ErrorStatusReturn>> ServiceGetError(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();

            List<R_ErrorStatusReturn> loResultData = null;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = SelectedCompanyId,
                    USER_ID = SelectedUserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_HANDOVER_PROCESSResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

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
            return loResultData;
        }
        #endregion
    }
}
