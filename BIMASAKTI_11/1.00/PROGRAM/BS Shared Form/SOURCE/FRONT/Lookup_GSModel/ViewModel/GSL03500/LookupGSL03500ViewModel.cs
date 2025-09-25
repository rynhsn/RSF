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
    public class LookupGSL03500ViewModel : R_ViewModel<GSL03500DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03500DTO> WarehouseGrid = new ObservableCollection<GSL03500DTO>();
        public GSL03500ParameterDTO WarehouseParameter = new GSL03500ParameterDTO();
        public bool LACTIVE_CHECKBOX { get; set; }
        public async Task GetWarehouseList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03500GetWarehouseListAsync(WarehouseParameter);

                WarehouseGrid = new ObservableCollection<GSL03500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03500DTO> GetWarehouse(GSL03500ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03500DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03500GetWarehouseAsync(poParameter);
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
