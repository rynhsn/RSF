using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL03800ViewModel : R_ViewModel<GSL03800DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03800DTO> LocationGrid = new ObservableCollection<GSL03800DTO>();
        public GSL03800ParameterDTO LookupParam = new GSL03800ParameterDTO();
        public List<GSLPropertyDTO> PropertyList = new List<GSLPropertyDTO>();
        public bool LACTIVE_CHECKBOX;
        public async Task GetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSLPropertyListAsync();

                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLocationList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03800GetLocationListAsync(LookupParam);

                LocationGrid = new ObservableCollection<GSL03800DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03800DTO> GetLocation(GSL03800ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03800DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03800GetLocationAsync(poParameter);
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
