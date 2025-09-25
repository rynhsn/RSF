using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GSM06000Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM06000Model.ViewModel
{
    public class GSM06020ViewModel : R_ViewModel<GSM06020DTO>
    {
        private readonly GSM06020Model _model = new GSM06020Model();
        public GSM06020DTO loEntity = new GSM06020DTO();
        public ObservableCollection<GSM06020DTO> loGridList = new ObservableCollection<GSM06020DTO>();
        public GSM06020ParameterDTO loParameterGSM06020 = new GSM06020ParameterDTO();
        
        //for checker
        public string? lcParameterCYearFormat = "";
        public string? lcParameterCPeriodMode = "";


        public async Task GetAllCashBankDocList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GetAllCashBankDocNumberingStreamAsync(pcCCB_CODE: loParameterGSM06020.CCB_CODE, pcCCB_ACCOUNT_NO: loParameterGSM06020.CCB_ACCOUNT_NO, pcCPERIOD_MODE: loParameterGSM06020.CPERIOD_MODE);

                loGridList = new ObservableCollection<GSM06020DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private string GenerateCCYearWithPERIODNO(GSM06020DTO poEntity)
        {
            string loResult;

            if (loParameterGSM06020.CPERIOD_MODE == "P")
                loResult = poEntity.CCYEAR + "-" + poEntity.CPERIOD_NO;
            else
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loResult = poEntity.CCYEAR;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private int GetLastNo()
        {
            int loReturn = 0;

            if (loGridList.Count == 0)
            {
                loReturn = -1;
                goto Exit;
            }
            
            foreach (var item in loGridList)
            {
                if (!string.IsNullOrEmpty(item.CPERIOD_NO) && loReturn < 13)
                   loReturn = int.Parse(item.CPERIOD_NO);
            }
            
            Exit:
            return loReturn;

        }

        private string CheckerPeriodLastNo(GSM06020DTO poEntity)
        {
            int ICheckerLastNo = GetLastNo();
            
            if (ICheckerLastNo != -1)
            {
                int IPeriod_No = ICheckerLastNo;
                IPeriod_No++;
                poEntity.CPERIOD_NO = IPeriod_No.ToString("D2");
            }
            else
            {
                poEntity.CPERIOD_NO = "00";
            }

            return poEntity.CPERIOD_NO;
        }
        
        
        
        public async Task GetParameterInfo()
        {
            var loEx = new R_Exception();

            try
            {

                var loResult =
                    await _model.GetParameterCashBankDocNumberingAsync(pcCCB_CODE: loParameterGSM06020.CCB_CODE, pcCCB_ACCOUNT_NO:  loParameterGSM06020.CCB_ACCOUNT_NO);
                var TemploTempParameter = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetEntity(GSM06020DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantGSM06020.CPERIOD_MODE, loParameterGSM06020.CPERIOD_MODE);
                var loResult = await _model.R_ServiceGetRecordAsync(poEntity);
                loResult.CCB_CODE = loParameterGSM06020.CCB_CODE;
                loResult.CCB_ACCOUNT_NO = loParameterGSM06020.CCB_ACCOUNT_NO;

                
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        
        public void GeneratePeriod(GSM06020DTO poParam)
        {
            
            var lcYear = DateTime.Now;
            var lcUser = poParam.CUSER_LOGIN_ID;
            lcParameterCPeriodMode = loParameterGSM06020.CPERIOD_MODE;

            poParam.DCREATE_DATE = lcYear;
            poParam.DUPDATE_DATE = lcYear;

            poParam.CCREATE_BY = lcUser;
            poParam.CUPDATE_BY = lcUser;
            
            poParam.ISTART_NUMBER = 1;
            poParam.ILAST_NUMBER = 0;

            if (lcParameterCYearFormat == "1")
                poParam.CCYEAR = lcYear.ToString("yy");
            else
                poParam.CCYEAR = lcYear.ToString("yyyy");

            if (loParameterGSM06020.CPERIOD_MODE == "Y")
                poParam.CPERIOD_NO = "";
            else
                poParam.CPERIOD_NO = CheckerPeriodLastNo(poParam);

            poParam.CCYEARWITHCPERIOD_NO = GenerateCCYearWithPERIODNO(poParam);
        }

        public async Task SaveCashBankDoc(GSM06020DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (eCRUDMode.NormalMode == peCRUDMode)
                {
                }

                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                loResult.CCB_CODE = loParameterGSM06020.CCB_CODE;
                loResult.CCB_ACCOUNT_NO = loParameterGSM06020.CCB_ACCOUNT_NO;
                loEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteCashBankDoc(GSM06020DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                poEntity.CCB_CODE = loParameterGSM06020.CCB_CODE;
                poEntity.CCB_ACCOUNT_NO = loParameterGSM06020.CCB_ACCOUNT_NO;
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}