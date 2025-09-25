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
    public class LookupGSL03700ViewModel : R_ViewModel<GSL03700DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03700DTO> MessageGrid = new ObservableCollection<GSL03700DTO>();
        public async Task GetMessageList(GSL03700ParameterDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03700GetMessageListAsync(poEntity);
                loResult.ForEach(x =>
                {
                    x.CMESSAGE_NO_NAME = x.CMESSAGE_NO + " - " + x.CMESSAGE_NAME;
                });

                MessageGrid = new ObservableCollection<GSL03700DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03700DTO> GetMessage(GSL03700ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03700DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03700GetMessageAsync(poParameter);
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
