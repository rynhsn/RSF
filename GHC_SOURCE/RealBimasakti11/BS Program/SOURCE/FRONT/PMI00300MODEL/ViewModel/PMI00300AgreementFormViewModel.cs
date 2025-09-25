using PMI00300COMMON;
using PMI00300COMMON.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMI00300MODEL.ViewModel
{
    public class PMI00300AgreementFormViewModel : R_ViewModel<PMI00300GetAgreementFormListDTO>
    {
        public PMI00300AgreementFormModel loModel = new PMI00300AgreementFormModel();

        public ObservableCollection<PMI00300GetAgreementFormListDTO> loAgreementFormList = new ObservableCollection<PMI00300GetAgreementFormListDTO>();
        public PMI00300GetAgreementFormListDTO loSelectedAgreementForm = new PMI00300GetAgreementFormListDTO();

        public PMI00300AgreementFormDisplayProcessDTO loDisplayProcess = new PMI00300AgreementFormDisplayProcessDTO();
        public PMI00300GetPeriodYearRangeDTO loPeriodYearRange = new PMI00300GetPeriodYearRangeDTO();

        public async Task InitialProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetPeriodYearRangeAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementFormListAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new PMI00300GetAgreementFormListParameterDTO()
                {
                    CPROPERTY_ID = loDisplayProcess.CPROPERTY_ID,
                    CBUILDING_ID = loDisplayProcess.CBUILDING_ID,
                    CFLOOR_ID = loDisplayProcess.CFLOOR_ID,
                    CUNIT_ID = loDisplayProcess.CUNIT_ID,
                    IPERIOD_YEAR = loDisplayProcess.IPERIOD_YEAR,
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMI00300_GET_AGREEMENT_FORM_LIST_STREAMING_CONTEXT, loParam);
                var loResult = await loModel.GetAgreementFormListAsync();

                foreach (var item in loResult.Data)
                {
                    if (!string.IsNullOrWhiteSpace(item.CSTART_DATE))
                    {
                        if (DateTime.TryParseExact(item.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)) item.DSTART_DATE = result;
                    }

                    if (!string.IsNullOrWhiteSpace(item.CEND_DATE))
                    {
                        if (DateTime.TryParseExact(item.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)) item.DEND_DATE = result;
                    }
                    //cr04
                    if (!string.IsNullOrWhiteSpace(item.CHO_ACTUAL_DATE))
                    {
                        if (DateTime.TryParseExact(item.CHO_ACTUAL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)) item.DHO_ACTUAL_DATE = result;
                    }

                    if (item.IEXPIRED_DAYS > 0) item.CEXPIRED_DAYS = item.IEXPIRED_DAYS.ToString();
                }

                loAgreementFormList = new ObservableCollection<PMI00300GetAgreementFormListDTO>(loResult.Data);

                loSelectedAgreementForm = loAgreementFormList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodYearRangeAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (loPeriodYearRange.IMAX_YEAR == 0 || loPeriodYearRange.IMIN_YEAR == 0)
                {
                    var loResult = await loModel.GetPeriodYearRangeAsync();
                    loPeriodYearRange = loResult.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}