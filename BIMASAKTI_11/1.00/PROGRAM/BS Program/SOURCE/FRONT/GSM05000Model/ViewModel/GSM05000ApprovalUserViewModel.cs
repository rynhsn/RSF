using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GSM05000Model.ViewModel;

public class GSM05000ApprovalUserViewModel : R_ViewModel<GSM05000ApprovalUserDTO>
{
    private GSM05000ApprovalUserModel _Model = new();
    public ObservableCollection<GSM05000ApprovalUserDTO> GridList = new();
    public ObservableCollection<GSM05000ApprovalDepartmentDTO> DepartmentList = new();
    public GSM05000ApprovalUserDTO Entity = new();
    public GSM05000ApprovalHeaderDTO HeaderEntity = new();
    public string TransactionCode = "";
    
    public async Task GetApprovalHeader()
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, TransactionCode);
            var loReturn = await _Model.GetApprovalHeaderAsync();
            HeaderEntity = loReturn;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetDepartmentList()
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, TransactionCode);
            var loReturn = await _Model.GetApprovalDepartmentAsync();
            DepartmentList = new ObservableCollection<GSM05000ApprovalDepartmentDTO>(loReturn.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetApprovalList()
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, TransactionCode);
            var loReturn = await _Model.GetApprovalListAsync();
            GridList = new ObservableCollection<GSM05000ApprovalUserDTO>(loReturn.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
}