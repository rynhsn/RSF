using LMM01500COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Input;
using R_Storage;
using R_StorageCommon;

namespace LMM01500BACK
{
    public class LMM01510Cls : R_BusinessObject<LMM01511DTO>
    {
        private LoggerLMM01510 _Logger;

        public LMM01510Cls()
        {
            _Logger = LoggerLMM01510.R_GetInstanceLogger();
        }

        public List<LMM01510DTO> GetAllTemplateAndBankAccount(LMM01510DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01510DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_INVGRP_DEPT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_INVGRP_DEPT_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01510DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_DeleteParameter loDeleteParameter;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                if (String.IsNullOrEmpty(poEntity.CINVOICE_TEMPLATE) == false)
                {
                    loDeleteParameter = new R_DeleteParameter()
                    {
                        StorageId = poEntity.CINVOICE_TEMPLATE,
                        UserId = poEntity.CUSER_ID
                    };
                    R_StorageUtility.DeleteFile(loDeleteParameter, loConn);
                }

                // set action delete
                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT {@poParameter}", loDbParam);

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
                _Logger.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
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
            loEx.ThrowExceptionIfErrors();
        }

        protected override LMM01511DTO R_Display(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01511DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_ReadResult loReadResult = null;
            R_ReadParameter loReadParameter;

            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_INVGRP_DEPT_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
               .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_INVGRP_DEPT_DETAIL {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<LMM01511DTO>(loDataTable).FirstOrDefault();

                //Get Storage
                if (String.IsNullOrEmpty(poEntity.CINVOICE_TEMPLATE) == false)
                {
                    loReadParameter = new R_ReadParameter()
                    {
                        StorageId = poEntity.CINVOICE_TEMPLATE
                    };

                    loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                    loResult.Data = loReadResult.Data;
                    loResult.FileExtension = loReadResult.FileExtension;
                    loResult.FileName = loReadResult.FileName;
                    loResult.FileNameExtension = loReadResult.FileName + loReadResult.FileExtension;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
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
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM01511DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            R_ConnectionAttribute loConnAttr;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_SaveResult loSaveResult;

            try
            {
                loConn = loDb.GetConnection();
                loConnAttr = loDb.GetConnectionAttribute();
                loCmd = loDb.GetCommand();


                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    //Set Action 
                    poNewEntity.CACTION = "ADD";

                    if (String.IsNullOrEmpty(poNewEntity.CINVOICE_TEMPLATE) == false)
                    {
                        //Add and create Storage ID
                        R_AddParameter loAddParameter;

                        loAddParameter = new R_AddParameter()
                        {
                            StorageType = R_EStorageType.Cloud,
                            ProviderCloudStorage = R_EProviderForCloudStorage.azure,
                            FileName = poNewEntity.FileName,
                            FileExtension = poNewEntity.FileExtension,
                            UploadData = poNewEntity.Data,
                            UserId = poNewEntity.CUSER_ID,
                            BusinessKeyParameter = new R_BusinessKeyParameter()
                            {
                                CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                                CDATA_TYPE = "TestStorage",
                                CKEY01 = poNewEntity.CPROPERTY_ID,
                                CKEY02 = poNewEntity.CINVGRP_CODE,
                                CKEY03 = poNewEntity.CDEPT_CODE
                            }
                        };
                        loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                        //Set Storage ID CINVOICE_TEMPLATE
                        poNewEntity.CINVOICE_TEMPLATE = loSaveResult.StorageId;
                    }
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    //Set Action 
                    poNewEntity.CACTION = "EDIT";

                    if (String.IsNullOrEmpty(poNewEntity.CINVOICE_TEMPLATE) == false)
                    {
                        //Get and Update Storage ID
                        R_UpdateParameter loUpdateParameter;

                        loUpdateParameter = new R_UpdateParameter()
                        {
                            StorageId = poNewEntity.CINVOICE_TEMPLATE,
                            UploadData = poNewEntity.Data,
                            UserId = poNewEntity.CUSER_ID,
                            OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                            {
                                FileName = poNewEntity.FileName,
                                FileExtension = poNewEntity.FileExtension,
                            }
                        };
                        loSaveResult = R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);

                        //Set Storage ID CINVOICE_TEMPLATE
                        poNewEntity.CINVOICE_TEMPLATE = loSaveResult.StorageId;
                    }
                }

                lcQuery = "RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poNewEntity.CINVOICE_TEMPLATE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poNewEntity.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poNewEntity.CBANK_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_INVGRP_BANK_ACC_DEPT {@poParameter}", loDbParam);

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
                _Logger.LogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
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

            loEx.ThrowExceptionIfErrors();
        }
    }
}
