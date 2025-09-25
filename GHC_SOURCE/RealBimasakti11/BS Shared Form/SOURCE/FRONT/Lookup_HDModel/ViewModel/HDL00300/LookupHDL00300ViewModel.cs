using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_HDCOMMON.DTOs.HDL00300;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_HDModel.ViewModel.HDL00300
{
    public class LookupHDL00300ViewModel
    {
        private PublicHDLookupModel _model = new PublicHDLookupModel();
        private PublicHDLookupRecordModel _modelRecord = new PublicHDLookupRecordModel();
        public ObservableCollection<HDL00300DTO> PublicLocationLookup = new ObservableCollection<HDL00300DTO>();
        public HDL00300DTO PublicLocationLookupRecord { get; set; }
        public HDL00300ParameterDTO ParameterLookup { get; set; }
        

        public async Task GetPublicLocation()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.HDL00300PublicLocationLookupAsync(ParameterLookup);
                PublicLocationLookup = new ObservableCollection<HDL00300DTO>(loResult);
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<HDL00300DTO> GetPublicLocationRecord(HDL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00300DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.HDL00300GetRecordAsync(poEntity);
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