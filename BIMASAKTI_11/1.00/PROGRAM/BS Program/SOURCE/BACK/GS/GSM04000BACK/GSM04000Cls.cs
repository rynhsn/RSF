using GSM04000Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Diagnostics;
using GSM04000Common.DTO_s;
using GSM04000Common.DTO_s.Print;

namespace GSM04000Back
{
    public class GSM04000Cls : R_BusinessObject<DepartmentDTO>
    {
        private RSP_GS_MAINTAIN_DEPARTMENTResources.Resources_Dummy_Class _rspDept = new();

        private RSP_GS_UPLOAD_DEPARTMENTResources.Resources_Dummy_Class _rspUploadDept = new();

        private readonly ActivitySource _activitySource;

        private LoggerGSM04000 _logger;

        public GSM04000Cls()
        {
            _logger = LoggerGSM04000.R_GetInstanceLogger();
            _activitySource = GSM04000Activity.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(DepartmentDTO poEntity)
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

                lcQuery = "RSP_GS_MAINTAIN_DEPARTMENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_NAME", DbType.String, int.MaxValue, poEntity.CDEPT_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, int.MaxValue, poEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CMANAGER_NAME", DbType.String, int.MaxValue,
                    poEntity.CMANAGER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CBRANCH_CODE", DbType.String, int.MaxValue, poEntity.CBRANCH_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LEVERYONE", DbType.Boolean, int.MaxValue, poEntity.LEVERYONE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL1", DbType.String, int.MaxValue, poEntity.CEMAIL1);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL2", DbType.String, int.MaxValue, poEntity.CEMAIL2);
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

        protected override DepartmentDTO R_Display(DepartmentDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            DepartmentDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_DEPT_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<DepartmentDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(DepartmentDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = "";
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_DEPARTMENT";

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }

                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_NAME", DbType.String, int.MaxValue, poNewEntity.CDEPT_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, int.MaxValue,
                    poNewEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CMANAGER_NAME", DbType.String, int.MaxValue,
                    poNewEntity.CMANAGER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CBRANCH_CODE", DbType.String, int.MaxValue,
                    poNewEntity.CBRANCH_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LEVERYONE", DbType.Boolean, int.MaxValue, poNewEntity.LEVERYONE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL1", DbType.String, int.MaxValue, poNewEntity.CEMAIL1);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL2", DbType.String, int.MaxValue, poNewEntity.CEMAIL2);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public List<DepartmentDTO> GetDepartmentList(GeneralParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<DepartmentDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_DEPT_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<DepartmentDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public void ActiveInactiveDept(ActiveInactiveParam poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_ACTIVE_INACTIVE_DEPT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue,
                    poEntity.LNEW_ACTIVE_STATUS);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public bool CheckIsUserDeptExist(DepartmentDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            bool loRtn = true;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("");
                string lcQuery =
                    $"SELECT TOP 1 1 FROM GSM_DEPT_USER (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID AND CDEPT_CODE = @CDEPT_CODE";
                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loResult = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = loResult == null ? false : true;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        public void DeleteAssignedUserDept(DepartmentDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "DELETE GSM_DEPT_USER WHERE CCOMPANY_ID = @CCOMPANY_ID AND CDEPT_CODE = @CDEPT_CODE";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //helper

        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ",
                parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }


        #region Group Print Methods
        /*
         * Get Base Header Logo Company
         * Digunakan untuk mendapatkan logo perusahaan
         * kemudian dikirim sebagai response ke controller dalam bentuk GSM04000PrintBaseHeaderLogoDTO
         */
        public GSM04000PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
        {
            using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany)); // Start activity
            var loEx = new R_Exception(); // Create new exception object
            GSM04000PrintBaseHeaderLogoDTO loResult = null; // Create new instance of GSM04000PrintBaseHeaderLogoDTO
            R_Db loDb = null; // Database object    
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loDb = new R_Db(); // Create new instance of R_Db
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType
                    .ReportConnectionString); // Get database connection
                loCmd = loDb.GetCommand(); // Get database command

                var lcQuery = $"SELECT dbo.RFN_GET_COMPANY_LOGO('{pcCompanyId}') as BLOGO"; // Query to get company logo
                loCmd.CommandText = lcQuery; // Set command text to query
                loCmd.CommandType = CommandType.Text; // Set command type to text

                _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false); // Execute the query
                loResult = R_Utility.R_ConvertTo<GSM04000PrintBaseHeaderLogoDTO>(loDataTable)
                    .FirstOrDefault(); // Convert the data table to GSM04000PrintBaseHeaderLogoDTO

                //ambil company name
                lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{pcCompanyId}'"; // Query to get company name
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _logger.LogDebug(lcQuery);
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult =
                    R_Utility.R_ConvertTo<GSM04000PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

                loResult!.CCOMPANY_NAME = loCompanyNameResult?.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
            }
            catch (Exception ex)
            {
                loEx.Add(ex); // Add the exception to the exception object
                _logger.LogError(loEx); // Log the exception
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }

                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }


            loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
            return loResult; // Return the company logo
        }

        #endregion
    }
}