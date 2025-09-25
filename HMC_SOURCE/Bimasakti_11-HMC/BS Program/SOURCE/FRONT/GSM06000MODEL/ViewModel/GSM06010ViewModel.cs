using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GSM06000Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM06000Model.ViewModel
{
    public class GSM06010ViewModel : R_ViewModel<GSM06010DTO>
    {
        private readonly GSM06000Model _mainModel = new GSM06000Model();
        private readonly GSM06010Model _model = new GSM06010Model();
        public int iCounterSample = 0;
        

        public GSM06010DTO loEntity = new GSM06010DTO();
        public GSM06020ParameterDTO loParamGSM06020Parameter = new GSM06020ParameterDTO();
        public ObservableCollection<GSM06010GridDTO>? loGridList = new ObservableCollection<GSM06010GridDTO>();

        public int PropertyTempSequence01;
        public int PropertyTempSequence02;
        public string? PropertyTypeContext = "";

        public List<GSM06000CodeDTO> loTypeModeList { get; set; } = new List<GSM06000CodeDTO>();

        public GSM06010ParameterDTO loParameterGSM06010 { get; set; } = new GSM06010ParameterDTO();

        public List<ComboBoxPeriodMode> OptionCYearFormatModes { get; set; } = new List<ComboBoxPeriodMode>
        {
            new ComboBoxPeriodMode { Id = "2", Description = "YYYY" },
            new ComboBoxPeriodMode { Id = "1", Description = "YY" }
        };

        public List<GSM06010DelimiterInfoDTO> OptionTypeCPeriodMode { get; set; } = new List<GSM06010DelimiterInfoDTO>
        {
            new GSM06010DelimiterInfoDTO { CCODE = " ", CDESCRIPTION = "None" },
            new GSM06010DelimiterInfoDTO { CCODE = "-", CDESCRIPTION = "-" },
            new GSM06010DelimiterInfoDTO { CCODE = ".", CDESCRIPTION = "." },
            new GSM06010DelimiterInfoDTO { CCODE = "/", CDESCRIPTION = "/" }
        };

        public List<ComboBoxPeriodMode> OptionPeriodModes { get; set; } = new List<ComboBoxPeriodMode>
        {
            new ComboBoxPeriodMode { Id = "N", Description = "None" },
            new ComboBoxPeriodMode { Id = "P", Description = "Periodically" },
            new ComboBoxPeriodMode { Id = "Y", Description = "Yearly" }
        };


        public async Task GetAllCashBankList()
        {
            var loEx = new R_Exception();

            try
            {
                if (loParameterGSM06010.CCB_CODE != null)
                {
                    var loResult = await _model.GetAllCashBankInfoStreamAsync(loParameterGSM06010.CCB_CODE);

                    if (loResult.Data != null) loGridList = new ObservableCollection<GSM06010GridDTO>(loResult.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public void AfterAddValidationGSM06010(GSM06010DTO poParam)
        {
            poParam.CPERIOD_MODE = OptionPeriodModes.FirstOrDefault()?.Id;
            poParam.CPERIOD_DELIMITER = OptionTypeCPeriodMode.FirstOrDefault()?.CCODE;
            PropertyTempSequence01 = 1;
            poParam.CYEAR_FORMAT = OptionCYearFormatModes.FirstOrDefault()?.Id;
            poParam.INUMBER_LENGTH = 2;
            poParam.CNUMBER_DELIMITER = OptionTypeCPeriodMode.FirstOrDefault()?.CCODE;
            PropertyTempSequence02 = 1;
            poParam.CPREFIX = "";
            poParam.CPREFIX_DELIMITER = OptionTypeCPeriodMode.FirstOrDefault()?.CCODE;
            poParam.CSUFFIX = "";
        }

        public async Task GetTypeCPeriodModeList()
        {
            var loEx = new R_Exception();

            try
            {
                GSM06010ListDTO<GSM06010DelimiterInfoDTO>? loResult = await _model.GetDelimiterInfoCashBankInfoStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTypeCNumberDelimiterList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetDelimiterInfoCashBankInfoStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetParameterInfo(GSM06000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                loParameterGSM06010.CCB_CODE = poParam.CCB_CODE;
                loParameterGSM06010.CCB_NAME = poParam.CCB_NAME;
                PropertyTypeContext = poParam.CCB_TYPE;
                //var loResult = await _model.GetParameterCashBankInfoAsync(poCCB_CODE);
                //PropertyTypeContext = loResult.CCB_TYPE;
                //loParameterGSM06010 = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _mainModel.GetCashTypeAsync();
                if (loResult.Type != null) loTypeModeList = loResult.Type;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(GSM06010DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                PropertyTempSequence01 = loResult.CSEQUENCE01 == "01" ? 1 : 2;
                PropertyTempSequence02 = loResult.CSEQUENCE02 == "01" ? 1 : 2;

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveCashBank(GSM06010DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.AddMode == peCRUDMode) poNewEntity.CBANK_TYPE = PropertyTypeContext;

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteCashBank(GSM06010DTO poEntity)
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

        public void ConvertParamGSM06000DocNumbering(GSM06010DTO poData)
        {
            R_Exception loException = new R_Exception();

            try
            {
                loParamGSM06020Parameter.CCB_NAME = loParameterGSM06010.CCB_NAME;
                loParamGSM06020Parameter.CCB_CODE = loParameterGSM06010.CCB_CODE;
                loParamGSM06020Parameter.CCB_ACCOUNT_NO = poData.CCB_ACCOUNT_NO;
                loParamGSM06020Parameter.CPERIOD_MODE = poData.CPERIOD_MODE;
                ComboBoxPeriodMode selectedPeriodMode = OptionPeriodModes.FirstOrDefault(mode => mode.Id == poData.CPERIOD_MODE);
                if (selectedPeriodMode != null)
                    loParamGSM06020Parameter.CPERIOD_MODE_DESCR = selectedPeriodMode.Description;
                loParamGSM06020Parameter.CYEAR_FORMAT = poData.CYEAR_FORMAT;

            }
            catch (Exception ex)
            {
                loException.Add(ex);    
            }
            loException.ThrowExceptionIfErrors();
        }

        #region REF NO / Sample Code

        public string? lcREFNO;
        public string? lcREFNO_LENGTH;

        public void getRefNo()
        {
            var loEx = new R_Exception();

            try
            {
                string? loResult;
                if (PropertyTempSequence01 == null)
                {
                    loResult = string.Concat(
                    Data.CPREFIX.Trim()
                    + Data.CPREFIX_DELIMITER
                    + (
                        PropertyTempSequence01 == 1 ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        PropertyTempSequence01 == 2 ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    )
                    + ("")
                    + (
                        PropertyTempSequence02 == 1 ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        PropertyTempSequence02 == 2 ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    )
                    + (PropertyTempSequence02 == null ? "" :
                        (PropertyTempSequence02 == 1 ? Data.CPERIOD_DELIMITER :
                            (PropertyTempSequence02 == 2 ? Data.CNUMBER_DELIMITER : "")))
                    + Data.CSUFFIX.Trim()
                );
                }
                else
                {
                    loResult = string.Concat(
                    Data.CPREFIX.Trim()
                    + Data.CPREFIX_DELIMITER
                    + (
                        PropertyTempSequence01 == 1 ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        PropertyTempSequence01 == 2 ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    )
                    + (                        (PropertyTempSequence01 == 1 ? Data.CNUMBER_DELIMITER :
                            (PropertyTempSequence01 == 2 ? Data.CNUMBER_DELIMITER : "")))
                    + (
                        PropertyTempSequence02 == 1 ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        PropertyTempSequence02 == 2 ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    )
                    + (PropertyTempSequence02 == null ? "" :
                        (PropertyTempSequence02 == 1 ? Data.CPERIOD_DELIMITER :
                            (PropertyTempSequence02 == 2 ? Data.CNUMBER_DELIMITER : "")))
                    + Data.CSUFFIX.Trim()
                );
                }

                lcREFNO = loResult;
                lcREFNO_LENGTH = lcREFNO.Length.ToString();

                if (lcREFNO.Length > 30)
                {
                    loEx.Add("", "Reference Number length cannot exceed 30 characters");
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #endregion
    }

    public class ComboBoxPeriodMode
    {
        public string? Id { get; set; }
        public string? Description { get; set; }
    }
}