using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using TXB00200Common;
using TXB00200Common.DTOs;
using TXB00200Common.Params;

namespace TXB00200Model.ViewModel
{
    public class TXB00200ViewModel : R_ViewModel<TXB00200PropertyDTO>
    {
        private TXB00200Model _model = new TXB00200Model();

        public List<TXB00200PropertyDTO> PropertyList = new List<TXB00200PropertyDTO>();
        public List<TXB00200PeriodDTO> PeriodList = new List<TXB00200PeriodDTO>();
        
        public TXB00200NextPeriodDTO NextPeriod { get; set; } = new TXB00200NextPeriodDTO();
        public string SelectedPropertyId { get; set; }
        public int SelectedYear { get; set; } = DateTime.Now.Year;
        public string SelectedPeriodNo { get; set; }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<TXB00200ListDTO<TXB00200PropertyDTO>>(
                        nameof(ITXB00200.TXB00200GetPropertyList));
                PropertyList = loReturn.Data;
                SelectedPropertyId = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetNextPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<TXB00200SingleDTO<TXB00200NextPeriodDTO>>(
                        nameof(ITXB00200.TXB00200GetNextPeriod));
                NextPeriod = loReturn.Data;
                SelectedPeriodNo = NextPeriod.CMONTH;
                SelectedYear = int.TryParse(NextPeriod.CYEAR, out var loYear) ? loYear : DateTime.Now.Year;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetPeriodList()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new TXB00200YearParam { CYEAR = SelectedYear.ToString() };

                var loReturn =
                    await _model.GetAsync<TXB00200ListDTO<TXB00200PeriodDTO>, TXB00200YearParam>(
                        nameof(ITXB00200.TXB00200GetPeriodList), loParam);
                PeriodList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task ProcessSoftPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new TXB00200SoftCloseParam()
                {
                    CPROPERTY_ID = SelectedPropertyId,
                    CTAX_PERIOD_YEAR = SelectedYear.ToString(),
                    CTAX_PERIOD_MONTH = SelectedPeriodNo
                };
                
                var loResult =
                    await _model.GetAsync<TXB00200ListDTO<TXB00200SoftCloseParam>, TXB00200SoftCloseParam>(
                        nameof(ITXB00200.TXB00200ProcessSoftClose), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}