using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMA00400HandoverStorageBack;
using R_Common;
using System.Diagnostics;
using R_MultiTenantDb;
using PMA00400Logger;

namespace PMA00400Services
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class PMA00400Controller : ControllerBase
    {
        private readonly ActivitySource _activitySource;
        private APILogger _logger;

        public PMA00400Controller(ILogger<PMA00400Controller> logger)
        {
            APILogger.R_InitializeLogger(logger);
            _logger = APILogger.R_GetInstanceLogger();
            _activitySource = PMA00400Activity.R_InitializeAndGetActivitySource(nameof(PMA00400Controller));
        }

        [HttpGet]
        public async Task<FileStreamResult> GetFileHandover([FromQuery] string tenantId, [FromQuery] string guid)
        {
            using Activity activity = _activitySource.StartActivity("GetFileHandover");
            string lcMethodName = nameof(GetFileHandover);
            //_logger!.Info("Get Data HANDOVER_IMAGES_CHECKLIST");
            _logger.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));
            //string loReturn = null;
            MemoryStream stream = new MemoryStream();
            string filename = "";
            var loEx = new R_Exception();

            string lcConnectionName = "";

            try
            {
                if (R_MultiTenantDbRepository.IsMultiTenantDb)
                {
                    lcConnectionName = $"{tenantId}_CONNECTIONSTRING";
                }

                PMA00400HandoverStorageCls loCls = new PMA00400HandoverStorageCls();
                _logger.LogInfo("Call method GetHandoverPDFURL");
                var loReturn = await loCls.GetHandoverStorage(lcConnectionName, guid );

                string lcFileExtension = loReturn.FileExtension.StartsWith('.') ?
                    loReturn.FileExtension : ("." + loReturn.FileExtension);

                filename = loReturn.FileName + lcFileExtension;
                stream = new MemoryStream(loReturn.Data);


            }
            catch (Exception ex)
            {
                _logger.LogInfo(string.Format("Error {0}", ex));
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = filename
            };

        }
        [HttpGet]
        public List<string> GetConnectionNames()
        {
            using Activity activity = _activitySource.StartActivity("GetConnectionNames");
            List<string> loReturnNames = R_MultiTenantDbRepository.R_GetConnectionStrings().Select(x => x.Name).ToList();
            return loReturnNames;
        }

    }
}