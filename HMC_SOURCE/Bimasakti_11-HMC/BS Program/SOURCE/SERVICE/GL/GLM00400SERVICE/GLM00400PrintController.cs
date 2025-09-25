using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Reflection;

namespace GLM00400SERVICE
{
    public class GLM00400PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GLM00400PrintParamDTO _AllGLM00400Parameter;
        private LoggerGLM00400Print _LoggerPrint;

        #region instantiate
        public GLM00400PrintController(ILogger<LoggerGLM00400Print> logger)
        {
            //Initial and Get Logger
            LoggerGLM00400Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerGLM00400Print.R_GetInstanceLogger();

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GLM00400.frx"); 
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllGLM00400Parameter));
            pcDataSourceName = "ResponseDataModel";
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }
        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO AllAllocationJournalPost(GLM00400PrintParamDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllAllocationJournalPost");
            GLM00400PrintLogKeyDTO<GLM00400PrintParamDTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GLM00400PrintLogKeyDTO<GLM00400PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllAllocationJournalPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GLM00400PrintLogKeyDTO<GLM00400PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            _LoggerPrint.LogInfo("End AllAllocationJournalPost");
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamAllocationJournalGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            GLM00400PrintLogKeyDTO<GLM00400PrintParamDTO> loResultGUID = null;
            FileStreamResult loRtn = null;
            _LoggerPrint.LogInfo("Start AllStreamAllocationJournalGet");

            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GLM00400PrintLogKeyDTO<GLM00400PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllGLM00400Parameter = loResultGUID.poParam;

                _LoggerPrint.LogInfo("Read File Report AllStreamAllocationJournalGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamAllocationJournalGet");

            return loRtn;
        }

        #region Helper
        private GLM00400ResultWithBaseHeaderPrintDTO GenerateDataPrint(GLM00400PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400ResultWithBaseHeaderPrintDTO loRtn = new GLM00400ResultWithBaseHeaderPrintDTO();

            try
            {
                // Initial Result Data Rtn
                GLM00400ResultPrintDTO loData = new GLM00400ResultPrintDTO();

                var loCls = new GLM00400Cls(_LoggerPrint);

                _LoggerPrint.LogInfo("Call Method GetAllPrintAllocationHeader, GetAllPrintAllocationAccount, And GetAllPrintAllocationAccountCenter Report");
                // get Header From DB
                List<GLM00400PrintHDDTO> loHDData = loCls.GetAllPrintAllocationHeader(poParam);

                // get detail data from alloc alloc id
                List<GLM00400PrintAccountDTO> loACCOUNTData = new List<GLM00400PrintAccountDTO>();
                List<GLM00400PrintCenterDTO> loCENTERData = new List<GLM00400PrintCenterDTO>();
                
                foreach (var item in loHDData)
                {
                    poParam.CALLOC_ID = item.CALLOC_ID;
                    var loDetailAllocAccount = loCls.GetAllPrintAllocationAccount(poParam);
                    loACCOUNTData.AddRange(loDetailAllocAccount);

                    var loDetailAllocCenter = loCls.GetAllPrintAllocationAccountCenter(poParam);
                    loCENTERData.AddRange(loDetailAllocCenter);
                }

                _LoggerPrint.LogInfo("Grouping Report");
                // Group by CALLOC_ in loHDData and create dummy data in loACCOUNTData
                var groupedData = loHDData
                 .GroupJoin(
                     loACCOUNTData,
                     hd => hd.CALLOC_ID,
                     account => account.CALLOC_ID,
                     (hd, accounts) => new GLM00400PrintHDResultDTO
                     {
                         CALLOC_NO = hd.CALLOC_NO,
                         CALLOC_NAME = hd.CALLOC_NAME,
                         CYEAR = hd.CYEAR,
                         CALLOC_ID = hd.CALLOC_ID,
                         AllocationAccount = accounts.ToList(),
                         AllocationCenter = loCENTERData
                             .Where(center => center.CALLOC_ID == hd.CALLOC_ID)
                             .GroupBy(center => new { center.CPERIOD, center.CALLOC_PERIOD }, detail => new GLM00400PrintCenterDetailDTO
                             {
                                 CPERIOD = detail.CPERIOD,
                                 CTARGET_CENTER = detail.CTARGET_CENTER,
                                 NVALUE = detail.NVALUE
                             }) // Group by CPERIOD
                             .Select(group => new GLM00400PrintCenterDTO
                             {
                                 CPERIOD = group.Key.CPERIOD,
                                 CALLOC_PERIOD = group.Key.CALLOC_PERIOD,
                                 CenterDetail = group.ToList()
                             })
                             .ToList()
                     })
                 .ToList();

                loData.Header = loHDData.FirstOrDefault();

                if (loData.Header is not null)
                    loData.Header.CYEAR = poParam.CYEAR;

                loData.HeaderData = groupedData;

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Allocation Journal",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.Allocation = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion
    }
}