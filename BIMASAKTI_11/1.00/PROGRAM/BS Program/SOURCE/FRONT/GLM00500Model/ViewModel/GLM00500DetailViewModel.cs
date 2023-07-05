using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00500Model.ViewModel;

public class GLM00500DetailViewModel : R_ViewModel<GLM00500BudgetDTDTO>
{
    private GLM00500DetailModel _model = new();
    public ObservableCollection<GLM00500BudgetDTGridDTO> BudgetDTList = new();
    public ObservableCollection<GLM00500FunctionDTO> RoundingMethodList = new();

    //header
    public GLM00500BudgetHDDTO BudgetHDEntity = new();
    public GLM00500BudgetDTDTO BudgetDTEntity = new();
    public GLM00500PeriodCount PeriodCount = new();
    
    public string SelectedAccountType { get; set; } = "N";
    public string SelectedInputMethod { get; set; } = "MN";

    public List<KeyValuePair<string, string>> CGLACCOUNT_TYPE { get; } = new()
    {
        new("N", "Normal Account"),
        new("S", "Statistic Account")
    };
    
    public List<KeyValuePair<string, string>> CINPUT_METHOD { get; } = new()
    {
        new("MN", "Manually"),
        new("MO", "Monthly"),
        new("AN", "Annually")
    };
    
    public List<KeyValuePair<string, string>> CDIST_METHOD { get; } = new()
    {
        new("EV", "Evenly"),
        new("BW", "Budget Weighting")
    };
    
    //init 
    public async Task Init(object poEntity)
    {
        BudgetHDEntity = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(poEntity);
        await GetRoundingMethodList();
    }

    //get rounding method list
    public async Task GetRoundingMethodList()
    {
        var loEx = new R_Exception();

        try
        {
            var loResult = await _model.GLM00500GetRoundingMethodListModel();
            RoundingMethodList = new ObservableCollection<GLM00500FunctionDTO>(loResult.Data);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    //get list
    public async Task GetBudgetDTList(string pcBudgetId, string pcAccountType)
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CBUDGET_ID, pcBudgetId);
            R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CGLACCOUNT_TYPE, pcAccountType);
            var loResult = await _model.GLM00500GetBudgetDTListModel();
            BudgetDTList = new ObservableCollection<GLM00500BudgetDTGridDTO>(loResult.Data);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    //get record
    public async Task GetBudgetDT(GLM00500BudgetDTDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            BudgetDTEntity = await _model.R_ServiceGetRecordAsync(poEntity);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    //save record
    public async Task SaveBudgetDT(GLM00500BudgetDTDTO poEntity, eCRUDMode peCRUDMode)
    {
        var loEx = new R_Exception();

        try
        {
            BudgetDTEntity = await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    //delete record
    public async Task DeleteBudgetDT(GLM00500BudgetDTDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            await _model.R_ServiceDeleteAsync(poEntity);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    //get period count
    public async Task GetPeriodCount()
    {
        var loEx = new R_Exception();

        try
        {
            PeriodCount = await _model.GLM00500GetPeriodCountModel();
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
}