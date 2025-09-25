using Microsoft.Extensions.Logging;
using R_MultiTenantDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA00400Console
{
    public class GetConnectionName
    {
        ILogger<GetConnectionName> _logger;
        public GetConnectionName(ILogger<GetConnectionName> poLogger)
        {
            _logger = poLogger;
        }
        public List<string> GetConnectionNames()
        {
            List<string> loConnectionNames;

            _logger.LogInformation("Get All ConnectionStrings");

            loConnectionNames = R_MultiTenantDbRepository.R_GetConnectionStrings().Select(x => x.Name).ToList();

            return loConnectionNames;
        }
    }
}
