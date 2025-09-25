using System.Data;
using System.Data.Common;
using PMR00400Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMR00400Back;

public class PMR00400Cls
{
    public List<PMR00400DataDTO> GetDataDb(PMR00400ParamDTO poParams)
    {
        var loEx = new R_Exception();
        List<PMR00400DataDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection("R_ReportConnectionString");
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PMR00400_GET_REPORT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTYPE", DbType.String, 20, poParams.CTYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_BUILDING_ID", DbType.String, 20, poParams.CFROM_BUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTO_BUILDING_ID", DbType.String, 20, poParams.CTO_BUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_UNIT_TYPE_ID", DbType.String, 20, poParams.CFROM_UNIT_TYPE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTO_UNIT_TYPE_ID", DbType.String, 20, poParams.CTO_UNIT_TYPE_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 20, poParams.CLANG_ID);
            
            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<PMR00400DataDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public PMR00400PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        var loEx = new R_Exception();
        PMR00400PrintBaseHeaderLogoDTO loResult = null;
    
        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_ReportConnectionString");
            var loCmd = loDb.GetCommand();
    
    
            var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, pcCompanyId);
    
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loResult = R_Utility.R_ConvertTo<PMR00400PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
    
        return loResult;
    }
    
}