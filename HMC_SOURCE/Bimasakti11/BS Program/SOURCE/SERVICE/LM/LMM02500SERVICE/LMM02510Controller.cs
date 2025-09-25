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
using System.Reflection;

namespace LMM02500Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LMM02510Controller : ControllerBase, ILMM02510
    {
        private LoggerLMM02500? _loggerLMM02510;

        public LMM02510Controller(ILogger<LMM02510Controller> logger)
        {
            LoggerLMM02500.R_InitializeLogger(logger);
            _loggerLMM02510 = LoggerLMM02500.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02500ProfileAndTaxInfoDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02500ProfileAndTaxInfoDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerLMM02510.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM02500ProfileAndTaxInfoDTO>();

            try
            {
                _loggerLMM02510.LogInfo(string.Format("Initialize the loCls object as a new instance of LMM02510Cls in method {0}", lcMethod));
                var loCls = new LMM02510Cls();
                _loggerLMM02510.LogDebug("{@LMM02510Cls}", loCls);


                _loggerLMM02510.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.Profile.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                if (poParameter.Entity.Profile.CCOMPANY_ID != "")
                    poParameter.Entity.Profile.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM02510.LogDebug("{@ObjectParameter}", poParameter.Entity);


                _loggerLMM02510.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerLMM02510.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMM02510.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMM02510.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM02500ProfileAndTaxInfoDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02500ProfileAndTaxInfoDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceSave);
            _loggerLMM02510.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM02500ProfileAndTaxInfoDTO> loReturn = new();
            try
            {
                _loggerLMM02510.LogInfo(string.Format("Initialize the loCls object as a new instance of LMM02510Cls in method {0}", lcMethod));
                LMM02510Cls loCls = new ();
                _loggerLMM02510.LogDebug("{@ObjectLMM02510Cls}", loCls);

                _loggerLMM02510.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.Profile.CCOMPANY_ID= R_BackGlobalVar.COMPANY_ID;
                if(poParameter.Entity.Profile.CCOMPANY_ID != null )
                    poParameter.Entity.Profile.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM02510.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerLMM02510.LogInfo(string.Format("Checking Data From Profile, and edit if Profile has empty string or null in method {0}", nameof(R_ServiceSave)));
                ProfileCheckerValidation(poParameter.Entity.Profile);

                _loggerLMM02510.LogInfo(string.Format("Initialize the loReturn object with a new instance and set its data property using loCls.R_Save in method {0}", nameof(R_ServiceSave)));
                loReturn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
                _loggerLMM02510.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02510.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02510.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02500ProfileAndTaxInfoDTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceDelete);
            _loggerLMM02510.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loReturn = new();
            try
            {
                _loggerLMM02510.LogInfo(string.Format("Set the property of poParameter.Entity value in method {0}", lcMethod));
                poParameter.Entity.Profile.CCOMPANY_ID= R_BackGlobalVar.COMPANY_ID;
                if(poParameter.Entity.Profile.CCOMPANY_ID != null )
                    poParameter.Entity.Profile.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM02510.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerLMM02510.LogInfo(string.Format("Initialize the loCls object as a new instance of LMM02510Cls in method {0}", lcMethod));
                LMM02510Cls loCls = new ();
                _loggerLMM02510.LogDebug("{@ObjectLMM02510Cls}", loCls);

                _loggerLMM02510.LogInfo(string.Format("Perform the delete operation using the R_Delete method of LMM02510Cls in method {0}", lcMethod));
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02510.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02510.LogInfo(string.Format("End Method {0}", lcMethod));
            return loReturn;
        }

        private void ProfileCheckerValidation(LMM02500ProfileDTO poProfile)
        {
            string? lcMethod = nameof(ProfileCheckerValidation);
            _loggerLMM02510.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loException = new R_Exception();
            PropertyInfo[]? properties = null;

            try
            {
                _loggerLMM02510.LogInfo(string.Format("Retrieve an array of PropertyInfo objects for the properties of the poProfile object's type in method {0}", lcMethod));
                properties = poProfile.GetType().GetProperties();

                _loggerLMM02510.LogInfo(string.Format("Iterate through the properties in method {0}", lcMethod));
                foreach (var prop in properties)
                {
                    _loggerLMM02510.LogInfo(string.Format("Check if the property type is a string in method {0}", lcMethod));
                    if (prop.PropertyType == typeof(string))
                    {
                        _loggerLMM02510.LogInfo(string.Format("Get the current value of the property in method {0}", lcMethod));
                        var value = (string?)prop.GetValue(poProfile);
                        _loggerLMM02510.LogInfo(string.Format("Check if the value is null in method {0}", lcMethod));
                        if (value == null)
                        {
                            _loggerLMM02510.LogInfo(string.Format("Set the property's value to an empty string in method {0}", lcMethod));
                            prop.SetValue(poProfile, "");
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }
            if (loException.Haserror)
                _loggerLMM02510.LogError(loException);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02510.LogInfo(string.Format("End Method {0}", lcMethod));

        }

    }
}
