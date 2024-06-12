using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM05000Model.ViewModel;

public class GSM05000NumberingViewModel : R_ViewModel<GSM05000NumberingGridDTO>
{
    private GSM05000NumberingModel _GSM05000NumberingModel = new();
    public ObservableCollection<GSM05000NumberingGridDTO> GridList = new();
    public GSM05000NumberingGridDTO Entity = new();
    public GSM05000NumberingHeaderDTO HeaderEntity = new();
    public string TransactionCode = "";

    public async Task GetNumberingList()
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, TransactionCode);
            var loReturn = await _GSM05000NumberingModel.GetNumberingListStreamAsync();
            GridList = new ObservableCollection<GSM05000NumberingGridDTO>(loReturn);
            _setPeriod();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetNumberingHeader()
    {
        var loEx = new R_Exception();

        try
        {
            GSM05000TrxCodeParamsDTO loParams = new() { CTRANS_CODE = TransactionCode };
            var loReturn = await _GSM05000NumberingModel.GetNumberingHeaderAsync(loParams);
            HeaderEntity = loReturn;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetEntityNumbering(GSM05000NumberingGridDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            poEntity.CTRANSACTION_CODE = HeaderEntity.CTRANS_CODE;
            Entity = await _GSM05000NumberingModel.R_ServiceGetRecordAsync(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task SaveEntity(GSM05000NumberingGridDTO poNewEntity, eCRUDMode peCrudMode)
    {
        var loEx = new R_Exception();
        try
        {
            Entity = await _GSM05000NumberingModel.R_ServiceSaveAsync(poNewEntity, peCrudMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task DeleteEntity(GSM05000NumberingGridDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000NumberingModel.R_ServiceDeleteAsync(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void _setPeriod()
    {
        var loEx = new R_Exception();

        try
        {
            if (HeaderEntity.CPERIOD_MODE == "P")
            {
                foreach (var VARIABLE in GridList)
                {
                    VARIABLE.CPERIOD = VARIABLE.CCYEAR + "-" + VARIABLE.CPERIOD_NO;
                }
            }
            else
            {
                foreach (var VARIABLE in GridList)
                {
                    VARIABLE.CPERIOD = VARIABLE.CCYEAR;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }

    public GSM05000NumberingGridDTO GeneratePeriod(GSM05000NumberingGridDTO poParam)
    {
        var lnYear = DateTime.Now.Year;
        var lnPeriod = 1;

        if (HeaderEntity.CPERIOD_MODE != "N")
        {
            if (GridList.Count > 0)
            {
                var loLastYear = Convert.ToInt32(GridList.OrderByDescending(x => x.CCYEAR).FirstOrDefault().CCYEAR);
                if (HeaderEntity.CPERIOD_MODE == "Y")
                {
                    lnYear = loLastYear + 1;
                }
                else if (HeaderEntity.CPERIOD_MODE == "P")
                {
                    var lnLastPeriod = GridList.OrderByDescending(x => x.CCYEAR).ThenByDescending(x => x.CPERIOD_NO)
                        .FirstOrDefault();
                    lnPeriod = int.Parse(lnLastPeriod.CPERIOD_NO) < 12
                        ? Convert.ToInt32(lnLastPeriod.CPERIOD_NO) + 1
                        : 1;
                    lnYear = loLastYear;
                }
            }

            // poParam.CCYEAR = HeaderEntity.CYEAR_FORMAT == "1" ? lnYear.ToString("D2") : lnYear.ToString("D4");

            // poParam.CCYEAR = HeaderEntity.CYEAR_FORMAT == "1" ? lnYear.ToString().Substring(2, 2) : lnYear.ToString();

            poParam.CCYEAR = lnYear.ToString();
            poParam.CPERIOD_NO = lnPeriod.ToString("D2");
            poParam.CPERIOD = HeaderEntity.CPERIOD_MODE == "P"
                ? poParam.CCYEAR + "-" + poParam.CPERIOD_NO
                : poParam.CCYEAR;
        }
        else
        {
            poParam.CCYEAR = "";
            poParam.CPERIOD_NO = "";
            poParam.CPERIOD = "";
        }

        return poParam;
    }
}