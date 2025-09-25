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
    public class PMT00521ViewModel : R_ViewModel<PMT00520DTO>
    {
        private PMT00520Model _PMT00520Model = new PMT00520Model();
        private PMT00500InitModel _PMT00500InitModel = new PMT00500InitModel();

        #region Property Class
        public PMT00520DTO LOI_Utilities { get; set; } = new PMT00520DTO();
        public List<PMT00500UniversalDTO> VAR_UTILITY_TYPE { get; set; } = new List<PMT00500UniversalDTO>();
        public LinkedList<PMT00500AgreementBuildingUtilitiesDTO> VAR_METER_NO { get; set; } = new LinkedList<PMT00500AgreementBuildingUtilitiesDTO>();
        public ObservableCollection<PMT00520DTO> LOIUtiliesGrid { get; set; } = new ObservableCollection<PMT00520DTO>();
        public bool StatusChange { get; set; }
        private LinkedList<PMT00520DTO> LOIUtiliesList { get; set; } = new LinkedList<PMT00520DTO>();
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            List<PMT00500UniversalDTO> loUniversalListResult = null;

            try
            {
                loUniversalListResult = await _PMT00500InitModel.GetAllUniversalListAsync("_BS_UTILITY_CHARGES_TYPE");
                VAR_UTILITY_TYPE = loUniversalListResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUtilitiesList(PMT00520DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00520Model.GetLOIUtilitiesStreamAsync(poEntity);
                loResult.ForEach(x =>
                {
                    x.CCHARGES_ID_NAME = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")";
                    x.DSTART_DATE = DateTime.TryParseExact(x.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate) ? (DateTime?)ldStartDate : null;
                });

                LOIUtiliesGrid = new ObservableCollection<PMT00520DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUtilities(PMT00520DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00520Model.R_ServiceGetRecordAsync(poEntity);

                await GetBuildingUtilitiesList(loResult);
                LOI_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveLOIUtilities(PMT00520DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00520Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                LOI_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteLOIUtilities(PMT00520DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMT00520Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetBuildingUtilitiesList(PMT00520DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500ParameterAgreementBuildingUtilitiesDTO>(poEntity);
                var loResult = await _PMT00500InitModel.GetAllBuildingUtilitiesListAsync(loParam);

                VAR_METER_NO = new LinkedList<PMT00500AgreementBuildingUtilitiesDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT00520DTO> ActiveInactiveProcessAsync(object poParameter)
        {
            R_Exception loException = new R_Exception();
            PMT00520DTO loRtn = null;

            try
            {
                // set Status
                var loParamActive = R_FrontUtility.ConvertObjectToObject<PMT00520ActiveInactiveDTO>(poParameter);
                loParamActive.LACTIVE = StatusChange;

                await _PMT00520Model.ChangeStatusLOIUtilityAsync(loParamActive);

                var loData = (PMT00520DTO)poParameter;
                var loResult = await _PMT00520Model.R_ServiceGetRecordAsync(loData);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        #region Utility List
        public async Task GetLOIUtilitiesListGrid(PMT00520DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00520Model.GetLOIUtilitiesStreamAsync(poEntity);
                loResult.ForEach(x =>
                {
                    x.CUNIT_ID = poEntity.CUNIT_ID;
                    x.CBUILDING_ID = poEntity.CBUILDING_ID;
                    x.CFLOOR_ID = poEntity.CFLOOR_ID;
                    x.CDEPT_CODE = poEntity.CDEPT_CODE;
                    x.CCHARGES_ID_NAME = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")";

                    LOIUtiliesList.AddLast(x);
                });

                LOIUtiliesGrid = new ObservableCollection<PMT00520DTO>(LOIUtiliesList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task RemoveLOIUtilityListGrid(PMT00520DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loListData = LOIUtiliesList.Where(x => x.CUNIT_ID == poEntity.CUNIT_ID).ToList();

                foreach (PMT00520DTO Item in loListData)
                {
                    LOIUtiliesList.Remove(Item);
                }

                LOIUtiliesGrid = new ObservableCollection<PMT00520DTO>(LOIUtiliesList);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
