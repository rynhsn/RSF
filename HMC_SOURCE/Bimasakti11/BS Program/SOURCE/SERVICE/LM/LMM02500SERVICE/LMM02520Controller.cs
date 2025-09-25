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
    public class LMM02520Controller : ControllerBase, ILMM02520
    {
        private LoggerLMM02500? _loggerLMM02520;

        public LMM02520Controller(ILogger<LMM02520Controller> logger)
        {
            LoggerLMM02500.R_InitializeLogger(logger);
            _loggerLMM02520 = LoggerLMM02500.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02520GridDTO> GetAllTenantGroupListStream()
        {
            string? lcMethod = nameof(GetAllTenantGroupListStream);
            _loggerLMM02520.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LMM02500DetailDbParameterDTO loDbPar;
            List<LMM02520GridDTO> loRtnTmp;
            LMM02520Cls loCls;
            IAsyncEnumerable<LMM02520GridDTO>? loReturn = null;

            try
            {
                _loggerLMM02520.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                loDbPar = new();
                _loggerLMM02520.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02520.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (loDbPar.CCOMPANY_ID != null)
                {
                    loDbPar.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                    loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantParameterLMM02500.CPROPERTY_ID);
                    loDbPar.CTENANT_GROUP_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantDetailLMM02500.CTENANT_GROUP_ID);
                }
                _loggerLMM02520.LogDebug("{@ObjectParameter}", loDbPar);

                _loggerLMM02520.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                loCls = new();
                _loggerLMM02520.LogDebug("{@ObjectLMM02500Cls}", loCls);

                _loggerLMM02520.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetAllTenantGroupStreamDb(loDbPar);
                _loggerLMM02520.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                _loggerLMM02520.LogInfo(string.Format("Convert Data into Stream in Method {0}", lcMethod));
                loReturn = GetAllTenantGroupListProcessStream(loRtnTmp);
                _loggerLMM02520.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _loggerLMM02520.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _loggerLMM02520.LogInfo(string.Format("End Method {0}", lcMethod));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private async IAsyncEnumerable<LMM02520GridDTO> GetAllTenantGroupListProcessStream(List<LMM02520GridDTO> poParameter)
         {

             foreach (LMM02520GridDTO item in poParameter)
             {
                 yield return item;
             }
            await Task.CompletedTask;
         }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02520DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02520DTO> poParameter)
        {
            string? lcMethod = nameof(R_ServiceGetRecord);
            _loggerLMM02520.LogInfo(string.Format("Start Method {0}", lcMethod));
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<LMM02520DTO>();
            var loCls = new LMM02520Cls();

            try
            {
                _loggerLMM02520.LogInfo(string.Format("Set the property of poParameter.Entity Value in method {0}", lcMethod));
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                if (poParameter.Entity.CCOMPANY_ID != null)
                    poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                _loggerLMM02520.LogDebug("{@ObjectParameter}", poParameter.Entity);

                _loggerLMM02520.LogInfo(string.Format("Call the R_GetRecord method of loCls with poParameter.Entity and assign the result to loRtn.data in method {0}", lcMethod));
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
                _loggerLMM02520.LogDebug("{@ObjectReturn}", loRtn.data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMM02520.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMM02520.LogInfo(string.Format("End Method {0}", lcMethod));
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM02520DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02520DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02520DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public SaveMoveTenantGroupResultDTO SaveMoveTenantGroupCategory(ObjectParameterLMM02500MoveTenantGroup poParameter)
        {
            string? lcMethod = nameof(SaveMoveTenantGroupCategory);
            _loggerLMM02520.LogInfo(string.Format("Start Method {0}", lcMethod));

            var loEx = new R_Exception();
            SaveMoveTenantGroupResultDTO loRtn = new SaveMoveTenantGroupResultDTO();
            SaveMoveTenantGroupParameterDbDTO loParam;
            try
            {
                _loggerLMM02520.LogInfo(string.Format("Initialize the loCls object as a new instance of LMM02510Cls in method {0}", lcMethod));
                LMM02520Cls loCls = new LMM02520Cls();
                _loggerLMM02520.LogDebug("{@ObjectLMM02520Cls}", loCls);

                _loggerLMM02520.LogInfo(string.Format("Set the property of poParameter value in method {0}", lcMethod));
#pragma warning disable CS8601 // Possible null reference assignment.
                loParam = new SaveMoveTenantGroupParameterDbDTO()
                {
                    CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLOGIN_USER_ID = R_BackGlobalVar.USER_ID,
                    CTENANT_ID = poParameter.CTENANT_ID,
                    CSELECTED_PROPERTY_ID = poParameter.CPROPERTY_ID,
                    CFROM_TENANT_CATEGORY_ID = poParameter.CFROM_TENANT_GROUP,
                    CTO_TENANT_CATEGORY_ID = poParameter.CTO_TENANT_GROUP
                };
#pragma warning restore CS8601 // Possible null reference assignment.
                _loggerLMM02520.LogDebug("{@ObjectParameter}", loParam);


                _loggerLMM02520.LogInfo(string.Format("Perform the Move Tenant operation using the SaveMoveTenantGroupCategoryDb method of LMM02520Cls in method {0}", lcMethod));
                loCls.SaveMoveTenantGroupCategoryDb(loParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.Haserror)
                _loggerLMM02520.LogError(loEx);

            loEx.ThrowExceptionIfErrors();

            _loggerLMM02520.LogInfo(string.Format("End Method {0}", lcMethod));
            return loRtn;
        }
    }
}
