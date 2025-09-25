using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM10000BACK;
using PMM10000COMMON.Interface;
using PMM10000COMMON.Logs;
using PMM10000COMMON.Upload;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM10000TemplateController : ControllerBase, IPMM10000Template
    {
        private LoggerPMM10000 _logger;
        private readonly ActivitySource _activitySource;

        public PMM10000TemplateController(ILogger<PMM10000TemplateController> logger)
        {
            LoggerPMM10000.R_InitializeLogger(logger);
            _logger = LoggerPMM10000.R_GetInstanceLogger();
            _activitySource = PMM10000Activity.R_InitializeAndGetActivitySource(nameof(PMM10000TemplateController));
        }

        [HttpPost]
        public PMM10000TemplateDTO DownloadTemplateFile()
        {
            string lcMethodName = nameof(DownloadTemplateFile);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loException = new R_Exception();
            var loRtn = new PMM10000TemplateDTO();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_PM_API");
                var lcResourceFile = "BIMASAKTI_PM_API.Template.SLA Call Type.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }
    }
}
