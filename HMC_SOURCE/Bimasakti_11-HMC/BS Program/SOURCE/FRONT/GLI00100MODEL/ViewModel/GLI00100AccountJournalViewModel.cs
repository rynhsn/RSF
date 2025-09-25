using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GLI00100Model.ViewModel;

public class GLI00100AccountJournalViewModel : R_ViewModel<GLI00100TransactionGridDTO>
{
    private GLI00100AccountJournalModel _model = new();
    public ObservableCollection<GLI00100TransactionGridDTO> DataList = new();

    public GLI00100AccountAnalysisDetailDTO DataHeader = new();
    public GLI00100PopupParamsDTO PopupParams = new();
    public GLI00100JournalParamDTO PopupParamsJournal = new();

    public async Task GetList()
    {
        var loEx = new R_Exception();
        try
        {
            DataList.Clear();
            
            R_FrontContext.R_SetStreamingContext(GLI00100ContextConstant.CCENTER_CODE, PopupParams.CCENTER_CODE);
            R_FrontContext.R_SetStreamingContext(GLI00100ContextConstant.CGLACCOUNT_NO, PopupParams.CGLACCOUNT_NO);
            R_FrontContext.R_SetStreamingContext(GLI00100ContextConstant.CCURRENCY_TYPE, PopupParams.CCURRENCY_TYPE);
            R_FrontContext.R_SetStreamingContext(GLI00100ContextConstant.CPERIOD, PopupParams.CYEAR + PopupParams.CMONTH);

            var loResult = await _model.GLI00100GetTransactionGridStreamModel();
            DataList = new ObservableCollection<GLI00100TransactionGridDTO>(loResult);
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
            var loParam = new GLI00100AccountAnalysisDetailParamDTO()
            {
                CCENTER_CODE = PopupParams.CCENTER_CODE,
                CGLACCOUNT_NO = PopupParams.CGLACCOUNT_NO,
                CCURRENCY_TYPE = PopupParams.CCURRENCY_TYPE,
                CPERIOD = PopupParams.CYEAR + PopupParams.CMONTH
            };

            DataHeader = await _model.GLI00100GetAccountAnalysisDetailModel(loParam);
        }
        catch (R_Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}