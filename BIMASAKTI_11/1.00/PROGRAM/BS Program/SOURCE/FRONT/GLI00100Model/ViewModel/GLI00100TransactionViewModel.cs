using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GLI00100Model.ViewModel;

public class GLI00100TransactionViewModel : R_ViewModel<GLI00100TransactionGridDTO>
{
    private GLI00100TransactionModel _model = new();
    private GLI00100InitModel _initModel = new();
    public ObservableCollection<GLI00100JournalGridDTO> DataList = new();
    
    public GLI00100JournalDTO DataHeader = new();
    public GLI00100JournalParamDTO PopupParams = new();
    public GLI00100GSMCompanyDTO  GSMCompany = new();
    
    public DateTime DREF_DATE { get; set; }
    public DateTime DDOC_DATE { get; set; }

    public async Task GetGSMCompany()
    {
        var loEx = new R_Exception();
        try
        {
            GSMCompany = await _initModel.GLI00100GetGSMCompanyModel();
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetList()
    {
        var loEx = new R_Exception();
        try
        {
            DataList.Clear();
            
            R_FrontContext.R_SetStreamingContext(GLI00100ContextConstant.CREC_ID, PopupParams.CREC_ID);

            var loResult = await _model.GLI00100GetJournalGridStreamModel();
            DataList = new ObservableCollection<GLI00100JournalGridDTO>(loResult);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetHeader()
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = new GLI00100JournalParamDTO()
            {
                CREC_ID = PopupParams.CREC_ID
            };

            DataHeader = await _model.GLI00100GetJournalDetailModel(loParam);
            DREF_DATE = DateTime.ParseExact(DataHeader.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            DDOC_DATE = DateTime.ParseExact(DataHeader.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    
    
    
}