using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02200ViewModel : R_ViewModel<GSL02200DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL02200DTO> BuildingGrid = new ObservableCollection<GSL02200DTO>();

        public async Task GetBuildingList(GSL02200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02200GetBuildingListAsync(poParameter);

                BuildingGrid = new ObservableCollection<GSL02200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02200DTO> GetBuilding(GSL02200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL02200DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL02200GetBuildingAsync(poParameter);
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
