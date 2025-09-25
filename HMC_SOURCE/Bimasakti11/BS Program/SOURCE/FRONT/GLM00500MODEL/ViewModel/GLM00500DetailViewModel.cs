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
    public GLM00500BudgetDTDTO BudgetDTEntity = new();
    public GLM00500GenerateAccountBudgetDTO GenerateAccountBudget = new();

    public GLM00500PeriodCountDTO PeriodCount = new();
    public GLM00500GSMCompanyDTO Company = new();
    public string SelectedAccountType { get; set; } = "N";

    public List<KeyValuePair<string, string>> CGLACCOUNT_TYPE { get; } = new()
    {
        new("N", "Normal Account"),
        new("S", "Statistic Account")
    };
    
    //list 01 - 12
    public List<KeyValuePair<string, string>> CPERIOD { get; } = new()
    {
        new("01", "01"),
        new("02", "02"),
        new("03", "03"),
        new("04", "04"),
        new("05", "05"),
        new("06", "06"),
        new("07", "07"),
        new("08", "08"),
        new("09", "09"),
        new("10", "10"),
        new("11", "11"),
        new("12", "12")
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

    #region popup generate

    private GLM00500HeaderModel _headerModel = new();
    public List<GLM00500BudgetHDDTO> BudgetHDList = new();
    public GLM00500GSMPeriodDTO Periods = new();
    public GLM00500GLSystemParamDTO SystemParams = new();
    public int SelectedYear;

    public List<KeyValuePair<string, string>> CBY { get; } = new()
    {
        new("P", "Percentage"),
        new("V", "Value")
    };
    
    public List<KeyValuePair<string, string>> CUPDATE_METHOD { get; } = new()
    {
        new("C", "Clear All Existing Data"),
        new("S", "Skip Existing Data"),
        new("R", "Replace Existing Data")
    };
    
    public List<KeyValuePair<string, string>> CBASED_ON { get; } = new()
    {
        new("EB", "Existing Budget"),
        new("AV", "Actual Value")
    };

    #endregion

    //init 
    public async Task Init(object poEntity)
    {
        BudgetHDEntity = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(poEntity);
        await GetRoundingMethodList();
        await GetBudgetWeightingList();
        await GetPeriodCount();
        await GetCompany();
    }

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
            var loResult = await _model.GLM00500GetBudgetWeightingListStreamModel();
            BudgetWeightingList = new ObservableCollection<GLM00500BudgetWeightingDTO>(loResult);
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
            var loResult = await _model.GLM00500GetBudgetDTListStreamModel();
            BudgetDTList = new ObservableCollection<GLM00500BudgetDTGridDTO>(loResult);
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
            GLM00500YearParamsDTO loParams = new() {CYEAR = BudgetHDEntity.CYEAR};
            PeriodCount = await _model.GLM00500GetPeriodCountModel(loParams);
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

    public async Task<GLM00500BudgetCalculateDTO> CalculateBudget(GLM00500BudgetHDDTO poHeader,
        GLM00500BudgetDTDTO poEntity)
    {
        var loEx = new R_Exception();
        GLM00500BudgetCalculateDTO loResult = null;

        try
        {
            var loParam = new GLM00500CalculateParamDTO()
            {
                INO_PERIOD = PeriodCount.INO_PERIOD,
                CCURRENCY_TYPE = poHeader.CCURRENCY_TYPE,
                NBUDGET = poEntity.NBUDGET,
                CROUNDING_METHOD = poEntity.CROUNDING_METHOD,
                CDIST_METHOD = poEntity.CDIST_METHOD,
                CBW_CODE = poEntity.CBW_CODE
            };

            loResult = await _model.GLM00500BudgetCalculateModel(loParam);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loResult;
    }


    #region popup generate
    
    
    public async Task GetBudgetHDList(int pnYear)
    {
        var loEx = new R_Exception();
        List<GLM00500BudgetHDDTO> loResult;

        try
        {
            R_FrontContext.R_SetStreamingContext(GLM00500ContextContant.CYEAR, pnYear.ToString());
            loResult = await _headerModel.GLM00500GetBudgetHDListStreamModel();
            BudgetHDList = loResult;
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetPeriods()
    {
        var loEx = new R_Exception();
        GLM00500GSMPeriodDTO loResult;

        try
        {
            loResult = await _headerModel.GLM00500GetPeriodsModel();
            Periods = loResult;
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetSystemParams()
    {
        var loEx = new R_Exception();

        try
        {
            var loResult = await _headerModel.GLM00500GetSystemParamModel();
            SystemParams = loResult;
            SelectedYear = int.Parse(SystemParams.CSOFT_PERIOD_YY);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    public async Task GenerateBudget(GLM00500GenerateAccountBudgetDTO result)
    {
        var loEx = new R_Exception();

        try
        {
            await _model.GLM00500GenerateBudgetModel(result);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}