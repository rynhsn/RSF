using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM00500Common;
using PMM00500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ContextFrontEnd;

namespace PMM00500Model
{
    public class PMM00510ViewModel : R_ViewModel<PMM00510DTO>
    {
        private PMM00510Model _model = new PMM00510Model();
        #region radioPrintDTO
        public List<RadioPrintDTO> radioPrint = new List<RadioPrintDTO>
        {
            new RadioPrintDTO { CCODE = "01", CDESCRIPTION = "Charge Id" },
            new RadioPrintDTO { CCODE = "02", CDESCRIPTION = "Charge Name" }
        };

        public List<ReportTypeExtension> radioPrintExtTypeList = new List<ReportTypeExtension>
        {
            new ReportTypeExtension() { CKEY = "PDF", CNAME = "PDF" },
            new ReportTypeExtension() { CKEY = "XLSX", CNAME = "XLSX" },
            new ReportTypeExtension() { CKEY = "XLS", CNAME = "XLS" },
            new ReportTypeExtension() { CKEY = "CSV", CNAME = "CSV" },
        };
        public string _printType = "PDF";
        public class RadioPrintDTO
        {
            public string CCODE { get; set; }
            public string CDESCRIPTION { get; set; }
        }
        #endregion
        #region List
        public ObservableCollection<PMM00500GridDTO> ChargesList { get; set; } = new ObservableCollection<PMM00500GridDTO>();
        public List<PropertyListStreamChargeDTO> PropertyList { get; set; } = new List<PropertyListStreamChargeDTO>();
        public List<ChargesTaxTypeDTO> loChargeTaxTypeList = new List<ChargesTaxTypeDTO>();
        public List<ChargesTaxCodeDTO> loChargeTaxCodeList = new List<ChargesTaxCodeDTO>();
        public List<AccurualDTO> loAccrualList = new List<AccurualDTO>();
        public List<UnitChargesTypeDTO> loUnitChargesTypeList = new List<UnitChargesTypeDTO>();
        #endregion

        public PMM00510DTO Charge = new PMM00510DTO();
        public PMM00510DTO loTmp = new PMM00510DTO();
        public string
            PropertyValue = "",
            PropertyValueName = "",
            ChargeTypeValue = "";
        public bool
            recurringComboEnable = false,
            enableTaxExemption = false,
            enableWitholdingTax = false,
            enableOtherTax = false,
            enableTaxable = false,
            enableButton = false;

        #region GetList
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetPropertyListChargesAsync();
                if (loResult != null)
                {
                    PropertyList = loResult.Data;
                    if (PropertyValue == "" && loResult.Data.Count > 0)
                    {
                        var firstProperty = PropertyList.FirstOrDefault();
                        PropertyValue = firstProperty.CPROPERTY_ID ?? "";
                        PropertyValueName = firstProperty.CPROPERTY_NAME ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetGridList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _model.GetAllChargesListAsync(pcProperty: PropertyValue, pcChargesType: ChargeTypeValue);
                ChargesList = new ObservableCollection<PMM00500GridDTO>(loReturn.Data);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTaxType()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetChargesTaxTypeAsync();
                loChargeTaxTypeList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTaxCode()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetChargesTaxCodeAsync();
                loChargeTaxCodeList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAccrual()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetAccrualListAsync();
                loAccrualList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUnitChargesType()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetUnitChargesTypeAsync();
                loUnitChargesTypeList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        public async Task GetChargesById(PMM00510DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                Charge = loResult;
                Charge.CCHARGE_TYPE_ID = Charge.CCHARGES_TYPE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveCharge(PMM00510DTO poEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = PropertyValue;
                poEntity.CCHARGES_TYPE = ChargeTypeValue;
                poEntity.CCHARGE_TYPE_ID = ChargeTypeValue;
                await _model.R_ServiceSaveAsync(poEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteCharge(PMM00510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = PropertyValue;
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMM00510DTO> ActiveInactiveProcessAsync(PMM00510DTO poData)
        {
            R_Exception loException = new R_Exception();
            PMM00510DTO loRtn = null;

            try
            {
                poData.LACTIVE = !poData.LACTIVE;
                await _model.RSP_LM_ACTIVE_INACTIVE_MethodAsync(poData);
                var loRes = await _model.R_ServiceGetRecordAsync(poData);
                loRtn = loRes;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task CopyNewProcessAsync(PMM00510DTO poData)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await _model.CopyNewProcessAsync(poData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}

