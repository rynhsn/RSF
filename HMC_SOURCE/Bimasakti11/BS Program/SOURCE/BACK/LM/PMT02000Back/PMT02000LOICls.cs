using PMT02000COMMON.Logs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02000COMMON.Utility;
using PMT02000COMMON.LOI_List;
using R_CommonFrontBackAPI;
using System.Reflection.Metadata;
using PMT02000COMMON.Upload;
using PMT02000COMMON.Upload.Agreement;
using PMT02000COMMON.Upload.Unit;
using PMT02000COMMON.Upload.Utility;

namespace PMT02000Back
{
    public class PMT02000LOICls : R_BusinessObject<PMT02000LOIHeader_DetailDTO>
    {
        private LoggerPMT02000 _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_MAINTAIN_HANDOVERResources.Resources_Dummy_Class _objectRSP= new();


        public PMT02000LOICls()
        {
            _logger = LoggerPMT02000.R_GetInstanceLogger();
            _activitySource = PMT02000Activity.R_GetInstanceActivitySource();
        }
        public PMT02000VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODEDb(PMT02000DBParameter poParameter)
        {
            string? lcMethod = nameof(GetVAR_GSM_TRANSACTION_CODEDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            PMT02000VarGsmTransactionCodeDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, "802130");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT02000VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() != null
                    ? R_Utility.R_ConvertTo<PMT02000VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault()
                    : new PMT02000VarGsmTransactionCodeDTO();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }

