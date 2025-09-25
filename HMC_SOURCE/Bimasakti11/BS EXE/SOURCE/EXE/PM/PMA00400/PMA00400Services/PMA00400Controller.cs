using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMA00400HandoverStorageBack;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using R_MultiTenantDb;
using R_MultiTenantDb.Abstract;

namespace PMA00400Services
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class PMA00400Controller : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PMA00400Controller));

        [HttpGet]
        public FileStreamResult GetFileHandover([FromQuery] string tenantId, [FromQuery] string guid)
        {
            string lcMethodName = nameof(GetFileHandover);
            //_logger!.Info("Get Data HANDOVER_IMAGES_CHECKLIST");
            _logger.Info(string.Format("START process method {0} on Controller", lcMethodName));
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
                _logger.Info("Call method GetHandoverPDFURL");
                var loReturn = loCls.GetHandoverStorage(lcConnectionName, guid );

                string lcFileExtension = loReturn.FileExtension.StartsWith('.') ?
                    loReturn.FileExtension : ("." + loReturn.FileExtension);

                filename = loReturn.FileName + lcFileExtension;
                stream = new MemoryStream(loReturn.Data);


            }
            catch (Exception ex)
            {
                _logger.Info(string.Format("Error {0}", ex));
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.Info(string.Format("END process method {0} on Controller", lcMethodName));

            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = filename
            };

        }
        [HttpGet]
        public List<string> GetConnectionNames()
        {
            List<string> loReturnNames = R_MultiTenantDbRepository.R_GetConnectionStrings().Select(x => x.Name).ToList();
            return loReturnNames;
        }

    }
}