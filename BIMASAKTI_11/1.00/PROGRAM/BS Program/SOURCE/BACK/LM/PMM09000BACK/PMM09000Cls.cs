using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using PMM09000COMMON.UtiliyDTO;
using System.Data.SqlClient;
using RSP_PM_MAINTAIN_AMORTIZATIONResources;

namespace PMM09000Back
{
    public class PMM09000Cls : R_BusinessObject<PMM09000EntryHeaderDetailDTO>
    {
        private LoggerPMM09000 _logger;
        private readonly ActivitySource _activitySource;

        Resources_Dummy_Class _resources = new();

        public PMM09000Cls()
        {
            _logger = LoggerPMM09000.R_GetInstanceLogger();
            _activitySource = PMM09000Activity.R_GetInstanceActivitySource();
        }

        protected override PMM09000EntryHeaderDetailDTO R_Display(PMM09000EntryHeaderDetailDTO poEntity)
        {
            string lcMethodName = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            PMM09000EntryHeaderDetailDTO? loReturn = new PMM09000EntryHeaderDetailDTO();
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db loDb;
            string lcQuery;
            try
            {

                lcQuery = "RSP_PM_GET_AMORTIZATION_DT";
                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                PMM09000EntryHeaderDTO poHeader = new PMM09000EntryHeaderDTO();

                if (poEntity.Header.CREF_NO != null)
                {
                    poHeader = (PMM09000EntryHeaderDTO)poEntity.Header!;
                }
                _logger.LogDebug("{@ObjectHeader} ", poHeader);

                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poHeader.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poHeader.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_OPTION", DbType.String, 2, poHeader.CUNIT_OPTION);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poHeader.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_TYPE", DbType.String, 20, poHeader.CTRANS_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 40, poHeader.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poHeader.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                var loReturnHeader = R_Utility.R_ConvertTo<PMM09000EntryHeaderDTO>(loReturnTemp).ToList().FirstOrDefault() ?? new PMM09000EntryHeaderDTO();

                //GET amortization detail
                PMM09000ClsList ClsList = new PMM09000ClsList();
                PMM09000DbParameterDTO loParameter = R_Utility.R_ConvertObjectToObject<PMM09000EntryHeaderDTO, PMM09000DbParameterDTO>(poHeader);
                _logger.LogInfo("Convert DTO Header to Parameter");
                _logger.LogDebug("{@Parameter}", loParameter);

                var loTempAmortizationScheduleList = ClsList.GetAmortizationChargesList(loParameter);
                _logger.LogInfo("Get List Amortization");
                _logger.LogDebug("{@ObjectData}", loTempAmortizationScheduleList);

                loReturn.Header = loReturnHeader;
                loReturn.Details = loTempAmortizationScheduleList;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End process method GetHeader on Cls");
            return loReturn!;
        }

        protected override void R_Saving(PMM09000EntryHeaderDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string? lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "NEW";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }

                var loHeader = poNewEntity.Header;
                var loListDetail = poNewEntity.Details;
                _logger.LogInfo("GET Header and Detail");
                _logger.LogDebug("{@ObjectHeader}", loHeader!);
                _logger.LogDebug("{@ObjectDetail}", loListDetail!);

                #region Convert to Temp Table
                List<TempTableAmortizationCharges> loListAmortizationCharges = new ();

                lcQuery = $"CREATE TABLE #AMOR_SCH " +
                         $"(CSEQ_NO VARCHAR(3), " +
                         $"CCHARGES_TYPE_ID  VARCHAR(2), " +
                         $"CCHARGES_ID  VARCHAR(20), " +
                         $"CSTART_DATE   VARCHAR(8), " +
                         $"CEND_DATE   VARCHAR(8), " +
                         $"NAMOUNT NUMERIC(18,2) )";

                _logger.LogDebug("{@ObjectQuery} ", lcQuery);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                if (loListDetail.Count>0)
                {
                    _logger.LogInfo("Detail is not null");

                    loListAmortizationCharges = loListDetail
                         .Select(x => new TempTableAmortizationCharges
                         {
                             CSEQ_NO = x.CSEQ_NO,
                             CCHARGES_TYPE_ID = x.CCHARGES_TYPE,
                             CCHARGES_ID = x.CCHARGES_ID,
                             CSTART_DATE = x.CSTART_DATE,
                             CEND_DATE = x.CEND_DATE,
                             NAMOUNT = (decimal)x.NAMOUNT
                         }).ToList();
                    loDb.R_BulkInsert<TempTableAmortizationCharges>((SqlConnection)loConn, "#AMOR_SCH", loListAmortizationCharges);

                    _logger.LogInfo("Exec Temp Table [#AMOR_SCH]");
                    _logger.LogDebug("{@DataTempTable}", loListAmortizationCharges);
                }
                #endregion

                lcQuery = "RSP_PM_MAINTAIN_AMORTIZATION";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, loHeader.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, loHeader.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_OPTION", DbType.String, 2, loHeader.CUNIT_OPTION);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, loHeader.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, loHeader.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, loHeader.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CAGREEMENT_NO", DbType.String, 20, loHeader.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, int.MaxValue, loHeader.CDESCRIPTION ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_DEPT_CODE", DbType.String, 20, loHeader.CTRANS_DEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CAMORTIZATION_NO", DbType.String, 20, loHeader.CAMORTIZATION_NO ?? "");

