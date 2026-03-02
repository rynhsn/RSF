using BaseHeaderReportCOMMON;
using FAT00700Back;
using FAT00700Common.DTOs;
using FAT00700Common.Print;
using FAT00700Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace FAT00700Service
{
    public class FAT00700ReportController : R_ReportControllerBase
    {
        private LoggerFAT00700 _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private FAT00700ReportParam _Parameter;
        private readonly ActivitySource _activitySource;

        public FAT00700ReportController(ILogger<FAT00700ReportController> logger)
        {
            LoggerFAT00700.R_InitializeLogger(logger);
            _logger = LoggerFAT00700.R_GetInstanceLogger();
            _activitySource = FAT00700Activity.R_InitializeAndGetActivitySource(nameof(FAT00700ReportController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        // Event handler: Specify the .frx report template file path
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
        {
            pcfiletemplate = Path.Combine("Reports", "FAT00700Report.frx");
        }

        // Event handler: Provide report data and data source name
        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateData(_Parameter));
            pcDataSourceName = "ResponseDataModel";
        }

        // Event handler: Set report number and date formatting
        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }

        // POST endpoint: Cache parameters and return GUID
        [HttpPost]
        [Route("rpt/FAT00700Print/FATransactionPost")]
        public R_DownloadFileResultDTO FATransactionPost(FAT00700ReportParam poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(FATransactionPost));
            _logger.LogInfo("Start - Post FAT00700 Report");
            R_Exception loException = new();
            FAT00700ReportLogKeyDTO loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new FAT00700ReportLogKeyDTO
                {
                    poParam = poParam,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY),
                    poGlobalVar = R_ReportGlobalVar.R_GetReportDTO()
                };

                _logger.LogInfo("Set GUID Param - Post FAT00700 Report");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Post FAT00700 Report");
            return loRtn;
        }

        // GET endpoint: Retrieve cached parameters and generate report stream
        [HttpGet, AllowAnonymous]
        [Route("rpt/FAT00700Print/FATransactionReport")]
        public FileStreamResult FATransactionReport(string pcGuid)
        {
            using var loActivity = _activitySource.StartActivity(nameof(FATransactionReport));
            _logger.LogInfo("Start - Get FAT00700 Report");
            R_Exception loException = new();
            FileStreamResult loRtn = null;
            FAT00700ReportLogKeyDTO loResultGUID = null;
            try
            {
                // Get cached parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<FAT00700ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

                // Set log key and global vars
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                R_ReportGlobalVar.R_SetFromReportDTO(loResultGUID.poGlobalVar);

                _Parameter = loResultGUID.poParam;

                // Generate report based on print mode
                if (_Parameter.LIS_PRINT)
                {
                    loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF),
                        R_ReportUtility.GetMimeType(R_FileType.PDF));
                }
                else
                {
                    var loFileType = (R_FileType)Enum.Parse(typeof(R_FileType), _Parameter.CREPORT_FILETYPE);
                    loRtn = File(_ReportCls.R_GetStreamReport(peExport: loFileType),
                        R_ReportUtility.GetMimeType(loFileType),
                        $"{_Parameter.CREPORT_FILENAME}.{_Parameter.CREPORT_FILETYPE}");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Get FAT00700 Report");
            return loRtn;
        }

        // Generate report data
        private FAT00700ReportWithBaseHeaderDTO GenerateData(FAT00700ReportParam poParam)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GenerateData));
            var loEx = new R_Exception();
            var loRtn = new FAT00700ReportWithBaseHeaderDTO();
            var loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);

            try
            {
                // Get base header data (company logo, print info, etc.)
                loRtn.BaseHeaderData = new BaseHeaderDTO();
                loRtn.BaseHeaderColumn = new BaseHeaderColumnDTO();

                // Set report title
                loRtn.Title = "FA Transaction Report";

                // Map report parameters to GetReportDataParameterDTO
                var loReportParam = new GetReportDataParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANGID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CDEPT_CODE = poParam.CDEPT_CODE,
                    CTRANSACTION_CODE = poParam.CTRANSACTION_CODE,
                    CREFERENCE_NO = poParam.CREFERENCE_NO,
                    CASSET_CODE = poParam.CASSET_CODE,
                    CFROM_DATE = poParam.CFROM_DATE,
                    CTO_DATE = poParam.CTO_DATE,
                    LPRINT_DETAIL = poParam.LPRINT_DETAIL,
                    LPRINT_SUMMARY = poParam.LPRINT_SUMMARY
                };

                // Call Back layer GetReportData method
                var loReportCls = new FAT00700ReportCls();
                loRtn.Data = loReportCls.GetReportData(loReportParam);

                // Set report labels and column headers
                loRtn.Label = new FAT00700LabelDTO();
                loRtn.Column = new FAT00700ColumnPrintDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}

