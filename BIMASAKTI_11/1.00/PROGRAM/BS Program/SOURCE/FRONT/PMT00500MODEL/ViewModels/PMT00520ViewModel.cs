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
    public class PMT00520ViewModel : R_ViewModel<PMT00510DTO>
    {
        private PMT00510Model _PMT00510Model = new PMT00510Model();

        #region Property Class
        public PMT00500DTO LOI { get; set; } = new PMT00500DTO();
        public ObservableCollection<PMT00510DTO> LOIUNITGrid { get; set; } = new ObservableCollection<PMT00510DTO>();
        public PMT00510DTO LOI_Unit { get; set; } = new PMT00510DTO();
        public bool LIS_SINGLE_UNIT { get; set; }
        #endregion

        public async Task GetLOIUnitList(PMT00510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00510Model.GetLOIUnitListStreamAsync(poEntity);
                if (loResult.Count > 0)
                {
                    LIS_SINGLE_UNIT = loResult.FirstOrDefault().LSINGLE_UNIT;

                }
                else
                {
                    LIS_SINGLE_UNIT = false;
                }

                LOIUNITGrid = new ObservableCollection<PMT00510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUnit(PMT00510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID) ? LOI.CPROPERTY_ID : poEntity.CPROPERTY_ID;
                poEntity.CDEPT_CODE = string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE) ? LOI.CDEPT_CODE : poEntity.CDEPT_CODE;
                var loResult = await _PMT00510Model.R_ServiceGetRecordAsync(poEntity);

                LOI_Unit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveLOIUnit(PMT00510DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                var loResult = await _PMT00510Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                LOI_Unit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteLOIUnit(PMT00510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                await _PMT00510Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
