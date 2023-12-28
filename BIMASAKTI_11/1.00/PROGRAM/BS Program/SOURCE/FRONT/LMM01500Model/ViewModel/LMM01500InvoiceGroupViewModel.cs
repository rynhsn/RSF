using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace LMM01500Model.ViewModel;

public class LMM01500InvoiceGroupViewModel : R_ViewModel<LMM01500InvGrpDTO>
{
    private LMM01500InitModel _initModel = new();
    private LMM01500InvoiceGroupModel _model = new();

    public ObservableCollection<LMM01500InvGrpGridDTO> GridList = new();
    public LMM01500InvGrpDTO Entity = new();
    public List<LMM01500PropertyDTO> PropertyList = new();

    public string PropertyId = string.Empty;

    public async Task Init()
    {
        await GetPropertyList();
    }

    private async Task GetPropertyList()
    {
        var loEx = new R_Exception();

        try
        {
            var loReturn = await _initModel.GetAllPropertyAsync();
            PropertyList = loReturn.Data;
            PropertyId = PropertyList.FirstOrDefault()?.CPROPERTY_ID ?? string.Empty;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetGridList()
    {
        var loEx = new R_Exception();

        try
        {
            R_FrontContext.R_SetStreamingContext(LMM01500ContextConstant.CPROPERTY_ID, PropertyId);
            var loReturn = await _model.GetAllStreamAsync();
            GridList = new ObservableCollection<LMM01500InvGrpGridDTO>(loReturn);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetEntity(LMM01500InvGrpDTO poParam)
    {
        var loEx = new R_Exception();

        try
        {
            var loReturn = await _model.R_ServiceGetRecordAsync(poParam);
            Entity = loReturn;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task DeleteEntity(LMM01500InvGrpDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            await _model.R_ServiceDeleteAsync(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task SaveEntity(LMM01500InvGrpDTO poNewEntity, eCRUDMode peCRUDMode)
    {
        var loEx = new R_Exception();

        try
        {
            poNewEntity.CPROPERTY_ID = PropertyId;
            Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}