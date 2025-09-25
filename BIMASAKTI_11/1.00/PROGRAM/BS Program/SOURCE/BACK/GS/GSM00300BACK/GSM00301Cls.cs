using System;
using System.Collections.Generic;
using GSM00300COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace GSM00300BACK
{
    #region "Non Async Version"
    // public class GSM00301Cls : R_BusinessObject<TaxInfoDTO>
    // {
    //     //var & constructor
    //     private RSP_GS_MAINTAIN_TAX_INFOResources.Resources_Dummy_Class _rsp = new();
    //     private readonly ActivitySource _activitySource;
    //     private LoggerGSM00300 _logger;
    //     public GSM00301Cls()
    //     {
    //         _logger = LoggerGSM00300.R_GetInstanceLogger();
    //         _activitySource = GSM00300Activity.R_GetInstanceActivitySource();
    //     }
    //
    //     //method
    //     public List<GSBCodeInfoDTO> GetRftGSBCodeInfoList(GSBCodeInfoParam poParam)
    //     {
    //         using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
    //         R_Exception loEx = new();
    //         List<GSBCodeInfoDTO> loRtn = null;
    //         R_Db loDB;
    //         DbConnection loConn;
    //         DbCommand loCmd;
    //         string lcQuery;
    //         try
    //         {
    //             loDB = new R_Db();
    //             loConn = loDB.GetConnection();
    //             loCmd = loDB.GetCommand();
    //
    //             lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO(@CLASS_APPLICATION, @CCOMPANY_ID, @CLASS_ID, @REC_ID_LIST, @CLANG_ID)";
    //             loCmd.CommandType = CommandType.Text;
    //             loCmd.CommandText = lcQuery;
    //
    //             loDB.R_AddCommandParameter(loCmd, "@CLASS_APPLICATION", DbType.String, int.MaxValue, poParam.CLASS_APPLICATION);
    //             loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
    //             loDB.R_AddCommandParameter(loCmd, "@CLASS_ID", DbType.String, int.MaxValue, poParam.CLASS_ID);
    //             loDB.R_AddCommandParameter(loCmd, "@REC_ID_LIST", DbType.String, int.MaxValue, poParam.REC_ID_LIST);
    //             loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);
    //
    //             ShowLogDebug(lcQuery, loCmd.Parameters);
    //             var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
    //             loRtn = R_Utility.R_ConvertTo<GSBCodeInfoDTO>(loRtnTemp).ToList();
    //         }
    //         catch (Exception ex)
    //         {
    //             loEx.Add(ex);
    //             ShowLogError(loEx);
    //         }
    //         loEx.ThrowExceptionIfErrors();
    //         return loRtn;
    //     }
    //     protected override void R_Deleting(TaxInfoDTO poEntity)
    //     {
    //         throw new NotImplementedException();
    //     }
    //     protected override TaxInfoDTO R_Display(TaxInfoDTO poEntity)
    //     {
    //         using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
    //         R_Exception loEx = new R_Exception();
    //         TaxInfoDTO loRtn = null;
    //         R_Db loDB;
    //         DbConnection loConn;
    //         DbCommand loCmd;
    //         string lcQuery;
    //         try
    //         {
    //             loDB = new R_Db();
    //             loConn = loDB.GetConnection();
    //             loCmd = loDB.GetCommand();
    //
    //             lcQuery = "RSP_GS_GET_COMPANY_TAX_INFO";
    //             loCmd.CommandType = CommandType.StoredProcedure;
    //             loCmd.CommandText = lcQuery;
    //
    //             loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
    //             loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);
    //
    //             ShowLogDebug(lcQuery, loCmd.Parameters);
    //             var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
    //             loRtn = R_Utility.R_ConvertTo<TaxInfoDTO>(loRtnTemp).FirstOrDefault();
    //         }
    //         catch (Exception ex)
    //         {
    //             loEx.Add(ex);
    //             ShowLogError(loEx);
    //         }
    //         loEx.ThrowExceptionIfErrors();
    //         return loRtn;
    //     }
    //     protected override void R_Saving(TaxInfoDTO poNewEntity, eCRUDMode poCRUDMode)
    //     {
    //         using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
    //         R_Exception loEx = new R_Exception();
    //         string lcQuery;
    //         R_Db loDb;
    //         DbCommand loCmd;
    //         DbConnection loConn = null;
    //         string lcAction = "";
    //         try
    //         {
    //             loDb = new R_Db();
    //             loConn = loDb.GetConnection();
    //             loCmd = loDb.GetCommand();
    //             R_ExternalException.R_SP_Init_Exception(loConn);
    //
    //             lcQuery = "RSP_GS_MAINTAIN_TAX_INFO";
    //
    //             switch (poCRUDMode)
    //             {
    //                 case eCRUDMode.EditMode:
    //                     lcAction = "EDIT";
    //                     break;
    //             }
    //             loCmd.CommandType = CommandType.StoredProcedure;
    //             loCmd.CommandText = lcQuery;
    //             loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poNewEntity.CCOMPANY_ID);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE ", DbType.String, int.MaxValue, poNewEntity.CTAX_TYPE);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_ID ", DbType.String, int.MaxValue, poNewEntity.CTAX_ID);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME ", DbType.String, int.MaxValue, poNewEntity.CTAX_NAME);
    //             loDb.R_AddCommandParameter(loCmd, "@CID_TYPE ", DbType.String, int.MaxValue, poNewEntity.CID_TYPE);
    //             loDb.R_AddCommandParameter(loCmd, "@CID_NO ", DbType.String, int.MaxValue, poNewEntity.CID_NO);
    //             loDb.R_AddCommandParameter(loCmd, "@CID_EXPIRED_DATE ", DbType.String, int.MaxValue, poNewEntity.CID_EXPIRED_DATE);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_ADDRESS ", DbType.String, int.MaxValue, poNewEntity.CTAX_ADDRESS);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE1 ", DbType.String, int.MaxValue, poNewEntity.CTAX_PHONE1);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE2 ", DbType.String, int.MaxValue, poNewEntity.CTAX_PHONE2);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_EMAIL ", DbType.String, int.MaxValue, poNewEntity.CTAX_EMAIL);
    //             loDb.R_AddCommandParameter(loCmd, "@CTAX_EMAIL2 ", DbType.String, int.MaxValue, poNewEntity.CTAX_EMAIL2);
    //             loDb.R_AddCommandParameter(loCmd, "@CACTION ", DbType.String, int.MaxValue, lcAction);
    //             loDb.R_AddCommandParameter(loCmd, "@CUSER_ID ", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);
    //
    //             try
    //             {
    //                 ShowLogDebug(lcQuery, loCmd.Parameters);
    //                 loDb.SqlExecNonQuery(loConn, loCmd, false);
    //             }
    //             catch (Exception ex)
    //             {
    //                 loEx.Add(ex);
    //             }
    //             loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
    //         }
    //         catch (Exception ex)
    //         {
    //             loEx.Add(ex);
    //             ShowLogError(loEx);
    //         }
    //         finally
    //         {
    //             if (loConn != null)
    //             {
    //                 if (loConn.State != ConnectionState.Closed)
    //                 {
    //                     loConn.Close();
    //                 }
    //
    //                 loConn.Dispose();
    //             }
    //         }
    //
    //         loEx.ThrowExceptionIfErrors();
    //     }
    //
    //     //helper
    //     private void ShowLogDebug(string query, DbParameterCollection parameters)
    //     {
    //         var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
    //         _logger.LogDebug($"EXEC {query} {paramValues}");
    //     }
    //     private void ShowLogError(Exception ex)
    //     {
    //         _logger.LogError(ex);
    //     }
    // }
    #endregion

    #region "Async Version"
    public class GSM00301Cls : R_BusinessObjectAsync<TaxInfoDTO>
    {
        //var & constructor
        private RSP_GS_MAINTAIN_TAX_INFOResources.Resources_Dummy_Class _rsp = new();
        private readonly ActivitySource _activitySource;
        private LoggerGSM00300 _logger;
        public GSM00301Cls()
        {
            _logger = LoggerGSM00300.R_GetInstanceLogger();
            _activitySource = GSM00300Activity.R_GetInstanceActivitySource();
        }

        //method
        public async Task<List<GSBCodeInfoDTO>> GetRftGSBCodeInfoListAsync(GSBCodeInfoParam poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<GSBCodeInfoDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = await loDB.GetConnectionAsync();
                loCmd = loDB.GetCommand();

                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO(@CLASS_APPLICATION, @CCOMPANY_ID, @CLASS_ID, @REC_ID_LIST, @CLANG_ID)";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CLASS_APPLICATION", DbType.String, int.MaxValue, poParam.CLASS_APPLICATION);
                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLASS_ID", DbType.String, int.MaxValue, poParam.CLASS_ID);
                loDB.R_AddCommandParameter(loCmd, "@REC_ID_LIST", DbType.String, int.MaxValue, poParam.REC_ID_LIST);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = await loDB.SqlExecQueryAsync(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<GSBCodeInfoDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        protected override async Task R_DeletingAsync(TaxInfoDTO poEntity)
        {
            throw new NotImplementedException();
        }
        protected override async Task<TaxInfoDTO> R_DisplayAsync(TaxInfoDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            TaxInfoDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = await loDB.GetConnectionAsync();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_COMPANY_TAX_INFO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = await loDB.SqlExecQueryAsync(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<TaxInfoDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        protected override async Task R_SavingAsync(TaxInfoDTO poNewEntity, eCRUDMode poCRUDMode)
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
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_TAX_INFO";

                switch (poCRUDMode)
                {
                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE ", DbType.String, int.MaxValue, poNewEntity.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID ", DbType.String, int.MaxValue, poNewEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME ", DbType.String, int.MaxValue, poNewEntity.CTAX_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CID_TYPE ", DbType.String, int.MaxValue, poNewEntity.CID_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CID_NO ", DbType.String, int.MaxValue, poNewEntity.CID_NO);
                loDb.R_AddCommandParameter(loCmd, "@CID_EXPIRED_DATE ", DbType.String, int.MaxValue, poNewEntity.CID_EXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ADDRESS ", DbType.String, int.MaxValue, poNewEntity.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE1 ", DbType.String, int.MaxValue, poNewEntity.CTAX_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE2 ", DbType.String, int.MaxValue, poNewEntity.CTAX_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EMAIL ", DbType.String, int.MaxValue, poNewEntity.CTAX_EMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EMAIL2 ", DbType.String, int.MaxValue, poNewEntity.CTAX_EMAIL2);
                loDb.R_AddCommandParameter(loCmd, "@CACTION ", DbType.String, int.MaxValue, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID ", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
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
    #endregion
}
