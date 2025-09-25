using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON.DTOs.GSM02541;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GSM02500MODEL.View_Model
{
    public class UploadFloorViewModel : R_ViewModel<UploadFloorDTO>, R_IProcessProgressStatus
    {
        public Action<R_APIException> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }
        public Action SetValidInvalidAction { get; set; }
        public Action StartUploadUnitAction { get; set; }
        public Action<string, int> SetPercentageAndMessageAction { get; set; }

        //private UploadFloorModel loModel = new UploadFloorModel();

        public ObservableCollection<UploadFloorDTO> loUploadFloorDisplayList = new ObservableCollection<UploadFloorDTO>();

        public List<UploadFloorDTO> loUploadFloorList = new List<UploadFloorDTO>();

        public UploadFloorParameterDTO loParameter = new UploadFloorParameterDTO();


        public int SumValid { get; set; }
        public int SumInvalid { get; set; }
        public int SumList { get; set; }

        public string SelectedCompanyId = "";
        public string SelectedUserId = "";

        public bool IsOverWrite = false;

        public string PropertyName = "";
        public string SourceFileName = "";
        public string Message = "";
        public int Percentage = 0;
        public bool OverwriteData = false;
        public byte[] fileByte = null;

        public bool VisibleError = false;
        public bool IsErrorEmptyFile = false;
        public bool IsUploadSuccesful = false;
        public async Task SaveUploadFloorAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<UploadFloorDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_FLOOR_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_FLOOR_BUILDING_ID_CONTEXT, Value = loParameter.BuildingData.CBUILDING_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_FLOOR_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadFloorDisplayList.Count == 0)
                    return;

                Bigobject = loUploadFloorDisplayList.ToList<UploadFloorDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "GSM02500BACK.UploadFloorCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<UploadFloorDTO>>(loBatchPar, 10);
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
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                    IsUploadSuccesful = true;

                    loUploadFloorDisplayList.ToList().ForEach(x =>
                    {
                        x.Valid = "Y";
                        SumValid++;
                    });
                    //ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";

                    loResult = await ServiceGetError(pcKeyGuid);

                    VisibleError = true;
                }
                StartUploadUnitAction();
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
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            //R_Exception loException = new R_Exception();
            //ex.ErrorList.ForEach(l =>
            //{
            //    loException.Add(l.ErrNo, l.ErrDescp);
            //});

            ShowErrorAction(ex);
            StateChangeAction();
            StartUploadUnitAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            SetPercentageAndMessageAction(Message, Percentage);
            StateChangeAction();

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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_PROPERTY_FLOORResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                loUploadFloorDisplayList.ToList().ForEach(x =>
                {
                    if (loResultData.Any(y => y.SeqNo == x.No))
                    {
                        x.Notes = loResultData.Where(y => y.SeqNo == x.No).FirstOrDefault().ErrorMessage;
                        x.Valid = "N";
                        SumInvalid++;
                    }
                    else
                    {
                        x.Valid = "Y";
                        SumValid++;
                    }
                });

                if (loResultData.Any(x => x.SeqNo < 0))
                {
                    var loUnhandleEx = loResultData.Where(x => x.SeqNo < 0).Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    var loEx = new R_Exception();
                    loUnhandleEx.ForEach(x => loEx.Add(x));

                    loException = R_FrontUtility.R_ConvertToAPIException(loEx);
                }
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
