using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GSM05000Model.ViewModel;

public class GSM05000NumberingViewModel : R_ViewModel<GSM05000GridDTO>
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
            R_FrontContext.R_SetStreamingContext(GSM05000NumberingContextConstant.CTRANSACTION_CODE, TransactionCode);
            var loReturn = await _GSM05000NumberingModel.GetNumberingListAsync();
            GridList = new ObservableCollection<GSM05000NumberingGridDTO>(loReturn.Data);
            await _setPeriod();
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
            R_FrontContext.R_SetStreamingContext(GSM05000NumberingContextConstant.CTRANSACTION_CODE, TransactionCode);
            var loReturn = await _GSM05000NumberingModel.GetNumberingHeaderAsync();
            HeaderEntity = loReturn;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task _setPeriod()
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

}