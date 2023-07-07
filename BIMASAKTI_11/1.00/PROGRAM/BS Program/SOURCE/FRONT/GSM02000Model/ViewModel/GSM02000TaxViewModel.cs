using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using GSM02000Common;
using GSM02000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM02000Model.ViewModel
{
    public class GSM02000TaxViewModel : R_ViewModel<GSM02000TaxDTO>
    {
        private GSM02000TaxModel _model = new GSM02000TaxModel();
        public ObservableCollection<GSM02000TaxDTO> GridList = new ObservableCollection<GSM02000TaxDTO>();
        public ObservableCollection<GSM02000TaxSalesDTO> SalesGridList = new ObservableCollection<GSM02000TaxSalesDTO>();
        
        public GSM02000TaxDTO Entity = new GSM02000TaxDTO();
        public string SelectedSalesTaxId = "";
        
        public async Task GetGridList(string pcSalesTaxId)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTAX_ID, pcSalesTaxId);
                var loReturn = await _model.GetAllTaxListModel();
                GridList = new ObservableCollection<GSM02000TaxDTO>(loReturn.Data);

                if (GridList.Count > 0)
                {
                    foreach (var list in GridList)
                    {
                        list.DTAX_DATE = DateTime.ParseExact(list.CTAX_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetEntity(GSM02000TaxDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceGetRecordAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task SaveEntity(GSM02000TaxDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                Entity.DTAX_DATE = DateTime.ParseExact(Entity.CTAX_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteEntity(GSM02000TaxDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetSalesTaxList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetAllSalesTaxListModel();
                SalesGridList = new ObservableCollection<GSM02000TaxSalesDTO>(loReturn.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void GetSalesTaxEntity(string pcSalesTaxId)
        {
            var loEx = new R_Exception();

            try
            {
                SelectedSalesTaxId = pcSalesTaxId;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}