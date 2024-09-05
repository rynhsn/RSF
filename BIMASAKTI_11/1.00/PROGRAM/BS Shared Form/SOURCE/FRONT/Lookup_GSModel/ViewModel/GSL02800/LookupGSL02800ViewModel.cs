using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02800ViewModel : R_ViewModel<GSL02800DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL02800DTO> OtherUnitMasterGrid = new ObservableCollection<GSL02800DTO>();

        public async Task GetOtherUnitList(GSL02800ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02800GetOtherUnitMasterListAsync(poParameter);

                OtherUnitMasterGrid = new ObservableCollection<GSL02800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02800DTO> GetOtherUnit(GSL02800ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL02800DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL02800GetOtherUnitMasterAsync(poParameter);
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
