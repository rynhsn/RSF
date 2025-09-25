using PMT00500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00510ViewModel : R_ViewModel<PMT00500DTO>
    {
        private PMT00500Model _PMT00500Model = new PMT00500Model();
        private PMT00500InitModel _PMT00500InitModel = new PMT00500InitModel();

        #region Property Class
        public PMT00500TenureConvertDTO oControlYMD { get; set; } = new PMT00500TenureConvertDTO();
        public PMT00500DTO LOI { get; set; } = new PMT00500DTO();
        public PMT00500TransCodeInfoGSDTO VAR_GSM_TRANS_CODE_LOI { get; set; } = new PMT00500TransCodeInfoGSDTO();
        public List<PMT00500UniversalDTO> VAR_CHARGE_MODE { get; set; } = new List<PMT00500UniversalDTO>();
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            List<PMT00500UniversalDTO> loUniversalListResult = null;
            try
            {
                var loGsmTransCodeResult = await _PMT00500InitModel.GetTransCodeInfoAsync();
                VAR_GSM_TRANS_CODE_LOI = loGsmTransCodeResult;

                loUniversalListResult = await _PMT00500InitModel.GetAllUniversalListAsync("_BS_CHARGE_MODE");
                VAR_CHARGE_MODE = loUniversalListResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOI(PMT00500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00500Model.GetLOIAsync(poEntity);
                
                if (DateTime.TryParseExact(loResult.CFOLLOW_UP_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldFollowUpDate))
                {
                    loResult.DFOLLOW_UP_DATE = ldFollowUpDate;
                }
                else
                {
                    loResult.DFOLLOW_UP_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                {
                    loResult.DREF_DATE = ldRefDate;
                }
                else
                {
                    loResult.DREF_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                {
                    loResult.DSTART_DATE = ldStartDate;
                }
                else
                {
                    loResult.DSTART_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                {
                    loResult.DEND_DATE = ldEndDate;
                }
                else
                {
                    loResult.DEND_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CHO_PLAN_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldHOPlanDate))
                {
                    loResult.DHO_PLAN_DATE = ldHOPlanDate;
                }
                else
                {
                    loResult.DHO_PLAN_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CHO_ACTUAL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldHOActualDate))
                {
                    loResult.DHO_ACTUAL_DATE = ldHOActualDate;
                }
                else
                {
                    loResult.DHO_ACTUAL_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CACTUAL_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldActualStartDate))
                {
                    loResult.DACTUAL_START_DATE = ldActualStartDate;
                }
                else
                {
                    loResult.DACTUAL_START_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CACTUAL_END_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldActualEndDate))
                {
                    loResult.DACTUAL_END_DATE = ldActualEndDate;
                }
                else
                {
                    loResult.DACTUAL_END_DATE = null;
                }

                LOI = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT00500DTO> GetLOIWithResult(PMT00500DTO poEntity)
        {
            var loEx = new R_Exception();
            PMT00500DTO loRtn = null;

            try
            {
                var loResult = await _PMT00500Model.GetLOIAsync(poEntity);

                if (DateTime.TryParseExact(loResult.CFOLLOW_UP_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldFollowUpDate))
                {
                    loResult.DFOLLOW_UP_DATE = ldFollowUpDate;
                }
                else
                {
                    loResult.DFOLLOW_UP_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                {
                    loResult.DREF_DATE = ldRefDate;
                }
                else
                {
                    loResult.DREF_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                {
                    loResult.DSTART_DATE = ldStartDate;
                }
                else
                {
                    loResult.DSTART_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                {
                    loResult.DEND_DATE = ldEndDate;
                }
                else
                {
                    loResult.DEND_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CHO_PLAN_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldHOPlanDate))
                {
                    loResult.DHO_PLAN_DATE = ldHOPlanDate;
                }
                else
                {
                    loResult.DHO_PLAN_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CHO_ACTUAL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldHOActualDate))
                {
                    loResult.DHO_ACTUAL_DATE = ldHOActualDate;
                }
                else
                {
                    loResult.DHO_ACTUAL_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CACTUAL_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldActualStartDate))
                {
                    loResult.DACTUAL_START_DATE = ldActualStartDate;
                }
                else
                {
                    loResult.DACTUAL_START_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CACTUAL_END_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldActualEndDate))
                {
                    loResult.DACTUAL_END_DATE = ldActualEndDate;
                }
                else
                {
                    loResult.DACTUAL_END_DATE = null;
                }

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task SaveLOI(PMT00500DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CREF_DATE = poEntity.DREF_DATE.Value.ToString("yyyyMMdd");
                poEntity.CFOLLOW_UP_DATE = poEntity.DFOLLOW_UP_DATE != null ? poEntity.DFOLLOW_UP_DATE.Value.ToString("yyyyMMdd") : "";

                poEntity.CHO_PLAN_DATE = poEntity.DHO_PLAN_DATE.Value.ToString("yyyyMMdd");

                poEntity.CSTART_DATE = poEntity.DSTART_DATE.Value.ToString("yyyyMMdd");
                poEntity.CEND_DATE = "";

                poEntity.CDOC_DATE = poEntity.DDOC_DATE != null ? poEntity.DDOC_DATE.Value.ToString("yyyyMMdd") : "";
                poEntity.CNOTES = string.IsNullOrWhiteSpace(poEntity.CNOTES) ? "" : poEntity.CNOTES;

                PMT00500SaveDTO<PMT00500DTO> loSaveParam = new PMT00500SaveDTO<PMT00500DTO> { Data= poEntity, CRUDMode= poCRUDMode };
                var loResult = await _PMT00500Model.SaveLOIAsync(loSaveParam);

                if (DateTime.TryParseExact(loResult.CFOLLOW_UP_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldFollowUpDate))
                {
                    loResult.DFOLLOW_UP_DATE = ldFollowUpDate;
                }
                else
                {
                    loResult.DFOLLOW_UP_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                {
                    loResult.DREF_DATE = ldRefDate;
                }
                else
                {
                    loResult.DREF_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                {
                    loResult.DSTART_DATE = ldStartDate;
                }
                else
                {
                    loResult.DSTART_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                {
                    loResult.DEND_DATE = ldEndDate;
                }
                else
                {
                    loResult.DEND_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CHO_PLAN_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldHOPlanDate))
                {
                    loResult.DHO_PLAN_DATE = ldHOPlanDate;
                }
                else
                {
                    loResult.DHO_PLAN_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CHO_ACTUAL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldHOActualDate))
                {
                    loResult.DHO_ACTUAL_DATE = ldHOActualDate;
                }
                else
                {
                    loResult.DHO_ACTUAL_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CACTUAL_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldActualStartDate))
                {
                    loResult.DACTUAL_START_DATE = ldActualStartDate;
                }
                else
                {
                    loResult.DACTUAL_START_DATE = null;
                }

                if (DateTime.TryParseExact(loResult.CACTUAL_END_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldActualEndDate))
                {
                    loResult.DACTUAL_END_DATE = ldActualEndDate;
                }
                else
                {
                    loResult.DACTUAL_END_DATE = null;
                }

                LOI = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT00500DTO> SubmitRedraftLOI(PMT00500SubmitRedraftDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT00500DTO loRtn = null;

            try
            {
                loRtn = await _PMT00500Model.SubmitRedraftAgreementTransAsync(poEntity);
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
