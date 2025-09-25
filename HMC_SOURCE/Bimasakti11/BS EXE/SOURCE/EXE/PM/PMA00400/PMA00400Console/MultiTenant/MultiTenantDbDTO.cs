using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA00400Console.MultiTenant
{
    public class MultiTenantDbDTO
    {
        public string CTENANT_ID { get; set; } = string.Empty;
        public string CCONNECTIONSTRING { get; set; } = string.Empty;
        public string CPROVIDER_NAME { get; set; } = string.Empty;
        public string CKEYWORD_FOR_PASSWORD { get; set; } = string.Empty;
    }
}
