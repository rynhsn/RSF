using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMT01500Common.DTO._6._Deposit;
using PMT01500Common.Utilities;
using PMT01500Common.Utilities.Front;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT01500Model.ViewModel
{
    public class PMT01500DepositViewModel : R_ViewModel<PMT01500FrontDepositDetailDTO>
    {
        #region From Back
        private readonly PMT01500DepositModel _modelPMT01500DepositModel = new PMT01500DepositModel();
        public ObservableCollection<PMT01500DepositListDTO> loListPMT01500Deposit = new ObservableCollection<PMT01500DepositListDTO>();
        public PMT01500FrontDepositDetailDTO loEntityDeposit = new PMT01500FrontDepositDetailDTO();
        public PMT01500DepositHeaderDTO loEntityDepositHeader = new PMT01500DepositHeaderDTO();
        public PMT01500GetHeaderParameterDTO loParameterList = new PMT01500GetHeaderParameterDTO();
        #endregion

        #region For Front
        public PMT01500TempCCONTRACTOR_IDandNAMEDTO? _oTempCCONTRACTOR_ID = new PMT01500TempCCONTRACTOR_IDandNAMEDTO();
        public bool _lUsingContractor_ID = true;
        #endregion

        #region Deposit
        public async Task GetDepositHeader()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CPROPERTY_ID))
                {
                    var loResult = await _modelPMT01500DepositModel.GetDepositHeaderAsync(poParameter: loParameterList);

                    loResult.CUNIT_ID = loParameterList.CCHARGE_MODE == "02" ? loParameterList.CUNIT_ID : "";
                    loResult.CUNIT_NAME = loParameterList.CCHARGE_MODE == "02" ? loParameterList.CUNIT_NAME : "";
                    loEntityDepositHeader = loResult;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDepositList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(loParameterList.CREF_NO))
                {
                    var loResult = await _modelPMT01500DepositModel.GetDepositListAsync(poParameter: loParameterList);
                    if (loResult.Any())
                    {
                        foreach (var item in loResult)
                        {
                            item.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(item.CDEPOSIT_DATE)!;
                        }
                    }
                    loListPMT01500Deposit = new ObservableCollection<PMT01500DepositListDTO>(loResult);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(PMT01500FrontDepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _modelPMT01500DepositModel.R_ServiceGetRecordAsync(ConvertToEntityBack(poEntity));

                loEntityDeposit = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceSave(PMT01500FrontDepositDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // set Add PropertyId and Charges Type
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CPROPERTY_ID = loParameterList.CPROPERTY_ID;
                    poNewEntity.CDEPT_CODE = loParameterList.CDEPT_CODE;
                    poNewEntity.CREF_NO = loParameterList.CREF_NO;
                    poNewEntity.CTRANS_CODE = loParameterList.CTRANS_CODE;
                }

                poNewEntity.CCHARGE_MODE = loParameterList.CCHARGE_MODE;
                poNewEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                poNewEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                poNewEntity.CUNIT_ID = loParameterList.CUNIT_ID;


                var loResult = await _modelPMT01500DepositModel.R_ServiceSaveAsync(ConvertToEntityBack(poNewEntity), peCRUDMode);

                loEntityDeposit = ConvertToEntityFront(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(PMT01500FrontDepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                // Validation Before Delete
                poEntity.CCHARGE_MODE = loParameterList.CCHARGE_MODE;
                poEntity.CBUILDING_ID = loParameterList.CBUILDING_ID;
                poEntity.CFLOOR_ID = loParameterList.CFLOOR_ID;
                poEntity.CUNIT_ID = loParameterList.CUNIT_ID;

                await _modelPMT01500DepositModel.R_ServiceDeleteAsync(ConvertToEntityBack(poEntity));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Utilities

        private PMT01500FrontDepositDetailDTO ConvertToEntityFront(PMT01500DepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT01500FrontDepositDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500FrontDepositDetailDTO>(poEntity);
                    loReturn.DDEPOSIT_DATE = ConvertStringToDateTimeFormat(poEntity.CDEPOSIT_DATE);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn!;
        }

        private PMT01500DepositDetailDTO ConvertToEntityBack(PMT01500FrontDepositDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT01500DepositDetailDTO? loReturn = null;

            try
            {
                if (poEntity != null)
                {
                    loReturn = R_FrontUtility.ConvertObjectToObject<PMT01500DepositDetailDTO>(poEntity);
                    loReturn.CDEPOSIT_DATE = ConvertDateTimeToStringFormat(poEntity.DDEPOSIT_DATE);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loReturn!;
        }

        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
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

        private string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return "";
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