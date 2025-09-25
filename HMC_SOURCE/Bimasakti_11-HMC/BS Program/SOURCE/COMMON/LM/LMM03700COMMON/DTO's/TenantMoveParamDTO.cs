using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03700Common.DTO_s
{
    public class TenantMoveParamDTO : TenantParamDTO
    {
        public string CFROM_TENANT_CLASSIFICATION_ID { get; set; }
        public string CTO_TENANT_CLASSIFICATION_ID { get; set; }
        public List<string> LIST_CTENANT_ID { get; set; }
    }
}
