using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GLM00500Model.ViewModel;

public class GLM00500UploadViewModel : R_ViewModel<GLM00500UploadToSystemDTO>
{
    private GLM00500UploadModel _model = new();
    public ObservableCollection<GLM00500UploadFromSystemDTO> UploadedList = new();
    public ObservableCollection<GLM00500UploadErrorDTO> ErrorList = new();
    
    public GLM00500UploadCheckErrorDTO CheckErrorResult { get; set; }
    public List<GLM00500UploadToSystemDTO> UploadListResult { get; set; } = new();

    public string SourceFileName { get; set; }

    public List<GLM00500UploadToSystemDTO> ConvertData(IEnumerable<GLM00500UploadFromFileDTO> poData)
    {
        var loReturn = poData.Select(x => new GLM00500UploadToSystemDTO
        {
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
        }).ToList();
        
        return loReturn;
    }

    public async Task<GLM00500UploadCheckErrorDTO> CheckUpload(List<GLM00500UploadToSystemDTO> poData)
    {
        var loEx = new R_Exception();
        var loReturn = new GLM00500UploadCheckErrorDTO();
        // var llReturn = false;
        try
        {
            // loReturn = await _checkError(poData);
            loReturn = await _model.GLM00500UploadCheckBudgetModel(poData);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public async Task UploadBudget(GLM00500UploadCheckErrorDTO poResult, List<GLM00500UploadToSystemDTO> poData)
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CPROCESS_ID, poResult.CPROCES_ID);
            await _model.GLM00500UploadBudgetModel(poData);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetUploadBudgetList(GLM00500UploadCheckErrorDTO poResult)
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CPROCESS_ID, poResult.CPROCES_ID);
            
            var loReturn = await _model.GLM00500UploadGetBudgetListModel();
            UploadedList = new ObservableCollection<GLM00500UploadFromSystemDTO>(loReturn.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetUploadErrorList(GLM00500UploadCheckErrorDTO poResult)
    {
        var loEx = new R_Exception();

        try
        {
            var loList = UploadedList.ToList();
            var loParam = new List<GLM00500UploadParameterGetErrorDTO>();
            
            // isikan loList.CREC_ID ke loParam.CREC_ID gunakan linq
            loParam = loList.Select(x => new GLM00500UploadParameterGetErrorDTO
            {
                CREC_ID = x.CREC_ID
            }).ToList();
            
            var loReturn = await _model.GLM00500UploadGetErrorMsgModel(loParam);
            ErrorList = new ObservableCollection<GLM00500UploadErrorDTO>(loReturn.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
}