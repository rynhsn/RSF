using System;
using System.Collections.Generic;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs.LML01300;

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
        IAsyncEnumerable<LML01300DTO> LML01300LOIAgreementList();

        #region Utility
        LML00900InitialProcessDTO InitialProcess();
        #endregion
    }
}
