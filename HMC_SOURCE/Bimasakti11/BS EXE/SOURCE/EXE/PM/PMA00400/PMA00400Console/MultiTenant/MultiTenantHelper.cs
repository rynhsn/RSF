using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BackEnd;
using R_Common;
using R_MultiTenantDb.Abstract;
using R_CommonFrontBackAPI.Log;
using PMA00400Console.Logger;
using System.Data.Common;
using System.Data;
using R_MultiTenantDb;
using R_CommonFrontBackAPI;
using PMA00400Common.DTO;
using PMA00400Common.Parameter;
using log4net;

namespace PMA00400Console.MultiTenant
{
    public class MultiTenantHelper
    {
        private static ILog _logger;
        IMemoryCache _MemoryCache;
        List<TenantDTO> _Tenants;

        public MultiTenantHelper(IMemoryCache poMemoryCache)
        {
            _logger = LogManager.GetLogger(typeof(MultiTenantHelper));
            _MemoryCache = poMemoryCache;
            _Tenants = new List<TenantDTO>();
        }
        public List<TenantDTO> getTenants()
        {
            return _Tenants;
        }

        public void InjectMultiTenantConnectionString(string pcAppId, string pcConnectionMultiName)
        {
            string? lcMethodName = nameof(InjectMultiTenantConnectionString);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            R_Db? loDb = new R_Db();
            DbConnection? loConn = null;
            List<R_ConnectionStringDTO> loConnections = new List<R_ConnectionStringDTO>();
            List<MultiTenantDbDTO>? loMultiTenantDb = null;
            string lcCmd;
            string lcKey;
            try
            {
                loConn = loDb.GetConnection(pcConnectionMultiName);
                _logger!.Info(string.Format("Start Get connecction {0} on Cls", lcMethodName));
                //Set Cache
                lcCmd = "Select CTENANT_ID, CCONNECTIONSTRING, CPROVIDER_NAME, CKEYWORD_FOR_PASSWORD  from SAB_MULTI_TENANT_DB(nolock) where CAPPLICATION_ID={0}";
                _logger!.Debug(string.Format("Execute Command {0}", lcCmd));
                _logger!.Debug(string.Format("Parameter {0} : ", pcAppId));
                loMultiTenantDb = loDb.SqlExecObjectQuery<MultiTenantDbDTO>(lcCmd, loConn, false, pcAppId);
                loConnections = loMultiTenantDb.Select(y => new R_ConnectionStringDTO()
                {
                    Name = y.CTENANT_ID.ToLower().Trim(),
                    ConnectionString = y.CCONNECTIONSTRING.Trim(),
                    ProviderName = y.CPROVIDER_NAME.Trim(),
                    KeywordForPassword = y.CKEYWORD_FOR_PASSWORD.Trim()
                }
                                                      ).ToList();

                lcKey = R_MultiTenantDbEnumerationConstant.R_GetRedisMultiDbKey(new R_GetRedisMultiDbKeyParameterDTO() { ApplicationId = pcAppId });
                _MemoryCache.Set(lcKey, loConnections, DateTimeOffset.Now.AddDays(1.0D));

                //Populate Tenants
                lcCmd = "select distinct  CTENANT_ID ";
                lcCmd += "from( ";
                lcCmd += "Select CTENANT_ID=REPLACE(REPLACE(CTENANT_ID,'_report_connectionstring',''),'_connectionstring','') from SAB_MULTI_TENANT_DB(nolock) where CAPPLICATION_ID={0} ";
                lcCmd += ") as final";
                _logger!.Debug(string.Format("Execute Command {0}", lcCmd));
                _logger!.Debug(string.Format("Parameter {0} : ", pcAppId));
                _Tenants = loDb.SqlExecObjectQuery<TenantDTO>(lcCmd, loConn, false, pcAppId);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            finally
            {
                loDb = null;
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }
                    loConn.Dispose();
                    loConn = null;
                }
                if (loMultiTenantDb != null)
                {
                    loMultiTenantDb.Clear();
                }
                loMultiTenantDb = null;
            }
            loException.ThrowExceptionIfErrors();
            _logger!.Info(string.Format("END process method {0} on Cls", lcMethodName));
        }
    }
}
