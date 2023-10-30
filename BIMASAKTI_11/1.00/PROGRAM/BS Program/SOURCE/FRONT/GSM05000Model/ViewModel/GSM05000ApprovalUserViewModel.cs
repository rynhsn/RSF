using System;
using System.Collections.Generic;
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
    public ObservableCollection<GSM05000ApprovalDepartmentDTO> DepartmentLookup = new();
    public List<GSM05000ApprovalDepartmentDTO> DeptSeqList = new();
    public GSM05000ApprovalCopyDTO TempEntityForCopy = new();
    
    public GSM05000ApprovalUserDTO ApproverEntity = new();
    public GSM05000ApprovalHeaderDTO HeaderEntity = new();
    public GSM05000ApprovalDepartmentDTO DepartmentEntity = new();
    public string TransactionCode = "";

    public async Task GetApprovalHeader()
    {
        var loEx = new R_Exception();

        try
        {
            GSM05000TrxCodeParamsDTO loParams = new() { CTRANS_CODE = TransactionCode };
            var loReturn = await _Model.GetApprovalHeaderAsync(loParams);
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
                var loReturn = await _Model.GetApprovalDepartmentStreamAsync();
                DepartmentList = new ObservableCollection<GSM05000ApprovalDepartmentDTO>(loReturn);
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

    public void GetDepartmentEntity(GSM05000ApprovalDepartmentDTO poEntity)
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
            var loReturn = await _Model.GetApprovalListStreamAsync();
            //order by deptcode dan seq
            ApproverList = new ObservableCollection<GSM05000ApprovalUserDTO>(loReturn.OrderBy(x => x.CDEPT_CODE).ThenBy(x => x.CSEQUENCE));
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
            poEntity.CTRANS_CODE = TransactionCode;
            ApproverEntity = await _Model.R_ServiceGetRecordAsync(poEntity);
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
                poNewEntity.CTRANS_CODE = HeaderEntity.CTRANS_CODE;
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

    public void GenerateSequence(GSM05000ApprovalUserDTO poNewEntity)
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
    
    public async Task GetDeptSeqList(string poTransactionCode)
    {
        var loEx = new R_Exception();

        try
        {
            GSM05000TrxCodeParamsDTO loParams = new() { CTRANS_CODE = poTransactionCode };
            // var loReturn = await _Model.DepartmentChangeSequenceModel(loParams);
            var loReturn = await _Model.DepartmentChangeSequenceModelStream(loParams);
            // DeptSeqList = loReturn.Data;
            DeptSeqList = loReturn;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetUserSeqList(GSM05000ApprovalUserDTO poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, poParameter.CTRANS_CODE);
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CDEPT_CODE, poParameter.CDEPT_CODE);
            var loReturn = await _Model.GSM05000GetUserSequenceDataModelStream();
            //urutkan berdasarkan sequence
            ApproverList = new ObservableCollection<GSM05000ApprovalUserDTO>(loReturn.OrderBy(x => x.CSEQUENCE));
            // ApproverList = new ObservableCollection<GSM05000ApprovalUserDTO>(loReturn.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task UpdateSequence(List<GSM05000ApprovalUserDTO> poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            await _Model.GSM05000UpdateSequenceModel(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    // public async Task LookupDepartment(GSM05000ApprovalCopyDTO poParameter)
    public async Task LookupDepartment()
    {
        var loEx = new R_Exception();

        try
        {
            // R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CDEPT_CODE, poParameter.CDEPT_CODE);
            var loReturn = await _Model.LookupApprovalDepartmentStreamAsync();
            DepartmentLookup = new ObservableCollection<GSM05000ApprovalDepartmentDTO>(loReturn);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task CopyTo(GSM05000ApprovalCopyDTO poEntity)
    {
        var loEx = new R_Exception();
        
        try
        {
            GSM05000CopyToParamsDTO loParams = new()
            {
                CTRANSACTION_CODE = poEntity.CTRANSACTION_CODE, 
                CDEPT_CODE = poEntity.CDEPT_CODE, 
                CDEPT_CODE_TO = poEntity.CDEPT_CODE_FROM
            };
            await _Model.CopyToApprovalAsync(loParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }

    public async Task CopyFrom(GSM05000ApprovalCopyDTO poEntity)
    {
        var loEx = new R_Exception();
        
        try
        {
            GSM05000CopyFromParamsDTO loParams = new()
            {
                CTRANSACTION_CODE = poEntity.CTRANSACTION_CODE, 
                CDEPT_CODE = poEntity.CDEPT_CODE, 
                CDEPT_CODE_FROM = poEntity.CDEPT_CODE_FROM
            };
            await _Model.CopyFromApprovalAsync(loParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }
}