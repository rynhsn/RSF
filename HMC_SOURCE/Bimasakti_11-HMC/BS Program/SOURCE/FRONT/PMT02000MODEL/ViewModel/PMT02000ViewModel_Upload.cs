using PMT02000COMMON.DownloadTemplate;
using PMT02000COMMON.Upload;
using PMT02000COMMON.Upload.Agreement;
using PMT02000COMMON.Upload.Unit;
using PMT02000COMMON.Upload.Utility;
using PMT02000COMMON.Utility;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Error = R_BlazorFrontEnd.Exceptions.R_Error;

namespace PMT02000MODEL.ViewModel
{
    public class PMT02000ViewModel_Upload : R_IProcessProgressStatus
    {
        PMT02000LOIModel _model = new PMT02000LOIModel(); //PMT02000ModelTemplate
        public PMT02000ParameterUploadDTO _parameterUpload = new PMT02000ParameterUploadDTO();
        public ObservableCollection<AgreementUploadExcelDTO> AgreementGrid { get; set; } = new ObservableCollection<AgreementUploadExcelDTO>();
        public ObservableCollection<UnitUploadExcelDTO> UnitGrid { get; set; } = new ObservableCollection<UnitUploadExcelDTO>();
        public ObservableCollection<UtilityUploadExcelDTO> UtilityGrid { get; set; } = new ObservableCollection<UtilityUploadExcelDTO>();

        public Action<R_Exception>? DisplayErrorAction { get; set; }
        public Action? ShowSuccessAction { get; set; }
        public Action? StateChangeAction { get; set; }
        public DataSet? ExcelDataSetTemplate { get; set; }
        public DataSet? ExcelDataSetError { get; set; }
        public Func<Task>? ActionDataSetExcel { get; set; }
        // Func Proses is Success
        public Func<Task>? ActionIsCompleteSuccess { get; set; }
        public List<R_Error> ErrorList { get; set; } = new List<R_Error>();
        public string Message = "";
        public int Percentage = 0;

        #region Public Summary Upload
        public int SumDataAgreementExcel { get; set; }
        public int SumDataUnitExcel { get; set; }
        public int SumDataUtilityExcel { get; set; }

        public int SumValidDataAgreementExcel { get; set; }
        public int SumInvalidDataAgreementExcel { get; set; }
        public int SumValidDataUnitExcel { get; set; }
        public int SumInvalidDataUnitExcel { get; set; }
        public int SumValidDataUtilityExcel { get; set; }
        public int SumInvalidDataUtilityExcel { get; set; }
        public string lcFilterResult { get; set; } = "";
        #endregion
        public string? CompanyID { get; set; }
        public string? UserId { get; set; }
        public string VAR_LOI_TRANS_CODE = "802061"; //VAR_HO_TRANS_CODE = 802130
        public string VAR_HO_TRANS_CODE = "802130";
        public bool _lError { get; set; }

