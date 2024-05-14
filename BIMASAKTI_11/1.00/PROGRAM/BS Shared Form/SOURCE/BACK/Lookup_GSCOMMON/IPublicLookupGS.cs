using Lookup_GSCOMMON.DTOs;
using System.Collections.Generic;

namespace Lookup_GSCOMMON
{
    public interface IPublicLookup
    {
        IAsyncEnumerable<GSL00100DTO> GSL00100GetSalesTaxList();
        IAsyncEnumerable<GSL00110DTO> GSL00110GetTaxByDateList();
        IAsyncEnumerable<GSL00200DTO> GSL00200GetWithholdingTaxList();
        IAsyncEnumerable<GSL00300DTO> GSL00300GetCurrencyList();
        IAsyncEnumerable<GSL00400DTO> GSL00400GetJournalGroupList();
        IAsyncEnumerable<GSL00500DTO> GSL00500GetGLAccountList();
        IAsyncEnumerable<GSL00510DTO> GSL00510GetCOAList();
        IAsyncEnumerable<GSL00520DTO> GSL00520GetGOACOAList();
        IAsyncEnumerable<GSL00550DTO> GSL00550GetGOAList();
        IAsyncEnumerable<GSL00600DTO> GSL00600GetUnitTypeCategoryList();
        IAsyncEnumerable<GSL00700DTO> GSL00700GetDepartmentList();
        IAsyncEnumerable<GSL00710DTO> GSL00710GetDepartmentPropertyList();
        IAsyncEnumerable<GSL00800DTO> GSL00800GetCurrencyTypeList();
        IAsyncEnumerable<GSL00900DTO> GSL00900GetCenterList();
        IAsyncEnumerable<GSL01000DTO> GSL01000GetUserList();
        IAsyncEnumerable<GSL01100DTO> GSL01100GetUserApprovalList();
        IAsyncEnumerable<GSL01200DTO> GSL01200GetBankList();
        IAsyncEnumerable<GSL01300DTO> GSL01300GetBankAccountList();
        IAsyncEnumerable<GSL01400DTO> GSL01400GetOtherChargesList();
        IAsyncEnumerable<GSL01500ResultGroupDTO> GSL01500GetCashFlowGroupList();
        IAsyncEnumerable<GSL01500ResultDetailDTO> GSL01500GetCashDetailList();
        IAsyncEnumerable<GSL01600DTO> GSL01600GetCashFlowGroupTypeList();
        IAsyncEnumerable<GSL01700DTO> GSL01700GetCurrencyRateList();
        IAsyncEnumerable<GSL01701DTO> GSL01700GetRateTypeList();
        IAsyncEnumerable<GSL01702DTO> GSL01700GetLocalAndBaseCurrencyList();
        IAsyncEnumerable<GSL01800DTO> GSL01800GetCategoryList();
        IAsyncEnumerable<GSL01900DTO> GSL01900GetLOBList();
        IAsyncEnumerable<GSL02000CountryDTO> GSL02000GetCountryGeographyList();
        IAsyncEnumerable<GSL02000CityDTO> GSL02000GetCityGeographyList();
        IAsyncEnumerable<GSL02100DTO> GSL02100GetPaymentTermList();
        IAsyncEnumerable<GSL02200DTO> GSL02200GetBuildingList();
        IAsyncEnumerable<GSL02300DTO> GSL02300GetBuildingUnitList();
        IAsyncEnumerable<GSL02400DTO> GSL02400GetFloorList();
        IAsyncEnumerable<GSL02500DTO> GSL02500GetCBList();
        IAsyncEnumerable<GSL02600DTO> GSL02600GetCBAccountList();
    }
}
