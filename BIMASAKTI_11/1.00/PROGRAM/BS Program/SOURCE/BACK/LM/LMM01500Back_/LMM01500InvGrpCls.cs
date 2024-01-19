using System.Data;
using System.Data.Common;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_Storage;
using R_StorageCommon;

namespace LMM01500Back;

public class LMM01500InvGrpCls : R_BusinessObject<LMM01500InvGrpDTO>
{
    private LoggerLMM01500 _logger;

    public LMM01500InvGrpCls()
    {
        _logger = LoggerLMM01500.R_GetInstanceLogger();
    }

    protected override LMM01500InvGrpDTO R_Display(LMM01500InvGrpDTO poEntity)
    {
        R_Exception loEx = new();
        LMM01500InvGrpDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_INVOICE_GROUP";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 20, poEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CINVGRP_CODE" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMM01500InvGrpDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    protected override void R_Saving(LMM01500InvGrpDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loException = new R_Exception();
        R_Db loDb;
        DbCommand loCommand = null;
        DbConnection loConn = null;
        loDb = new R_Db();
        string lcStorageId = "";
        try
        {
            loConn = loDb.GetConnection();
            R_ExternalException.R_SP_Init_Exception(loConn);

            //SAVE DATA
            SaveDataSP(poNewEntity, poCRUDMode, loConn);
            //VALIDATE IS DATA EXIST

            if (poNewEntity.LINVOICE_TEMPLATE && !poNewEntity.LBY_DEPARTMENT)
            {
                
                lcStorageId= ValidateAlreadyExists(poNewEntity, loConn);
                poNewEntity.CSTORAGE_ID = lcStorageId;

                //SAVE TO CLOUD
                SaveFiletoCloud(poNewEntity, poCRUDMode, loConn);

            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
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
            if (loDb != null)
            {
                loDb = null;
            }
        }

        loException.ThrowExceptionIfErrors();
    }
    
    private void SaveDataSP(LMM01500InvGrpDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
    {
        var loEx = new R_Exception();
        string lcQuery;
        var loDb = new R_Db();
        DbConnection loConn;
        DbCommand loCmd;
        LMM01500InvGrpDTO loResult = null;
        var lcAction = "";

        try
        {
            lcAction = poCRUDMode switch
            {
                //Set Action 
                eCRUDMode.AddMode => "ADD",
                eCRUDMode.EditMode => "EDIT",
                _ => lcAction
            };

            loConn = poConnection;
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.StoredProcedure;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 50, poNewEntity.CINVGRP_NAME);
            loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 50, poNewEntity.CSEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 50, poNewEntity.CINVOICE_DUE_MODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 50, poNewEntity.CINVOICE_GROUP_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, 50, poNewEntity.IDUE_DAYS);
            loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, 50, poNewEntity.IFIXED_DUE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.ILIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IBEFORE_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, 50, poNewEntity.IAFTER_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SATURDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 50, poNewEntity.LDUE_DATE_TOLERANCE_SUNDAY);
            loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 50, poNewEntity.LUSE_STAMP);
            loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 50, poNewEntity.CSTAMP_ADD_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 50, poNewEntity.LBY_DEPARTMENT);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 50, poNewEntity.CINVOICE_TEMPLATE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poNewEntity.CBANK_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 50, poNewEntity.CBANK_ACCOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

            R_ExternalException.R_SP_Init_Exception(loConn);

