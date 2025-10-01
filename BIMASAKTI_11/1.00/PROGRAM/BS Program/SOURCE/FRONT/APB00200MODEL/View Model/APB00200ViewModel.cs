using APB00200COMMON.DTO_s;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace APB00200MODEL.View_Model
{
    public class APB00200ViewModel
    {
        //variabels
        private APB00200Model _model = new APB00200Model();

        public ObservableCollection<ErrorCloseAPProcessDTO> ListErrorProcess { get; set; } = new ObservableCollection<ErrorCloseAPProcessDTO>();
        public ClosePeriodDTO ClosePeriod { get; set; } = new ClosePeriodDTO();

        //actions
        public Func<Task> ShowErrorPopup { get; set; }

        public Func<Task> ShowSuccessPopup { get; set; }

        //methods
        public async Task InitialProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetClosePeriodAsync();
                ClosePeriod.CCURRENT_PERIOD = ConvertToDashFormat(ClosePeriod.CCURRENT_PERIOD) ?? "";
                ClosePeriod.CSOFT_PERIOD = ConvertToDashFormat(ClosePeriod.CSOFT_PERIOD) ?? "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetClosePeriodAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetClosePeriodAsync(new ClosePeriodParam());
                ClosePeriod = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessCloseAPPeriodAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loListError = await _model.ProcessCloseAPPeriodAsync(new CloseAPProcessParam()
                {
                    CPERIOD_MONTH = ClosePeriod.CPERIOD_MONTH,
                    CPERIOD_YEAR = ClosePeriod.CPERIOD_YEAR
                });
                CloseAPProcessResultDTO loResult = loListError.Data;
                if (loResult.LERROR)
                {
                    await ShowErrorPopup();
                }
                else
                {
                    await ShowSuccessPopup();
                    await GetClosePeriodAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetListErrorProcessCloseAPPrdAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loListError = await _model.GetList_ErrorProcessCloseAPPeriodAsync(new CloseAPProcessParam()
                {
                    CPERIOD_MONTH = ClosePeriod.CPERIOD_MONTH,
                    CPERIOD_YEAR = ClosePeriod.CPERIOD_YEAR
                });

                ListErrorProcess = new ObservableCollection<ErrorCloseAPProcessDTO>(loListError);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //helper
        private static string? ConvertToDashFormat(string? pcEntity) => !string.IsNullOrWhiteSpace(pcEntity) && pcEntity.Length == 6 ? $"{pcEntity[..4]}-{pcEntity.Substring(4, 2)}" : null;
    }
}