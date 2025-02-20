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
    public class PMT01320ViewModel : R_ViewModel<PMT01310DTO>
    {
        private PMT01310Model _PMT01310Model = new PMT01310Model();

        #region Property Class
        public PMT01300DTO LOI { get; set; } = new PMT01300DTO();
        public ObservableCollection<PMT01310DTO> LOIUNITGrid { get; set; } = new ObservableCollection<PMT01310DTO>();
        public PMT01310DTO LOI_Unit { get; set; } = new PMT01310DTO();
        public bool LIS_SINGLE_UNIT { get; set; }
        #endregion

        public async Task GetLOIUnitList(PMT01310DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01310Model.GetLOIUnitListStreamAsync(poEntity);
                if (loResult.Count > 0)
                {
                    LIS_SINGLE_UNIT = loResult.FirstOrDefault().LSINGLE_UNIT;

                }
                else
                {
                    LIS_SINGLE_UNIT = false;
                }

                LOIUNITGrid = new ObservableCollection<PMT01310DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUnit(PMT01310DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID) ? LOI.CPROPERTY_ID : poEntity.CPROPERTY_ID;
                poEntity.CDEPT_CODE = string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE) ? LOI.CDEPT_CODE : poEntity.CDEPT_CODE;
                var loResult = await _PMT01310Model.R_ServiceGetRecordAsync(poEntity);

                LOI_Unit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveLOIUnit(PMT01310DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                var loResult = await _PMT01310Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                LOI_Unit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteLOIUnit(PMT01310DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                await _PMT01310Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
