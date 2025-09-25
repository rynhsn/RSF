using PMT01300COMMON;
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

namespace PMT01300MODEL
{
    public class PMT01321ViewModel : R_ViewModel<PMT01320DTO>
    {
        private PMT01320Model _PMT01320Model = new PMT01320Model();
        private PMT01300InitModel _PMT01300InitModel = new PMT01300InitModel();

        #region Property Class
        public PMT01320DTO LOI_Utilities { get; set; } = new PMT01320DTO();
        public List<PMT01300UniversalDTO> VAR_UTILITY_TYPE { get; set; } = new List<PMT01300UniversalDTO>();
        public LinkedList<PMT01300AgreementBuildingUtilitiesDTO> VAR_METER_NO { get; set; } = new LinkedList<PMT01300AgreementBuildingUtilitiesDTO>();
        public ObservableCollection<PMT01320DTO> LOIUtiliesGrid { get; set; } = new ObservableCollection<PMT01320DTO>();
        public bool StatusChange { get; set; }
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            List<PMT01300UniversalDTO> loUniversalListResult = null;

            try
            {
                loUniversalListResult = await _PMT01300InitModel.GetAllUniversalListAsync("_BS_UTILITY_CHARGES_TYPE");
                VAR_UTILITY_TYPE = loUniversalListResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUtilitiesList(PMT01320DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01320Model.GetLOIUtilitiesStreamAsync(poEntity);
                loResult.ForEach(x => x.CCHARGES_ID_NAME = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")" );

                LOIUtiliesGrid = new ObservableCollection<PMT01320DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUtilities(PMT01320DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01320Model.R_ServiceGetRecordAsync(poEntity);

                await GetBuildingUtilitiesList(loResult);
                LOI_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveLOIUtilities(PMT01320DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01320Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                LOI_Utilities = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteLOIUtilities(PMT01320DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMT01320Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetBuildingUtilitiesList(PMT01320DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01300ParameterAgreementBuildingUtilitiesDTO>(poEntity);
                var loResult = await _PMT01300InitModel.GetAllBuildingUtilitiesListAsync(loParam);

                VAR_METER_NO = new LinkedList<PMT01300AgreementBuildingUtilitiesDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT01320DTO> ActiveInactiveProcessAsync(object poParameter)
        {
            R_Exception loException = new R_Exception();
            PMT01320DTO loRtn = null;

            try
            {
                // set Status
                var loParamActive = R_FrontUtility.ConvertObjectToObject<PMT01320ActiveInactiveDTO>(poParameter);
                loParamActive.LACTIVE = StatusChange;

                await _PMT01320Model.ChangeStatusLOIUtilityAsync(loParamActive);

                var loData = (PMT01320DTO)poParameter;
                var loResult = await _PMT01320Model.R_ServiceGetRecordAsync(loData);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
