using System;
using System.Collections.Generic;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;
using Lookup_PMCOMMON.DTOs.LML01400;
using Lookup_PMCOMMON.DTOs.LML01500;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs.LML01700;
using Lookup_PMCOMMON.DTOs.LML01800;
using Lookup_PMCOMMON.DTOs.LML01900;
using Lookup_PMCOMMON.DTOs.UtilityDTO;

namespace Lookup_PMCOMMON
{
    public interface IPublicLookupLM
    {
       IAsyncEnumerable<LML00200DTO> LML00200UnitChargesList();
        IAsyncEnumerable<LML00300DTO> LML00300SupervisorList();
        IAsyncEnumerable<LML00400DTO> LML00400UtilityChargesList();
        IAsyncEnumerable<LML00500DTO> LML00500SalesmanList();
        IAsyncEnumerable<LML00600DTO> LML00600TenantList();
        IAsyncEnumerable<LML00700DTO> LML00700DiscountList();
        IAsyncEnumerable<LML00800DTO> LML00800AgreementList();
        IAsyncEnumerable<LML00900DTO> LML00900TransactionList();
        IAsyncEnumerable<LML01000DTO> LML01000BillingRuleList();
        IAsyncEnumerable<LML01100DTO> LML01100TNCList();
        IAsyncEnumerable<LML01200DTO> LML01200InvoiceGroupList();
        IAsyncEnumerable<LML01300DTO> LML01300LOIAgreementList(); //LML01400AgreementUnitCharges
        IAsyncEnumerable<LML01400DTO> LML01400AgreementUnitChargesList();
        IAsyncEnumerable<LML01500DTO> LML01500SLACategoryList();
        IAsyncEnumerable<LML01600DTO> LML01600SLACallTypeList();
        IAsyncEnumerable<LML01700DTO> LML01700CancelReceiptFromCustomerList();
        IAsyncEnumerable<LML01700DTO> LML01700PrerequisiteCustReceiptList();
        IAsyncEnumerable<LML01800DTO> LML01800UnitTenantList();
        IAsyncEnumerable<LML01900DTO> LML01900StaffList();
        IAsyncEnumerable<PropertyDTO> PropertyList();
        IAsyncEnumerable<BuildingDTO> BuildingList();
        IAsyncEnumerable<FloorDTO> FloorList();
        IAsyncEnumerable<UnitDTO> UnitList();
        IAsyncEnumerable<DepartmentDTO> DepartmentList();
        IAsyncEnumerable<SupervisorDTO> SupervisorList();
        IAsyncEnumerable<StaffTypeDTO> StaffTypeList();
        LML00900InitialProcessDTO InitialProcess();
    }
}
