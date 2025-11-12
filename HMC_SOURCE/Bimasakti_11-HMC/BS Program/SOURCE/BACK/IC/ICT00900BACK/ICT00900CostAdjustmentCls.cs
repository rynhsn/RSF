using ICT00900COMMON.Logs;
using ICT00900COMMON.Utility_DTO;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT00900COMMON.Param;
using ICT00900COMMON.DTO;
using R_CommonFrontBackAPI;
using ICT00900COMMON;

namespace ICT00900BACK
{
    public class ICT00900CostAdjustmentCls : R_BusinessObjectAsync<ICT00900AjustmentDetailDTO>
    {
        private LoggerICT00900 _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_IC_MAINTAIN_ADJUSTMENTResources.Resources_Dummy_Class _oRSPMaintainAdj = new();
        private readonly RSP_IC_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class _oRSPChangeStatus = new();
        private readonly RSP_ICT00900_SUBMITResources.Resources_Dummy_Class _oRSPSubmit = new();

        public ICT00900CostAdjustmentCls()
        {
            _logger = LoggerICT00900.R_GetInstanceLogger();
            _activitySource = ICT00900Activity.R_GetInstanceActivitySource();
        }
        public async Task<VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODEDb(ICT00900ParameterAdjustment poParameter)
        {
            string? lcMethod = nameof(GetVAR_GSM_TRANSACTION_CODEDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            VarGsmTransactionCodeDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                DbConnection? loConn = await loDb.GetConnectionAsync();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, "505010");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() != null
                    ? R_Utility.R_ConvertTo<VarGsmTransactionCodeDTO>(loDataTable).FirstOrDefault() : new VarGsmTransactionCodeDTO();
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
        public async Task<VarGsmCompanyInfoDTO> GetVAR_GSM_COMPANY_INFODb(ICT00900ParameterAdjustment poParameter)
        {
            string? lcMethod = nameof(GetVAR_GSM_COMPANY_INFODb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            VarGsmCompanyInfoDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_GET_COMPANY_INFO";
                DbConnection? loConn = await loDb.GetConnectionAsync();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<VarGsmCompanyInfoDTO>(loDataTable).FirstOrDefault() != null
                    ? R_Utility.R_ConvertTo<VarGsmCompanyInfoDTO>(loDataTable).FirstOrDefault() : new VarGsmCompanyInfoDTO();
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
        public async Task<List<PropertyDTO>> GetPropertyListDb(PropertyParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetPropertyListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loReturn = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PropertyDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn;
        }
        public async Task<List<CurrencyDTO>> GetCurrencyListDb(PropertyParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetCurrencyListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<CurrencyDTO>? loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_CURRENCY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<CurrencyDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loReturn;
        }
        protected override async Task<ICT00900AjustmentDetailDTO> R_DisplayAsync(ICT00900AjustmentDetailDTO poEntity)
        {
            string? lcMethod = nameof(R_DisplayAsync);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            ICT00900AjustmentDetailDTO? loRtn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                loDb = new();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_IC_GET_ADJUSTMENT_DETAIL";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loProfileDataTable = await loDb.SqlExecQueryAsync(await loDb.GetConnectionAsync(), loCommand, true);
                loRtn = R_Utility.R_ConvertTo<ICT00900AjustmentDetailDTO>(loProfileDataTable).FirstOrDefault()!;
                _logger.LogDebug("{@ObjectReturn}", loRtn);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loRtn!;
        }
        protected override async Task R_SavingAsync(ICT00900AjustmentDetailDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            string lcMethodName = nameof(R_SavingAsync);
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
                loConn = await loDb.GetConnectionAsync();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                    default:
                        break;
                }

                lcQuery = "RSP_IC_MAINTAIN_ADJUSTMENT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 20, poNewEntity.CTRANS_CODE); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 40, poNewEntity.CREF_NO??""); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE); //
                loDb.R_AddCommandParameter(loCommand, "@CADJUST_METHOD", DbType.String, 20, poNewEntity.CADJUST_METHOD); //
                loDb.R_AddCommandParameter(loCommand, "@CPRODUCT_ID", DbType.String, 20, poNewEntity.CPRODUCT_ID); //
                loDb.R_AddCommandParameter(loCommand, "@NADJUST_AMOUNT", DbType.Decimal, 18, poNewEntity.NADJUST_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES ", DbType.String, 255, poNewEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CALLOC_ID", DbType.String, 20, poNewEntity.CALLOC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NLBASE_RATE", DbType.Decimal, 13, poNewEntity.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCommand, "@NLCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCommand, "@NBBASE_RATE", DbType.Decimal, 13, poNewEntity.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCommand, "@NBCURRENCY_RATE", DbType.Decimal, 13, poNewEntity.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCommand, "@NTRANS_AMOUNT", DbType.Decimal, 13, poNewEntity.NTRANS_AMOUNT);




                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                   var loDataTable =  await loDb.SqlExecQueryAsync(loConn, loCommand, false);
                    var loData = R_Utility.R_ConvertTo<ICT00900AjustmentDetailDTO>(loDataTable).FirstOrDefault()!;
                    if (poNewEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREF_NO = loData.CREF_NO;
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
                    }
                    loConn.Dispose();
                }
            }
            loException.ThrowExceptionIfErrors();
        }
        protected override async Task R_DeletingAsync(ICT00900AjustmentDetailDTO poEntity)
        {
            string lcMethodName = nameof(R_DeletingAsync);
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
                loConn = await loDb.GetConnectionAsync();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcAction = "DELETE";

                lcQuery = "RSP_IC_MAINTAIN_ADJUSTMENT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 20, poEntity.CTRANS_CODE); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 40, poEntity.CREF_NO); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, poEntity.CREF_DATE); //
                loDb.R_AddCommandParameter(loCommand, "@CADJUST_METHOD", DbType.String, 20, poEntity.CADJUST_METHOD); //
                loDb.R_AddCommandParameter(loCommand, "@CPRODUCT_ID", DbType.String, 20, poEntity.CPRODUCT_ID); //
                loDb.R_AddCommandParameter(loCommand, "@NADJUST_AMOUNT", DbType.Decimal, 18, poEntity.NADJUST_AMOUNT);
                loDb.R_AddCommandParameter(loCommand, "@CNOTES ", DbType.String, 255, poEntity.CNOTES);
                loDb.R_AddCommandParameter(loCommand, "@CALLOC_ID", DbType.String, 20, poEntity.CALLOC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
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
        public async Task<List<ICT00900AdjustmentDTO>> GetAdjustmentList(ICT00900ParameterAdjustment poParameter)
        {
            string? lcMethodName = nameof(GetAdjustmentList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<ICT00900AdjustmentDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = await loDb.GetConnectionAsync();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_IC_GET_ADJUSTMENT_LIST ";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<ICT00900AdjustmentDTO>(loDataTable).ToList();
                _logger.LogDebug("{@ObjectReturn} ", loReturn);
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
        public async Task<bool> ChangeStatus(ICT00900ParameterChangeStatusDTO poEntity)
        {
            string? lcMethodName = nameof(ChangeStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            bool llReturn = false;
            R_Exception loException = new R_Exception();
            string lcQuery;
            DbCommand loCommand = null;
            R_Db loDb;
            DbConnection loConn = null;
            try
            {
                loDb = new();
                loConn = await loDb.GetConnectionAsync();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_IC_UPDATE_TRANS_HD_STATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 20, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 40, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CNEW_STATUS", DbType.String, 2, poEntity.CSTATUS);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                try
                {
                    var loDataTable = await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
                    llReturn = true;
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    llReturn = false;
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                llReturn = false;
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                }
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loException.ThrowExceptionIfErrors();
            return llReturn;
        }
        public async Task<bool> SubmitCostAdjustment(ICT00900ParameterChangeStatusDTO poEntity)
        {
            string? lcMethodName = nameof(SubmitCostAdjustment);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            bool llReturn = false;

            R_Exception loException = new R_Exception();
            string lcQuery;
            DbCommand loCommand = null;
            R_Db loDb;
            DbConnection loConn = null;
            try
            {
                loDb = new();
                loConn = await loDb.GetConnectionAsync();
                R_ExternalException.R_SP_Init_Exception(loConn);
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_ICT00900_SUBMIT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                try
                {
                    var loDataTable = await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
                    llReturn = true;
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    llReturn = false;
                    _logger.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                llReturn = false;
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
                if (loCommand != null)
                {
                    loCommand.Dispose();
                }
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            loException.ThrowExceptionIfErrors();
            return llReturn;
        }
        public async Task<ICT00900AjustmentDetailDTO> GetProdBalanceInfoDb(ICT00900AjustmentDetailDTO poParameter)
        {
            string? lcMethod = nameof(GetProdBalanceInfoDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            ICT00900AjustmentDetailDTO? loReturn = null;
            R_Db loDb;
            try
            {
                loDb = new(); 
                var loCommand = loDb.GetCommand();
                string lcQuery = "RSP_IC_GET_PROD_BALANCE_INFO";
                DbConnection? loConn = await loDb.GetConnectionAsync();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CPRODUCT_ID", DbType.String, 20, poParameter.CPRODUCT_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<ICT00900AjustmentDetailDTO>(loDataTable).FirstOrDefault() != null
                    ? R_Utility.R_ConvertTo<ICT00900AjustmentDetailDTO>(loDataTable).FirstOrDefault() : new ICT00900AjustmentDetailDTO();
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
        public async Task<ICSystemParameterDTO> GetICSyctemParameter(BaseDTO poParameter)
        {
            string? lcMethod = nameof(GetICSyctemParameter);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            ICSystemParameterDTO? loReturn = null;
            R_Db loDb;
            try
            {
                loDb = new(); 
                var loCommand = loDb.GetCommand();
                string lcQuery = "RSP_IC_GET_SYSTEM_PARAM";
                DbConnection? loConn = await loDb.GetConnectionAsync();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 20, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<ICSystemParameterDTO>(loDataTable).FirstOrDefault() != null
                    ? R_Utility.R_ConvertTo<ICSystemParameterDTO>(loDataTable).FirstOrDefault() : new ICSystemParameterDTO();
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
        public async Task<LastCurrencyRateDTO> GetLastCurrency(LastCurrencyRateDTO poParameter)
        {
            string? lcMethod = nameof(GetLastCurrency);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            LastCurrencyRateDTO? loReturn = null;
            R_Db loDb;
            try
            {
                loDb = new();
                var loCommand = loDb.GetCommand();
                string lcQuery = "RSP_GS_GET_LAST_CURRENCY_RATE";
                DbConnection? loConn = await loDb.GetConnectionAsync();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 20, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATETYPE_CODE", DbType.String, 20, poParameter.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CRATE_DATE", DbType.String, 20, poParameter.CRATE_DATE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<LastCurrencyRateDTO>(loDataTable).FirstOrDefault() != null
                    ? R_Utility.R_ConvertTo<LastCurrencyRateDTO>(loDataTable).FirstOrDefault() : new LastCurrencyRateDTO();
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
    }
}
