using LMM01500COMMON;
using LMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace LMM01500Models
{
    public class LMM01510ViewModel : R_ViewModel<LMM01510DTO>
    {
        private LMM01510Model _LMM01510Model = new LMM01510Model();
        public ObservableCollection<LMM01510DTO> TemplateBankAccountGrid { get; set; } = new ObservableCollection<LMM01510DTO>();

        public LMM01510DTO TemplateBankAccount = new LMM01510DTO(); 

        public string PropertyValueContext = "";
        public string InvGrpCode { get; set; } = "";
        public string InvGrpName { get; set; } = "";
        public bool StatusChange;

        public async Task GetListTemplateBankAccount()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01510Model.LMM01510TemplateAndBankAccountListAsync(PropertyValueContext, InvGrpCode);

                TemplateBankAccountGrid = new ObservableCollection<LMM01510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
