using Lookup_GLBACK;
using Lookup_GLCOMMON;
using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;
using Lookup_GLCOMMON.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Globalization;

namespace Lookup_GLSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupGLController : ControllerBase, IPublicLookupGL
    {
        private LoggerLookupGL _loggerLookup;
        private readonly ActivitySource _activitySource;
        public PublicLookupGLController(ILogger<PublicLookupGLController> logger)
        {

            LoggerLookupGL.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupGL.R_GetInstanceLogger();
            _activitySource = LookupGLActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupGLController));
        }

        [HttpPost]
        public IAsyncEnumerable<GLL00100DTO> GLL00100ReferenceNoLookUp()
        {
            string lcMethodName = nameof(GLL00100ReferenceNoLookUp);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<GLL00100DTO> loRtn = null;
            List<GLL00100DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupGLCls();
                var poParameter = new GLL00100ParameterDTO();
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GLL00100ContextDTO.CTRANS_CODE);
                poParameter.CFROM_DEPT_CODE = R_Utility.R_GetStreamingContext<string>(GLL00100ContextDTO.CFROM_DEPT_CODE);
                poParameter.CTO_DEPT_CODE = R_Utility.R_GetStreamingContext<string>(GLL00100ContextDTO.CTO_DEPT_CODE);
                poParameter.CFROM_DATE = R_Utility.R_GetStreamingContext<string>(GLL00100ContextDTO.CFROM_DATE);
                poParameter.CTO_DATE = R_Utility.R_GetStreamingContext<string>(GLL00100ContextDTO.CTO_DATE);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GetAllAgreement");

                loReturnTemp = loCls.GLL00100ReferenceNoLookUpDb(poParameter);
                loRtn = GetStream(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        [HttpPost]
        public IAsyncEnumerable<GLL00110DTO> GLL00110ReferenceNoLookUpByPeriod()
        {
            string lcMethodName = nameof(GLL00100ReferenceNoLookUp);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            IAsyncEnumerable<GLL00110DTO> loRtn = null;
            List<GLL00110DTO> loReturnTemp;

            try
            {
                var loCls = new PublicLookupGLCls();
                var poParameter = new GLL00110ParameterDTO();
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GLL00110ContextDTO.CTRANS_CODE);
                poParameter.CFROM_DEPT_CODE = R_Utility.R_GetStreamingContext<string>(GLL00110ContextDTO.CFROM_DEPT_CODE);
                poParameter.CTO_DEPT_CODE = R_Utility.R_GetStreamingContext<string>(GLL00110ContextDTO.CTO_DEPT_CODE);
                poParameter.CFROM_DATE = R_Utility.R_GetStreamingContext<string>(GLL00110ContextDTO.CFROM_DATE);
                poParameter.CTO_DATE = R_Utility.R_GetStreamingContext<string>(GLL00110ContextDTO.CTO_DATE);
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _loggerLookup.LogInfo(string.Format("Get Parameter {0} on Controller", lcMethodName));
                _loggerLookup.LogDebug("DbParameter {@Parameter} ", poParameter);
                _loggerLookup.LogInfo("Call method GLL00110ReferenceNoLookUpByPeriod");

                loReturnTemp = loCls.GLL00110ReferenceNoLookUpByPeriodDb(poParameter);
                loRtn = GetStream(loReturnTemp);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loRtn!;
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                if (item is GLL00110DTO || item is GLL00100DTO)
                {
                    // Dapatkan semua properti dari item
                    var properties = item.GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        // Cari properti yang berawalan 'D' dan berakhiran 'Date'
                        if (property.Name.StartsWith("D") && property.Name.EndsWith("Date") && property.PropertyType == typeof(DateTime?))
                        {
                            // Dapatkan nama properti 'C' yang sesuai
                            var correspondingProperty = properties.FirstOrDefault(p => p.Name == "C" + property.Name.Substring(1) && p.PropertyType == typeof(string));

                            if (correspondingProperty != null)
                            {
                                // Dapatkan nilai properti 'C'
                                var stringValue = correspondingProperty.GetValue(item) as string;

                                if (!string.IsNullOrEmpty(stringValue))
                                {
                                    // Konversi nilai string ke DateTime?
                                    var dateValue = ConvertStringToDateTimeFormat(stringValue);

                                    // Set nilai properti 'D'
                                    property.SetValue(item, dateValue);
                                }
                            }
                        }
                    }
                }

                yield return item;
            }
        }


        private static DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                //return DateTime.MinValue; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
                return null;
            }
            else
            {
                DateTime result;

                // Jika string hanya memiliki 6 karakter, tambahkan "01" untuk merepresentasikan tanggalnya
                if (pcEntity.Length == 6)
                {
                    pcEntity += "01";
                }

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
        
        #endregion

    }
}
