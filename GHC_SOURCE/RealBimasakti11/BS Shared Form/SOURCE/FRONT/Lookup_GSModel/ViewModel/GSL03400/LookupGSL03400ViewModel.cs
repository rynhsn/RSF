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
    public class LookupGSL03400ViewModel : R_ViewModel<GSL03400DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03400DTO> DigitalSignGrid = new ObservableCollection<GSL03400DTO>();
        public GSL03400ParameterDTO DigitalSignParameter = new GSL03400ParameterDTO();
        public async Task GetDigitalSignList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03400GetDigitalSignListAsync();
                DigitalSignGrid = new ObservableCollection<GSL03400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03400DTO> GetDigitalSign(GSL03400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03400DTO loRtn = null;
            try
            {
                if (string.IsNullOrWhiteSpace(poParameter.CSEARCH_TEXT) == false && string.IsNullOrWhiteSpace(poParameter.CSIGN_ID))
                {
                    poParameter.CSIGN_ID = poParameter.CSEARCH_TEXT;
                }
                var loResult = await _modelRecord.GSL03400GetDigitalSignAsync(poParameter);
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
