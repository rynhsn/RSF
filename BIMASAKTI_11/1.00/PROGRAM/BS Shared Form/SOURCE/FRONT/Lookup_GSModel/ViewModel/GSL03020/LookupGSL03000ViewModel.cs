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
    public class LookupGSL03020ViewModel : R_ViewModel<GSL03020DTO>
    {

        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03020DTO> ProductUOMGrid = new ObservableCollection<GSL03020DTO>();

        public async Task GetProductUOMList(GSL03020ParameterDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03020GetProductUOMListAsync(poEntity);

                ProductUOMGrid = new ObservableCollection<GSL03020DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03020DTO> GetProductUOM(GSL03020ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03020DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03020GetProductUOMAsync(poParameter);
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
