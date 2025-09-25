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
    public class LookupGSL03010ViewModel : R_ViewModel<GSL03010DTO>
    {

        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03010DTO> ProductUnitGrid = new ObservableCollection<GSL03010DTO>();

        public async Task GetProductUnitList(GSL03010ParameterDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03010GetProductUnitListAsync(poEntity);

                ProductUnitGrid = new ObservableCollection<GSL03010DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03010DTO> GetProductUnit(GSL03010ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03010DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03010GetProductUnitAsync(poParameter);
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
