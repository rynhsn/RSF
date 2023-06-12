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

public class GSM05000ApprovalUserViewModel : R_ViewModel<GSM05000ApprovalUserDTO>
{
    private GSM05000ApprovalUserModel _Model = new();
    public ObservableCollection<GSM05000ApprovalUserDTO> ApproverList = new();
    public ObservableCollection<GSM05000ApprovalDepartmentDTO> DepartmentList = new();
    public GSM05000ApprovalUserDTO ApproverEntity = new();
    public GSM05000ApprovalHeaderDTO HeaderEntity = new();
    public GSM05000ApprovalDepartmentDTO DepartmentEntity = new();
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
            if (HeaderEntity.LDEPT_MODE)
            {
                R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, TransactionCode);
                var loReturn = await _Model.GetApprovalDepartmentAsync();
                DepartmentList = new ObservableCollection<GSM05000ApprovalDepartmentDTO>(loReturn.Data);
            }
            else
            {
                DepartmentList = new ObservableCollection<GSM05000ApprovalDepartmentDTO>();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetDepartmentEntity(GSM05000ApprovalDepartmentDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            DepartmentEntity = poEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetApproverList()
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, TransactionCode);
            var loReturn = await _Model.GetApprovalListAsync();
            ApproverList = new ObservableCollection<GSM05000ApprovalUserDTO>(loReturn.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetApproverEntity(GSM05000ApprovalUserDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            ApproverEntity = poEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    public async Task SaveEntity(GSM05000ApprovalUserDTO poNewEntity, eCRUDMode peCrudMode)
    {
        var loEx = new R_Exception();
        try
        {
            if (eCRUDMode.AddMode == peCrudMode)
            {
                poNewEntity.CDEPT_CODE = DepartmentEntity.CDEPT_CODE ?? "";
                poNewEntity.CTRANSACTION_CODE = HeaderEntity.CTRANSACTION_CODE;
            }

            ApproverEntity = await _Model.R_ServiceSaveAsync(poNewEntity, peCrudMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task DeleteEntity(GSM05000ApprovalUserDTO poNewEntity)
    {
        var loEx = new R_Exception();
        try
        {
            await _Model.R_ServiceDeleteAsync(poNewEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GenerateSequence(GSM05000ApprovalUserDTO poNewEntity)
    {
        var loEx = new R_Exception();
        var lnSequence = 1;

        try
        {
            // cek approval mode 
            if (HeaderEntity.CAPPROVAL_MODE != "1")
            {
                if (ApproverList.Count != 0)
                {
                    var loApprover = ApproverList.OrderByDescending(x => x.CSEQUENCE).FirstOrDefault();
                    lnSequence = Convert.ToInt32(loApprover.CSEQUENCE) + 1;
                }

                poNewEntity.CSEQUENCE = lnSequence.ToString("D3");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}