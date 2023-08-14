using Lookup_GSCOMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_GSCOMMON
{
    public interface IPublicLookup
    {
        GSLGenericList<GSL00100DTO> GSL00100GetSalesTaxList(GSL00100ParameterDTO poParameter);
        GSLGenericList<GSL00200DTO> GSL00200GetWithholdingTaxList(GSL00200ParameterDTO poParameter);
        GSLGenericList<GSL00300DTO> GSL00300GetCurrencyList();
        GSLGenericList<GSL00400DTO> GSL00400GetJournalGroupList(GSL00400ParameterDTO poParameter);
        GSLGenericList<GSL00500DTO> GSL00500GetGLAccountList(GSL00500ParameterDTO poParameter);
        GSLGenericList<GSL00510DTO> GSL00510GetCOAList(GSL00510ParameterDTO poParameter);
        GSLGenericList<GSL00520DTO> GSL00520GetGOACOAList(GSL00520ParameterDTO poParameter);
        GSLGenericList<GSL00550DTO> GSL00550GetGOAList(GSL00550ParameterDTO poParameter);
        GSLGenericList<GSL00600DTO> GSL00600GetUnitTypeCategoryList(GSL00600ParameterDTO poParameter);
        GSLGenericList<GSL00700DTO> GSL00700GetDepartmentList(GSL00700ParameterDTO poParameter);
        GSLGenericList<GSL00800DTO> GSL00800GetCurrencyTypeList(GSL00800ParameterDTO poParameter);
        GSLGenericList<GSL00900DTO> GSL00900GetCenterList(GSL00900ParameterDTO poParameter);
        GSLGenericList<GSL01000DTO> GSL01000GetUserList();
        GSLGenericList<GSL01100DTO> GSL01100GetUserApprovalList(GSL01100ParameterDTO poParameter);
        GSLGenericList<GSL01200DTO> GSL01200GetBankList(GSL01200ParameterDTO poParameter);
        GSLGenericList<GSL01300DTO> GSL01300GetBankAccountList(GSL01300ParameterDTO poParameter);
        GSLGenericList<GSL01400DTO> GSL01400GetOtherChargesList(GSL01400ParameterDTO poParameter);
        GSLGenericList<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList(GSL01500ParameterGroupDTO poParameter);
        GSLGenericList<GSL01500ResultDetailDTO> GSL01500GetCashDetailList(GSL01500ParameterDetailDTO poParameter);
        GSLGenericList<GSL01600DTO> GSL01600GetCashFlowGroupTypeList(GSL01600ParameterDTO poParameter);
        GSLGenericList<GSL01700DTO> GSL01700GetCurrencyRateList(GSL01700DTOParameter poParameter);
        GSLGenericList<GSL01701DTO> GSL01700GetRateTypeList();
        GSLGenericList<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList();
    }
}
