using Lookup_GSCOMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_GSCOMMON
{
    public interface IPublicLookup
    {
        //GSLGenericList<GSL00100DTO> GSL00100GetSalesTaxList(GSL00100ParameterDTO poParameter);
        //GSLGenericList<GSL00200DTO> GSL00200GetWithholdingTaxList(GSL00200ParameterDTO poParameter);
        //GSLGenericList<GSL00300DTO> GSL00300GetCurrencyList();
        GSLGenericList<GSL00500DTO> GSL00500GetGLAccountList(GSL00500ParameterDTO poParameter);
        //GSLGenericList<GSL00900DTO> GSL00900GetCenterList(GSL00900ParameterDTO poParameter);
        //GSLGenericList<GSL01000DTO> GSL01000GetUserList();
        GSLGenericList<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList(GSL01500ParameterGroupDTO poParameter);
        GSLGenericList<GSL01500ResultDetailDTO> GSL01500GetCashDetailList(GSL01500ParameterDetailDTO poParameter);
        

    }
}
