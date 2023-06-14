using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM05000Model.ViewModel;

public class GSM05000ApprovalReplacementViewModel : R_ViewModel<GSM05000ApprovalReplacementDTO>
{
    private GSM05000ApprovalReplacementModel _Model = new();
    public ObservableCollection<GSM05000ApprovalReplacementDTO> ReplacementList = new();
    public GSM05000ApprovalReplacementDTO ReplacementEntity = new();
    public string TransactionCode = "";
    public string DeptCode = "";
    public string SelectedUserId = "";
    
    public async Task GetReplacementList()
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, TransactionCode);
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CDEPT_CODE, DeptCode);
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CUSER_ID, SelectedUserId);
            var loReturn = await _Model.GetReplacementListAsync();
            
            ReplacementList = new ObservableCollection<GSM05000ApprovalReplacementDTO>(loReturn.Data);
            
            foreach (var list in ReplacementList)
            {
                list.DVALID_TO = DateTime.ParseExact(list.CVALID_TO, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetReplacementEntity(GSM05000ApprovalReplacementDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            ReplacementEntity = poEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task SaveEntity(GSM05000ApprovalReplacementDTO poNewEntity, eCRUDMode peCrudMode)
    {
        var loEx = new R_Exception();
        try
        {
            // if (eCRUDMode.AddMode == peCrudMode)
            // {
            //     poNewEntity.CDEPT_CODE = DeptCode;
            //     poNewEntity.CTRANSACTION_CODE = TransactionCode;
            //     poNewEntity.CUSER_REPLACEMENT = SelectedUserId;
            // }

            ReplacementEntity = await _Model.R_ServiceSaveAsync(poNewEntity, peCrudMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task DeleteEntity(GSM05000ApprovalReplacementDTO poNewEntity)
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

}