            try
            {
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
        }
        loEx.ThrowExceptionIfErrors();
    }

    private string ValidateAlreadyExists(LMM01500InvGrpDTO poNewEntity, DbConnection poConnection)
    {
        var loEx = new R_Exception();
        string lcQuery;
        var loDb = new R_Db();
        DbConnection loConn;
        DbCommand loCmd;
        string? lcStorageId= null;

        try
        {
            loConn = poConnection;
            loCmd = loDb.GetCommand();

            lcQuery = "SELECT CSTORAGE_ID FROM LMM_INVGRP (NOLOCK) " +
                      "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                      "AND CPROPERTY_ID = @CPROPERTY_ID " +
                      "AND CINVGRP_CODE = @CINVGRP_CODE ";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
             var loTemp= R_Utility.R_ConvertTo<LMM01500InvGrpDTO>(loDataTable).FirstOrDefault();
             lcStorageId = loTemp.CSTORAGE_ID;
            if (lcStorageId == null)
            {
                lcStorageId = "";
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return lcStorageId;
    }

    private void SaveFiletoCloud(LMM01500InvGrpDTO poNewEntity, eCRUDMode poCRUDMode, DbConnection poConnection)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_SaveResult loSaveResult;
            R_ConnectionAttribute loConnAttr;

            try
            {
                loConn = poConnection;
                loConnAttr = loDb.GetConnectionAttribute();
                loCmd = loDb.GetCommand();

                if (string.IsNullOrEmpty(poNewEntity.CSTORAGE_ID))
                {
                    //Add and create Storage ID
                    R_AddParameter loAddParameter;

                    loAddParameter = new R_AddParameter()
                    {
                        StorageType = R_EStorageType.Cloud,
                        ProviderCloudStorage = R_EProviderForCloudStorage.azure,
                        FileName = poNewEntity.CFILE_NAME,
                        FileExtension = poNewEntity.CFILE_EXTENSION,
                        UploadData = poNewEntity.ODATA,
                        UserId = poNewEntity.CUSER_ID,
                        BusinessKeyParameter = new R_BusinessKeyParameter()
                        {
                            CCOMPANY_ID = poNewEntity.CCOMPANY_ID,
                            CDATA_TYPE = "STORAGE_DATA_TABLE",
                            CKEY01 = poNewEntity.CPROPERTY_ID,
                            CKEY02 = poNewEntity.CINVGRP_CODE,
                        }
                    };
                    loSaveResult = R_StorageUtility.AddFile(loAddParameter, loConn, loConnAttr.Provider);

                    //Set Storage ID CSTORAGE_ID
                    poNewEntity.CSTORAGE_ID = loSaveResult.StorageId;

                    lcQuery = "UPDATE LMM_INVGRP SET CSTORAGE_ID = @CSTORAGE_ID " +
                              "WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                              "AND CPROPERTY_ID = @CPROPERTY_ID " +
                              "AND CINVGRP_CODE = @CINVGRP_CODE ";
                    loCmd.CommandText = lcQuery;
                    loCmd.CommandType = CommandType.Text;

                    loDb.R_AddCommandParameter(loCmd, "@CSTORAGE_ID", DbType.String, 50, poNewEntity.CSTORAGE_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 50, poNewEntity.CINVGRP_CODE);

                    loDb.SqlExecNonQuery(loConn, loCmd, true);
                }
                else
                {
                    //UPDATE INVOICE FILE
                    R_UpdateParameter loUpdateParameter;

                    loUpdateParameter = new R_UpdateParameter()
                    {
                        StorageId = poNewEntity.CSTORAGE_ID,
                        UploadData = poNewEntity.ODATA,
                        UserId = poNewEntity.CUSER_ID,
                        //mostly tambahkan nama dan file extension baru
                        OptionalSaveAs = new R_UpdateParameter.OptionalSaveAsParameter()
                        {
                            FileName = poNewEntity.CFILE_NAME.Trim(),
                            FileExtension = poNewEntity.CFILE_EXTENSION.Trim()
                        }
                    };
                    R_StorageUtility.UpdateFile(loUpdateParameter, loConn, loConnAttr.Provider);

                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    
    protected override void R_Deleting(LMM01500InvGrpDTO poEntity)
    {
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";
        R_DeleteParameter loDeleteParameter;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            if (!String.IsNullOrEmpty(poEntity.CSTORAGE_ID))
            {
                loDeleteParameter = new R_DeleteParameter()
                {
                    StorageId = poEntity.CSTORAGE_ID,
                    UserId = poEntity.CUSER_ID
                };
                R_StorageUtility.DeleteFile(loDeleteParameter, "R_DefaultConnectionString");
            }

            lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 8, poEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 100, poEntity.CINVGRP_NAME);
            loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 6, poEntity.CSEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 2, poEntity.CINVOICE_DUE_MODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 2, poEntity.CINVOICE_GROUP_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, Int32.MaxValue, poEntity.IDUE_DAYS);
            loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, Int32.MaxValue, poEntity.IFIXED_DUE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poEntity.ILIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poEntity.IBEFORE_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poEntity.IAFTER_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 1, poEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 1, poEntity.LDUE_DATE_TOLERANCE_SATURDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 1, poEntity.LDUE_DATE_TOLERANCE_SUNDAY);
            loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 1, poEntity.LUSE_STAMP);
            loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 20, poEntity.CSTAMP_ADD_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, poEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 1, poEntity.LBY_DEPARTMENT);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 100, poEntity.CINVOICE_TEMPLATE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 8, poEntity.CBANK_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 20, poEntity.CBANK_ACCOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CINVGRP_CODE" or
                        "@CINVGRP_NAME" or
                        "@CSEQUENCE" or
                        "@LACTIVE" or
                        "@CINVOICE_DUE_MODE" or
                        "@CINVOICE_GROUP_MODE" or
                        "@IDUE_DAYS" or
                        "@IFIXED_DUE_DATE" or
                        "@ILIMIT_INVOICE_DATE" or
                        "@IBEFORE_LIMIT_INVOICE_DATE" or
                        "@IAFTER_LIMIT_INVOICE_DATE" or
                        "@LDUE_DATE_TOLERANCE_HOLIDAY" or
                        "@LDUE_DATE_TOLERANCE_SATURDAY" or
                        "@LDUE_DATE_TOLERANCE_SUNDAY" or
                        "@LUSE_STAMP" or
                        "@CSTAMP_ADD_ID" or
                        "@CDESCRIPTION" or
                        "@LBY_DEPARTMENT" or
                        "@CINVOICE_TEMPLATE" or
                        "@CDEPT_CODE" or
                        "@CBANK_CODE" or
                        "@CBANK_ACCOUNT" or
                        "@CACTION" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            try
            {
                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
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

    public List<LMM01500InvGrpGridDTO> GetInvoiceGroupList(LMM01500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<LMM01500InvGrpGridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_INVOICE_GROUP_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMM01500InvGrpGridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}