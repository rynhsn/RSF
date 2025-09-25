using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00200;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APModel.ViewModel.APL00200
{
    public class LookupAPL00200ViewModel : R_ViewModel<APL00200DTO>
    {
        private PublicAPLookupModel _model = new PublicAPLookupModel();
        private PublicAPLookupRecordModel _modelRecord = new PublicAPLookupRecordModel();
        public ObservableCollection<APL00200DTO> ExpenditureGrid = new ObservableCollection<APL00200DTO>();
        public APL00200DTO loExpenditureEntity = new APL00200DTO();
        public APL00200ParameterDTO ParameterLookup { get; set; }
        public string Category { get; set; } = "";
        public List<APL00200DTO> RadioButton { get; set; } = new List<APL00200DTO>()
        {
            new APL00200DTO() { Code = "A", Desc = "All Categories"},
            new APL00200DTO() {  Code = "S", Desc = "Selected Categories" }
        };
        public async Task GetExpenditureList()
        {
            var loEx = new R_Exception();
            try
            {
                ParameterLookup.CCATEGORY_ID = loExpenditureEntity.CCATEGORY_ID;
                var loResult = await _model.APL00200ExpenditureLookUpAsync(ParameterLookup);
                ExpenditureGrid = new ObservableCollection<APL00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task<APL00200DTO> GetSuplierInfo(APL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00200DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.APL00200GetRecordAsync(poEntity);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
