using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM02500Common.DTO
{
    public class LMM02520ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }

    public class SaveMoveTenantGroupResultDTO : R_APIResultBaseDTO
    {
    }

    public class TenantListForMoveProcessDTO
    {
        public bool LSELECTED { get; set; } = false;
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
    }

    public class TenantGroupForMoveParameterFrontDTO
    {
        public string? CFROM_TENANT_GROUP;
        public string? CPROPERTY_ID;
        public string? CTENANT_ID;
        public string? CTO_TENANT_GROUP;
    }
}