        #region Template
        public async Task<PMT02000FileExcelDTO> DownloadTemplate(PMT02000DBParameter poParam)
        {
            var loEx = new R_Exception();
            PMT02000FileExcelDTO loResult = new PMT02000FileExcelDTO();

            try
            {
                SaveMultiListDataExcelDTO loDataTemplateConvert = new SaveMultiListDataExcelDTO();
                var loDataTemplate = await GetListTemplate(poParam);

                if (loDataTemplate.AgreementList.Count > 0)
                {
                    var loData = GetAgreementListDbToExcel(loDataTemplate.AgreementList);
                    loDataTemplateConvert.AgreementListExcel = loData;

                }
                if (loDataTemplate.UnitList.Count > 0)
                {
                    var loData = GetUnitListDbToExcel(loDataTemplate.UnitList, loDataTemplate.AgreementList.Count());
                    loDataTemplateConvert.UnitListExcel = loData;

                }
                if (loDataTemplate.UtilityList.Count > 0)
                {
                    var loData = GetUtilityListDbToExcel(loDataTemplate.UtilityList, (loDataTemplate.AgreementList.Count() + loDataTemplate.UnitList.Count()));
                    loDataTemplateConvert.UtilityListExcel = loData;

                }
                WriteToFileTemplate(poParameter: loDataTemplateConvert);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion
        #region GetTemplate
        public async Task<PMT0200MultiListDataDTO> GetListTemplate(PMT02000DBParameter poParameter)
        {
            R_Exception loException = new R_Exception();
            PMT0200MultiListDataDTO loReturn = new PMT0200MultiListDataDTO();
            try
            {
                poParameter.CTRANS_CODE = VAR_LOI_TRANS_CODE;
                poParameter.COUTPUT_TYPE = "A,UN,UT";

                PMT0200MultiListDataDTO loTemplate = await _model.GetHOTemplateDataAsync(poParameter);
                loReturn = loTemplate;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn;
        }
        public void WriteToFileTemplate(SaveMultiListDataExcelDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            try
            {
                //Set DataSetTable
                var loDataSet = new DataSet();
                DataTable loDataTable;

                #region Agreement
                var loAgreementExcelData = poParameter.AgreementListExcel;
                loDataTable = R_FrontUtility.R_ConvertTo(loAgreementExcelData);
                loDataTable.TableName = "Agreement";
                loDataSet.Tables.Add(loDataTable);
                #endregion

                #region Unit
                var loUnitExcelData = poParameter.UnitListExcel;
                loDataTable = R_FrontUtility.R_ConvertTo(loUnitExcelData);
                loDataTable.TableName = "Unit";

                loDataSet.Tables.Add(loDataTable);
                #endregion

                #region Utility
                var loUtilityExcelData = poParameter.UtilityListExcel;
                loDataTable = R_FrontUtility.R_ConvertTo(loUtilityExcelData);
                loDataTable.TableName = "Utility";

                loDataSet.Tables.Add(loDataTable);
                #endregion
                // Asign Dataset
                ExcelDataSetTemplate = loDataSet;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion
        #region assign (NO) for all data
        public async Task GetAgreementList(List<AgreementUploadExcelDTO> poEntity)
        {
            var loEx = new R_Exception();

            try
            {

                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    AgreementUploadExcelDTO loData = new AgreementUploadExcelDTO
                    {
                        NO = i + 1,
                        Property = loTemp.Property,
                        Transaction = loTemp.Transaction,
                        Department = loTemp.Department,
                        LOI_AgrmntNo = loTemp.LOI_AgrmntNo,
                        Building = loTemp.Building,
                        HORefNo = loTemp.HORefNo,
                        HORefDate = loTemp.HORefDate,
                        HOActualDate = loTemp.HOActualDate,
                        Notes = loTemp.Notes,
                        Valid = "Y"
                    };

                    if (DateTime.TryParseExact(loData.HORefDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var RefDate))
                    {
                        loData.HORefDateDisplay = RefDate;
                    }
                    else
                    {
                        loData.HORefDateDisplay = null;
                    }
                    if (DateTime.TryParseExact(loData.HOActualDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ActualDate))
                    {
                        loData.HOActualDateDisplay = ActualDate;
                    }
                    else
                    {
                        loData.HOActualDateDisplay = null;
                    }
                    return loData;
                }
                ));

                SumDataAgreementExcel = loData.Count();
                AgreementGrid = new ObservableCollection<AgreementUploadExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUnitList(List<UnitUploadExcelDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    UnitUploadExcelDTO loData = new UnitUploadExcelDTO
                    {
                        SEQ_ERROR = i + 1 + liCountGrid,
                        NO = i + 1,
                        Property = loTemp.Property,
                        Transaction = loTemp.Transaction,
                        Department = loTemp.Department,
                        LOI_AgrmntNo = loTemp.LOI_AgrmntNo,
                        Building = loTemp.Building,
                        Floor = loTemp.Floor,
                        Unit = loTemp.Unit,
                        ActualSize = loTemp.ActualSize,
                        Notes = "",
                        Valid = "Y"
                    };

                    return loData;
                }
                ));

                SumDataUnitExcel = loData.Count();
                UnitGrid = new ObservableCollection<UnitUploadExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUtilityList(List<UtilityUploadExcelDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Excel DTO and add SeqNo
                var loData = await Task.WhenAll(poEntity.Select(async (loTemp, i) =>
                {
                    UtilityUploadExcelDTO loData = new UtilityUploadExcelDTO
                    {
                        SEQ_ERROR = i + 1 + liCountGrid,
                        NO = i + 1,
                        Property = loTemp.Property,
                        Transaction = loTemp.Transaction,
                        Department = loTemp.Department,
                        LOI_AgrmntNo = loTemp.LOI_AgrmntNo,
                        Building = loTemp.Building,
                        Floor = loTemp.Floor,
                        Unit = loTemp.Unit,
                        ChargesTypeId = loTemp.ChargesTypeId,
                        ChargesTypeName = loTemp.ChargesTypeName,
                        ChargesId = loTemp.ChargesId,
                        ChargesName = loTemp.ChargesName,
                        MeterNo = loTemp.MeterNo,
                        StartPeriod = loTemp.StartPeriod,
                        MeterStart = loTemp.MeterStart,
                        Block1Start = loTemp.Block1Start,
                        Block2Start = loTemp.Block2Start,
                        Notes = "",
                        Valid = "Y"
                    };
                    return loData;
                }
                ));

                SumDataUtilityExcel = loData.Count();
                UtilityGrid = new ObservableCollection<UtilityUploadExcelDTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Convert Template from DB to EXCEL
        public List<SaveAgreementToExcelDTO> GetAgreementListDbToExcel(List<AgreementUploadDTO> poEntity)
        {
            var loEx = new R_Exception();
            List<SaveAgreementToExcelDTO> loReturn = new List<SaveAgreementToExcelDTO>();
            try
            {
                var loData = poEntity.Select((loTemp, i) => new SaveAgreementToExcelDTO
                {
                    Property = loTemp.CPROPERTY_ID,
                    Transaction = loTemp.CTRANS_CODE,
                    Department = loTemp.CDEPT_CODE,
                    LOI_AgrmntNo = loTemp.CLOI_REF_NO,
                    Building = loTemp.CBUILDING_ID,
                    HORefNo = loTemp.CHO_ACTUAL_DATE,
                    HORefDate = loTemp.CREF_DATE,
                    HOActualDate = loTemp.CHO_ACTUAL_DATE
                }).ToList();

                loReturn = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
        public List<SaveUnitToExcelDTO> GetUnitListDbToExcel(List<UnitUploadDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();
            List<SaveUnitToExcelDTO> loReturn = new List<SaveUnitToExcelDTO>();
            try
            {
                var loData = poEntity.Select((loTemp, i) => new SaveUnitToExcelDTO
                {
                    Property = loTemp.CPROPERTY_ID,
                    Transaction = loTemp.CTRANS_CODE,
                    Department = loTemp.CDEPT_CODE,
                    LOI_AgrmntNo = loTemp.CLOI_REF_NO,
                    Building = loTemp.CBUILDING_ID,
                     Floor= loTemp.CFLOOR_ID,
                    Unit = loTemp.CUNIT_ID,
                    ActualSize = loTemp.NACTUAL_AREA_SIZE
                }).ToList();
                loReturn = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
        public List<SaveUtilityToExcelDTO> GetUtilityListDbToExcel(List<UtilityUploadDataFromDBDTO> poEntity, int liCountGrid)
        {
            var loEx = new R_Exception();
            List<SaveUtilityToExcelDTO> loReturn = new List<SaveUtilityToExcelDTO>();
            try
            {
                var loData = poEntity.Select((loTemp, i) => new SaveUtilityToExcelDTO
                {
                    Property = loTemp.CPROPERTY_ID,
                    Transaction = loTemp.CTRANS_CODE,
                    Department = loTemp.CDEPT_CODE,
                    LOI_AgrmntNo = loTemp.CLOI_REF_NO,
                    Building = loTemp.CBUILDING_ID,
                    Floor = loTemp.CFLOOR_ID,
                    Unit = loTemp.CUNIT_ID,
                    ChargesTypeId = loTemp.CCHARGES_TYPE,
                    ChargesTypeName = loTemp.CCHARGES_TYPE_NAME,
                    ChargesId = loTemp.CCHARGES_ID,
                    ChargesName = loTemp.CCHARGES_NAME,
                    MeterNo = loTemp.CMETER_NO,
                    StartPeriod = loTemp.CSTART_INV_PRD,
                    MeterStart = loTemp.NMETER_START,
                    Block1Start = loTemp.NBLOCK1_START,
                    Block2Start = loTemp.NBLOCK2_START,
                }).ToList();
                loReturn = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
        #endregion

        public async Task SaveBulkFile()
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            PMT0200MultiListDataDTO loMultiList;
            List<R_KeyValue> loUserParameter;

            try
            {
                loUserParameter = new List<R_KeyValue>
                {
                    new R_KeyValue() { Key = ContextConstant.CPROPERTY_ID, Value = _parameterUpload.CPROPERTY_ID! },
                    new R_KeyValue() { Key = ContextConstant.CTRANS_CODE, Value = VAR_HO_TRANS_CODE }
                };
                loMultiList = new PMT0200MultiListDataDTO
                {
                    AgreementList = new List<AgreementUploadDTO>(),
                    UnitList = new List<UnitUploadDTO>(),
                    UtilityList = new List<UtilityUploadDataFromDBDTO>()
                };

                //Check Data
                if (AgreementGrid.Count > 0)
                {
                    loMultiList.AgreementList = AgreementGrid
                        .Select(x => new AgreementUploadDTO
                        {
                            NO = x.NO,
                            ISEQ_NO_ERROR = x.NO,
                            CCOMPANY_ID = CompanyID!,
                            CPROPERTY_ID = _parameterUpload.CPROPERTY_ID,
                            CTRANS_CODE = x.Transaction,
                            CDEPT_CODE = x.Department,
                            CLOI_REF_NO = x.LOI_AgrmntNo,
                            CBUILDING_ID = x.Building,
                            CREF_NO = x.HORefNo,
                            CREF_DATE = x.HORefDate,
                            CHO_ACTUAL_DATE = x.HOActualDate,
                        })
                        .ToList();
                }
                if (UnitGrid.Count > 0)
                {
                    loMultiList.UnitList = UnitGrid
                        .Select(x => new UnitUploadDTO
                        {
                            NO = x.NO,
                            ISEQ_NO_ERROR = x.SEQ_ERROR,
                            CCOMPANY_ID = CompanyID!,
                            CPROPERTY_ID = _parameterUpload.CPROPERTY_ID,
                            CTRANS_CODE = x.Transaction,
                            CDEPT_CODE = x.Department,
                            CLOI_REF_NO = x.LOI_AgrmntNo,
                            CBUILDING_ID = x.Building,
                            CFLOOR_ID = x.Floor,
                            CUNIT_ID = x.Unit,
                            NACTUAL_AREA_SIZE = x.ActualSize,
                        })
                        .ToList();
                }
                if (UtilityGrid.Count > 0)
                {
                    loMultiList.UtilityList = UtilityGrid
                       .Select(x => new UtilityUploadDataFromDBDTO
                       {
                           NO = x.NO,
                           ISEQ_NO_ERROR = x.SEQ_ERROR,
                           CCOMPANY_ID = CompanyID!,
                           CPROPERTY_ID = _parameterUpload.CPROPERTY_ID,
                           CTRANS_CODE = x.Transaction,
                           CDEPT_CODE = x.Department,
                           CLOI_REF_NO = x.LOI_AgrmntNo,
                           CBUILDING_ID = x.Building,
                           CFLOOR_ID = x.Floor,
                           CUNIT_ID = x.Unit,
                           CCHARGES_TYPE_ID = x.ChargesTypeId,
                           CCHARGES_TYPE_NAME = x.ChargesTypeName,
                           CCHARGES_ID = x.ChargesId,
                           CCHARGES_NAME = x.ChargesName,
                           CMETER_NO = x.MeterNo,
                           CSTART_INV_PRD = x.StartPeriod,
                           NMETER_START = x.MeterStart,
                           NBLOCK1_START = x.Block1Start,
                           NBLOCK2_START = x.Block2Start,
                       })
                       .ToList();
                }
                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();
                loBatchPar.COMPANY_ID = CompanyID!;
                loBatchPar.USER_ID = UserId!;
                loBatchPar.ClassName = "PMT02000Back.PMT02000UploadCls";
                loBatchPar.UserParameters = loUserParameter;
                loBatchPar.BigObject = loMultiList;

                var lcGuid = await loCls.R_BatchProcess<PMT0200MultiListDataDTO>(loBatchPar, 42);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Validate 
        public void CheckingDataWithEmptyDocNo()
        {
            R_Exception loEx = new R_Exception();
            List<AgreementUploadExcelDTO>? loAgreementFilterResult = null;
            List<UnitUploadExcelDTO>? loUnitFilterResult = null;
            List<UtilityUploadExcelDTO>? loUtilityFilterResult = null;
            try
            {
                if (AgreementGrid.Count() > 0)
                {
                    loAgreementFilterResult = AgreementGrid.Where(item => string.IsNullOrWhiteSpace(item.LOI_AgrmntNo)).ToList();
                    if (loAgreementFilterResult.Count() > 0)
                    {
                        lcFilterResult += "Agreement : ";
                        foreach (var item in loAgreementFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (UnitGrid.Count() > 0)
                {
                    loUnitFilterResult = UnitGrid.Where(item => string.IsNullOrWhiteSpace(item.LOI_AgrmntNo)).ToList();
                    if (loUnitFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Unit : ";
                        foreach (var item in loUnitFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
                if (UtilityGrid.Count() > 0)
                {
                    loUtilityFilterResult = UtilityGrid.Where(item => string.IsNullOrWhiteSpace(item.LOI_AgrmntNo)).ToList();
                    if (loUtilityFilterResult.Count() > 0)
                    {
                        lcFilterResult = lcFilterResult + "Utility : ";
                        foreach (var item in loUtilityFilterResult)
                        {
                            lcFilterResult = lcFilterResult + item.NO.ToString() + ", ";
                        }
                        lcFilterResult = lcFilterResult.Substring(0, lcFilterResult.Length - 2);
                        lcFilterResult = lcFilterResult + ". ";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success", pcKeyGuid);
                    _lError = false;
                    ShowSuccessAction();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = string.Format("Process Complete but fail", pcKeyGuid);
                    await ServiceGetError(pcKeyGuid);
                    _lError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            R_Exception loException = new R_Exception();

            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            ex.ErrorList.ForEach(x => loException.Add(x.ErrNo, x.ErrDescp));
            DisplayErrorAction.Invoke(loException);
            StateChangeAction();
            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }
        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;
            DataTable loDataTable;
            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyID!,
                    USER_ID = UserId!,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_UPLOAD_LEASE_AGREEMENTResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Any(y => y.SeqNo <= 0))
                {
                    var loUnhandleEx = loResultData.Select(x => new R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    ErrorList = new List<R_Error>(loUnhandleEx);
                    loUnhandleEx.ForEach(x => loException.Add(x));
                }

                if (loResultData.Any(y => y.SeqNo > 0))
                {
                    // Display Error Handle if get seq Agreement
                    AgreementGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.NO))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.NO).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataAgreementExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataAgreementExcel++;
                        }
                    });

                    // Display Error Handle if get seq Unit
                    UnitGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SEQ_ERROR))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_ERROR).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataUnitExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataUnitExcel++;
                        }
                    });

                    // Display Error Handle if get seq Utility
                    UtilityGrid.ToList().ForEach(x =>
                    {
                        //Assign ErrorMessage, Valid and Set Valid And Invalid Data
                        if (loResultData.Any(y => y.SeqNo == x.SEQ_ERROR))
                        {
                            x.Notes = loResultData.Where(y => y.SeqNo == x.SEQ_ERROR).FirstOrDefault().ErrorMessage;
                            x.Valid = "N";
                            SumInvalidDataUtilityExcel++;
                        }
                        else
                        {
                            x.Valid = "Y";
                            SumValidDataUtilityExcel++;
                        }
                    });

                    //Set DataSetTable
                    var loDataSet = new DataSet();

                    #region Agreement
                    var loAgreementExcelData = R_FrontUtility.ConvertCollectionToCollection<AgreementErrorDTO>(AgreementGrid);
                    loDataTable = R_FrontUtility.R_ConvertTo(loAgreementExcelData);
                    loDataTable.TableName = "Agreement";
                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    #region Unit
                    var loUnitExcelData = R_FrontUtility.ConvertCollectionToCollection<UnitErrorDTO>(UnitGrid);
                    loDataTable = R_FrontUtility.R_ConvertTo(loUnitExcelData);
                    loDataTable.TableName = "Unit";

                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    #region Utility
                    var loUtilityExcelData = R_FrontUtility.ConvertCollectionToCollection<UtilityErrorDTO>(UtilityGrid);
                    loDataTable = R_FrontUtility.R_ConvertTo(loUtilityExcelData);
                    loDataTable.TableName = "Utility";

                    loDataSet.Tables.Add(loDataTable);
                    #endregion

                    // Asign Dataset
                    ExcelDataSetError = loDataSet;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
