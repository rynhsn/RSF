using LMM02500Back;
using LMM02500Back.DTO;
using LMM02500Common;
using LMM02500Common.DTO;
using LMM02500Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM02500Controller : ControllerBase, ILMM02500
    {
        private LoggerLMM02500? _loggerLMM02500;

        public LMM02500Controller(ILogger<LMM02500Controller> logger)
        {
            LoggerLMM02500.R_InitializeLogger(logger);
            _loggerLMM02500 = LoggerLMM02500.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500ProfileDTO> GetAllTenantGroupStream()
        {
            string? lcMethod = nameof(GetAllTenantGroupStream);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));

            R_Exception loException = new R_Exception();
            LMM02500ListDbParameterDTO loDbPar;
            List<LMM02500ProfileDTO> loRtnTmp;
            LMM02500Cls loCls;
            IAsyncEnumerable<LMM02500ProfileDTO>? loReturn = null;

            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar = new();
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (loDbPar.CCOMPANY_ID != null)
                {
                    loDbPar.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                    loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantParameterLMM02500.CPROPERTY_ID);
                }
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                //Use Context!

                _loggerLMM02500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMM02500.LogDebug("{@ObjectLMM02500Cls}", loCls);

                _loggerLMM02500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.LMM02500ListDb(loDbPar);
                _loggerLMM02500.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerLMM02500.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = GetAllTenantGroupProcessStream(loRtnTmp);
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private async IAsyncEnumerable<LMM02500ProfileDTO> GetAllTenantGroupProcessStream(List<LMM02500ProfileDTO> poParameter)
        {

            foreach (LMM02500ProfileDTO item in poParameter)
            {
                yield return item;
            }
            await Task.CompletedTask;
        }

        [HttpPost]
        public LMM02500ListDTO<LMM02500ParameterDTO> GetParameterTenantGroup()
        {
            string? lcMethod = nameof(GetParameterTenantGroup);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500GetPropertyDbParameterDTO loDbPar;
            LMM02500Cls loCls;
            LMM02500ListDTO<LMM02500ParameterDTO>? loReturn = null;
            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar = new();
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (loDbPar.CCOMPANY_ID != null)
                {
                    loDbPar.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMM02500.LogDebug("{@ObjectLMM02500Cls}", loCls);

                _loggerLMM02500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = new LMM02500ListDTO<LMM02500ParameterDTO>() { Data = loCls.LMM02500GetPropertyDb(loDbPar) };
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public LMM02500TaxInfoDTO GetTaxInfoTenantGroup()
        {
            string? lcMethod = nameof(GetTaxInfoTenantGroup);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500DetailDbParameterDTO loDbPar;
            LMM02500Cls loCls;
            LMM02500TaxInfoDTO? loReturn = null;
            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar = new();
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (loDbPar.CCOMPANY_ID != null)
                {
                    loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantDetailLMM02500.CPROPERTY_ID);
                    loDbPar.CTENANT_GROUP_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantDetailLMM02500.CTENANT_GROUP_ID);
                    loDbPar.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                }
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMM02500.LogDebug("{@ObjectLMM02500Cls}", loCls);

                _loggerLMM02500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = loCls.GetTaxInfoTenantGroupDb(loDbPar);
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));


#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02500ProfileDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02500ProfileDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM02500ProfileDTO>();

            try
            {
                _loggerLMM02500.LogInfo(string.Format("Initialize the loCls object as a new instance of LMM02500Cls in method {0}", lcMethod));
                var loCls = new LMM02500Cls();
                _loggerLMM02500.LogDebug("{@LMM02500Cls}", loCls);

                _loggerLMM02500.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM02500.LogDebug("{@ObjectParameter}", poParameter.Entity);


                _loggerLMM02500.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMM02500.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM02500ProfileDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02500ProfileDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02500ProfileDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public LMM02500ListDTO<LMM02500ComboBoxType> GetTaxTypeComboBox()
        {
            string? lcMethod = nameof(GetTaxTypeComboBox);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500ComboBoxDbParameterDTO loDbPar;
            LMM02500Cls loCls;
            LMM02500ListDTO<LMM02500ComboBoxType>? loReturn = null;
            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar = new();
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (loDbPar.CCOMPANY_ID != null)
                    loDbPar.CULTURE_ID = R_BackGlobalVar.CULTURE;
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMM02500.LogDebug("{@ObjectLMM02500Cls}", loCls);

                _loggerLMM02500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = new LMM02500ListDTO<LMM02500ComboBoxType>() { Data = loCls.GetTaxTypeComboBoxDb(loDbPar) };
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public LMM02500ListDTO<LMM02500ComboBoxType> GetIdTypeComboBox()
        {
            string? lcMethod = nameof(GetIdTypeComboBox);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500Cls loCls;
            LMM02500ListDTO<LMM02500ComboBoxType>? loReturn = null;
            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                LMM02500ComboBoxDbParameterDTO loDbPar = new();
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (loDbPar.CCOMPANY_ID != null)
                    loDbPar.CULTURE_ID = R_BackGlobalVar.CULTURE;
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMM02500.LogDebug("{@ObjectLMM02500Cls}", loCls);

                _loggerLMM02500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = new LMM02500ListDTO<LMM02500ComboBoxType>() { Data = loCls.GetIdTypeComboBoxDb(loDbPar) };
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        [HttpPost]
        public LMM02500ListDTO<LMM02500ComboBoxType> GetTaxCodeComboBox()
        {
            string? lcMethod = nameof(GetTaxCodeComboBox);
            _loggerLMM02500.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500ComboBoxDbParameterDTO loDbPar;
            LMM02500Cls loCls;
            LMM02500ListDTO<LMM02500ComboBoxType>? loReturn = null;
            try
            {
                _loggerLMM02500.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar = new();
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02500.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (loDbPar.CCOMPANY_ID != null)
                    loDbPar.CULTURE_ID = R_BackGlobalVar.CULTURE;
                _loggerLMM02500.LogDebug("{@ObjectParameter}", loDbPar);

                //Use Context!
                _loggerLMM02500.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMM02500.LogDebug("{@ObjectLMM02500Cls}", loCls);

                _loggerLMM02500.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loReturn = new LMM02500ListDTO<LMM02500ComboBoxType>() { Data = loCls.GetTaxCodeComboBoxDb(loDbPar) };
                _loggerLMM02500.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02500.LogError("{@ErrorObject}", loException.Message);

            _loggerLMM02500.LogInfo(string.Format("End Method {0}", lcMethod));

            loException.ThrowExceptionIfErrors();
#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }



    }
}
