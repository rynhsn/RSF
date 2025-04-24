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
    public class PMT00560ViewModel : R_ViewModel<PMT00560DTO>
    {
        private PMT00560Model _PMT00560Model = new PMT00560Model();

        #region Property Class
        public PMT00500DTO LOI { get; set; } = new PMT00500DTO();
        public ObservableCollection<PMT00560DTO> LOIDocumentGrid { get; set; } = new ObservableCollection<PMT00560DTO>();
        public PMT00560DTO LOI_Document { get; set; } = new PMT00560DTO();
        #endregion

        public async Task GetLOIDocumentList(PMT00560DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00560Model.GetLOIDocumentStreamAsync(poEntity);
                loResult.ForEach(x =>
                {
                    x.DDOC_DATE = DateTime.TryParseExact(x.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate) ? (DateTime?)ldDocDate : null;
                    x.DEXPIRED_DATE = DateTime.TryParseExact(x.CEXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldExpiredDate) ? (DateTime?)ldExpiredDate : null;
                });
                LOIDocumentGrid = new ObservableCollection<PMT00560DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIDocument(PMT00560DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID) ? LOI.CPROPERTY_ID : poEntity.CPROPERTY_ID;
                poEntity.CDEPT_CODE = string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE) ? LOI.CDEPT_CODE : poEntity.CDEPT_CODE;
                var loResult = await _PMT00560Model.R_ServiceGetRecordAsync(poEntity);
                
                loResult.DDOC_DATE = DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate) ? (DateTime?)ldDocDate : null;
                loResult.DEXPIRED_DATE = DateTime.TryParseExact(loResult.CEXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldExpiredDate) ? (DateTime?)ldExpiredDate : null;

                LOI_Document = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveLOIDocument(PMT00560DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                poEntity.CDOC_DATE = poEntity.DDOC_DATE != null ? poEntity.DDOC_DATE.Value.ToString("yyyyMMdd") : "";
                poEntity.CEXPIRED_DATE = poEntity.DEXPIRED_DATE != null ? poEntity.DEXPIRED_DATE.Value.ToString("yyyyMMdd") : "";
                
                var loResult = await _PMT00560Model.R_ServiceSaveAsync(poEntity, poCRUDMode);
                    
                loResult.DDOC_DATE = DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate) ? (DateTime?)ldDocDate : null;
                loResult.DEXPIRED_DATE = DateTime.TryParseExact(loResult.CEXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldExpiredDate) ? (DateTime?)ldExpiredDate : null;

                LOI_Document = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteLOIDocument(PMT00560DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = LOI.CPROPERTY_ID;
                poEntity.CDEPT_CODE = LOI.CDEPT_CODE;
                await _PMT00560Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
