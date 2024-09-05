using GSM02500COMMON.DTOs.GSM02502;
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
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02520;

namespace GSM02500MODEL.View_Model
{
    public class UploadUnitTypeViewModel : R_ViewModel<UploadUnitTypeDTO>, R_IProcessProgressStatus
    {
        public Action<R_Exception> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        //private GSM02503UnitTypeModel loUnitTypeModel = new GSM02503UnitTypeModel();

        //private UploadUnitTypeModel loModel = new UploadUnitTypeModel();

        public ObservableCollection<UploadUnitTypeDTO> loUploadUnitTypeDisplayList = new ObservableCollection<UploadUnitTypeDTO>();

        public UploadUnitTypeParameterDTO loParameter = new UploadUnitTypeParameterDTO();

        public List<UploadUnitTypeDTO> loUploadUnitTypeList = new List<UploadUnitTypeDTO>();

        //public UploadUnitTypeResultDTO loRtn = new UploadUnitTypeResultDTO();

        //public CheckIsUnitTypeUsedDTO loCheckIsUnitTypeUsed = new CheckIsUnitTypeUsedDTO();

        //public CheckIsUnitTypeUsedResultDTO loCheckIsUnitTypeUsedRtn = new CheckIsUnitTypeUsedResultDTO();

        //public List<UploadUnitTypeErrorDTO> loErrorList = new List<UploadUnitTypeErrorDTO>();
        
        //public UploadUnitTypeErrorResultDTO loErrorRtn = new UploadUnitTypeErrorResultDTO();

        public int SumValid { get; set; }
        public int SumInvalid { get; set; }
        public int SumList { get; set; }

        public string SelectedCompanyId = "";
        public string SelectedUnitTypeCategory = "";
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
        public async Task ValidateUploadUnitType()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<UploadUnitTypeDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

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
                loUploadPar.ClassName = "GSM02500BACK.ValidateUploadUnitTypeCls";
                loUploadPar.BigObject = loUploadUnitTypeList;

                await loCls.R_BatchProcess<List<UploadUnitTypeDTO>>(loUploadPar, 7);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        public async Task GetUploadUnitTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {*//*
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_UNIT_TYPE_STREAMING_CONTEXT, loUploadUnitTypeList);
                loRtn = await loModel.GetUploadUnitTypeListStreamAsync();*//*

                GSM02503UnitTypeListDTO loResult = new GSM02503UnitTypeListDTO();

                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02503_PROPERTY_ID_STREAMING_CONTEXT, loParameter.PropertyData.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02503_UNIT_TYPE_CATEGORY_ID_STREAMING_CONTEXT, loParameter.SelectedUnitTypeCategory);
                loResult = await loUnitTypeModel.GetUnitTypeListStreamAsync();

                List<UploadUnitTypeDTO> loTemp = new List<UploadUnitTypeDTO>();
                loTemp = loUploadUnitTypeList.Select(x => new UploadUnitTypeDTO()
                {
                    CompanyId = x.CompanyId,
                    PropertyId = x.PropertyId,
                    UnitTypeCtg = x.UnitTypeCtg,
                    UnitTypeCode = x.UnitTypeCode,
                    UnitTypeName = x.UnitTypeName,
                    Description = x.Description,
                    GrossAreaSize = x.GrossAreaSize,
                    NetAreaSize = x.NetAreaSize,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = x.Notes,
                    Var_Exists = loResult.Data.Any(y => x.UnitTypeCode == y.CUNIT_TYPE_ID)
                }).ToList();

                loUploadUnitTypeDisplayList = new ObservableCollection<UploadUnitTypeDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task CheckIsUnitTypeUsedAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCheckIsUnitTypeUsedRtn = await loModel.CheckIsUnitTypeUsedAsync();
                loCheckIsUnitTypeUsed = loCheckIsUnitTypeUsedRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
*/
        public async Task SaveUploadUnitTypeAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<UploadUnitTypeDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadUnitTypeDisplayList.Count == 0)
                    return;

                Bigobject = loUploadUnitTypeDisplayList.ToList<UploadUnitTypeDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "GSM02500BACK.UploadUnitTypeCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<UploadUnitTypeDTO>>(loBatchPar, 7);
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
                    VisibleError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = $"Process Complete but fail with GUID {pcKeyGuid}";

                    try
                    {
                        loResult = await ServiceGetError(pcKeyGuid);
                        loUploadUnitTypeDisplayList.ToList().ForEach(x =>
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

            R_Exception loException = new R_Exception();
            ex.ErrorList.ForEach(l =>
            {
                loException.Add(l.ErrNo, l.ErrDescp);
            });

            ShowErrorAction(loException);
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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_PROPERTY_UNIT_TYPEResources"
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
        #endregion

    }
}
