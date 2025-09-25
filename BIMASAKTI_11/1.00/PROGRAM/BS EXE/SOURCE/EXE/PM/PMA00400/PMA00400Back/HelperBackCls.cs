using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PMA00400Common.DTO;
using PMA00400Logger;
using R_BackEnd;
using R_Common;
using R_MultiTenantDb.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA00400Back
{
    public class HelperBackCls
    {
        private ConsoleLogger _logger;
        IMemoryCache _MemoryCache;

        public HelperBackCls(IMemoryCache poMemoryCache)
        {
            _logger = LogManager.GetLogger(typeof(HelperBackCls));
            _MemoryCache = poMemoryCache;
        }

        //        public void InjectMultiTenantConnectionString(string pcAppId, string pcConnectionMultiName)
        //{
        //    R_Exception loException = new R_Exception();
        //    R_Db loDb = new R_Db();
        //    List<R_ConnectionStringDTO> loConnections = new List<R_ConnectionStringDTO>();
        //    List<MultiTenantDbDTO> loMultiTenantDb = new List<MultiTenantDbDTO>();
        //    string lcCmd;
        //    string lcKey;
        //    try
        //    {
        //        _logger.Info($"[DEBUG] Nama Connection yang dikirim: {pcConnectionMultiName}");

        //        // Ambil cache yang sudah ada
        //        lcKey = R_MultiTenantDbEnumerationConstant.R_GetRedisMultiDbKey(new R_GetRedisMultiDbKeyParameterDTO() { ApplicationId = pcAppId });
        //        if (_MemoryCache.TryGetValue(lcKey, out List<R_ConnectionStringDTO> loCachedConnections))
        //        {
        //            _logger.Info($"[DEBUG] Cache Sudah Ada, Jumlah Connection Sebelumnya: {loCachedConnections.Count}");
        //            _MemoryCache.Remove(lcKey);
        //        }
        //        else
        //        {
        //            _logger.Info("[DEBUG] Cache Belum Ada, Lanjutkan Inject");
        //        }

        //        lcCmd = $"Select CTENANT_ID, CCONNECTIONSTRING, CPROVIDER_NAME, CKEYWORD_FOR_PASSWORD from SAB_MULTI_TENANT_DB(nolock) where CAPPLICATION_ID='{pcAppId}'";
        //        _logger.Info($"[DEBUG] Query: {lcCmd}");

        //        loMultiTenantDb = loDb.SqlExecObjectQuery<MultiTenantDbDTO>(lcCmd, loDb.GetConnection(pcConnectionMultiName), true, pcAppId);
        //        _logger.Info($"[DEBUG] Jumlah Data dari DB: {loMultiTenantDb.Count}");

        //        loConnections = loMultiTenantDb
        //            .Select(y => new R_ConnectionStringDTO()
        //            {
        //                Name = y.CTENANT_ID.Trim(),
        //                ConnectionString = y.CCONNECTIONSTRING.Trim(),
        //                ProviderName = y.CPROVIDER_NAME.Trim(),
        //                KeywordForPassword = y.CKEYWORD_FOR_PASSWORD.Trim()
        //            }).ToList();

        //        foreach (var conn in loConnections)
        //        {
        //            _logger.Info($"Available Connection Name: {conn.Name}");
        //        }

        //        _logger.Info($"[DEBUG] Cache Baru Disimpan, Jumlah Connection: {loConnections.Count}");
        //        _MemoryCache.Set(lcKey, loConnections, DateTimeOffset.Now.AddDays(1.0D));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error("[ERROR] Gagal Inject Cache", ex);
        //        loException.Add(ex);
        //    }
        //}

        public void InjectMultiTenantConnectionString(string pcAppId, string pcConnectionMultiName)
        {
            string? lcMethodName = nameof(InjectMultiTenantConnectionString);
            _logger!.Info(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            List<R_ConnectionStringDTO> loConnections = new List<R_ConnectionStringDTO>();
            List<MultiTenantDbDTO> loMultiTenantDb = new List<MultiTenantDbDTO>();
            string lcCmd;
            string lcKey;
            try
            {
                lcCmd = $"Select CTENANT_ID, CCONNECTIONSTRING, CPROVIDER_NAME, CKEYWORD_FOR_PASSWORD from SAB_MULTI_TENANT_DB(nolock) where CAPPLICATION_ID='{pcAppId}'";
                _logger!.Debug(string.Format("Execute Command {0}", lcCmd));
                _logger!.Debug(string.Format("Parameter {0} : ", pcAppId));
                loMultiTenantDb = loDb.SqlExecObjectQuery<MultiTenantDbDTO>(lcCmd, loDb.GetConnection(pcConnectionMultiName), true, pcAppId);
                loConnections = loMultiTenantDb.Select(y => new R_ConnectionStringDTO() { Name = y.CTENANT_ID.Trim(), ConnectionString = y.CCONNECTIONSTRING.Trim(), ProviderName = y.CPROVIDER_NAME.Trim(), KeywordForPassword = y.CKEYWORD_FOR_PASSWORD.Trim() }).ToList();

                lcKey = R_MultiTenantDbEnumerationConstant.R_GetRedisMultiDbKey(new R_GetRedisMultiDbKeyParameterDTO() { ApplicationId = pcAppId });
                _MemoryCache.Set(lcKey, loConnections, DateTimeOffset.Now.AddDays(1.0D));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger!.Error(string.Format("Log Error {0} ", ex));
            }
            if (loException.Haserror)
            {
                _logger.Error(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger!.Info(string.Format("END process method {0} on Cls", lcMethodName));
        }
    }
}
