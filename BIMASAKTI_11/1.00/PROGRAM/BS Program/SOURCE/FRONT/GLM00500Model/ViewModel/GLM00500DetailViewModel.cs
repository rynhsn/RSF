using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public ObservableCollection<GLM00500BudgetWeightingDTO> BudgetWeightingList = new();

    public GLM00500BudgetHDDTO BudgetHDEntity = new();
    public GLM00500BudgetDTDTO BudgetDTEntity = new()
    {
        CINPUT_METHOD = "MN", 
        CDIST_METHOD = "EV", 
        CROUNDING_METHOD = "00"
    };
    public GLM00500PeriodCount PeriodCount = new();
    public GLM00500GSMCompanyDTO Company = new();
    
    public string SelectedAccountType { get; set; } = "N";

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
        await GetBudgetWeightingList();
        await GetPeriodCount();
        await GetCompany();
    }

    //get rounding method list
    public async Task GetRoundingMethodList()
    {
        var loEx = new R_Exception();

        try
        {
            var loResult = await _model.GLM00500GetRoundingMethodListModel();
            // RoundingMethodList = new ObservableCollection<GLM00500FunctionDTO>(loResult.Data);
            //order by ccode
            RoundingMethodList = new ObservableCollection<GLM00500FunctionDTO>(loResult.Data.OrderBy(x => x.CCODE));
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetBudgetWeightingList()
    {
        var loEx = new R_Exception();

        try
        {
            var loResult = await _model.GLM00500GetBudgetWeightingListModel();
            BudgetWeightingList = new ObservableCollection<GLM00500BudgetWeightingDTO>(loResult.Data);
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
    
    //get company
    public async Task GetCompany()
    {
        var loEx = new R_Exception();

        try
        {
            Company = await _model.GLM00500GetGSMCompanyModel();
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
}