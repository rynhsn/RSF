using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL01700ViewModel : R_ViewModel<GSL01700DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL01700DTO> CurrencyRateGrid = new ObservableCollection<GSL01700DTO>();

        public List<GSL01701DTO> RateTypeList = new List<GSL01701DTO>();

        public List<GSL01702DTO> LocalAndBaseCurrencyList = new List<GSL01702DTO>();

        public async Task GetRateTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01700GetRateTypeListAsync();

                RateTypeList = new List<GSL01701DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetLocalAndBaseCurrencyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01700GetLocalAndBaseCurrencyListAsync();

                LocalAndBaseCurrencyList = new List<GSL01702DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyRateList(GSL01700DTOParameter poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL01700GetCurrencyRateListAsync(poParameter);

                CurrencyRateGrid = new ObservableCollection<GSL01700DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
