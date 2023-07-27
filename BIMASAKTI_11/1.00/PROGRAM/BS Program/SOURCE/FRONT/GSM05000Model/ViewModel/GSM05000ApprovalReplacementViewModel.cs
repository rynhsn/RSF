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
    // public string TransactionCode = "";
    // public string DeptCode = "";
    public string SelectedUserId = "";
    
    public async Task GetReplacementList(string pcTransCode, string pcDeptCode, string? pcSelectedUserId)
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CTRANSACTION_CODE, pcTransCode);
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CDEPT_CODE, pcDeptCode);
            R_FrontContext.R_SetStreamingContext(GSM05000ContextConstant.CUSER_ID, pcSelectedUserId);
            var loReturn = await _Model.GetReplacementListAsync();
            
            ReplacementList = new ObservableCollection<GSM05000ApprovalReplacementDTO>(loReturn.Data);
            
            foreach (var list in ReplacementList)
            {
                list.DVALID_TO = DateTime.ParseExact(list.CVALID_TO, "yyyyMMdd", CultureInfo.InvariantCulture);
                list.DVALID_FROM = DateTime.ParseExact(list.CVALID_FROM, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task GetReplacementEntity(GSM05000ApprovalReplacementDTO poEntity, string pcTransCode, string pcDeptCode, string pcSelectedUserId)
    {
        var loEx = new R_Exception();

        try
        {
            poEntity.CTRANSACTION_CODE = pcTransCode;
            poEntity.CDEPT_CODE = pcDeptCode;
            poEntity.CUSER_ID = pcSelectedUserId;
            ReplacementEntity = await _Model.R_ServiceGetRecordAsync(poEntity);
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
            ReplacementEntity = await _Model.R_ServiceSaveAsync(poNewEntity, peCrudMode);
            
            ReplacementEntity.DVALID_TO = DateTime.ParseExact(ReplacementEntity.CVALID_TO, "yyyyMMdd", CultureInfo.InvariantCulture);
            ReplacementEntity.DVALID_FROM = DateTime.ParseExact(ReplacementEntity.CVALID_FROM, "yyyyMMdd", CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task DeleteEntity(GSM05000ApprovalReplacementDTO poEntity)
    {
        var loEx = new R_Exception();
        try
        {
            await _Model.R_ServiceDeleteAsync(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

}