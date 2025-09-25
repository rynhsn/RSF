using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace GLM00500Model.ViewModel;

public class GLM00500UploadViewModel : R_IProcessProgressStatus
{
    public ObservableCollection<GLM00500UploadForSystemDTO> UploadedList = new();

    public string CompanyId { get; set; }
    public string UserId { get; set; }

    public int TotalRows { get; set; }
    public int ValidRows { get; set; }
    public int InvalidRows { get; set; }

    public bool IsSuccess { get; set; } = false;
    public bool HasData { get; set; } = false;

    #region ProgressBar

    public string Message = "";
    public int Percentage = 0;

    #endregion

    public List<GLM00500UploadToSystemDTO> UploadListResult { get; set; } = new();

    public string keyGuid { get; set; }

    // Action StateHasChanged
    public Action StateChangeAction { get; set; }

    // Action Get Error Unhandle
    public Action<R_Exception> ShowErrorAction { get; set; }

    // Action Get DataSet
    public Func<Task> ActionDataSetExcel { get; set; }

    // DataSet Excel 
    public DataSet ExcelDataSet { get; set; }

    public async Task AssignData(IEnumerable<GLM00500UploadFromFileDTO> poData)
    {
        var loReturn = poData.Select((x, i) => new GLM00500UploadForSystemDTO
        {
            SEQ_NO = i + 1,
            BUDGET_YEAR = x.Budget_Year,
            BUDGET_NO = x.Budget_No,
            BUDGET_NAME = x.Budget_Name,
            CURRENCY_TYPE = x.Currency_Type,
            ACCOUNT_TYPE = x.Account_Type,
            ACCOUNT_NO = x.Account_No,
            CENTER = x.Center,
            PERIOD_1 = x.Period_1,
            PERIOD_2 = x.Period_2,
            PERIOD_3 = x.Period_3,
            PERIOD_4 = x.Period_4,
            PERIOD_5 = x.Period_5,
            PERIOD_6 = x.Period_6,
            PERIOD_7 = x.Period_7,
            PERIOD_8 = x.Period_8,
            PERIOD_9 = x.Period_9,
            PERIOD_10 = x.Period_10,
            PERIOD_11 = x.Period_11,
            PERIOD_12 = x.Period_12,
            PERIOD_13 = x.Period_13,
            PERIOD_14 = x.Period_14,
            PERIOD_15 = x.Period_15,
            VALID = "N"
        }).ToList();

        TotalRows = loReturn.Count;
        HasData = TotalRows > 0;
        ValidRows = 0;
        InvalidRows = 0;
        IsSuccess = false;

        UploadedList = new ObservableCollection<GLM00500UploadForSystemDTO>(loReturn);
    }

    public async Task UploadFile(List<GLM00500UploadForSystemDTO> poBigObject)
    {
        var loEx = new R_Exception();
        R_BatchParameter loUploadPar;
        R_ProcessAndUploadClient loCls;
        // List<GLM00500UploadToSystemDTO> Bigobject;
        // List<R_KeyValue> loUserParameters;
        R_IProcessProgressStatus loProgressStatus;

        try
        {
            //preapare Batch Parameter
            loUploadPar = new()
            {
                COMPANY_ID = CompanyId,
                USER_ID = UserId,
                ClassName = "GLM00500Back.GLM00500UploadCls",
                BigObject = poBigObject,
                // UserParameters = loUserParameters
            };

            //Instantiate ProcessClient
            loCls = new R_ProcessAndUploadClient(
                pcModuleName: "GL",
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: "R_DefaultServiceUrlGL",
                poProcessProgressStatus: this);

            keyGuid = await loCls.R_BatchProcess<List<GLM00500UploadForSystemDTO>>(loUploadPar, 30);

            UploadedList = new ObservableCollection<GLM00500UploadForSystemDTO>(poBigObject);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region ProgressBar

    public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
    {
        var loEx = new R_Exception();

        try
        {
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                IsSuccess = false;
            }
            else if (poProcessResultMode == eProcessResultMode.Fail)
            {
                Message = $"Process Complete but fail with GUID {pcKeyGuid}";
                IsSuccess = true;
                await ServiceGetError(pcKeyGuid);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        // Call Method Action StateHasChange
        StateChangeAction();

        loEx.ThrowExceptionIfErrors();
    }

    public async Task ProcessError(string pcKeyGuid, R_APIException ex)
    {
        Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

        // Call Method Action Error Unhandle
        var loEx = new R_Exception();
        ex.ErrorList.ForEach(x => loEx.Add(x.ErrNo, x.ErrDescp));
        ShowErrorAction(loEx);

        // Call Method Action StateHasChange
        StateChangeAction();

        await Task.CompletedTask;
    }

    public async Task ReportProgress(int pnProgress, string pcStatus)
    {
        Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

        Percentage = pnProgress;
        Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

        // StateChangeAction();

        await Task.CompletedTask;
    }

    private async Task ServiceGetError(string pcKeyGuid)
    {
        var loException = new R_Exception();

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
                RESOURCE_NAME = "RSP_GL_SAVE_BUDGET_UPLOADResources"
            };

            loCls = new R_ProcessAndUploadClient(pcModuleName: "GL",
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: "R_DefaultServiceUrlGL");

            // Get error result
            loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);
            // await Task.Delay(5000);
            // check error if unhandle
            if (loResultData.Any(y => y.SeqNo <= 0))
            {
                var loUnhandleEx = loResultData.Where(y => y.SeqNo <= 0).Select(x =>
                    new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                loUnhandleEx.ForEach(x => loException.Add(x));
            }

            if (loResultData.Any(y => y.SeqNo > 0))
            {
                // Display Error Handle if get seq
                UploadedList.ToList().ForEach(x =>
                {
                    //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                    if (loResultData.Any(y => y.SeqNo == x.SEQ_NO))
                    {
                        x.NOTES = loResultData.Where(y => y.SeqNo == x.SEQ_NO).FirstOrDefault().ErrorMessage;
                        x.VALID = "N";
                        InvalidRows++;
                    }
                    else
                    {
                        x.VALID = "Y";
                        ValidRows++;
                    }
                });

                //Set DataSetTable and get error
                var loExcelData =
                    R_FrontUtility.ConvertCollectionToCollection<GLM00500UploadForSystemDTO>(UploadedList);

                var loDataTable = R_FrontUtility.R_ConvertTo(UploadedList);
                loDataTable.TableName = "Budget";

                var loDataSet = new DataSet();
                loDataSet.Tables.Add(loDataTable);

                // Asign Dataset
                ExcelDataSet = loDataSet;

                // Dowload if get Error
                //await ActionDataSetExcel.Invoke();
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        await Task.CompletedTask;
    }

    #endregion
}