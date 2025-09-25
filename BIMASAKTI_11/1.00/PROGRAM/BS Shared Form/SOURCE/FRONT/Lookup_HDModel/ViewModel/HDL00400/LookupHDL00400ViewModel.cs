using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_HDCOMMON.DTOs.HDL00400;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_HDModel.ViewModel.HDL00400
{
    public class LookupHDL00400ViewModel
    {
        private PublicHDLookupModel _model = new PublicHDLookupModel();
        private PublicHDLookupRecordModel _modelRecord = new PublicHDLookupRecordModel();
        public ObservableCollection<HDL00400DTO> AssetLookup = new ObservableCollection<HDL00400DTO>();
        public HDL00400DTO AssetLookupRecord { get; set; }
        public HDL00400ParameterDTO ParameterLookup { get; set; }
        

        public async Task GetAssetList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.HDL00400AssetLookupAsync(ParameterLookup);
                AssetLookup = new ObservableCollection<HDL00400DTO>(loResult);
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<HDL00400DTO> GetAssetRecord(HDL00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00400DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.HDL00400GetRecordAsync(poEntity);
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