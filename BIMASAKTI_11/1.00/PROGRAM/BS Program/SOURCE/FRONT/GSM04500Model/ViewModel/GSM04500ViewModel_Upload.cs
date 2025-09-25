using GSM04500Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Excel;
using R_ProcessAndUploadFront;
using R_APICommonDTO;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Text.Json;
using System.ComponentModel.Design;
using System.Data;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace GSM04500Model.ViewModel
{
    public class GSM04500ViewModel_Upload : R_IProcessProgressStatus
    {

        public string Message = "";
        public int Percentage = 0;
        public Action StateChangeAction { get; set; }
        public DataSet ExcelDataSet { get; set; }
        public Func<Task> ActionDataSetExcel { get; set; }
        public Action<R_APIException> DisplayErrorAction { get; set; }
        public Action ShowSuccessAction { get; set; }

        public string PropertyValue { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public string JournalGroupTypeValue { get; set; } = "";
        public string CompanyId { get; set; }
        public string UserId { get; set; }

        public int SumListExcel { get; set; }
        public bool IsError { get; set; }
        public int SumValidDataExcel { get; set; }
        public int SumInvalidDataExcel { get; set; }
        public ObservableCollection<GSM04500UploadErrorValidateDTO> JournalGroupValidateUploadError { get; set; } = new ObservableCollection<GSM04500UploadErrorValidateDTO>();
        public bool _IsSuccess { get; set; }
        public bool VisibleColumn_LACCRUAL;
        //CONVERT DTO
        public async Task ConvertGrid(List<GSM04500UploadFromExcelDTO> poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                // Onchange Visible Error
                IsError = false;
                SumValidDataExcel = 0;
                SumInvalidDataExcel = 0;

                // Convert Excel DTO and add SeqNo
                List<GSM04500UploadErrorValidateDTO> Data = poEntity.Select((item, i)
                    => new GSM04500UploadErrorValidateDTO
                    {
                        No = i + 1,
                        JournalGroup = item.JournalGroup,
                        JournalGroupName = item.JournalGroupName,
                        EnableAccrual = item.EnableAccrual
                    }).ToList();

                SumListExcel = Data.Count;
                JournalGroupValidateUploadError = new ObservableCollection<GSM04500UploadErrorValidateDTO>(Data);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        //TO Visible EnableAccrual Column 
        public void ColumnAccrual_Visible()
        {
            switch (JournalGroupTypeValue)
            {
                case "10":
                case "11":
                case "40":
                    VisibleColumn_LACCRUAL = true;
                    break;
                default:
                    VisibleColumn_LACCRUAL = false;
                    break;
            }
        }

        //PROCESS SEND EXCEL DATA
        public async Task SaveFileBulk()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            List<GSM04500UploadErrorValidateDTO> ListFromExcel;
            List<R_KeyValue> loBatchParUserParameters;

            try
            {
                // set Param
                loBatchParUserParameters = new List<R_KeyValue>();
                loBatchParUserParameters.Add(new R_KeyValue()
                { Key = ContextConstant.CPROPERTY_ID, Value = "" });
                loBatchParUserParameters.Add(new R_KeyValue()
                { Key = ContextConstant.CJRNGRP_TYPE, Value = JournalGroupTypeValue });

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl",
                    poProcessProgressStatus: this);

                //Set Data
                if (JournalGroupValidateUploadError.Count == 0)
                    return;

                ListFromExcel = JournalGroupValidateUploadError.ToList();

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyId;
                loBatchPar.USER_ID = UserId;
                loBatchPar.UserParameters = loBatchParUserParameters;
                loBatchPar.ClassName = "GSM04500Back.GSM04500UploadJournalGroupCls";
                loBatchPar.BigObject = ListFromExcel;

                await loCls.R_BatchProcess<List<GSM04500UploadErrorValidateDTO>>(loBatchPar, 10);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Progress Bar
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success");
                    ShowSuccessAction();
                    IsError = false;
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    IsError = true;
                    Message = $"Process Complete but fail";
                    await ServiceGetError(pcKeyGuid);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            //IF ERROR CONNECTION, PROGRAM WILL RUN THIS METHOD
            R_Exception loException = new R_Exception();

            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);
           // ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));

            DisplayErrorAction(ex);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            StateChangeAction();
            await Task.CompletedTask;
        }
        #endregion

        #region GetError
        private async Task ServiceGetError(string pcKeyGuid)
        {
            // R_Exception loException = new R_Exception();
            R_APIException loException = new R_APIException();
            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyId,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_GS_UPLOAD_JOURNAL_GROUPResources"
                };

                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GS",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrl");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle, jika nilai dari seq negatif maka error unhandle maka dipisahkan disini
                var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                R_Exception loEx = new R_Exception();
                loUnhandleEx.ForEach(x => loEx.Add(x));
                loException = R_FrontUtility.R_ConvertToAPIException(loEx);

                // ERROR, jika nilai dari seq positif maka error handle dari data yang diinput
                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq
                    JournalGroupValidateUploadError.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, ErrorFlag and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.No))
                        {
                            x.ErrorMessage = loResultData.Where(y => y.SeqNo == x.No).FirstOrDefault().ErrorMessage;
                            x.ErrorFlag = "N";
                            SumInvalidDataExcel++;
                        }
                        else
                        {
                            SumValidDataExcel++;
                        }
                    });

                    // Convert DB DTO => excel, for user downlaod
                    List<GSM04500UploadFromExcelDTO> loData = JournalGroupValidateUploadError.Select((item)
                        => new GSM04500UploadFromExcelDTO
                        {
                            JournalGroup = item.JournalGroup,
                            JournalGroupName = item.JournalGroupName,
                            EnableAccrual = item.EnableAccrual,
                            Notes = item.ErrorMessage
                        }).ToList();
                    //   Set DataSetTable and get error
                    //var loExcelData =
                    //    R_FrontUtility.ConvertCollectionToCollection<GSM04500UploadFromExcelDTO>(JournalGroupValidateUploadError);

                    var loDataTable = R_FrontUtility.R_ConvertTo(loData);
                    loDataTable.TableName = "Journal Group";

                    var loDataSet = new DataSet();
                    loDataSet.Tables.Add(loDataTable);

                    // Assign Dataset
                    ExcelDataSet = loDataSet;

                    //// Download if get Error
                    //await ActionDataSetExcel.Invoke();
                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion


    }
}