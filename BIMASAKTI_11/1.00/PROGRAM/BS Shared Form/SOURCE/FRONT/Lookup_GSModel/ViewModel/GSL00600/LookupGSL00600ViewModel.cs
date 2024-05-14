using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00600ViewModel : R_ViewModel<GSL00600DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00600DTO> UnitTypeCategoryGrid = new ObservableCollection<GSL00600DTO>();

        public async Task GetUnitTypeCategoryList(GSL00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00600GetUnitTypeCategoryListAsync(poParam);

                UnitTypeCategoryGrid = new ObservableCollection<GSL00600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL00600DTO> GetUnitTypeCategory(GSL00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSL00600DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00600GetUnitTypeCategoryAsync(poParam);
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