                loDb.R_AddCommandParameter(loCommand, "@LCUT_OF_PRD", DbType.Boolean, 20, loHeader.LCUT_OFF_PRD);
                loDb.R_AddCommandParameter(loCommand, "@CCUT_OF_PRD", DbType.String, 6, loHeader.CCUT_OF_PRD);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, loHeader.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, loHeader.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_ACCRUAL", DbType.String, 20, loHeader.CCHARGES_ID ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, loHeader.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    var loTransType = R_Utility.R_ConvertTo<PMM09000EntryHeaderDTO>(loDataTable).FirstOrDefault();

                    if (loTransType.CTRANS_TYPE != null)
                    {
                        poNewEntity.Header.CTRANS_TYPE = loTransType.CTRANS_TYPE;
                        poNewEntity.Header.CREF_NO = loHeader.CAMORTIZATION_NO;
                    }
                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                        loConn.Dispose();
                    }
                    loConn = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(PMM09000EntryHeaderDetailDTO poEntity)
        {
            string lcMethodName = nameof(R_Deleting);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string? lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string? lcAction = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcAction = "DELETE";

                var loHeader = poEntity.Header;
                var loListDetail = poEntity.Details;
                _logger.LogInfo("GET Header and Detail");
                _logger.LogDebug("{@ObjectHeader}", loHeader!);
                _logger.LogDebug("{@ObjectDetail}", loListDetail!);

                #region Convert to Temp Table
                List<TempTableAmortizationCharges>? loListAmortizationCharges = null;

                lcQuery = $"CREATE TABLE #AMOR_SCH " +
                         $"(CSEQ_NO VARCHAR(3), " +
                         $"CCHARGES_TYPE_ID  VARCHAR(2), " +
                         $"CCHARGES_ID  VARCHAR(20), " +
                         $"CSTART_DATE   VARCHAR(8), " +
                         $"CEND_DATE   VARCHAR(8), " +
                         $"NAMOUNT NUMERIC(18,2) )";

                _logger.LogDebug("{@ObjectQuery} ", lcQuery);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                if (loListDetail.Count > 0)
                {
                    _logger.LogInfo("Detail is not null");
                    loListAmortizationCharges = loListDetail
                         .Select(x => new TempTableAmortizationCharges
                         {
                             CSEQ_NO = x.CSEQ_NO,
                             CCHARGES_TYPE_ID = x.CCHARGES_TYPE,
                             CCHARGES_ID = x.CCHARGES_ID,
                             CSTART_DATE = x.CSTART_DATE,
                             CEND_DATE = x.CEND_DATE,
                             NAMOUNT = x.NAMOUNT
                         }).ToList();
                    loDb.R_BulkInsert((SqlConnection)loConn, "#AMOR_SCH", loListAmortizationCharges);

                    _logger.LogInfo("Exec Temp Table [#AMOR_SCH]");
                    _logger.LogDebug("{@DataTempTable}", loListAmortizationCharges);
                }
                #endregion

                lcQuery = "RSP_PM_MAINTAIN_AMORTIZATION";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, loHeader.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, loHeader.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_OPTION", DbType.String, 2, loHeader.CUNIT_OPTION);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, loHeader.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, loHeader.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, loHeader.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CAGREEMENT_NO", DbType.String, 20, loHeader.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, int.MaxValue, loHeader.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_DEPT_CODE", DbType.String, 20, loHeader.CTRANS_DEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CAMORTIZATION_NO", DbType.String, 20, loHeader.CAMORTIZATION_NO);

                loDb.R_AddCommandParameter(loCommand, "@LCUT_OF_PRD", DbType.Boolean, 20, loHeader.LCUT_OFF_PRD);
                loDb.R_AddCommandParameter(loCommand, "@CCUT_OF_PRD", DbType.String, 6, loHeader.CCUT_OF_PRD);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, loHeader.CSTART_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, loHeader.CEND_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_ACCRUAL", DbType.String, 20, loHeader.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, loHeader.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        public void UpdateAmortizationStatus(PMM09000DbParameterDTO poParameter)
        {
            string lcMethodName = nameof(UpdateAmortizationStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_PM_UPDATE_AMORTIZATION_STATUS ";
                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_OPTION", DbType.String, 2, poParameter.CUNIT_OPTION);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_TYPE", DbType.String, 20, poParameter.CTRANS_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 40, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, poParameter.CACTION);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID); ;

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCommand, false);
                    _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                }
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
