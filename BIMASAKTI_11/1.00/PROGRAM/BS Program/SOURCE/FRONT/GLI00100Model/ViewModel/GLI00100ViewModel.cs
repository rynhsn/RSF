using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GLI00100Common.DTOs;
using GLI00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLI00100Model.ViewModel;

public class GLI00100ViewModel : R_ViewModel<GLI00100AccountDTO>
{
    private GLI00100Model _model = new();
    private GLI00100InitModel _initModel = new();
    public ObservableCollection<GLI00100AccountGridDTO> GLAccountList = new();
    
    public GLI00100AccountDTO GLAccountDetail = new();
    public GLI00100AccountAnalysisDTO GLAccountAnalysisDetail = new();
    
    public GLI00100GLSystemParamDTO GLSystemParam = new();
    public GLI00100GSMPeriodDTO GSMPeriod = new();
    public GLI00100PeriodCountDTO PeriodCount = new();
    public List<GLI00100BudgetDTO> BudgetList = new();
    
    public int Year { get; set; }
    public string CenterCode { get; set; }
    public string CenterName { get; set; }
    public string BudgetNo { get; set; }
    public string CurrencyTypeValue { get; set; } = "L";
    public List<KeyValuePair<string, string>> CCURRENCY_TYPE { get; } = new()
    {
        new KeyValuePair<string, string>("L", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "LocalCurrency")),
        new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "BaseCurrency"))
    };
    
    public List<KeyValuePair<string, string>> CBSIS { get; } = new()
    {
        // new KeyValuePair<string, string>("I", "Income Statement"),
        new KeyValuePair<string, string>("I", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "IncomeStatement")),
        // new KeyValuePair<string, string>("B", "Balance Sheet")
        new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "BalanceSheet"))
    };
    
    public List<KeyValuePair<string, string>> CDBCR { get; } = new()
    {
        // new KeyValuePair<string, string>("D", "Debit"),
        new KeyValuePair<string, string>("D", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Debit")),
        // new KeyValuePair<string, string>("C", "Credit")
        new KeyValuePair<string, string>("C", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Credit"))
    };
    
    public async Task Init()
    {
        await GetGLSystemParam();
        await GetGSMPeriod();
        // await GetAccountList();
    }

    public async Task GetAccountList()
    {
        var loEx = new R_Exception();
        try
        {
            GLAccountList.Clear();
            var loResult = await _initModel.GLI00100GetGLAccountListStreamModel();
            GLAccountList = new ObservableCollection<GLI00100AccountGridDTO>(loResult);
            
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetAccountDetail(GLI00100AccountParameterDTO poParams)
    {
        var loEx = new R_Exception();

        try
        {
            GLAccountAnalysisDetail = new GLI00100AccountAnalysisDTO();
            GLAccountDetail = await _model.GLI00100GetAccountDetailModel(poParams);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetAccountAnalysisDetail(GLI00100AccountAnalysisParameterDTO poParams)
    {
        var loEx = new R_Exception();

        try
        {
            GLAccountAnalysisDetail = await _model.GLI00100GetAccountAnalysisDetailModel(poParams);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetGSMPeriod()
    {
        var loEx = new R_Exception();

        try
        {
            GSMPeriod = await _initModel.GLI00100GetGSMPeriodModel();
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetGLSystemParam()
    {
        var loEx = new R_Exception();

        try
        {
            GLSystemParam = await _initModel.GLI00100GetGLSystemParamModel();
            Year = int.Parse(GLSystemParam.CSOFT_PERIOD_YY);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetPeriodCount()
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = new GLI00100YearParamsDTO();
            loParam.CYEAR = Year.ToString();
            PeriodCount = await _initModel.GLI00100GetPeriodCountModel(loParam);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetBudgetList(GLI00100BudgetParamsDTO poParams)
    {
        var loEx = new R_Exception();

        try
        {
            BudgetList = await _model.GLI00100GetBudgetStreamModel(poParams);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}