        public List<PMT02000PropertyDTO> GetAllPropertyList(PMT02000DBParameter poParameter)
        {
            string lcMethodName = nameof(GetAllPropertyList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<PMT02000PropertyDTO>? loReturn = null;
            R_Db loDb;
            DbCommand loCommand;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT02000PropertyDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public List<PMT02000LOIDTO> GetLOIList(PMT02000DBParameter poParameter)
        {
            string? lcMethodName = nameof(GetLOIList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<PMT02000LOIDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_HANDOVER_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT02000LOIDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        public List<PMT02000LOIHandOverUnitDTO> GetHOUnitList(PMT02000DBParameter poParameter)
        {
            string? lcMethodName = nameof(GetLOIList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<PMT02000LOIHandOverUnitDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_HANDOVER_UNIT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT02000LOIHandOverUnitDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }


        public List<PMT02000LOIHandoverUtilityDTO> GetUtilityList(PMT02000DBParameter poParameter)
        {
            string? lcMethodName = nameof(GetUtilityList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<PMT02000LOIHandoverUtilityDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_HANDOVER_UTILITY";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                     .Where(x => x != null && x.ParameterName.StartsWith("@"))
                     .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loReturn = R_Utility.R_ConvertTo<PMT02000LOIHandoverUtilityDTO>(loDataTable).ToList();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End process method GetDetailListLOI on Cls");

            return loReturn!;
        }
        protected override PMT02000LOIHeader_DetailDTO R_Display(PMT02000LOIHeader_DetailDTO poEntity)
        {

            string lcMethodName = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            PMT02000LOIHeader_DetailDTO? loReturn = null;
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db loDb;
            try
            {

                var lcQuery = "RSP_PM_GET_HANDOVER_DETAIL";
                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 6, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poEntity.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CSAVEMODE ", DbType.String, 7, poEntity.CSAVEMODE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                loReturn = R_Utility.R_ConvertTo<PMT02000LOIHeader_DetailDTO>(loReturnTemp).ToList().FirstOrDefault() ?? new PMT02000LOIHeader_DetailDTO();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                PMT02000DBParameter poParam = R_Utility.R_ConvertObjectToObject<PMT02000LOIHeader_DetailDTO, PMT02000DBParameter>(poEntity);

                // List<PMT02000LOIHandOverUnitDTO> PoGrouping = GroupingByUnitID(poParam);
                List<PMT02000LOIHandOverUnitDTO> GetHandOverUnitList = GetHOUnitList(poParam);
                List<PMT02000LOIHandoverUtilityDTO> GetHandOverUtilityList = GetUtilityList(poParam);

                loReturn.ListUnit = GetHandOverUnitList;
                loReturn.ListUtility = GetHandOverUtilityList;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            _logger.LogInfo("End process method GetHeader on Cls");
            return loReturn!;
        }

        protected override void R_Saving(PMT02000LOIHeader_DetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_Saving);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCommand;
            DbConnection loConn = null;
            string lcAction = null;
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

                #region Convert to Term Table Unit

                var loListDetailUnit = poNewEntity.ListUnit;

                List<PMT02000LOIDetailUnitListTempTableDTO> loListTempTableUnit = null;

                lcQuery = $"CREATE TABLE #GRID_UNIT " +
                         $"(CFLOOR_ID VARCHAR(20), " +
                         $"CUNIT_ID VARCHAR(20), " +
                         $"NACTUAL_AREA_SIZE NUMERIC(8,2) )";

                _logger.LogDebug("{@ObjectQuery} ", lcQuery);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                if (loListDetailUnit.Count() > 0)
                {
                    loListTempTableUnit = loListDetailUnit
                         .Select(x => new PMT02000LOIDetailUnitListTempTableDTO
                         {
                             CFLOOR_ID = x.CFLOOR_ID,
                             CUNIT_ID = x.CUNIT_ID,
                             NACTUAL_AREA_SIZE = x.NACTUAL_AREA_SIZE,
                         }).ToList();
                    loDb.R_BulkInsert((SqlConnection)loConn, "#GRID_UNIT", loListTempTableUnit);
                }
                #endregion

                #region Convert to Term Table Utility
                //List<PMT02000LOIHandoverUtilityDTO> loListDetailUtility = loListDetailUnit
                //    .Where(unit => unit.ListUtility != null) // Filter hanya unit yang memiliki ListUtility
                //    .SelectMany(unit => unit.ListUtility) // Menggabungkan semua ListUtility menjadi satu
                //    .ToList(); // Convert menjadi List;


                List<PMT02000LOIHandoverUtilityDTO> loListDetailUtility = poNewEntity.ListUtility!;

                List<PMT02000LOIDetailUtilityListTempTableDTO> loListTempTableUtility = null;
                lcQuery = $"CREATE TABLE #GRID_UTILITY " +
                          $"(CFLOOR_ID VARCHAR(20), " +
                          $"CUNIT_ID VARCHAR(20), " +
                          $"CCHARGES_TYPE VARCHAR(2), " +
                          $"CCHARGES_ID VARCHAR(20), " +
                          $"CMETER_NO VARCHAR(50), " +
                          $"CSTART_YEAR VARCHAR(4), " +
                          $"CSTART_MONTH VARCHAR(2)," +
                          $"NMETER_START NUMERIC(16,2), " +
                          $"NBLOCK1_START NUMERIC(16,2), " +
                          $"NBLOCK2_START NUMERIC(16,2) )";


                _logger.LogDebug("{@ObjectQuery} ", lcQuery);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                if (loListDetailUtility.Count() > 0)
                {
                    loListTempTableUtility = loListDetailUtility
                       .Select(x => new PMT02000LOIDetailUtilityListTempTableDTO
                       {
                           CFLOOR_ID = x.CFLOOR_ID,
                           CUNIT_ID = x.CUNIT_ID,
                           CCHARGES_TYPE = x.CCHARGES_TYPE,
                           CCHARGES_ID = x.CCHARGES_ID,
                           CMETER_NO = x.CMETER_NO,
                           CSTART_YEAR = x.CYEAR,
                           CSTART_MONTH = x.CMONTH,
                           NMETER_START = x.NMETER_START,
                           NBLOCK1_START = x.NBLOCK1_START,
                           NBLOCK2_START = x.NBLOCK2_START,
                       }).ToList();

                    loDb.R_BulkInsert((SqlConnection)loConn, "#GRID_UTILITY", loListTempTableUtility);
                }
                #endregion

                lcQuery = "RSP_PM_MAINTAIN_HANDOVER";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.VAR_TRANS_CODE); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CHO_REF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, poNewEntity.CHO_REF_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CHAND_OVER_DATE", DbType.String, 8, poNewEntity.CHO_ACTUAL_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, poNewEntity.CHO_PLAN_START_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, poNewEntity.CHO_PLAN_END_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_DEPT", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_TRANSCODE ", DbType.String, 10, poNewEntity.VAR_LOI_TRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_REFNO", DbType.String, 30, poNewEntity.CREF_NO);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    var loEntity = R_Utility.R_ConvertTo<PMT02000LOIHeader_DetailDTO>(loDataTable).FirstOrDefault();
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

        protected override void R_Deleting(PMT02000LOIHeader_DetailDTO poEntity)
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

                lcQuery = "RSP_PM_MAINTAIN_HANDOVER";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLINK_DEPT", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.VAR_TRANS_CODE); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO); //
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CHAND_OVER_DATE", DbType.String, 8, ""); //
                loDb.R_AddCommandParameter(loCommand, "@CSTART_DATE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CEND_DATE", DbType.String, 8, "");
                loDb.R_AddCommandParameter(loCommand, "@CLINK_TRANSCODE ", DbType.String, 10, "");
                loDb.R_AddCommandParameter(loCommand, "@CLINK_REFNO", DbType.String, 30, "");


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

        public PMT0200MultiListDataDTO GetAllHOTemplate(PMT02000DBParameter poParameter)
        {
            string? lcMethodName = nameof(GetHOTemplateEachTab);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            PMT0200MultiListDataDTO? loReturn = new ();
            try
            {
                string[] OutputTypeSeprate = poParameter.COUTPUT_TYPE
                                            .Split(',')
                                            .Select(x => x.Trim())
                                            .ToArray();

                 List<AgreementUploadDTO> loListTemplateAgreement = GetHOTemplateEachTab<AgreementUploadDTO>(poParameter, OutputTypeSeprate[0]);
                List<UnitUploadDTO> loListTemplateUnit = GetHOTemplateEachTab<UnitUploadDTO>(poParameter, OutputTypeSeprate[1]);
                List<UtilityUploadDataFromDBDTO> loListTemplateUtility = GetHOTemplateEachTab<UtilityUploadDataFromDBDTO>(poParameter, OutputTypeSeprate[2]);

                loReturn.AgreementList = loListTemplateAgreement;
                loReturn.UnitList = loListTemplateUnit;
                loReturn.UtilityList = loListTemplateUtility;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loReturn;
        }
        public List<T> GetHOTemplateEachTab<T>(PMT02000DBParameter poParameter, string OutputType)
        {
            string? lcMethodName = nameof(GetHOTemplateEachTab);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<T>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_DOWN_HO_TEMPLATE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@COUTPUT_TYPE", DbType.String, 3, OutputType);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                var loListTemplate = R_Utility.R_ConvertTo<T>(loDataTable).ToList();
                loReturn = loListTemplate;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        #region Process SubmitRedraft
        public void ProcessSubmitRedraft(PMT02000DBParameter poParameter)
        {
            string lcMethodName = nameof(ProcessSubmitRedraft);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            DbConnection? loConn = null;
            DbCommand? loCommand = null;
            R_Db loDb;
            try
            {
                var lcQuery = "RSP_PM_UPDATE_HANDOVER";
                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID); ;
                loDb.R_AddCommandParameter(loCommand, "@CNEW_STATUS", DbType.String, 2, poParameter.VAR_NEW_STATUS);

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
            _logger.LogInfo("End process method GetHeader on Cls");
        }
        #endregion

    }
}
