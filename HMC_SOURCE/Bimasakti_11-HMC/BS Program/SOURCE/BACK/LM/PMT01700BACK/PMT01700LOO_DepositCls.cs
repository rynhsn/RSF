using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
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
using PMT01700COMMON.Logs;
using PMT01700COMMON.DTO.Utilities.Response;
using PMT01700COMMON.DTO.Utilities.ParamDb;
using PMT01700COMMON.DTO.Utilities;

namespace PMT01700BACK
{
    public class PMT01700LOO_DepositCls : R_BusinessObject<PMT01700LOO_Deposit_DepositDetailDTO>
    {
        private readonly LoggerPMT01700? _logger;
        private readonly ActivitySource _activitySource;
        private readonly RSP_PM_MAINTAIN_AGREEMENT_DEPOSITResources.Resources_Dummy_Class _oRSP = new();

        public PMT01700LOO_DepositCls()
        {
            _logger = LoggerPMT01700.R_GetInstanceLogger();
            _activitySource = PMT01700Activity.R_GetInstanceActivitySource();
        }
        protected override void R_Deleting(PMT01700LOO_Deposit_DepositDetailDTO poEntity)
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

                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, ""); //Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean, 2, false);
                loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, "");
                // loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_TYPE", DbType.String, 2, poEntity.CDEPOSIT_JRNGRP_TYPE);
                //loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_CODE", DbType.String, 8, poEntity.CDEPOSIT_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_ID", DbType.String, 20, poEntity.CDEPOSIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_DATE", DbType.String, 8, poEntity.CDEPOSIT_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NDEPOSIT_AMT", DbType.Decimal, 255, poEntity.NDEPOSIT_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, Int32.MaxValue, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

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

        protected override PMT01700LOO_Deposit_DepositDetailDTO R_Display(PMT01700LOO_Deposit_DepositDetailDTO poEntity)
        {
            string? lcMethodName = nameof(R_Display);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            PMT01700LOO_Deposit_DepositDetailDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_DEPOSIT_DT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE); //
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO); //
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO ", DbType.String, 20, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                var loTempReturn = R_Utility.R_ConvertTo<PMT01700LOO_Deposit_DepositDetailDTO>(loDataTable).ToList().FirstOrDefault();
                loReturn = loTempReturn != null ? loTempReturn : new();
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
        }

        protected override void R_Saving(PMT01700LOO_Deposit_DepositDetailDTO poNewEntity, eCRUDMode poCRUDMode)
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
                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }
                lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO ?? "");
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, ""); //Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, "");//Blank, nanti kalo error konfirmasi ke Pak IB (30 Mei 2024)
                loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean, 2, false);
                loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, "");
                // loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_JRNGRP_TYPE", DbType.String, 2, poNewEntity.CDEPOSIT_JRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_ID", DbType.String, 20, poNewEntity.CDEPOSIT_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_DATE", DbType.String, 8, poNewEntity.CDEPOSIT_DATE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCommand, "@NDEPOSIT_AMT", DbType.Decimal, int.MaxValue, poNewEntity.NDEPOSIT_AMT);
                loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION", DbType.String, Int32.MaxValue, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTAX_ID ", DbType.String, 20, poNewEntity.CTAX_ID ?? "");

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                    var loEntity = R_Utility.R_ConvertTo<PMT01700LOO_Deposit_DepositDetailDTO>(loDataTable).FirstOrDefault()!;

                    if (loEntity != null && poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CSEQ_NO = loEntity.CSEQ_NO ?? "";
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
        public PMT01700LOO_Deposit_DepositHeaderDTO DepositHeader(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            string? lcMethodName = nameof(DepositHeader);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            PMT01700LOO_Deposit_DepositHeaderDTO? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                var loTempReturn = R_Utility.R_ConvertTo<PMT01700LOO_Deposit_DepositHeaderDTO>(loDataTable).ToList().FirstOrDefault();
                loReturn = loTempReturn != null ? loTempReturn : new();
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
        }
        public List<PMT01700LOO_Deposit_DepositListDTO> DepositListDb(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            string? lcMethodName = nameof(DepositListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<PMT01700LOO_Deposit_DepositListDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_PM_GET_AGREEMENT_DEPOSIT_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
                loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 10,"");
                loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID ", DbType.String, 30,"" );
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700LOO_Deposit_DepositListDTO>(loDataTable).ToList();

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
        }
        public List<PMT01700ResponseCurrencyParameterDTO> GetComboBoxDataCurrencyDb(PMT01700BaseParameterDTO poParam)
        {
            string? lcMethod = nameof(GetComboBoxDataCurrencyDb);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01700ResponseCurrencyParameterDTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new(); loCommand = loDb.GetCommand();
                lcQuery = "RSP_GS_GET_CURRENCY_LIST";
                DbConnection? loConn = loDb.GetConnection();
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMT01700ResponseCurrencyParameterDTO>(loDataTable).ToList();
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
