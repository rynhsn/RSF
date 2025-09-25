using GSM02500COMMON.DTOs.GSM02540;
using R_APICommonDTO;
using R_BlazorFrontEnd.Excel;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02520;

namespace GSM02500MODEL.View_Model
{
    public class UploadOtherUnitViewModel : R_ViewModel<UploadOtherUnitDTO>, R_IProcessProgressStatus
    {
        public Action<R_APIException> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        //private UploadOtherUnitModel loModel = new UploadOtherUnitModel();

        //private GSM02541Model loOtherUnitModel = new GSM02541Model();

        public ObservableCollection<UploadOtherUnitDTO> loUploadOtherUnitDisplayList = new ObservableCollection<UploadOtherUnitDTO>();

        public List<UploadOtherUnitDTO> loUploadOtherUnitList = new List<UploadOtherUnitDTO>();

        //public UploadOtherUnitResultDTO loRtn = new UploadOtherUnitResultDTO();

        public UploadOtherUnitParameterDTO loParameter = new UploadOtherUnitParameterDTO();

        //public List<UploadOtherUnitErrorDTO> loErrorList = new List<UploadOtherUnitErrorDTO>();
        
        //public UploadOtherUnitErrorResultDTO loErrorRtn = new UploadOtherUnitErrorResultDTO();

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
        public bool IsUploadSuccesful = true;
/*
        public async Task ValidateUploadOtherUnit()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<UploadOtherUnitDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_OTHER_UNIT_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_OTHER_UNIT_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = SelectedCompanyId;
                loUploadPar.USER_ID = SelectedUserId;
                loUploadPar.UserParameters = loUserParam;
                loUploadPar.ClassName = "GSM02500BACK.ValidateUploadOtherUnitCls";
                loUploadPar.BigObject = loUploadOtherUnitList;

                await loCls.R_BatchProcess<List<UploadOtherUnitDTO>>(loUploadPar, 15);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        public async Task GetUploadOtherUnitListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {*//*
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_OTHER_UNIT_STREAMING_CONTEXT, loUploadOtherUnitList);
                loRtn = await loModel.GetUploadOtherUnitListStreamAsync();*//*

                GSM02541ResultDTO loResult = new GSM02541ResultDTO();

                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02540_PROPERTY_ID_STREAMING_CONTEXT, loParameter.PropertyData.CPROPERTY_ID);
                loResult = await loOtherUnitModel.GetOtherUnitListStreamAsync();

                List<UploadOtherUnitDTO> loTemp = new List<UploadOtherUnitDTO>();
                loTemp = loUploadOtherUnitList.Select(x => new UploadOtherUnitDTO()
                {
                    CompanyId = x.CompanyId,
                    PropertyId = x.PropertyId,
                    OtherUnitId = x.OtherUnitId,
                    OtherUnitName = x.OtherUnitName,
                    OtherUnitType = x.OtherUnitType,
                    Building = x.Building,
                    Floor = x.Floor,
                    Location = x.Location,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = x.Notes,
                    Var_Exists = loResult.Data.Any(y => x.OtherUnitId == y.COTHER_UNIT_ID)
                }).ToList();

                loUploadOtherUnitDisplayList = new ObservableCollection<UploadOtherUnitDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
*/
        public async Task SaveUploadOtherUnitAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<UploadOtherUnitDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_OTHER_UNIT_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_OTHER_UNIT_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadOtherUnitDisplayList.Count == 0)
                    return;

                Bigobject = loUploadOtherUnitDisplayList.ToList<UploadOtherUnitDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "GSM02500BACK.UploadOtherUnitCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<UploadOtherUnitDTO>>(loBatchPar, 15);
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
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";

                    loResult = await ServiceGetError(pcKeyGuid);

                    VisibleError = true;
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
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            //R_Exception loException = new R_Exception();
            //ex.ErrorList.ForEach(l =>
            //{
            //    loException.Add(l.ErrNo, l.ErrDescp);
            //});

            ShowErrorAction(ex);
            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_OTHER_UNITResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                loUploadOtherUnitDisplayList.ToList().ForEach(x =>
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