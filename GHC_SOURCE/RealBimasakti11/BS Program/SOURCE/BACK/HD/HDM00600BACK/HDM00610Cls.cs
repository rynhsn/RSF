using HDM00600COMMON;
using HDM00600COMMON.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace HDM00600BACK
{
    public class HDM00610Cls : R_BusinessObject<PricelistDTO>//crud for next priing
    {
        //var & const
        private RSP_HD_MAINTAIN_PRICELISTResources.Resources_Dummy_Class _rsp = new();
        private readonly ActivitySource _activitySource;
        private PricelistMaster_Logger _logger;
        public HDM00610Cls()
        {
            _logger = PricelistMaster_Logger.R_GetInstanceLogger();
            _activitySource = PricelistMaster_Activity.R_GetInstanceActivitySource();
        }

        //methods
        protected override void R_Deleting(PricelistDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_HD_MAINTAIN_PRICELIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "CPRICELIST_ID", DbType.String, int.MaxValue, poEntity.CPRICELIST_ID);
                loDb.R_AddCommandParameter(loCmd, "CPRICELIST_NAME", DbType.String, int.MaxValue, poEntity.CPRICELIST_NAME);
                loDb.R_AddCommandParameter(loCmd, "CCHARGES_ID", DbType.String, int.MaxValue, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "CUNIT", DbType.String, int.MaxValue, poEntity.CUNIT);
                loDb.R_AddCommandParameter(loCmd, "CCURRENCY_CODE", DbType.String, int.MaxValue, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "IPRICE", DbType.String, int.MaxValue, poEntity.IPRICE);
                loDb.R_AddCommandParameter(loCmd, "CDESCRIPTION", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "CVALID_ID", DbType.String, int.MaxValue, poEntity.CVALID_ID);
                loDb.R_AddCommandParameter(loCmd, "CSTART_DATE", DbType.String, int.MaxValue, poEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, "DELETE");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    ShowLogError(loEx);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
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

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        protected override PricelistDTO R_Display(PricelistDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            PricelistDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_HD_GET_PRICELIST_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPRICELIST_ID", DbType.String, int.MaxValue, poEntity.CPRICELIST_ID);
                loDB.R_AddCommandParameter(loCmd, "@CVALID_ID", DbType.String, int.MaxValue, poEntity.CVALID_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poEntity.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PricelistDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        protected override void R_Saving(PricelistDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = "";
            PricelistDTO loNewData = poNewEntity;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }
                lcQuery = "RSP_HD_MAINTAIN_PRICELIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPRICELIST_ID", DbType.String, int.MaxValue, poNewEntity.CPRICELIST_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPRICELIST_NAME", DbType.String, int.MaxValue, poNewEntity.CPRICELIST_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, int.MaxValue, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, int.MaxValue, poNewEntity.CUNIT);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, int.MaxValue, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@IPRICE", DbType.Int32, int.MaxValue, poNewEntity.IPRICE);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CVALID_ID", DbType.String, int.MaxValue, poNewEntity.CVALID_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, int.MaxValue, poNewEntity.CSTART_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    var loTempResult = R_Utility.R_ConvertTo<PricelistDTO>(loDataTable).FirstOrDefault();
                    poNewEntity.CVALID_ID = loTempResult.CVALID_ID??"";
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
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
            loEx.ThrowExceptionIfErrors();
        }

        //helper
        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }
        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }
    }
}
