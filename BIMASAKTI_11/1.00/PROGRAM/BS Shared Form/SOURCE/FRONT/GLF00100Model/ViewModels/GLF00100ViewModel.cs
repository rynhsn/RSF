using GLF00100COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GLF00100Model
{
    public class GLF00100ViewModel : R_ViewModel<GLF00100DTO>
    {
        private GLF00100Model _GLF00100Model = new GLF00100Model();
        public ObservableCollection<GLF00101DTO> JrnlDetailGrid { get; set; } = new ObservableCollection<GLF00101DTO>();
        public GLF00100DTO JrnlDetail { get; set; } = new GLF00100DTO();
        public GLF00100InitialDTO CompanyInfo { get; set; } = new GLF00100InitialDTO();

        public DateTime? RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        public async Task GetCompanyInfo()
        {
            var loEx = new R_Exception();

            try
            {
                CompanyInfo = await _GLF00100Model.GetInfoCompanyAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalDetail(GLF00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLF00100Model.GetJournalDetailAsync(poParam);

                if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                {
                    RefDate = ldRefDate;
                }
                else
                {
                    RefDate = null;
                }
                if (DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                {
                    DocDate = ldDocDate;
                }
                else
                {
                    DocDate = null;
                }

                JrnlDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalDetailList(GLF00101DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLF00100Model.GetJournalDetailListAsync(poParam);

                JrnlDetailGrid = new ObservableCollection<GLF00101DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
