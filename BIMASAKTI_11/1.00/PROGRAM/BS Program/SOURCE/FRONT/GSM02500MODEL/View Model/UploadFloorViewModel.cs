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
        public Action<R_Exception> ShowErrorAction { get; set; }
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
        public bool IsUploadSuccesful = true;
/*
        public async Task ValidateUploadFloor()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<UploadFloorDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_FLOOR_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_FLOOR_BUILDING_ID_CONTEXT, Value = loParameter.BuildingData.CBUILDING_ID });
                
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
                loUploadPar.ClassName = "GSM02500BACK.ValidateUploadFloorCls";
                loUploadPar.BigObject = loUploadFloorList;

                await loCls.R_BatchProcess<List<UploadFloorDTO>>(loUploadPar, 10);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }*/
/*
        public async Task GetUploadFloorListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {*//*
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_FLOOR_STREAMING_CONTEXT, loUploadFloorList);
                loRtn = await loModel.GetUploadFloorListStreamAsync();*//*
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02520_BUILDING_ID_STREAMING_CONTEXT, loParameter.BuildingData.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02520_PROPERTY_ID_STREAMING_CONTEXT, loParameter.PropertyData.CPROPERTY_ID);

                GSM02520ListDTO loResult = new GSM02520ListDTO();
                loResult = await loFloorModel.GetFloorListStreamAsync();

                List<UploadFloorDTO> loTemp = new List<UploadFloorDTO>();

                loTemp = loUploadFloorList.Select(x => new UploadFloorDTO()
                {
                    CompanyId = x.CompanyId,
                    PropertyId = x.PropertyId,
                    BuildingId = x.BuildingId,
                    FloorName = x.FloorName,
                    FloorCode = x.FloorCode,
                    UnitCategory = x.UnitCategory,
                    UnitType = x.UnitType,
                    Description = x.Description,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = x.Notes,
                    Var_Exists = loResult.Data.Any(y => x.FloorCode == y.CFLOOR_ID)
                }).ToList();

                loUploadFloorDisplayList = new ObservableCollection<UploadFloorDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
*//*
        public async Task CheckIsFloorUsedAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCheckIsFloorUsedRtn = await loModel.CheckIsFloorUsedAsync();
                loCheckIsFloorUsed = loCheckIsFloorUsedRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }*/
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
            R_Exception loException = new R_Exception();
            List<R_ErrorStatusReturn> loResult = null;

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    SetPercentageAndMessageAction(Message, Percentage);
                    VisibleError = false;
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                    SetPercentageAndMessageAction(Message, Percentage);

                    try
                    {
                        loResult = await ServiceGetError(pcKeyGuid);
                        loUploadFloorDisplayList.ToList().ForEach(x =>
                        {
                            if (loResult.Any(y => y.SeqNo == x.No))
                            {
                                x.Notes = loResult.Where(y => y.SeqNo == x.No).FirstOrDefault().ErrorMessage;
                                x.Valid = "N";
                                SumInvalid++;
                            }
                            else
                            {
                                x.Valid = "Y";
                                SumValid++;
                            }
                        });

                        if (loResult.Any(x => x.SeqNo < 0))
                        {
                            loResult.Where(x => x.SeqNo < 0).ToList().ForEach(x => loException.Add(x.SeqNo.ToString(), x.ErrorMessage));
                        }
                        SetValidInvalidAction();
                    }
                    catch (Exception ex)
                    {
                        loException.Add(ex);
                    }
                    if (loException.HasError)
                    {
                        ShowErrorAction(loException);
                    }
                    VisibleError = true;
                }
                StartUploadUnitAction();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loException.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
            SetPercentageAndMessageAction(Message, Percentage);

            R_Exception loException = new R_Exception();
            ex.ErrorList.ForEach(l =>
            {
                loException.Add(l.ErrNo, l.ErrDescp);
            });

            ShowErrorAction(loException);
            StateChangeAction();
            StartUploadUnitAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            SetPercentageAndMessageAction(Message, Percentage);
            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        private async Task<List<R_ErrorStatusReturn>> ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

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
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            return loResultData;
        }
/*
        private async Task GetError(string pcKeyGuid)
        {
            //R_APIException loException;
            //R_ProcessAndUploadClient loCls;
            //List<R_ErrorStatusReturn> loErrRtn;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_FLOOR_ERROR_GUID_STREAMING_CONTEXT, pcKeyGuid);

                loErrorRtn = await loModel.GetErrorProcessListAsync();
                loErrorList = loErrorRtn.Data;


                loUploadFloorList = loErrorList.Select(x => new UploadFloorDTO
                {
                    CompanyId = SelectedCompanyId,
                    PropertyId = loParameter.PropertyData.CPROPERTY_ID,
                    BuildingId = loParameter.BuildingData.CBUILDING_ID,
                    FloorCode = x.FloorCode,
                    FloorName = x.FloorName,
                    Description = x.Description,
                    UnitCategory = x.UnitCategory,
                    UnitType = x.UnitType,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = x.ErrorMessage,
                    Var_Exists = false

                }).ToList();

                loUploadFloorDisplayList = new ObservableCollection<UploadFloorDTO>(loUploadFloorList);

                VisibleError = true;
                IsUploadSuccesful = !VisibleError;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }*/
        #endregion
    }
}
