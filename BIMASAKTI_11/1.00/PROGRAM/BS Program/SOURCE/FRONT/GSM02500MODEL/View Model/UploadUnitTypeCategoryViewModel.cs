using GSM02500COMMON.DTOs.GSM02520;
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
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs;

namespace GSM02500MODEL.View_Model
{
    public class UploadUnitTypeCategoryViewModel : R_ViewModel<UploadUnitTypeCategoryDTO>, R_IProcessProgressStatus
    {
        public Action<R_Exception> ShowErrorAction { get; set; }
        public Action StateChangeAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        //private GSM02502Model loUnitTypeCategoryModel = new GSM02502Model();

        //private UploadUnitTypeCategoryModel loModel = new UploadUnitTypeCategoryModel();

        public ObservableCollection<UploadUnitTypeCategoryDTO> loUploadUnitTypeCategoryDisplayList = new ObservableCollection<UploadUnitTypeCategoryDTO>();

        public UploadUnitTypeCategoryParameterDTO loParameter = new UploadUnitTypeCategoryParameterDTO();

        public List<UploadUnitTypeCategoryDTO> loUploadUnitTypeCategoryList = new List<UploadUnitTypeCategoryDTO>();

        //public UploadUnitTypeCategoryResultDTO loRtn = new UploadUnitTypeCategoryResultDTO();

        //public List<UploadUnitTypeCategoryErrorDTO> loErrorList = new List<UploadUnitTypeCategoryErrorDTO>();
        
        //public UploadUnitTypeCategoryErrorResultDTO loErrorRtn = new UploadUnitTypeCategoryErrorResultDTO();

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
        public async Task ValidateUploadUnitTypeCategory()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<UploadUnitTypeCategoryDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_CATEGORY_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_CATEGORY_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

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
                loUploadPar.ClassName = "GSM02500BACK.ValidateUploadUnitTypeCategoryCls";
                loUploadPar.BigObject = loUploadUnitTypeCategoryList;

                await loCls.R_BatchProcess<List<UploadUnitTypeCategoryDTO>>(loUploadPar, 6);

                VisibleError = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        public async Task GetUploadUnitTypeCategoryListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {*//*
                R_FrontContext.R_SetStreamingContext(ContextConstant.UPLOAD_UNIT_TYPE_CATEGORY_STREAMING_CONTEXT, loUploadUnitTypeCategoryList);
                loRtn = await loModel.GetUploadUnitTypeCategoryListStreamAsync();*//*

                GSM02502ListDTO loResult = new GSM02502ListDTO();

                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM02502_PROPERTY_ID_STREAMING_CONTEXT, loParameter.PropertyData.CPROPERTY_ID);
                loResult = await loUnitTypeCategoryModel.GetUnitTypeCategoryListStreamAsync();

                List<UploadUnitTypeCategoryDTO> loTemp = new List<UploadUnitTypeCategoryDTO>();
                loTemp = loUploadUnitTypeCategoryList.Select(x => new UploadUnitTypeCategoryDTO()
                {
                    CompanyId = x.CompanyId,
                    PropertyId = x.PropertyId,
                    UnitTypeCategoryCode = x.UnitTypeCategoryCode,
                    UnitTypeCategoryName = x.UnitTypeCategoryName,
                    Description = x.Description,
                    PropertyType = x.PropertyType,
                    InvitationInvoicePeriod = x.InvitationInvoicePeriod,
                    Active = x.Active,
                    NonActiveDate = x.NonActiveDate,
                    Notes = x.Notes,
                    Var_Exists = loResult.Data.Any(y => x.UnitTypeCategoryCode == y.CUNIT_TYPE_CATEGORY_ID)
                }).ToList();

                loUploadUnitTypeCategoryDisplayList = new ObservableCollection<UploadUnitTypeCategoryDTO>(loTemp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
*/
        public async Task SaveUploadUnitTypeCategoryAsync()
        {
            R_Exception loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            string lcGuid = "";
            List<UploadUnitTypeCategoryDTO> Bigobject;
            List<R_KeyValue> loUserParam;

            try
            {
                loUserParam = new List<R_KeyValue>();

                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_CATEGORY_PROPERTY_ID_CONTEXT, Value = loParameter.PropertyData.CPROPERTY_ID });
                loUserParam.Add(new R_KeyValue() { Key = ContextConstant.UPLOAD_UNIT_TYPE_CATEGORY_IS_OVERWRITE_CONTEXT, Value = IsOverWrite });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Check Data
                if (loUploadUnitTypeCategoryDisplayList.Count == 0)
                    return;

                Bigobject = loUploadUnitTypeCategoryDisplayList.ToList<UploadUnitTypeCategoryDTO>();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = SelectedCompanyId;
                loBatchPar.USER_ID = SelectedUserId;
                loBatchPar.ClassName = "GSM02500BACK.UploadUnitTypeCategoryCls";
                loBatchPar.UserParameters = loUserParam;
                loBatchPar.BigObject = Bigobject;

                lcGuid = await loCls.R_BatchProcess<List<UploadUnitTypeCategoryDTO>>(loBatchPar, 6);
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
                        loUploadUnitTypeCategoryDisplayList.ToList().ForEach(x =>
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
                    RESOURCE_NAME = "RSP_GS_UPLOAD_PROPERTY_UNIT_TYPE_CTGResources"
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
