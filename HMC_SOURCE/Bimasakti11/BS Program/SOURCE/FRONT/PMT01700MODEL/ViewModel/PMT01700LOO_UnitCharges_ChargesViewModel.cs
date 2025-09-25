using PMT01700COMMON.Context._1._Other_Untit_List;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges;
using PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.Front;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Response;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL.ViewModel
{
    public class PMT01700LOO_UnitCharges_ChargesViewModel : R_ViewModel<PMT01700LOO_UnitCharges_ChargesDetailDTO>
    {
        #region From Back

        private readonly PMT01700LOO_UnitCharges_ChargesModel _model = new PMT01700LOO_UnitCharges_ChargesModel();
        public PMT01700LOO_Deposit_DepositHeaderDTO oHeaderEntity = new PMT01700LOO_Deposit_DepositHeaderDTO();
        public ObservableCollection<PMT01700LOO_UnitCharges_ChargesListDTO> oListCharges = new ObservableCollection<PMT01700LOO_UnitCharges_ChargesListDTO>();
        public PMT01700LOO_UnitCharges_ChargesDetailDTO oEntityCharges = new PMT01700LOO_UnitCharges_ChargesDetailDTO();

        public List<PMT01700ComboBoxDTO>? oComboBoxFeeMethod = new List<PMT01700ComboBoxDTO>();
        public List<PMT01700ComboBoxDTO>? oComboBoxPeriodMode = new List<PMT01700ComboBoxDTO>();

        public ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> oListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();
        public PMT01700LOO_UnitCharges_Charges_ChargesItemDTO? oEntityChargesItem;

        public ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO> loTempListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>();

        public PMT01700ControlYMD _oControlYMD = new PMT01700ControlYMD();

        public PMT01700ParameterChargesTab oParameterChargeTab = new PMT01700ParameterChargesTab();
        //  public PMT01700LOO_UnitUtilities_ParameterDTO oParameterGetItemCharges = new PMT01700LOO_UnitUtilities_ParameterDTO();
        #endregion

        public string _cCurrencyCode = "";
        public decimal NTotalItemDetil;
        public decimal _nTempFEE_AMT;
        //CR02
        public bool LTAXABLE = false;
        public bool EnabledFeeMethod = false;


        #region DataCBillingMode

        public List<PMT01700ComboBoxDTO> loRadioGroupDataCBILLING_MODE = new List<PMT01700ComboBoxDTO>
        {
            new PMT01700ComboBoxDTO { CCODE = "01", CDESCRIPTION = "DP" },
            new PMT01700ComboBoxDTO { CCODE = "02", CDESCRIPTION = "By Period" },
        };

        #endregion


        #region Charges
        public async Task GetFeeMethodList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetFeeMethodListAsync();
                if (loResult != null)
                {
                    oComboBoxFeeMethod = loResult.Data;
                    oComboBoxFeeMethod = new List<PMT01700ComboBoxDTO>(loResult.Data);

                    //var loItemDailyToRemove = oComboBoxPeriodMode.FirstOrDefault(item => item.CCODE == "03");
                    //if (loItemDailyToRemove != null)
                    //{
                    //    oComboBoxPeriodMode.Remove(loItemDailyToRemove);
                    //}
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPeriodModeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetPeriodModeListAsync();
                if (loResult != null)
                {
                    oComboBoxPeriodMode = loResult.Data;
                    oComboBoxPeriodMode = new List<PMT01700ComboBoxDTO>(loResult.Data);

                    //var loItemDailyToRemove = oComboBoxPeriodMode.FirstOrDefault(item => item.CCODE == "03");
                    //if (loItemDailyToRemove != null)
                    //{
                    //    oComboBoxPeriodMode.Remove(loItemDailyToRemove);
                    //}
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetChargesList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, oParameterChargeTab.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CDEPT_CODE, oParameterChargeTab.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CTRANS_CODE, oParameterChargeTab.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CREF_NO, oParameterChargeTab.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CCHARGE_MODE, oParameterChargeTab.CCHARGE_MODE);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CUNIT_ID, "");
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CFLOOR_ID, "");
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CBUILDING_ID, "");

                var loResult = await _model.GetChargesListAsync();
                if (loResult.Data.Any())
                {
                    foreach (var item in loResult.Data)
                    {
                        item.DSTART_DATE = ConvertStringToDateTimeFormat(item.CSTART_DATE!)!;
                        item.DEND_DATE = ConvertStringToDateTimeFormat(item.CEND_DATE!)!;
                    }
                }
                oListCharges = new ObservableCollection<PMT01700LOO_UnitCharges_ChargesListDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetEntityCharges(PMT01700LOO_UnitCharges_ChargesDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                if (poEntity != null)
                {
                    var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                    loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                    loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);
                    oEntityCharges = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceSaveCharges(PMT01700LOO_UnitCharges_ChargesDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                int liMax_ISEQ = 0;
                if (oListChargesItem.Count > 0)
                {
                    liMax_ISEQ = oListChargesItem.Max(x => x.ISEQ);
                }
                // Menggunakan LINQ untuk meningkatkan ISEQ jika nilainya 0
                oListChargesItem.Where(item => item.ISEQ == 0)
                                .ToList()
                                .ForEach(item => item.ISEQ = liMax_ISEQ += 1);

                switch (peCRUDMode)
                {
                    case eCRUDMode.NormalMode:
                        break;
                    case eCRUDMode.AddMode:
                        poNewEntity.CPROPERTY_ID = oParameterChargeTab.CPROPERTY_ID;
                        poNewEntity.CBUILDING_ID = oParameterChargeTab.CBUILDING_ID;
                        poNewEntity.CTRANS_CODE = oParameterChargeTab.CTRANS_CODE;
                        poNewEntity.CDEPT_CODE = oParameterChargeTab.CDEPT_CODE;
                        poNewEntity.CREF_NO = oParameterChargeTab.CREF_NO;
                        poNewEntity.CCHARGE_MODE = oParameterChargeTab.CCHARGE_MODE;
                        poNewEntity.CBUILDING_ID = oParameterChargeTab.CBUILDING_ID;
                        poNewEntity.CFLOOR_ID = oParameterChargeTab.CFLOOR_ID;
                        poNewEntity.CUNIT_ID = oParameterChargeTab.COTHER_UNIT_ID;

                        // Menggunakan LINQ untuk meningkatkan ISEQ jika nilainya 0
                        oListChargesItem.Where(item => item.ISEQ == 0)
                                        .ToList()
                                        .ForEach(item => item.ISEQ = liMax_ISEQ + 1);
                        poNewEntity.ChargeItemList = oListChargesItem.ToList();
                        break;
                    case eCRUDMode.EditMode:
                        poNewEntity.CPROPERTY_ID = oParameterChargeTab.CPROPERTY_ID;
                        poNewEntity.CBUILDING_ID = oParameterChargeTab.CBUILDING_ID;
                        poNewEntity.CTRANS_CODE = oParameterChargeTab.CTRANS_CODE;
                        poNewEntity.CDEPT_CODE = oParameterChargeTab.CDEPT_CODE;
                        poNewEntity.CREF_NO = oParameterChargeTab.CREF_NO;
                        poNewEntity.CCHARGE_MODE = oParameterChargeTab.CCHARGE_MODE;
                        poNewEntity.CBUILDING_ID = oParameterChargeTab.CBUILDING_ID;
                        poNewEntity.CFLOOR_ID = oParameterChargeTab.CFLOOR_ID;
                        poNewEntity.CUNIT_ID = oParameterChargeTab.COTHER_UNIT_ID;
                        poNewEntity.ChargeItemList = oListChargesItem.ToList();
                        break;
                    case eCRUDMode.DeleteMode:
                        break;
                    default:
                        break;
                }

                poNewEntity.CSTART_DATE = ConvertDateTimeToStringFormat(poNewEntity.DSTART_DATE);
                poNewEntity.CEND_DATE = ConvertDateTimeToStringFormat(poNewEntity.DEND_DATE);

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loResult.DSTART_DATE = ConvertStringToDateTimeFormat(loResult.CSTART_DATE);
                loResult.DEND_DATE = ConvertStringToDateTimeFormat(loResult.CEND_DATE);

                oEntityCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceDeleteCharges(PMT01700LOO_UnitCharges_ChargesDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CCHARGE_MODE = oParameterChargeTab.CCHARGE_MODE;
                poEntity.CBUILDING_ID = oParameterChargeTab.CBUILDING_ID;
                poEntity.CFLOOR_ID = oParameterChargeTab.CFLOOR_ID;
                poEntity.CUNIT_ID = oParameterChargeTab.COTHER_UNIT_ID;

                poEntity.CSTART_DATE = ConvertDateTimeToStringFormat(poEntity.DSTART_DATE);
                poEntity.CEND_DATE = ConvertDateTimeToStringFormat(poEntity.DEND_DATE);
                // Validation Before Delete
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region ChargesItem
        public async Task GetChargesItemList(PMT01700LOO_UnitUtilities_ParameterDTO oParameterGetItemCharges)
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CPROPERTY_ID, oParameterGetItemCharges.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CDEPT_CODE, oParameterGetItemCharges.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CTRANS_CODE, oParameterGetItemCharges.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CREF_NO, oParameterGetItemCharges.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01700ContextDTO.CSEQ_NO, oParameterGetItemCharges.CSEQ_NO);

                var loResult = await _model.GetDetailItemListAsync();
                oListChargesItem = new ObservableCollection<PMT01700LOO_UnitCharges_Charges_ChargesItemDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public void GetChargesItem(PMT01700LOO_UnitCharges_Charges_ChargesItemDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var temp = poEntity;
                oEntityChargesItem = poEntity;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region Utilities
        public DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
                    //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                    return null;
                }
            }
        }
        public string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }
        #endregion
    }
}
